using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{

    public abstract class IPRMethodBase
    {

        public abstract IPRMethodType MethodType { get; }

        public abstract IPRMethodFeatures Features { get; }

        public abstract string DisplayName { get; }

        public List<FlowDataRow> GenerateIPR(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult)
        {

            ValidateRawData(inputData, ref validationResult);

            return ComputeIPR(inputData);

        }

        protected abstract void ValidateRawData(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult);

        protected abstract List<FlowDataRow> ComputeIPR(IPRInputData inputData);


    }
}
