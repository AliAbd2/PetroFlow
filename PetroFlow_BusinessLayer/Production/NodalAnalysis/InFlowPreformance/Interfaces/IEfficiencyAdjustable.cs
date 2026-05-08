using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{
    public interface IEfficiencyAdjustable
    {

        List<InFlowDataRow> GenerateWithEfficiency(IPRInputData input, double efficiency, 
            ref NodalAnalysisValidationResult validationResult);

    }
}
