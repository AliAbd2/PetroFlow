using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Main__IPR_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System.Reflection;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;
using PetroFlow_BusinessLayer.General_Utility.Validation;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    public class InFlowPerformanceRelationship
    {

        private readonly Dictionary<IPRMethodType, IPRMethodBase>
            _methods = GetIPRMethods();

        private static Dictionary<IPRMethodType, IPRMethodBase> GetIPRMethods()
        {

            return Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t =>
                        !t.IsAbstract &&
                        typeof(IPRMethodBase).IsAssignableFrom(t))
                    .Select(t =>
                        (IPRMethodBase)Activator.CreateInstance(t)!)
                    .ToDictionary(x => x.MethodType);

        }

        public IReadOnlyList<IPRMethodBase> GetAvailableMethods(
        IPRMethodFeatures requiredFeatures)
        {

            return _methods.Values
                .Where(method =>
                (method.Features & requiredFeatures)
                == requiredFeatures)
                .ToList();

        }

        public IPRMethodBase GetIPRMethod(IPRMethodType methodType)
        {

            return _methods[methodType];

        }

        private void ValidateRequest(IPRRequest request)
        {

            ArgumentNullException.ThrowIfNull(request);

            ArgumentNullException.ThrowIfNull(request.InputData);

            if (!Enum.IsDefined(typeof(IPRScenarioType),
                request.Scenario))
            {
                throw new InvalidParameterException(
                    new ErrorMessage(
                        "IPR Scenario Error",
                        "Invalid scenario selected."));
            }

            if (!Enum.IsDefined(typeof(IPRMethodType),
                request.Method))
            {
                throw new InvalidParameterException(
                    new ErrorMessage(
                        "IPR Method Error",
                        "Invalid IPR method selected."));
            }

        }

        private IReadOnlyList<FlowDataRow> GeneratePresentIPR(IPRMethodBase method, 
            IPRInputData  input, ref NodalAnalysisValidationResult validationResult)
        {

            return method.GenerateIPR(input, ref validationResult);

        }

        private IReadOnlyList<FlowDataRow> GenerateFutureIPR(IFuturePredictable method,
            IPRInputData input, ref NodalAnalysisValidationResult validationResult)
        {
            
            return method.GenerateFutureIPR(input, ref validationResult);

        }

        public IPROutput ProcessRequest(IPRRequest request)
        {

            ValidateRequest(request);

            IReadOnlyList<FlowDataRow> output;
            NodalAnalysisValidationResult validationResult = new();

            if (!_methods.TryGetValue(
                request.Method,
                out IPRMethodBase method))
            {
                throw new InvalidParameterException(
                    new ErrorMessage(
                        "IPR Method Selection Error",
                        "The selected method is not available."));
            }

            output = request.Scenario switch
            {
                IPRScenarioType.Present =>
                    GeneratePresentIPR(
                        method,
                        request.InputData,
                        ref validationResult),

                IPRScenarioType.Future when method is IFuturePredictable futureMethod =>
                    GenerateFutureIPR(
                        futureMethod,
                        request.InputData,
                        ref validationResult),

                IPRScenarioType.Future =>
                    throw new InvalidParameterException(
                        new ErrorMessage(
                            "IPR Method Capability Error",
                            "The selected method does not support future prediction."
                        )),

                _ => throw new InvalidParameterException(
                    new ErrorMessage(
                        "IPR Scenario Error",
                        "The selected scenario is not supported."
                    ))
            };

            return new IPROutput(output, validationResult);

        }

    }

}
