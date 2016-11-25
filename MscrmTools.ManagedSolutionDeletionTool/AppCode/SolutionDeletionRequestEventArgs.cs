using System;

namespace MscrmTools.ManagedSolutionDeletionTool.AppCode
{
    public class SolutionDeletionRequestEventArgs : EventArgs
    {
        public SolutionDeletionRequestEventArgs(Solution solution)
        {
            Solution = solution;
        }

        public Solution Solution { get; }
    }
}