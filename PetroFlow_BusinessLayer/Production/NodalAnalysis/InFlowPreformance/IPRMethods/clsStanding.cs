using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
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

    public class clsStanding : IIPRMethod, IEfficiencyAdjustable, IFuturePredictable
    {
        public string Name { get; set; }

        public enIPRMethodType MethodType { get { return enIPRMethodType.Standing; } }

        public double ReservoirPressure { get; set; }

        public double? BubblePointPressure { get; set; }

        public List<clsInFlowDataRow> TestsData { get; set; }

        public List<clsInFlowDataRow> GeneratedData { get; set; }

        public clsCurvePlotSettings CurvePlotSetting { get; set; }

        public bool IsInputValid { get; set; }

        public clsIPRGenerationSettings GenerationSettings { get; set; }

        private double TestFlowEfficiency;

        private double TestPresentMaxFlowRate;

        private double ProductivityIndex;

        private double PresentOilRelativePermeability;

        private double PresentOilViscosity;

        private double PresentOilFomationVoilumeFactor;

        // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
        // If the bubble point pressure is not provided, the reservoir is treated as saturated.
        private bool IsSaturated
        {
            get
            {

                if (BubblePointPressure == null)
                    return true;

                return ReservoirPressure <= BubblePointPressure;

            }
        }

        public clsStanding()
        {
            Name = "";
            TestsData = new();
            GeneratedData = new();
            CurvePlotSetting = new clsCurvePlotSettings();

        }

        public Utility.Validation.ValidationResult SetInputData(clsPresentIPRDataInput inputData)
        {
            Utility.Validation.ValidationResult validationResult = new();

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

            ReservoirPressure = inputData.ReservoirPressure.Value;
            TestsData = inputData.TestsData;
            BubblePointPressure = inputData.BubblePointPressure;
            TestFlowEfficiency = inputData.TestFlowEfficiency.Value;

            IsInputValid = true;

            return validationResult;
        }

        private void CalculateTestPresentMaxFlowRate(double flowEfficiency)
        {

            // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
            // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]

            double x = 1 - TestsData[0].BottomHolePressure / ReservoirPressure; // 1 - pwf / pr
            double y = 1.8 * flowEfficiency * (x) - 0.8 * Math.Pow(flowEfficiency, 2) * Math.Pow(x, 2);

            TestPresentMaxFlowRate = TestsData[0].FlowRate / y;


        }

        private void CalculateProductivityIndex()
        {

            // Calculate the productivity index:

            // if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.

            if (BubblePointPressure == null)
                throw new InvalidParameterException("Bubble Point Pressure is not provided.");

            if (TestsData[0].BottomHolePressure >= BubblePointPressure)
                ProductivityIndex = clsIPRGeneralFunctions.ProductivityIndex(TestsData[0].FlowRate,
                    ReservoirPressure, TestsData[0].BottomHolePressure);
            else
            {

                double x = 1 - TestsData[0].BottomHolePressure / BubblePointPressure.Value;// (1 - pwf / pb)
                double y = 1.8 * x - 0.8 * (TestFlowEfficiency) * Math.Pow(x, 2);
                ProductivityIndex =  TestsData[0].FlowRate /
                    ((ReservoirPressure - BubblePointPressure.Value) + (BubblePointPressure.Value / 1.8 * y));
            }

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir(double flowEfficiency)
        {

            // A method to Generate the IPR for a saturated reservoir.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            CalculateTestPresentMaxFlowRate(flowEfficiency);

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (flowEfficiency > 1)
            {

                GenerationSettings.MinimumPressure = Math.Floor(ReservoirPressure * (1 - (1 / flowEfficiency))) + 1;

                double maxFlowRate = TestPresentMaxFlowRate * (0.624 + 0.376 * flowEfficiency);

                // adding the qo max to the data.
                data.Add(new clsInFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (double pressure = GenerationSettings.MinimumPressure; pressure <= ReservoirPressure; 
                pressure += GenerationSettings.PressureStepSize)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (flowEfficiency) * x - 0.8 * Math.Pow(flowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestPresentMaxFlowRate * y;

                data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            return data;


        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir(double flowEfficiency)
        {

            // A method to generate the IPR data for an undersaturated reservoir.

            // The procedure:
            // 1. check if the test bottom-hole pressure (pwf) is above or below the bubble point.
            // 2. if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.
            // 3. assume bottom-hole pressure (pwf) and calculate qo using:
            // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

            CalculateProductivityIndex();

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
                    double x = 1 - pressure / BubblePointPressure.Value; // (1 - pwf / pb)
                    double y = 1.8 * x - 0.8 * (flowEfficiency) * Math.Pow(x, 2); // [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]
                    double z = ProductivityIndex * (ReservoirPressure - BubblePointPressure.Value); // J * (pr - pb)

                    qo = z + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;

                }

                data.Add(new clsInFlowDataRow(pressure, qo));

            }

            return data;

        }

        /// <summary>
        /// Generates the inflow performance relationship (IPR) data rows for the current reservoir conditions.
        /// </summary>
        /// <remarks>The method selects the appropriate calculation based on whether the reservoir is
        /// saturated or undersaturated. Use the returned data to analyze well performance under the current reservoir
        /// state.</remarks>
        /// <returns>A list of <see cref="clsInFlowDataRow"/> objects representing the calculated IPR data. The list reflects
        /// either saturated or undersaturated reservoir conditions, depending on the current state.</returns>
        public void GenerateIPR()
        {

            if (!IsInputValid)
                throw new InvalidOperationException("Invalid operation: " +
                    "Calculation method was called before input data was set. Call SetInputData() first.");

            if (GenerationSettings.MinimumPressure > ReservoirPressure)
                throw new InvalidParameterException("Minimum pressure must be less than the reservoir pressure.");

            if (IsSaturated)
                GeneratedData = GenerateIPR_SaturatedReservoir(TestFlowEfficiency);
            else
                GeneratedData = GenerateIPR_UnderSaturatedReservoir(TestFlowEfficiency);

        }

        /// <summary>
        /// Updates the flow efficiency of the reservoir model and returns the resulting inflow data rows.
        /// </summary>
        /// <param name="newFlowEfficiency">The new flow efficiency value to apply to the reservoir. Must be a positive number.</param>
        /// <returns>A list of <see cref="clsInFlowDataRow"/> objects representing the inflow data after the flow efficiency
        /// change.</returns>
        public (List<clsInFlowDataRow>, ValidationResult) GenerateWithEfficiency(double newFlowEfficiency)
        {

            Utility.Validation.ValidationResult validationResult = new();

            if (!IsInputValid)
                throw new InvalidOperationException("Invalid operation: " +
                    "Calculation method was called before input data was set. Call SetInputData() first.");

            if (GenerationSettings.MinimumPressure > ReservoirPressure)
                throw new InvalidParameterException("Minimum pressure must be less than the reservoir pressure.");

            if (newFlowEfficiency <= 0)
                throw new InvalidParameterException(
                    "Invalid Test Flow Efficiency: A positive value greater than zero is required.");

            if (newFlowEfficiency > 1)
                validationResult.Warnings.Add("Flow efficiency is greater than 1. " +
                    "The Standing method is limited in this range;" +
                    " the maximum flow rate will be estimated using an approximate relationship.");

            if (IsSaturated)
                return (GenerateIPR_SaturatedReservoir(newFlowEfficiency), validationResult);
            else
                return (GenerateIPR_UnderSaturatedReservoir(newFlowEfficiency), validationResult);

        }

        public Utility.Validation.ValidationResult ValidateFutureInput(clsFutureIPRDataInput futureDataInput)
        {

            Utility.Validation.ValidationResult validationResult = new();

            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            if (futureDataInput.FutureReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future Reservoir pressure has not been provided.");

            if (futureDataInput.FutureReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure: A positive value greater than zero is required.");

            if (futureDataInput.FutureReservoirPressure > ReservoirPressure)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure: future reservoir pressure must be less than present reservoir pressure.");

            //=====================================
            // --- Oil Relative Permeability ---
            //=====================================
            if (futureDataInput.PresentOilRelativePermeability == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil relative permeability has not been provided.");

            if (futureDataInput.PresentOilRelativePermeability > 1 ||
                futureDataInput.PresentOilRelativePermeability < 0)
                throw new InvalidParameterException(
                    "Invalid present oil relative permeability: A value between 0 and 1 is required.");

            if (futureDataInput.FutureOilRelativePermeability == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil relative permeability has not been provided.");

            if (futureDataInput.FutureOilRelativePermeability > 1 ||
                futureDataInput.FutureOilRelativePermeability < 0)
                throw new InvalidParameterException(
                    "Invalid future oil relative permeability: A value between 0 and 1 is required.");

            //=====================================
            // --- Oil Formation Volume Factor ---
            //=====================================
            if (futureDataInput.PresentOilFormationVolumeFactor == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil formation volume factor has not been provided.");

            if (futureDataInput.PresentOilFormationVolumeFactor <= 0)
                throw new InvalidParameterException(
                    "Invalid present oil formation volume factor: A positive value greater than zero is required.");

            if (futureDataInput.FutureOilFormationVolumeFactor == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil formation volume factor has not been provided.");

            if (futureDataInput.FutureOilFormationVolumeFactor <= 0)
                throw new InvalidParameterException(
                    "Invalid future oil formation volume factor: A positive value greater than zero is required.");

            //========================
            // --- Oil Viscosity ---
            //========================
            if (futureDataInput.PresentOilViscosity == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present oil viscosity has not been provided.");

            if (futureDataInput.PresentOilViscosity <= 0)
                throw new InvalidParameterException(
                    "Invalid present oil viscosity: A positive value greater than zero is required.");

            if (futureDataInput.FutureOilViscosity == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future oil viscosity has not been provided.");

            if (futureDataInput.FutureOilViscosity <= 0)
                throw new InvalidParameterException(
                    "Invalid future oil viscosity: A positive value greater than zero is required.");

            PresentOilRelativePermeability = futureDataInput.PresentOilRelativePermeability.Value;
            PresentOilViscosity = futureDataInput.PresentOilViscosity.Value;
            PresentOilFomationVoilumeFactor = futureDataInput.PresentOilFormationVolumeFactor.Value;

            return validationResult;

        }

        private double CalcualtFutureMaxFlowrate(clsFutureIPRDataInput dataInput)
        {

            // A method to calcualte Future Max Flowate.
            // The procedure:
            // 1. Calculate the present max flowrate.
            // 2. using the fluid properties calcuate the future max flowrate using the following eqution:
            // qo(max)F = qo(max)P * [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            CalculateTestPresentMaxFlowRate(TestFlowEfficiency);

            double FutureReservoirPressure = dataInput.FutureReservoirPressure.Value;
            double FutureOilReleativePremeability = dataInput.FutureOilRelativePermeability.Value;
            double FutureOilViscosity = dataInput.FutureOilViscosity.Value;
            double FutureOilFomationVoilumeFactor = dataInput.FutureOilFormationVolumeFactor.Value;

            double PresentPressureFunction = PresentOilRelativePermeability / (PresentOilViscosity * PresentOilFomationVoilumeFactor);

            double FuturePressureFunction = FutureOilReleativePremeability / (FutureOilViscosity * FutureOilFomationVoilumeFactor);
             
            double x = (FutureReservoirPressure * FuturePressureFunction) 
                / (ReservoirPressure * PresentPressureFunction);// [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            return TestPresentMaxFlowRate * x;

        }

        public void GenerateFutureIPR(clsFutureIPRDataInput dataInput)
        {

            // A method to generate The future IPR using Standing.

            ValidateFutureInput(dataInput);

            List<clsInFlowDataRow> Data = new List<clsInFlowDataRow>();

            double FutureReservoirPressure = dataInput.FutureReservoirPressure.Value;
            double FutureOilReleativePremeability = dataInput.FutureOilRelativePermeability.Value;
            double FutureOilViscosity = dataInput.FutureOilViscosity.Value;
            double FutureOilFomationVoilumeFactor = dataInput.FutureOilFormationVolumeFactor.Value;

            double futureMaxFlowrate = CalcualtFutureMaxFlowrate(dataInput);

            // Genrate the IPR using the followin equation:
            // qoF = qo(max)F * [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]

            for (double Pressure = GenerationSettings.MinimumPressure; Pressure <= FutureReservoirPressure; 
                Pressure += GenerationSettings.PressureStepSize)
            {

                double x = Pressure / FutureReservoirPressure;// (Pwf/Pr)
                double y = 1 - 0.2 * x - 0.8 * x * x; // [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]
                double oilFlowrate = futureMaxFlowrate * y;

                Data.Add(new clsInFlowDataRow(Pressure, oilFlowrate));

            }

            GeneratedData = Data;

        }


    }

}
