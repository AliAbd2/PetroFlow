using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // This class implements the Jones, Blount, and Glaze method for generating the
    // Inflow Performance Relationship (IPR) for oil wells.
    //
    // Their method is simple: they rearrange the following equation:
    //
    // qo = (0.00708 * ko * h * (Pr - Pwf)) / (μo * Bo * [ln(0.472 * re / rw) + S])
    //
    // into the following form:
    //
    // Pr - Pwf = A * qo + B * qo²
    //
    // By dividing both sides by qo:
    //
    // (Pr - Pwf) / qo = A + B * qo
    //
    // which represents a straight-line equation.
    //
    // Where:
    // A = intercept
    // B = slope
    //
    // Using the least-squares method, the constants A and B are determined.
    // The oil flow rate (qo) can then be calculated using:
    //
    // qo = (-A + √(A² + 4B(Pr - Pwf))) / (2B)
    public class clsJonesBlountGlaze : IIPRMethod
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

        private double Slope;

        private double Intercept;

        public clsJonesBlountGlaze()
        {

            CurvePlotSetting = new clsCurvePlotSettings();

        }

        public ValidationResult SetInputData(clsPresentIPRDataInput inputData)
        {
            ValidationResult validationResult = new();

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

            if (inputData.TestsData.Count < 2)
                throw new InvalidParameterException(
                    "Invalid test data: At least two test data rows is required.");

            if (inputData.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more flow rates are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more bottom hole pressures are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure >= inputData.ReservoirPressure))
                throw new InvalidParameterException(
                    "Bottom-hole pressure must be less than reservoir pressure.");


            ReservoirPressure = inputData.ReservoirPressure.Value;
            TestsData = inputData.TestsData;

            IsInputValid = true;
            return validationResult;
        }

        private void DetermineSlopeIntercept()
        {

            // A method to Determine Slope(B) and Intercept(A) using least squares method.
            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // Intercept = (sum(y) - Slope * sum(x)) / n
            // where n is the number of records.

            int n = TestsData.Count;
            List<double> q = TestsData.Select(x => x.FlowRate).ToList();
            List<double> piTerm = TestsData.Select(x =>
            (ReservoirPressure - x.BottomHolePressure) / x.FlowRate).ToList();

            double Sumx = q.Sum();
            double Sumy = piTerm.Sum();
            double SumX2 = q.Sum(x => x * x);
            double Sumxy = q.Zip(piTerm, (x, y) => x * y).Sum();


            Slope = (n * Sumxy - Sumx * Sumy) / (n * SumX2 - Math.Pow(Sumx, 2));

            Intercept = (Sumy - Slope * Sumx) / n;


        }

        public void GenerateIPR()
        {

            // A method to generate the IPR using Jones, Blount, and Glaze method.

            if (!IsInputValid)
                throw new InvalidOperationException("Invalid operation: " +
                    "Calculation method was called before input data was set. Call SetInputData() first.");

            if (GenerationSettings.MinimumPressure > ReservoirPressure)
                throw new InvalidParameterException("Minimum pressure must be less than the reservoir pressure.");

            DetermineSlopeIntercept();

            // This method will assume Bottom-Hole Flowing Pressure and calcualte Flow Rate Using:
            // qo = (-A + √(A² + 4B(Pr - Pwf))) / (2B)
            // Where:
            // qo : Oil Flow Rate.
            // Pr : Reservoir Pressure.
            // Pwf: Bottom Hole Flowing Pressure.
            // A  : Intercept.
            // B  : Slope.


            List<clsInFlowDataRow> DataRows = new List<clsInFlowDataRow>();

            double flowRate = 0;

            for (double Pressure = GenerationSettings.MinimumPressure; Pressure <= ReservoirPressure;
                Pressure += GenerationSettings.PressureStepSize)
            {

                double x = -Intercept + 
                    Math.Sqrt(Intercept * Intercept + 4 * Slope * (ReservoirPressure - Pressure));

                flowRate = x / (2 * Slope);

                DataRows.Add(new clsInFlowDataRow(Pressure, flowRate));

            }

            GeneratedData = DataRows;

        }


    }
}
