using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using System.Reflection;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    public class InFlowPerformanceRelationship
    {

        private List<IPRMethodBase> _methods;

        public InFlowPerformanceRelationship()
        {

            _methods = GetIPRMethods();

        }

        private List<IPRMethodBase> GetIPRMethods()
        {

            return Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t =>
                        !t.IsAbstract &&
                        typeof(IPRMethodBase).IsAssignableFrom(t))
                    .Select(t =>
                        (IPRMethodBase)Activator.CreateInstance(t)!)
                    .ToList();

        }

        public IReadOnlyList<IPRMethodBase> GetAvailableMethods(
        IPRMethodFeatures requiredFeatures)
        {

            return _methods
                .Where(method => (method.Features & requiredFeatures) == requiredFeatures)
                .ToList();
        }

        public IReadOnlyList<string> GetAvailableMethodsName(IPRMethodFeatures requiredFeatures)
        {

            return GetAvailableMethods(requiredFeatures).Select(method => method.DisplayName).ToList();

        }



    }

}
