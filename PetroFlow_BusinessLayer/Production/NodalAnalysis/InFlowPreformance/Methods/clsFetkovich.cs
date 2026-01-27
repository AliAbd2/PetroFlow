using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // This class implements Fetkovich’s method for generating the
    // Inflow Performance Relationship (IPR) for oil wells.
    //
    // Fetkovich showed that oil-well inflow behavior can be expressed as:
    //
    //     qo = C · (Pr² − Pwf²)ⁿ
    //
    // where:
    //     qo  = oil flow rate
    //     Pr  = reservoir pressure
    //     Pwf = flowing bottom-hole pressure
    //     C   = flow coefficient
    //     n   = flow exponent related to well and reservoir characteristics
    //
    // By applying a base-10 logarithmic transformation:
    //
    //     log(qo) = log(C) + n · log(Pr² − Pwf²)
    //
    // the equation becomes linear, allowing the exponent n to be
    // determined from production test data.
    //
    // This class determines the exponent n using:
    //   • n = 1 when only one test point is available
    //   • Δlog(qo) / Δlog(Pr² − Pwf²) when two test points are available
    //   • Least-squares linear regression when three or more test points exist
    //
    // After determining n, the class calculates the productivity index (J)
    // using Fetkovich’s formulations for:
    //   • Saturated reservoirs
    //   • Undersaturated reservoirs (with or without test points above Pb)
    //
    // Finally, the class generates IPR data over a specified pressure range
    // using the calculated productivity index and exponent.
    //
    // The class supports both saturated and undersaturated reservoirs,
    // with optional bubble-point pressure input.

    public class clsFetkovich
    {

        private double ReservoirPressure;

        private double? BubblePointPressure;

        private List<clsInFlowDataRow> TestsData;

        private double Slope;

        private double ProductivityIndex;

        private double PressureStepSize;

        private double MinimumPressure;

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

        public clsFetkovich(double reservoirPressure, List<clsInFlowDataRow> testsData, 
            double? bubblePointPressure = null,
            double? pressureStepSize = null, double? minimumPressure = null)
        {

            ReservoirPressure = reservoirPressure;
            BubblePointPressure = bubblePointPressure;
            TestsData = testsData;
            PressureStepSize = pressureStepSize == null ? 1 : (double)pressureStepSize;
            MinimumPressure = minimumPressure == null ? 0 : (double)minimumPressure;

        }

        private void Determineslope()
        {
            // This is a method to determaine the slope.
            // the method to determine the slope is depends on the number of tests:
            // if there is one test only then the slop is assumed to be = 1.
            // if two tests are availble then use slope = delta y / delta x formula.
            // if three or more tests are available then use the least squares method.

            if (TestsData.Any(t =>
                t.FlowRate <= 0 ||
                ReservoirPressure * ReservoirPressure <= t.BottomHolePressure * t.BottomHolePressure))
            {
                throw new InvalidOperationException(
                    "Invalid test data: logarithm arguments must be positive.");
            }

            // detemain the number of recoreds.
            int n = TestsData.Count;

            double pr2 = ReservoirPressure * ReservoirPressure;

            if (n == 1)
            {
                Slope = 1;
                return;
            }

            if (n == 2)
            {

                double flowrate1 = TestsData[0].FlowRate;
                double flowrate2 = TestsData[1].FlowRate;

                double pressure1 = TestsData[0].BottomHolePressure;
                double pressure2 = TestsData[1].BottomHolePressure;

                double deltaLogQ =
                    Math.Log10(flowrate2) - Math.Log10(flowrate1);

                double deltaLogX =
                    Math.Log10(pr2 - pressure2 * pressure2) -
                    Math.Log10(pr2 - pressure1 * pressure1);

                Slope = deltaLogQ / deltaLogX;

                return;

            }

            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // where n is the number of records.



            List<double> logX = TestsData.Select(x => Math.Log10(
                pr2 - x.BottomHolePressure * x.BottomHolePressure)).ToList();
            List<double> logY = TestsData.Select(y => Math.Log10(y.FlowRate)).ToList();

            double sumX = logX.Sum();
            double sumY = logY.Sum();
            double sumX2 = logX.Sum(x => x * x);
            double sumXY = logX.Zip(logY, (x, y) => x * y).Sum();

            double m = (n * sumXY - sumX * sumY)
                     / (n * sumX2 - sumX * sumX);

            Slope = m;

        }

        private void DetermineProductivityIndex()
        {
            // A method to calculate the productivity index for fetkovich method.
            // There is three cases:
            // 1. if the reservoir is saturated then:
            // J = 2 * q / (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
            // 2. if the reservoir is undersaturated and there is a test point 
            // bottom hole pressure > bubble point then:
            // J = q / (Pr - Pwf)
            // 3. if the rservoir is undersaturated and there is not a test point
            // bottom hole pressure > bubble point then:
            // J = q / ((Pr - Pwf) + Pb / 2 *([1 - (Pwf / Pb)^2]^n)

            Determineslope();

            double productivityIndex = 0;

            double bottomHolePressure = TestsData[0].BottomHolePressure;
            double flowRate = TestsData[0].FlowRate;


            if (flowRate <= 0 || bottomHolePressure <= 0 || ReservoirPressure <= 0)
                throw new InvalidOperationException("Invalid pressures or flow rate.");

            if (!IsSaturated && BubblePointPressure <= 0)
                throw new InvalidOperationException("Bubble point pressure must be positive.");



            // Case 1:
            if (IsSaturated)
            {
                double x = Math.Pow((bottomHolePressure / ReservoirPressure), 2); // (Pwf / Pr)^2
                double y = ReservoirPressure * Math.Pow(1 - x, Slope); // (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
                productivityIndex = 2 * flowRate / y; 

            }

            // Case 2:
            else if (!IsSaturated && bottomHolePressure > BubblePointPressure)
                productivityIndex = flowRate / (ReservoirPressure - bottomHolePressure);

            // Case 3:
            else
            {

                double x = Math.Pow((bottomHolePressure / (double)BubblePointPressure), 2); // (Pwf / Pb)^2
                double y = Math.Pow(1 - x, Slope); // [1 - (Pwf / Pb)^2]^n
                double z = (ReservoirPressure - (double)BubblePointPressure) +
                    (double)BubblePointPressure / 2 * y;
                productivityIndex = flowRate / z;
            }


            ProductivityIndex = productivityIndex;

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to generate the IPR data for saturated reservoir using Fetkovich method.


            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>();
            double flowRate = 0;

            DetermineProductivityIndex();

            for (double pressure = MinimumPressure; 
                pressure <= ReservoirPressure; pressure += PressureStepSize)
            {
                // q = J * Pr / 2 * [ 1 - (Pwf / Pr)^d2 * n ]

                double x = 1 - Math.Pow(pressure / ReservoirPressure, 2 * Slope); // [ 1 - (Pwf / Pr)^d2 * n ]
                flowRate = ProductivityIndex * ReservoirPressure / 2 * x;

                dataRows.Add(new clsInFlowDataRow(pressure, flowRate));


            }

            return dataRows;


        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir()
        {


            // A method to generate the IPR data for undersaturated reservoir using Fetkovich method.


            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>();
            double flowRate = 0;

            DetermineProductivityIndex();

            for (double pressure = MinimumPressure;
                pressure <= ReservoirPressure; pressure += PressureStepSize)
            {

                if (pressure > BubblePointPressure)
                {
                    // q = J * (Pr - Pwf)
                    flowRate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex,
                        ReservoirPressure, pressure);

                }
                else
                {
                    // q = J * (Pr - Pb) + J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n

                    double x = Math.Pow((1 - Math.Pow((pressure / (double)BubblePointPressure), 2)), Slope); //[ 1 - (Pwf / Pb)^2 ]^n
                    double y = ProductivityIndex * (double)BubblePointPressure / 2 * x; // J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n
                    flowRate = ProductivityIndex * (ReservoirPressure - (double)BubblePointPressure) + y;

                }

                dataRows.Add(new clsInFlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }

        public List<clsInFlowDataRow> GenerateIPR()
        {

            if (IsSaturated)
                return GenerateIPR_SaturatedReservoir();
            else
                return GenerateIPR_UnderSaturatedReservoir();

        }

    }
}
