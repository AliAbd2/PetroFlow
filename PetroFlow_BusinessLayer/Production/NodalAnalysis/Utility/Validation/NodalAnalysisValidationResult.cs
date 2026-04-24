using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation
{
    public class NodalAnalysisValidationResult
    {

        public List<string> Warnings { get; } = new();

        public NodalAnalysisValidationResult()
        {

        }

    }


}
