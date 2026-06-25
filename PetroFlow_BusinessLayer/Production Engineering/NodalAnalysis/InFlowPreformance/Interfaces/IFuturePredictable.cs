using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

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
