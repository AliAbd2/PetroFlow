using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
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
    public class JonesBlountGlaze : IPRMethodBase
    {

      
        public JonesBlountGlaze()
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


        }

        private (double slope, double intercept) DetermineSlopeIntercept(IPRInputData inputData)
        {

            // A method to Determine Slope(B) and Intercept(A) using least squares method.
            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // Intercept = (sum(y) - Slope * sum(x)) / n
            // where n is the number of records.

            List<InFlowDataRow> TestsData = inputData.TestsData.ToList();
            double ReservoirPressure = inputData.ReservoirPressure.Value;

            int n = TestsData.Count;
            List<double> q = TestsData.Select(x => x.FlowRate).ToList();
            List<double> piTerm = TestsData.Select(x =>
            (ReservoirPressure - x.BottomHolePressure) / x.FlowRate).ToList();

            double Sumx = q.Sum();
            double Sumy = piTerm.Sum();
            double SumX2 = q.Sum(x => x * x);
            double Sumxy = q.Zip(piTerm, (x, y) => x * y).Sum();


            double slope = (n * Sumxy - Sumx * Sumy) / (n * SumX2 - Math.Pow(Sumx, 2));

            double intercept = (Sumy - slope * Sumx) / n;

            return (slope, intercept);


        }

        protected override List<InFlowDataRow> ComputeIPR(IPRInputData input)
        {

            // A method to generate the IPR using Jones, Blount, and Glaze method.


            // This method will assume Bottom-Hole Flowing Pressure and calcualte Flow Rate Using:
            // qo = (-A + √(A² + 4B(Pr - Pwf))) / (2B)
            // Where:
            // qo : Oil Flow Rate.
            // Pr : Reservoir Pressure.
            // Pwf: Bottom Hole Flowing Pressure.
            // A  : Intercept.
            // B  : Slope.


            List<InFlowDataRow> DataRows = new List<InFlowDataRow>();

            double flowRate = 0;

            (double slope, double intercept) jonesCoeff = DetermineSlopeIntercept(input);

            double Slope = jonesCoeff.slope;
            double Intercept = jonesCoeff.intercept;

            double ReservoirPressure = input.ReservoirPressure.Value;


            IPRGenerationSettings GenerationSettings = new IPRGenerationSettings(
                input.GenerationSettings.PressureStepSize,
                input.GenerationSettings.MinimumPressure);


            for (double Pressure = GenerationSettings.MinimumPressure; Pressure <= ReservoirPressure;
                Pressure += GenerationSettings.PressureStepSize)
            {

                double x = -Intercept + 
                    Math.Sqrt(Intercept * Intercept + 4 * Slope * (ReservoirPressure - Pressure));

                flowRate = x / (2 * Slope);

                DataRows.Add(new InFlowDataRow(Pressure, flowRate));

            }

            return DataRows;

        }


    }
}
