using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Main__IPR_Classes
{
    public class IPROutput
    {

        public IReadOnlyList<FlowDataRow> Output { get; init; }

        public NodalAnalysisValidationResult ValidationResult { get; init; }

        public IPROutput(IReadOnlyList<FlowDataRow> output,
            NodalAnalysisValidationResult validationResult)
        {

            Output = output;
            ValidationResult = validationResult;

        }
}
}
