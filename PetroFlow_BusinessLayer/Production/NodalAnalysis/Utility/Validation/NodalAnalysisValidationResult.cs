using PetroFlow_BusinessLayer.General_Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation
{
    public sealed class NodalAnalysisValidationResult
    {
        public IReadOnlyList<ErrorMessage> Warnings => _warnings;

        private readonly List<ErrorMessage> _warnings = new();

        private readonly HashSet<ErrorMessage> _uniqueWarnings = new();

        public void AddWarning(ErrorMessage warning)
        {
            if (_uniqueWarnings.Add(warning))
            {
                _warnings.Add(warning);
            }
        }
    }

}
