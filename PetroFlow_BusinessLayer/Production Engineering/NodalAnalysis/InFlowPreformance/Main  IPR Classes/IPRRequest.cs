using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Main__IPR_Classes
{
    public class IPRRequest
    {

        public IPRMethodType Method { get; init; }

        public IPRScenarioType Scenario { get; init; }

        public IPRInputData InputData { get; init; }


        public IPRRequest(IPRMethodType method,
            IPRScenarioType scenario, IPRInputData inputData)
        {

            Method = method;
            Scenario = scenario;
            InputData = inputData;

        }

    }
}
