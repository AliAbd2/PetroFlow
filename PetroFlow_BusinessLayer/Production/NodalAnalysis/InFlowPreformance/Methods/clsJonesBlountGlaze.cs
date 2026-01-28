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
    public class clsJonesBlountGlaze
    {

        private double ReservoirPressure;

        private List<clsInFlowDataRow> TestsData;

        private double Slope;

        private double Intercept;

        private double PressureStepSize;

        private double MinimumPressure;

        public clsJonesBlountGlaze(double reservoirPressure, List<clsInFlowDataRow> testData,
            double? pressureStepSize = null, double? minimumPressure = null)
        {

            ReservoirPressure = reservoirPressure;
            TestsData = testData;
            PressureStepSize = pressureStepSize == null ? 1 : (double)pressureStepSize;
            MinimumPressure = minimumPressure == null ? 0 : (double)minimumPressure;

        }

        private void DetermineSlopeIntercept()
        {

            // A method to Determine Slope(B) and Intercept(A) using least squares method.
            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // Intercept = (sum(y) - Slope * sum(x)) / n
            // where n is the number of records.

            if (TestsData.Count < 2)
                throw new Exception("Invalid number of tests.");


            if (TestsData.Any(t =>
                t.FlowRate <= 0 ||
                t.BottomHolePressure >= ReservoirPressure || t.BottomHolePressure < 0))
            {
                throw new InvalidOperationException(
                    "Invalid test data: check flow rates and bottom-hole pressures.");
            }

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

        public List<clsInFlowDataRow> GenerateIPR()
        {

            // A method to generate the IPR using Jones, Blount, and Glaze method.

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

            for (double Pressure = MinimumPressure; Pressure <= ReservoirPressure;
                Pressure += PressureStepSize)
            {

                double x = -Intercept + 
                    Math.Sqrt(Intercept * Intercept + 4 * Slope * (ReservoirPressure - Pressure));

                flowRate = x / (2 * Slope);

                DataRows.Add(new clsInFlowDataRow(Pressure, flowRate));

            }

            return DataRows;

        }


    }
}
