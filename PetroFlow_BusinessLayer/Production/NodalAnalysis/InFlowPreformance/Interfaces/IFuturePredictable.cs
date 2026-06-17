using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{
    public interface IFuturePredictable
    {

        void ValidateFutureRawDataInput(IPRInputData Input,
            ref NodalAnalysisValidationResult validationResult);

        List<FlowDataRow> GenerateFutureIPR(IPRInputData Input,
            ref NodalAnalysisValidationResult validationResult);

    }
}
