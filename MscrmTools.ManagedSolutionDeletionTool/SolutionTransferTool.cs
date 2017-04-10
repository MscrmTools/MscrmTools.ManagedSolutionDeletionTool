using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using System.Linq;
using MscrmTools.ManagedSolutionDeletionTool.AppCode;
using MscrmTools.ManagedSolutionDeletionTool.UserControls;
using XrmToolBox.Extensibility.Interfaces;

namespace MscrmTools.ManagedSolutionDeletionTool
{
    public partial class SolutionTransferTool : PluginControlBase, IGitHubPlugin, IHelpPlugin
    {
        #region Variables

        private int currentsColumnOrder;
        private List<Solution> sols;

        #endregion Variables

        #region Constructor

        public SolutionTransferTool()
        {
            InitializeComponent();
            sols = new List<Solution>();
        }

        #endregion Constructor

        #region Properties

        public string RepositoryName { get { return "MscrmTools.ManagedSolutionDeletionTool"; } }
        public string UserName { get { return "MscrmTools"; } }
        public string HelpUrl { get { return "https://github.com/MscrmTools/MscrmTools.ManagedSolutionDeletionTool/wiki"; } }

        #endregion

        #region UI Events

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbLoadSolutionsClick(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveSolutions);
        }

        private void tsbDeleteSolutions_Click(object sender, EventArgs e)
        {
            if (lstSourceSolutions.SelectedItems.Count == 0)
            {
                return;
            }

            var solutions = lstSourceSolutions.SelectedItems.Cast<ListViewItem>().Select(i => (Solution)i.Tag).ToList();

            var message = solutions.All(s => s.DependentSolutions.Count == 0)
                ? "Are you sure you want to delete selected solution(s)?"
                : "Are you sure you want to delete selected solution(s) and all their dependent solutions?\n\nSolutions:\n" +
                  string.Join("\n",solutions.SelectMany(s => s.GetDependentSolutions()).Select(s2 => s2.FriendlyName).Distinct().ToList());

            if (MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.No)
            {
                return;
            }

            DeleteSolutions(solutions);
        }

