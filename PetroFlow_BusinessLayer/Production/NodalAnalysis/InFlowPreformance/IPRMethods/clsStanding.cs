using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System.ComponentModel.DataAnnotations;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // A class that represents Standing's method for IPR.
    //
    // Standing's method is a modification of Vogel's method that handles
    // non-zero skin factor scenarios.
    //
    // Standing introduced the Flow Efficiency (FE) concept as a factor that
    // represents the effect of changes in skin factor on the flowing bottom-hole pressure (Pwf).
    //
    // The Flow Efficiency concept can be used to evaluate the effect of increasing FE
    // through stimulation.
    //
    // Therefore, this class is designed to store test values as objects and to provide
    // methods that calculate the effect of changing FE.

    public class clsStanding : IPRMethodBase, IFuturePredictable
    {
        public clsStanding()
        {


        }


        protected override void ValidateRawData(IPRInputData inputData, 
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            if (inputData.ReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Reservoir pressure has not been provided.");

            if (inputData.ReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid reservoir pressure: A positive value greater than zero is required.");


            //=====================
            // --- Test Data ---
            //=====================
            if (inputData.TestsData == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Test data has not been provided.");

            if (inputData.TestsData.Count < 1)
                throw new InvalidParameterException(
                    "Invalid test data: At least one test data row is required.");

            if (inputData.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more flow rates are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more bottom hole pressures are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure >= inputData.ReservoirPressure))
                throw new InvalidParameterException(
                    "Invalid test data: One or more bottom hole pressures are greater than reservoir pressure.");

            if (inputData.TestsData.Count > 1)
                validationResult.Warnings.Add(
                    "Multiple test data rows were provided. Only the first row will be used.");


            //===============================
            // --- Test Flow Efficiency ---
            //===============================
            if (inputData.TestFlowEfficiency == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Test Flow Efficiency has not been provided.");

            if (inputData.TestFlowEfficiency <= 0)
                throw new InvalidParameterException(
                    "Invalid Test Flow Efficiency: A positive value greater than zero is required.");

            if (inputData.TestFlowEfficiency > 1)
                validationResult.Warnings.Add("Flow efficiency is greater than 1. " +
                    "The Standing method is limited in this range;" +
                    " the maximum flow rate will be estimated using an approximate relationship.");


            //================================
            // --- Bubble Point Pressure ---
            //================================
            if (inputData.BubblePointPressure == null)
            {
                validationResult.Warnings.Add(
                    "Bubble point pressure was not provided. Reservoir will be assumed saturated.");
            }
            else
            {

                if (inputData.BubblePointPressure <= 0)
                    throw new InvalidParameterException(
                        "Invalid bubble point pressure: A positive value greater than zero is required.");

                if (inputData.BubblePointPressure.Value > inputData.ReservoirPressure.Value)
                {
                    validationResult.Warnings.Add(
                        "Bubble point pressure is greater than reservoir pressure. Reservoir will behave as saturated.");
                }

            }

        }

        private double CalculateTestPresentMaxFlowRate(IPRInputData input)
        {

            // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
            // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]

            double ReservoirPressure = input.ReservoirPressure.Value;
            double flowEfficiency = input.TestFlowEfficiency.Value;

            double x = 1 - input.TestsData[0].BottomHolePressure / ReservoirPressure; // 1 - pwf / pr
            double y = 1.8 * flowEfficiency * (x) - 0.8 * Math.Pow(flowEfficiency, 2) * Math.Pow(x, 2);

            return input.TestsData[0].FlowRate / y;


        }

        private double CalculateProductivityIndex(IPRInputData input)
        {

            // Calculate the productivity index:

            // if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.

            double BubblePointPressure = input.BubblePointPressure.Value;
            double ReservoirPressure = input.ReservoirPressure.Value;
            double TestFlowEfficiency = input.TestFlowEfficiency.Value;
            double ProductivityIndex;

            if (BubblePointPressure == null)
                throw new InvalidParameterException("Bubble Point Pressure is not provided.");

            if (input.TestsData[0].BottomHolePressure >= BubblePointPressure)
                ProductivityIndex = IPRGeneralFunctions.ProductivityIndex(input.TestsData[0].FlowRate,
                    ReservoirPressure, input.TestsData[0].BottomHolePressure);
            else
            {

                double x = 1 - input.TestsData[0].BottomHolePressure / BubblePointPressure;// (1 - pwf / pb)
                double y = 1.8 * x - 0.8 * (TestFlowEfficiency) * Math.Pow(x, 2);
                ProductivityIndex = input.TestsData[0].FlowRate /
                    ((ReservoirPressure - BubblePointPressure) + (BubblePointPressure / 1.8 * y));
            }

            return ProductivityIndex;

        }

        private List<InFlowDataRow> GenerateIPR_SaturatedReservoir(
            IPRInputData input)
        {

            // A method to Generate the IPR for a saturated reservoir.

            List<InFlowDataRow> data = new List<InFlowDataRow>(); // A list to store the data.

            double flowEfficiency = input.TestFlowEfficiency.Value;

            double TestPresentMaxFlowRate = CalculateTestPresentMaxFlowRate(input);

            IPRGenerationSettings GenerationSettings = new(input.GenerationSettings.PressureStepSize,
                input.GenerationSettings.MinimumPressure);
            double ReservoirPressure = input.ReservoirPressure.Value;

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (flowEfficiency > 1)
            {

                GenerationSettings.MinimumPressure = Math.Floor(ReservoirPressure * (1 - (1 / flowEfficiency))) + 1;

                double maxFlowRate = TestPresentMaxFlowRate * (0.624 + 0.376 * flowEfficiency);

                // adding the qo max to the data.
                data.Add(new InFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (double pressure = GenerationSettings.MinimumPressure; pressure <= ReservoirPressure; 
                pressure += GenerationSettings.PressureStepSize)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (flowEfficiency) * x - 0.8 * Math.Pow(flowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestPresentMaxFlowRate * y;

                data.Add(new InFlowDataRow(pressure, flowrate));

            }

            return data;


        }

        private List<InFlowDataRow> GenerateIPR_UnderSaturatedReservoir(
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

            List<InFlowDataRow> data = new List<InFlowDataRow>();

            double ProductivityIndex = CalculateProductivityIndex(input);
            IPRGenerationSettings GenerationSettings = new(input.GenerationSettings.PressureStepSize,
                input.GenerationSettings.MinimumPressure);
            double BubblePointPressure = input.BubblePointPressure.Value;
            double ReservoirPressure = input.ReservoirPressure.Value;
            double flowEfficiency = input.TestFlowEfficiency.Value;

            // Generate the IPR:
            for (double pressure = GenerationSettings.MinimumPressure; pressure <= ReservoirPressure; 
                pressure += GenerationSettings.PressureStepSize)
            {

                double qo = 0;

                if (pressure >= BubblePointPressure)
                    qo = ProductivityIndex * (ReservoirPressure - pressure);
                else
                {
                    // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].
                    double x = 1 - pressure / BubblePointPressure; // (1 - pwf / pb)
                    double y = 1.8 * x - 0.8 * (flowEfficiency) * Math.Pow(x, 2); // [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]
                    double z = ProductivityIndex * (ReservoirPressure - BubblePointPressure); // J * (pr - pb)

                    qo = z + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;

                }

                data.Add(new InFlowDataRow(pressure, qo));

            }

            return data;

        }

        /// <summary>
        /// Generates the inflow performance relationship (IPR) data rows for the current reservoir conditions.
        /// </summary>
        /// <remarks>The method selects the appropriate calculation based on whether the reservoir is
        /// saturated or undersaturated. Use the returned data to analyze well performance under the current reservoir
        /// state.</remarks>
        /// <returns>A list of <see cref="InFlowDataRow"/> objects representing the calculated IPR data. The list reflects
        /// either saturated or undersaturated reservoir conditions, depending on the current state.</returns>

        protected override List<InFlowDataRow> ComputeIPR(IPRInputData input, 
            ref NodalAnalysisValidationResult validationResult)
        {
            bool IsSaturated;
            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            if (input.BubblePointPressure.HasValue)
                IsSaturated = input.ReservoirPressure.Value <=
                    input.BubblePointPressure.Value;
            else
                IsSaturated = true;

            if (IsSaturated)
                return GenerateIPR_SaturatedReservoir(input);
            else
                return GenerateIPR_UnderSaturatedReservoir(input);

        }

        /// <summary>
        /// Updates the flow efficiency of the reservoir model and returns the resulting inflow data rows.
        /// </summary>
        /// <param name="newFlowEfficiency">The new flow efficiency value to apply to the reservoir. Must be a positive number.</param>
        /// <returns>A list of <see cref="InFlowDataRow"/> objects representing the inflow data after the flow efficiency
        /// change.</returns>
        //public List<InFlowDataRow> GenerateWithEfficiency(IPRInputData input,
        //    ref NodalAnalysisValidationResult validationResult)
        //{


        //    ValidateRawData(input, ref validationResult);

        //    IPRGenerationSettings GenerationSettings = input.GenerationSettings;
        //    double ReservoirPressure = input.ReservoirPressure.Value;
        //    double NewFlowEfficiency = input.NewFlowEfficiency.Value;

        //    bool IsSaturated;
        //    // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
        //    // If the bubble point pressure is not provided, the reservoir is treated as saturated.
        //    if (input.BubblePointPressure.HasValue)
        //        IsSaturated = input.ReservoirPressure.Value <=
        //            input.BubblePointPressure.Value;
        //    else
        //        IsSaturated = true;

        //    if (GenerationSettings.MinimumPressure > ReservoirPressure)
        //        throw new InvalidParameterException("Minimum pressure must be less than the reservoir pressure.");

        //    if (NewFlowEfficiency <= 0)
        //        throw new InvalidParameterException(
        //            "Invalid Test Flow Efficiency: A positive value greater than zero is required.");

        //    if (NewFlowEfficiency > 1)
        //        validationResult.Warnings.Add("Flow efficiency is greater than 1. " +
        //            "The Standing method is limited in this range;" +
        //            " the maximum flow rate will be estimated using an approximate relationship.");

        //    if (IsSaturated)
        //        return (GenerateIPR_SaturatedReservoir(newFlowEfficiency), validationResult);
        //    else
        //        return (GenerateIPR_UnderSaturatedReservoir(newFlowEfficiency), validationResult);

        //}

        public void ValidateFutureRawDataInput(IPRInputData Input,
            ref NodalAnalysisValidationResult validationResult)
        {


            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            if (Input.FutureReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future Reservoir pressure has not been provided.");

            if (Input.FutureReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure: A positive value greater than zero is required.");

            if (Input.FutureReservoirPressure >= 
                Input.ReservoirPressure.Value)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure: future reservoir pressure must be less than present reservoir pressure.");

            //=====================================
            // --- Oil Relative Permeability ---
            //=====================================
            if (Input.PresentOilRelativePermeability == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil relative permeability has not been provided.");

            if (Input.PresentOilRelativePermeability > 1 ||
                Input.PresentOilRelativePermeability < 0)
                throw new InvalidParameterException(
                    "Invalid present oil relative permeability: A value between 0 and 1 is required.");

            if (Input.FutureOilRelativePermeability == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil relative permeability has not been provided.");

            if (Input.FutureOilRelativePermeability > 1 ||
                Input.FutureOilRelativePermeability < 0)
                throw new InvalidParameterException(
                    "Invalid future oil relative permeability: A value between 0 and 1 is required.");

            //=====================================
            // --- Oil Formation Volume Factor ---
            //=====================================
            if (Input.PresentOilFormationVolumeFactor == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil formation volume factor has not been provided.");

            if (Input.PresentOilFormationVolumeFactor <= 0)
                throw new InvalidParameterException(
                    "Invalid present oil formation volume factor: A positive value greater than zero is required.");

            if (Input.FutureOilFormationVolumeFactor == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil formation volume factor has not been provided.");

            if (Input.FutureOilFormationVolumeFactor <= 0)
                throw new InvalidParameterException(
                    "Invalid future oil formation volume factor: A positive value greater than zero is required.");

            //========================
            // --- Oil Viscosity ---
            //========================
            if (Input.PresentOilViscosity == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil viscosity has not been provided.");

            if (Input.PresentOilViscosity <= 0)
                throw new InvalidParameterException(
                    "Invalid present oil viscosity: A positive value greater than zero is required.");

            if (Input.FutureOilViscosity == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil viscosity has not been provided.");

            if (Input.FutureOilViscosity <= 0)
                throw new InvalidParameterException(
                    "Invalid future oil viscosity: A positive value greater than zero is required.");


        }

        private double CalcualtFutureMaxFlowrate(IPRInputData input)
        {

            // A method to calcualte Future Max Flowate.
            // The procedure:
            // 1. Calculate the present max flowrate.
            // 2. using the fluid properties calcuate the future max flowrate using the following eqution:
            // qo(max)F = qo(max)P * [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]



            double TestPresentMaxFlowRate = CalculateTestPresentMaxFlowRate(input);

            double FutureReservoirPressure = input.FutureReservoirPressure.Value;
            double FutureOilReleativePremeability = input.FutureOilRelativePermeability.Value;
            double FutureOilViscosity = input.FutureOilViscosity.Value;
            double FutureOilFomationVoilumeFactor = input.FutureOilFormationVolumeFactor.Value;

            double PresentPressureFunction = input.PresentOilRelativePermeability.Value /
                (input.PresentOilViscosity.Value * input.PresentOilFormationVolumeFactor.Value);

            double FuturePressureFunction = FutureOilReleativePremeability / (FutureOilViscosity * FutureOilFomationVoilumeFactor);
             
            double x = (FutureReservoirPressure * FuturePressureFunction) 
                / (input.ReservoirPressure.Value * PresentPressureFunction);// [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            return TestPresentMaxFlowRate * x;

        }

        public List<InFlowDataRow> GenerateFutureIPR(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            // A method to generate The future IPR using Standing.

            ValidateFutureRawDataInput(input, ref validationResult);

            List<InFlowDataRow> Data = new List<InFlowDataRow>();

            double FutureReservoirPressure = input.FutureReservoirPressure.Value;
            double FutureOilReleativePremeability = input.FutureOilRelativePermeability.Value;
            double FutureOilViscosity = input.FutureOilViscosity.Value;
            double FutureOilFomationVoilumeFactor = input.FutureOilFormationVolumeFactor.Value;
            IPRGenerationSettings GenerationSettings = input.GenerationSettings;

            double futureMaxFlowrate = CalcualtFutureMaxFlowrate(input);

            // Genrate the IPR using the followin equation:
            // qoF = qo(max)F * [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]

            for (double Pressure = GenerationSettings.MinimumPressure; Pressure <= FutureReservoirPressure; 
                Pressure += GenerationSettings.PressureStepSize)
            {

                double x = Pressure / FutureReservoirPressure;// (Pwf/Pr)
                double y = 1 - 0.2 * x - 0.8 * x * x; // [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]
                double oilFlowrate = futureMaxFlowrate * y;

                Data.Add(new InFlowDataRow(Pressure, oilFlowrate));

            }

            return Data;

        }


    }

}
