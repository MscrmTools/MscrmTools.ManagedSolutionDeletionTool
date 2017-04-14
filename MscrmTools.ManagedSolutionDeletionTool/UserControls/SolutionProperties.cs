using System;
using System.Linq;
using System.Windows.Forms;
using MscrmTools.ManagedSolutionDeletionTool.AppCode;
using System.Diagnostics;

namespace MscrmTools.ManagedSolutionDeletionTool.UserControls
{
    public partial class SolutionProperties : UserControl
    {
        private readonly Solution solution;
        private readonly string manageSolutionUrl;

        public SolutionProperties(Solution solution, string crmUrl)
        {
            InitializeComponent();

            this.solution = solution;

            manageSolutionUrl = $"{crmUrl}/tools/solution/edit.aspx?id={solution.Id}";

            lblSolutionName.Text = solution.FriendlyName;

            pnlDependency.Visible = solution.DependentSolutions.Any();
            pnlNoDependency.Visible = !solution.DependentSolutions.Any();

            var rootNode = new TreeNode(lblSolutionName.Text) {Tag = solution};
            tvDependencies.Nodes.Add(rootNode);
            AddChildNodes(solution, rootNode);

            tvDependencies.ExpandAll();
        }

        public event EventHandler<SolutionDeletionRequestEventArgs> SolutionDeletionRequested;

        private void AddChildNodes(Solution requiredSolution, TreeNode parentNode)
        {
            foreach (var dependantSolution in requiredSolution.DependentSolutions)
            {
                TreeNode node = new TreeNode(dependantSolution.FriendlyName)
                {
                    Tag = dependantSolution
                };
                parentNode.Nodes.Add(node);

                AddChildNodes(dependantSolution, node);
            }
        }

        private void llDeleteSolution_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SolutionDeletionRequested?.Invoke(this, new SolutionDeletionRequestEventArgs(solution));
        }

        private void llDeleteSolutionDependencies_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SolutionDeletionRequested?.Invoke(this, new SolutionDeletionRequestEventArgs(solution));
        }

        private void lblSolutionName_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(manageSolutionUrl);
            Process.Start(sInfo);
        }
    }
}