        private void lstSourceSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentsColumnOrder)
            {
                lstSourceSolutions.Sorting = lstSourceSolutions.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                lstSourceSolutions.ListViewItemSorter = new ListViewItemComparer(e.Column, lstSourceSolutions.Sorting);
            }
            else
            {
                currentsColumnOrder = e.Column;
                lstSourceSolutions.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }

        private void lstSourceSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSourceSolutions.SelectedItems.Count != 1)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2Collapsed = true;
                return;
            }

            var slp = new SolutionProperties((Solution)lstSourceSolutions.SelectedItems[0].Tag);
            slp.Dock = DockStyle.Fill;
            slp.SolutionDeletionRequested += Slp_SolutionDeletionRequested;
            splitContainer1.Panel2.Controls.Add(slp);
            splitContainer1.Panel2Collapsed = false;
        }

        #endregion UI Events

        #region Methods

        /// <summary>
        /// Retrieves unmanaged solutions from the source organization
        /// </summary>
        public void RetrieveSolutions()
        {
            sols = new List<Solution>();
            lstSourceSolutions.Items.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading solutions...",
                Work = (sender, evt) =>
                {
                    var solutions = Service.RetrieveMultiple(new QueryExpression("solution")
                    {
                        ColumnSet = new ColumnSet("publisherid", "installedon", "version", "uniquename", "friendlyname",
                            "description", "ismanaged"),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                //new ConditionExpression("ismanaged", ConditionOperator.Equal, true),
                                new ConditionExpression("isvisible", ConditionOperator.Equal, true),
                                new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Active"),
                                new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Default")
                            }
                        }
                    }).Entities;

                    foreach (var solution in solutions)
                    {
                        var sol = new Solution(solution.Id, solution.GetAttributeValue<string>("uniquename")) { Entity = solution };
                        sols.Add(sol);

                        var dependentComponents = ((RetrieveDependenciesForUninstallResponse)
                            Service.Execute(new RetrieveDependenciesForUninstallRequest
                            {
                                SolutionUniqueName = sol.UniqueName
                            })).EntityCollection.Entities;

                        if (dependentComponents.Count > 0)
                        {

                            var requiredSolutions = dependentComponents.Select(
                                e => e.GetAttributeValue<Guid>("dependentcomponentbasesolutionid")).Distinct();

                            var dependantSolutions = Service.RetrieveMultiple(new QueryExpression("solution")
                            {
                                ColumnSet = new ColumnSet(true),
                                Criteria = new FilterExpression
                                {
                                    Conditions =
                                {
                                    new ConditionExpression("solutionid", ConditionOperator.In,
                                        requiredSolutions.ToArray())
                                }
                                },

                            }).Entities;

                            foreach (var ds in dependantSolutions)
                            {
                                if (ds.GetAttributeValue<string>("uniquename") == "Active")
                                {
                                    continue;
                                }

                                var existingSolution = sols.FirstOrDefault(s => s.Id == ds.Id);
                                if (existingSolution == null)
                                {
                                    existingSolution = new Solution(ds.Id, ds.GetAttributeValue<string>("uniquename"));
                                    existingSolution.Entity = ds;
                                    sols.Add(existingSolution);
                                }
                                existingSolution.RequiredSolutions.Add(sol);
                                sol.DependentSolutions.Add(existingSolution);
                            }
                        }
                    }
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured while retrieving solutions: " + evt.Error.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var items = new List<ListViewItem>();

                    foreach (var solution in sols)
                    {
                        var item = new ListViewItem
                        {
                            Tag = solution,
                            Text = solution.Entity.GetAttributeValue<String>("uniquename")
                        };
                        item.SubItems.Add(solution.FriendlyName);
                        item.SubItems.Add(solution.Entity.GetAttributeValue<bool>("ismanaged") ? "Managed": "Unmanaged");
                        item.SubItems.Add(solution.Entity.GetAttributeValue<String>("version"));
                        item.SubItems.Add(
                            solution.Entity.GetAttributeValue<DateTime>("installedon").ToString("yy-MM-dd HH:mm"));
                        item.SubItems.Add(solution.Entity.GetAttributeValue<EntityReference>("publisherid").Name);
                        item.SubItems.Add(solution.Entity.GetAttributeValue<String>("description"));
                        item.ForeColor = solution.DependentSolutions.Count > 0 ? Color.Red : Color.Green;

                        items.Add(item);
                    }

                    lstSourceSolutions.Items.AddRange(items.ToArray());
                }
            });
        }

        private void Slp_SolutionDeletionRequested(object sender, SolutionDeletionRequestEventArgs e)
        {
            var message = string.Format(e.Solution.DependentSolutions.Count == 0
                   ? "Are you sure you want to delete solution '{0}'?"
                   : "Are you sure you want to delete solution '{0}' and all its dependent solutions?",
                   e.Solution.FriendlyName);

            if (MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.No)
            {
                return;
            }

            DeleteSolutions(new List<Solution> { e.Solution });
        }

        private void DeleteSolutions(List<Solution> solutions)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deleting solution(s)...",
                AsyncArgument = solutions,
                Work = (bw, evt) =>
                {
                    var solutionsToDelete = (List<Solution>)evt.Argument;

                    foreach (var solution in solutionsToDelete)
                    {
                        DeleteSolution(solution, bw);
                    }
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured while deleting solution(s): " + evt.Error.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    RetrieveSolutions();
                }
            });
        }

        private void DeleteSolution(Solution solution, BackgroundWorker worker)
        {
            foreach (Solution dependentSolution in solution.DependentSolutions)
            {
                DeleteSolution(dependentSolution, worker);
            }

            worker.ReportProgress(0, string.Format("Deleting solution '{0}'...", solution.FriendlyName));
            Service.Delete(solution.Entity.LogicalName, solution.Entity.Id);
        }

        #endregion Methods
    }
}