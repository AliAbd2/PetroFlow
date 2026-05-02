using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation
{
    public class NodalAnalysisValidationResult
    {

        public List<string> Warnings { get; } = new();

        private HashSet<string> _uniqueWarnings = new();

        public NodalAnalysisValidationResult()
        {

        }

        public void AddWarning(string warning)
        {

            if (_uniqueWarnings.Add(warning))
            {
                Warnings.Add(warning);
            }

        }

    }


}
