using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        private double TestFlowEfficiency;

        private double TestPresentMaxFlowRate;

        private double PressureStepSize;

        private double MinimumPressure;

        private double ProductivityIndex;

        private double? PresentOilRelativePermeability;

        private double? PresentOilViscosity;

        private double? PresentOilFomationVoilumeFactor;

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

            CurvePlotSetting = new clsCurvePlotSettings();

        }

        public clsValidationResult SetInputData(Dictionary<enIPRData, object> inputData)
        {
            clsValidationResult validationResult = new();

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            if (!inputData.TryGetValue(enIPRData.ReservoirPressure, out var pressureObj))
                throw new exMissingRequiredInputException(
                    "Cannot generate IPR: Reservoir pressure has not been provided.");

            if (pressureObj is not double reservoirPressure)
                throw new exInvalidIPRParameterException(
                    "Invalid reservoir pressure: Expected a numeric value.");

            if (reservoirPressure <= 0)
                throw new exInvalidIPRParameterException(
                    "Invalid reservoir pressure: A positive value greater than zero is required.");

            //=====================
            // --- Test Data ---
            //=====================

            if (!inputData.TryGetValue(enIPRData.TestData, out var testDataObj))
                throw new exMissingRequiredInputException(
                    "Cannot generate IPR: Test data has not been provided.");

            if (testDataObj is not List<clsInFlowDataRow> rows)
                throw new exInvalidIPRParameterException(
                    "Invalid test data: Expected a list of InFlow data rows.");

            if (rows.Count == 0)
                throw new exInvalidIPRParameterException(
                    "Invalid test data: At least one test data row is required.");

            if (rows.Any(x => x.FlowRate <= 0))
                throw new exInvalidIPRParameterException(
                    "Invalid test data: One or more flow rates are zero or negative.");

            if (rows.Any(x => x.BottomHolePressure <= 0))
                throw new exInvalidIPRParameterException(
                    "Invalid test data: One or more bottom hole pressures are zero or negative.");

            //================================
            // --- Bubble Point Pressure ---
            //================================

            double? bubblePointPressure = null;

            if (!inputData.TryGetValue(enIPRData.BubblePointPressure, out var bubbleObj) || bubbleObj == null)
            {
                validationResult.Warnings.Add(
                    "Bubble point pressure was not provided. Reservoir will be assumed saturated.");
            }
            else
            {
                if (bubbleObj is not double bp)
                    throw new exInvalidIPRParameterException(
                        "Invalid bubble point pressure: Expected a numeric value.");

                if (bp <= 0)
                    throw new exInvalidIPRParameterException(
                        "Invalid bubble point pressure: A positive value greater than zero is required.");

                bubblePointPressure = bp;
            }

            ReservoirPressure = reservoirPressure;
            TestsData = rows;
            BubblePointPressure = bubblePointPressure;

            return validationResult;
        }

        private void CalcualteTestPresentMaxFlowRate()
        {

            // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
            // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]

            if (TestFlowEfficiency > 0)
            {

                double x = 1 - TestsData[0].BottomHolePressure / ReservoirPressure; // 1 - pwf / pr
                double y = 1.8 * TestFlowEfficiency * (x) - 0.8 * Math.Pow(TestFlowEfficiency, 2) * Math.Pow(x, 2);

                TestPresentMaxFlowRate = TestsData[0].FlowRate / y;
            }
            else
            {

                double x = TestsData[0].BottomHolePressure / ReservoirPressure;
                double y = 1 - 0.2 * x - 0.8 * x * x;

                TestPresentMaxFlowRate = TestsData[0].FlowRate / y;
            }



        }

        private void CalcualateProductivityIndex()
        {

            // Calculate the productivity index:

            // if the test bottom-hole pressure is above or equal to the bubble point:
            // Calculate the productivity index using: J = qo / (Pr - Pwf),
            // otherwise: J = qo / {(pr - pb) + pb/1.8 * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]}.

            if (TestsData[0].BottomHolePressure >= BubblePointPressure)
                ProductivityIndex = clsIPRGeneralFunctions.ProductivityIndex(TestsData[0].FlowRate,
                    ReservoirPressure, TestsData[0].BottomHolePressure);
            else
            {

                double x = 1 - TestsData[0].BottomHolePressure / (double)BubblePointPressure;// (1 - pwf / pb)
                double y = 1.8 * x - 0.8 * (TestFlowEfficiency) * Math.Pow(x, 2);
                ProductivityIndex =  TestsData[0].FlowRate /
                    ((ReservoirPressure - (double)BubblePointPressure) + ((double)BubblePointPressure / 1.8 * y));
            }

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to Generate the IPR for a saturated reservoir.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            CalcualteTestPresentMaxFlowRate();

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            int minimumBottomHolePressure = 0;
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (TestFlowEfficiency > 1)
            {

                minimumBottomHolePressure = (int)Math.Floor(ReservoirPressure * (1 - (1 / TestFlowEfficiency))) + 1;

                double maxFlowRate = TestPresentMaxFlowRate * (0.624 + 0.376 * TestFlowEfficiency);

                // adding the qo max to the data.
                data.Add(new clsInFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (int pressure = minimumBottomHolePressure; pressure <= ReservoirPressure; pressure++)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (TestFlowEfficiency) * x - 0.8 * Math.Pow(TestFlowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestPresentMaxFlowRate * y;

                data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            return data;


        }

        private List<clsInFlowDataRow> ChangeFlowEfficiency_SaturatedReservoir(double newFlowEfficiency)
        {

            // A method to generate the IPR for flow Efficiency different from the test flow Efficiency.


            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            CalcualteTestPresentMaxFlowRate();

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            int minimumBottomHolePressure = 0;
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (newFlowEfficiency > 1)
            {

                minimumBottomHolePressure = (int)Math.Floor(ReservoirPressure * (1 - (1 / newFlowEfficiency))) + 1;

                double maxFlowRate = TestPresentMaxFlowRate * (0.624 + 0.376 * newFlowEfficiency);

                // adding the qo max to the data.
                data.Add(new clsInFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (int pressure = minimumBottomHolePressure; pressure <= ReservoirPressure; pressure++)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (newFlowEfficiency) * x - 0.8 * Math.Pow(newFlowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestPresentMaxFlowRate * y;

                data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            return data;

        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir()
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

            CalcualateProductivityIndex();

            // Generate the IPR:
            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double qo = 0;

                if (pressure >= BubblePointPressure)
                    qo = ProductivityIndex * (ReservoirPressure - pressure);
                else
                {
                    // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].
                    double x = 1 - pressure / (double)BubblePointPressure; // (1 - pwf / pb)
                    double y = 1.8 * x - 0.8 * (TestFlowEfficiency) * Math.Pow(x, 2); // [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]
                    double z = ProductivityIndex * (ReservoirPressure - (double)BubblePointPressure); // J * (pr - pb)

                    qo = z + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;

                }

                    data.Add(new clsInFlowDataRow(pressure, qo));

            }

            return data;

        }

        private List<clsInFlowDataRow> ChangeFlowEfficiency_UnderSaturatedReservoir(double newFlowEfficiency)
        {

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

            CalcualateProductivityIndex();

            double newProductivityIndex = ProductivityIndex * newFlowEfficiency / TestFlowEfficiency;


            // Generate the IPR:
            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double qo = 0;

                if (pressure >= BubblePointPressure)
                    qo = newProductivityIndex * (ReservoirPressure - pressure);
                else
                {
                    // qo = J * (pr - pb) + ( J * Pb / 1.8) * [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2].
                    double x = 1 - pressure / (double)BubblePointPressure; // (1 - pwf / pb)
                    double y = 1.8 * x - 0.8 * (newFlowEfficiency) * Math.Pow(x, 2); // [1.8 * (1 - pwf / pb) - 0.8 * (FE) * (1 - (pwf / pb))^2]
                    double z = newProductivityIndex * (ReservoirPressure - (double)BubblePointPressure); // J * (pr - pb)

                    qo = z + (newProductivityIndex * (double)BubblePointPressure / 1.8) * y;

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

            if (IsSaturated)
                GeneratedData = GenerateIPR_SaturatedReservoir();
            else
                GeneratedData = GenerateIPR_UnderSaturatedReservoir();

        }

        /// <summary>
        /// Updates the flow efficiency of the reservoir model and returns the resulting inflow data rows.
        /// </summary>
        /// <param name="newFlowEfficiency">The new flow efficiency value to apply to the reservoir. Must be a positive number.</param>
        /// <returns>A list of <see cref="clsInFlowDataRow"/> objects representing the inflow data after the flow efficiency
        /// change.</returns>
        public void GenerateWithEfficiency(double newFlowEfficiency)
        {

            if (IsSaturated)
                GeneratedData = ChangeFlowEfficiency_SaturatedReservoir(newFlowEfficiency);
            else
                GeneratedData = ChangeFlowEfficiency_UnderSaturatedReservoir(newFlowEfficiency);

        }

        private double CalcualtFutureMaxFlowrate(double FutureReservoirPressure, 
            double FutureOilReleativePremeability, double FutureOilViscosity,
            double FutureOilRelativePermeability)
        {

            // A method to calcualte Future Max Flowate.
            // The procedure:
            // 1. Calculate the present max flowrate.
            // 2. using the fluid properties calcuate the future max flowrate using the following eqution:
            // qo(max)F = qo(max)P * [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            CalcualteTestPresentMaxFlowRate();


            if (PresentOilRelativePermeability == null ||
                PresentOilViscosity == null ||
                PresentOilFomationVoilumeFactor == null)
                throw new Exception("One or more Present Pressure function data is missing.");

            double PresentPressureFunction = (double)(PresentOilRelativePermeability / (PresentOilViscosity * PresentOilFomationVoilumeFactor));

            double FuturePressureFunction = (FutureOilReleativePremeability / (FutureOilViscosity * FutureOilRelativePermeability));
             
            double x = (FutureReservoirPressure * FuturePressureFunction) 
                / (ReservoirPressure * PresentPressureFunction);// [ (PrF * (KroF / (μoF * BoF))) / (PrP * (KroP / (μoP * BoP)))]

            return TestPresentMaxFlowRate * x;

        }

        public void GenerateFutureIPR(double futureReservoirPressure,
            double futureOilRelativePermeability, double futureOilFormationVolumeFactor, double FutureOilViscosity)
        {

            // A method to generate The future IPR using Standing.


            List<clsInFlowDataRow> Data = new List<clsInFlowDataRow>();
            
            double futureMaxFlowrate = CalcualtFutureMaxFlowrate(futureReservoirPressure,
                futureOilFormationVolumeFactor, FutureOilViscosity, futureOilRelativePermeability);

            // Genrate the IPR using the followin equation:
            // qoF = qo(max)F * [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]

            for (double Pressure = MinimumPressure; Pressure <= futureReservoirPressure; 
                Pressure += PressureStepSize)
            {

                double x = Pressure / futureReservoirPressure;// (Pwf/Pr)
                double y = 1 - 0.2 * x - 0.8 * x * x; // [ 1 - 0.2 (Pwf/Pr) - 0.8(Pwf / Pr)^2 ]
                double oilFlowrate = futureMaxFlowrate * y;

                Data.Add(new clsInFlowDataRow(Pressure, oilFlowrate));

            }

            GeneratedData = Data;

        }


    }

}
