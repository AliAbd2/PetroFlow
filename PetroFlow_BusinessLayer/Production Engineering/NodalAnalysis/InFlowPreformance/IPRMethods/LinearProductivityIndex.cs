using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.Errors_and_Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRMethods
{
    internal class LinearProductivityIndex : IPRMethodBase
    {

        public override IPRMethodType MethodType => IPRMethodType.LinearProductivityIndex;

        public override string DisplayName => "Linear Productivity Index";

        public override IPRMethodFeatures Features => 
            IPRMethodFeatures.Oil
            | IPRMethodFeatures.VerticalWell;

        public override IPRRequirements InputRequirements => new()
        {

            Present = IPRInputFields.ReservoirPressure,

            Future = IPRFutureInputFields.None,

            TestData = IPRTestDataRequirement.SinglePoint

        };

        protected override void ValidateRawData(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            Validation.IsGreaterThanZero(input.ReservoirPressure, "Reservoir Pressure");

            //=====================
            // --- Test Data ---
            //=====================
            if (input.TestsData == null)
                throw new MissingRequiredInputException(IPRErrorMessages.MissingTestData);

            if (input.TestsData.Count < 1)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount(DisplayName, 1));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);

            if (input.TestsData.Count > 1)
                validationResult.AddWarning(
                    IPRErrorMessages.OnlyOneTestDataPointWillBeUsedWarning);

        }

        protected override List<FlowDataRow> ComputeIPR(IPRInputData input)
        {
            // Implement the logic to generate IPR data using the Linear Productivity Index method

            List<FlowDataRow> iprData = new List<FlowDataRow>();

            double pr = input.ReservoirPressure!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure;
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings!.PressureStepSize;

            double productivityIndex = IPRGeneralFunctions.ProductivityIndex(qTest, pr, pwf);

            // Generate multiple IPR data points to maintain a consistent output format
            // across all IPR methods. Although the linear PI model is fully defined by
            // two points, generating a complete data set simplifies downstream
            // calculations such as VLP generation and operating point analysis.
            for (double pressure = minimumPressure;
                pressure <= pr;
                pressure += pressureStepSize)
            {

                double qo = productivityIndex * (pr - pressure);

                iprData.Add(new FlowDataRow(pressure, qo));

            }

            return iprData;

        }

    }
}
