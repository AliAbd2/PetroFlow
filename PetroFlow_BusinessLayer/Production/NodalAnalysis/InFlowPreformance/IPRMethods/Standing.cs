using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    internal class Standing : IPRMethodBase, IFuturePredictable
    {

        public override IPRMethodType MethodType => IPRMethodType.Standing;

        public override string DisplayName => "Standing";

        public override IPRMethodFeatures Features => 
            IPRMethodFeatures.Oil
            | IPRMethodFeatures.FuturePrediction
            | IPRMethodFeatures.VerticalWell;

        const double StandingLinearCoefficient = 1.8;
        const double StandingQuadraticCoefficient = 0.8;

        private void ValidateTestData(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {
            // A method to validate the test data.
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
        }

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
            ValidateTestData(input, ref validationResult);


            //===============================
            // --- Test Flow Efficiency ---
            //===============================
            Validation.IsGreaterThanZero(input.TestFlowEfficiency, "Test Flow Efficiency");

            if (input.TestFlowEfficiency > 1)
            {
                validationResult.AddWarning(
                    new ErrorMessage(
                        "Outside Recommended Range",
                        "Flow efficiency is greater than 1.0. The Standing model is not directly applicable in this range, " +
                        "so the maximum flow rate will be estimated using an approximate relationship."));
            }


            //================================
            // --- Bubble Point Pressure ---
            //================================
            if (input.BubblePointPressure == null)
                validationResult.AddWarning(
                    IPRErrorMessages.BubblePointPressureNotProvidedWarning);
            else
                Validation.IsGreaterThanZero(input.BubblePointPressure, "Bubble Point Pressure");


        }

        private double CalculateTestMaxFlowRate(IPRInputData input)
        {

            // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
            // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]

            double pr = input.ReservoirPressure!.Value;
            double FE = input.TestFlowEfficiency!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure;

            double pressureRatio = 1 - pwf / pr;
            double standingTerm = StandingLinearCoefficient * FE * pressureRatio 
                - StandingQuadraticCoefficient * pressureRatio * pressureRatio
                * FE * FE;

            return qTest / standingTerm;


        }

        private List<FlowDataRow> GenerateIPR_SaturatedReservoir(
            IPRInputData input)
        {

            // A method to Generate the IPR for a saturated reservoir.

            List<FlowDataRow> data = new List<FlowDataRow>(); // A list to store the data.

            double FE = input.TestFlowEfficiency!.Value;
            double pr = input.ReservoirPressure!.Value;
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;

            double testMaxFlowRate = CalculateTestMaxFlowRate(input);

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (FE > 1)
            {

                double standingFlowRateCorrectionCoefficient1 = 0.624;
                double standingFlowRateCorrectionCoefficient2 = 0.376;

                minimumPressure = Math.Floor(pr * (1 - (1 / FE))) + 1;

                double maxFlowRate = testMaxFlowRate *
                    (standingFlowRateCorrectionCoefficient1 
                    + standingFlowRateCorrectionCoefficient2 * FE);

                // adding the qo max to the data.
                data.Add(new FlowDataRow(0, maxFlowRate));

            }



            // Generating the IPR:
            for (double pressure = minimumPressure; pressure <= pr; 
                pressure += pressureStepSize)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double pressureRatio = 1 - pressure / pr;

                double standingTerm = StandingLinearCoefficient * (FE) * pressureRatio
                    - StandingQuadraticCoefficient * FE * FE * pressureRatio * pressureRatio;

                double flowrate = testMaxFlowRate * standingTerm;

                data.Add(new FlowDataRow(pressure, flowrate));

            }

            return data;


        }

        private double CalculateProductivityIndex(IPRInputData input)
        {

            // Calculate the productivity index:

            // if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.

            double pb = input.BubblePointPressure!.Value;
            double pr = input.ReservoirPressure!.Value;
            double FE = input.TestFlowEfficiency!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure;
            double ProductivityIndex;

            if (pwf >= pb)
                ProductivityIndex = IPRGeneralFunctions.ProductivityIndex(qTest,
                    pr, pwf);
            else
            {

                double pressureRatio = 1 - pwf / pb;

                double standingTerm = StandingLinearCoefficient * pressureRatio
                    - StandingQuadraticCoefficient * FE * pressureRatio * pressureRatio;

                ProductivityIndex = qTest /
                    (pr - pb + (pb / StandingLinearCoefficient * standingTerm));
            }

            return ProductivityIndex;

        }

        private List<FlowDataRow> GenerateIPR_UnderSaturatedReservoir(
            IPRInputData input)
        {

            // A method to generate the IPR data for an undersaturated reservoir.

            // The procedure:
            // 1. check if the test bottom-hole pressure (pwf) is above or below the bubble point.
            // 2. if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.
            // 3. assume bottom-hole pressure (pwf) and calculate qo using:
            // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].

            List<FlowDataRow> data = new List<FlowDataRow>();

            double productivityIndex = CalculateProductivityIndex(input);
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;
            double pb = input.BubblePointPressure!.Value;
            double pr = input.ReservoirPressure!.Value;
            double flowEfficiency = input.TestFlowEfficiency!.Value;

            // Generate the IPR:
            for (double pressure = minimumPressure; pressure <= pr; 
                pressure += pressureStepSize)
            {

                double qo = 0;

                if (pressure >= pb)
                    qo = productivityIndex * (pr - pressure);
                else
                {
                    // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].
                    double pressureRatio = 1 - pressure / pb;

                    double standingTerm = StandingLinearCoefficient * pressureRatio
                        - StandingQuadraticCoefficient * (flowEfficiency) * pressureRatio * pressureRatio;

                    qo = productivityIndex * (pr - pb) 
                        + productivityIndex * pb / StandingLinearCoefficient * standingTerm;

                }

                data.Add(new FlowDataRow(pressure, qo));

            }

            return data;

        }

        protected override List<FlowDataRow> ComputeIPR(IPRInputData input)
        {

            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            bool isSaturated = IPRGeneralFunctions.IsSaturatedReservoir(input);

            if (isSaturated)
                return GenerateIPR_SaturatedReservoir(input);
            else
                return GenerateIPR_UnderSaturatedReservoir(input);

        }

        public void ValidateFutureRawDataInput(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {


            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            Validation.IsGreaterThanZero(input.FutureReservoirPressure, "Future Reservoir Pressure");
            Validation.IsGreaterThan(input.ReservoirPressure, input.FutureReservoirPressure,
                "Future Reservoir Pressure", "Reservoir Pressure");

            //=====================================
            // --- Oil Relative Permeability ---
            //=====================================
            Validation.IsInRange(input.PresentOilRelativePermeability, 0, 1, "Present Oil Relative Permeability");
            Validation.IsInRange(input.FutureOilRelativePermeability, 0, 1, "Future Oil Relative Permeability");

            //=====================================
            // --- Oil Formation Volume Factor ---
            //=====================================
            Validation.IsGreaterThanZero(input.PresentOilFormationVolumeFactor, "Present Oil Formation Volume Factor");
            Validation.IsGreaterThanZero(input.FutureOilFormationVolumeFactor, "Future Oil Formation Volume Factor");

            //========================
            // --- Oil Viscosity ---
            //========================
            Validation.IsGreaterThanZero(input.PresentOilViscosity, "Present Oil Viscosity");
            Validation.IsGreaterThanZero(input.FutureOilViscosity, "Future Oil Viscosity");

            //=====================
            // --- Test Data ---
            //=====================
            ValidateTestData(input, ref validationResult);

        }

        private double CalculatePresentMaxFlowRate(IPRInputData input)
        {
            // A method to calculate the present max flowrate using the test data and the vogel's equation.
            double pr = input.ReservoirPressure!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure;

            double pressureRatio = pwf / pr;

            double vogelTerm = IPRGeneralFunctions.CalculateVogelTerm(pressureRatio);

            return qTest / vogelTerm;
        }

        private double CalcualtFutureMaxFlowrate(IPRInputData input)
        {

            // A method to calcualte Future Max Flowate.
            // The procedure:
            // 1. Calculate the present max flowrate.
            // 2. using the fluid properties calcuate the future max flowrate using the following eqution:
            // qo(max)F = qo(max)P * [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            double TestPresentMaxFlowRate = CalculatePresentMaxFlowRate(input);

            double prp = input.ReservoirPressure!.Value;
            double prf = input.FutureReservoirPressure!.Value;
            double krop = input.PresentOilRelativePermeability!.Value;
            double krof = input.FutureOilRelativePermeability!.Value;
            double muop = input.PresentOilViscosity!.Value;
            double muf = input.FutureOilViscosity!.Value;
            double bop = input.PresentOilFormationVolumeFactor!.Value;
            double bof = input.FutureOilFormationVolumeFactor!.Value;

            double PresentPressureFunction = krop / (muop * bop);

            double FuturePressureFunction = krof / (muf * bof);
             
            double pressureFunctonRation = (prf * FuturePressureFunction) 
                / (prp * PresentPressureFunction);

            return TestPresentMaxFlowRate * pressureFunctonRation;

        }

        public List<FlowDataRow> GenerateFutureIPR(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            // A method to generate The future IPR using Standing.

            ValidateFutureRawDataInput(input, ref validationResult);

            List<FlowDataRow> Data = new List<FlowDataRow>();

            double prf = input.FutureReservoirPressure!.Value;
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;

            double futureMaxFlowrate = CalcualtFutureMaxFlowrate(input);

            // Genrate the IPR using the followin equation:
            // qoF = qo(max)F * [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]

            for (double Pressure = minimumPressure; Pressure <= prf; 
                Pressure += pressureStepSize)
            {

                double pressureRatio = Pressure / prf;

                double vogelTerm = IPRGeneralFunctions.CalculateVogelTerm(pressureRatio);

                double oilFlowrate = futureMaxFlowrate * vogelTerm;

                Data.Add(new FlowDataRow(Pressure, oilFlowrate));

            }

            return Data;

        }


    }

}
