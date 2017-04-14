namespace MscrmTools.ManagedSolutionDeletionTool.UserControls
{
    partial class SolutionProperties
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSolutionName = new System.Windows.Forms.LinkLabel();
            this.lblCanBeUninstall = new System.Windows.Forms.Label();
            this.pnlNoDependency = new System.Windows.Forms.Panel();
            this.llDeleteSolution = new System.Windows.Forms.LinkLabel();
            this.pnlDependency = new System.Windows.Forms.Panel();
            this.tvDependencies = new System.Windows.Forms.TreeView();
            this.llDeleteSolutionDependencies = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlNoDependency.SuspendLayout();
            this.pnlDependency.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSolutionName
            // 
            this.lblSolutionName.AutoSize = true;
            this.lblSolutionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSolutionName.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolutionName.Location = new System.Drawing.Point(0, 0);
            this.lblSolutionName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSolutionName.Name = "lblSolutionName";
            this.lblSolutionName.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblSolutionName.Size = new System.Drawing.Size(135, 31);
            this.lblSolutionName.TabIndex = 0;
            this.lblSolutionName.TabStop = true;
            this.lblSolutionName.Text = "[SolutionName]";
            this.lblSolutionName.Click += new System.EventHandler(this.lblSolutionName_Click);
            // 
            // lblCanBeUninstall
            // 
            this.lblCanBeUninstall.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCanBeUninstall.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCanBeUninstall.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCanBeUninstall.Location = new System.Drawing.Point(0, 0);
            this.lblCanBeUninstall.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCanBeUninstall.MaximumSize = new System.Drawing.Size(0, 65);
            this.lblCanBeUninstall.Name = "lblCanBeUninstall";
            this.lblCanBeUninstall.Size = new System.Drawing.Size(313, 21);
            this.lblCanBeUninstall.TabIndex = 1;
            this.lblCanBeUninstall.Text = "This solution has no dependent solutions and can be deleted immediatly";
            // 
            // pnlNoDependency
            // 
            this.pnlNoDependency.Controls.Add(this.llDeleteSolution);
            this.pnlNoDependency.Controls.Add(this.lblCanBeUninstall);
            this.pnlNoDependency.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNoDependency.Location = new System.Drawing.Point(0, 31);
            this.pnlNoDependency.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNoDependency.Name = "pnlNoDependency";
            this.pnlNoDependency.Size = new System.Drawing.Size(313, 39);
            this.pnlNoDependency.TabIndex = 2;
            // 
            // llDeleteSolution
            // 
            this.llDeleteSolution.AutoSize = true;
            this.llDeleteSolution.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.llDeleteSolution.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llDeleteSolution.Location = new System.Drawing.Point(0, 20);
            this.llDeleteSolution.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llDeleteSolution.Name = "llDeleteSolution";
            this.llDeleteSolution.Size = new System.Drawing.Size(101, 19);
            this.llDeleteSolution.TabIndex = 2;
            this.llDeleteSolution.TabStop = true;
            this.llDeleteSolution.Text = "Delete solution";
            this.llDeleteSolution.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteSolution_LinkClicked);
            // 
            // pnlDependency
            // 
            this.pnlDependency.Controls.Add(this.tvDependencies);
            this.pnlDependency.Controls.Add(this.llDeleteSolutionDependencies);
            this.pnlDependency.Controls.Add(this.label1);
            this.pnlDependency.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDependency.Location = new System.Drawing.Point(0, 70);
            this.pnlDependency.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDependency.Name = "pnlDependency";
            this.pnlDependency.Size = new System.Drawing.Size(313, 321);
            this.pnlDependency.TabIndex = 3;
            // 
            // tvDependencies
            // 
            this.tvDependencies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDependencies.Location = new System.Drawing.Point(0, 62);
            this.tvDependencies.Margin = new System.Windows.Forms.Padding(2);
            this.tvDependencies.Name = "tvDependencies";
            this.tvDependencies.Size = new System.Drawing.Size(313, 240);
            this.tvDependencies.TabIndex = 4;
            // 
            // llDeleteSolutionDependencies
            // 
            this.llDeleteSolutionDependencies.AutoSize = true;
            this.llDeleteSolutionDependencies.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.llDeleteSolutionDependencies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llDeleteSolutionDependencies.Location = new System.Drawing.Point(0, 302);
            this.llDeleteSolutionDependencies.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llDeleteSolutionDependencies.Name = "llDeleteSolutionDependencies";
            this.llDeleteSolutionDependencies.Size = new System.Drawing.Size(292, 19);
            this.llDeleteSolutionDependencies.TabIndex = 3;
            this.llDeleteSolutionDependencies.TabStop = true;
            this.llDeleteSolutionDependencies.Text = "Delete solution and all its dependent solutions";
            this.llDeleteSolutionDependencies.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteSolutionDependencies_LinkClicked);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label1.Size = new System.Drawing.Size(313, 62);
            this.label1.TabIndex = 2;
            this.label1.Text = "This solution has dependent solutions. You need to delete dependent solutions bef" +
    "ore you can delete this solution\r\n\r\nThe following treeview shows the solution de" +
    "pendencies";
            // 
            // SolutionProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDependency);
            this.Controls.Add(this.pnlNoDependency);
            this.Controls.Add(this.lblSolutionName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SolutionProperties";
            this.Size = new System.Drawing.Size(313, 575);
            this.pnlNoDependency.ResumeLayout(false);
            this.pnlNoDependency.PerformLayout();
            this.pnlDependency.ResumeLayout(false);
            this.pnlDependency.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCanBeUninstall;
        private System.Windows.Forms.Panel pnlNoDependency;
        private System.Windows.Forms.LinkLabel llDeleteSolution;
        private System.Windows.Forms.Panel pnlDependency;
        private System.Windows.Forms.TreeView tvDependencies;
        private System.Windows.Forms.LinkLabel llDeleteSolutionDependencies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblSolutionName;
    }
}
