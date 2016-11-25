using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace MscrmTools.ManagedSolutionDeletionTool.AppCode
{
    public class Solution
    {
        public Solution()
        {
            DependentSolutions = new List<Solution>();
            RequiredSolutions = new List<Solution>();
        }

        public Solution(Guid id, string uniqueName) : this()
        {
            Id = id;
            UniqueName = uniqueName;
        }

        public Guid Id { get; set; }

        public string UniqueName { get; set; }

        public string FriendlyName
        {
            get { return Entity.GetAttributeValue<string>("friendlyname"); }
        }

        public Entity Entity { get; internal set; }

        public List<Solution> DependentSolutions;
        public List<Solution> RequiredSolutions;

        public List<Solution> GetDependentSolutions(Solution solution = null, List<Solution> list = null)
        {
            var currentSolution = solution ?? this;
            var sols = list ?? new List<Solution> {this};

            foreach (var ds in currentSolution.DependentSolutions)
            {
                sols.Add(ds);
                GetDependentSolutions(ds, list);
            }

            return sols;
        }

        public override string ToString()
        {
            return UniqueName;
        }
    }
}
