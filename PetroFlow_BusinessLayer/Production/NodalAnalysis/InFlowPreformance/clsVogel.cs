using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    // A class to store Vogel's equations to solve the IPR using Vogel's method.

    // Vogel's method can be applied under the following characteristics:
    /*
     * 1. vertical oil well.
     * 2. One stabilized Test (qo, Pwf).
     * 3. Zero skin factor.
     * 4. saturated and unsaturated Reservoir.
     * 
     */

    public class clsVogel
    {

        private double ReservoirPressure;

        private double? BubblePointPressure;

        private double TestBottomHolePressure;

        private double TestFlowRate;

        private double MaxFlowRate;

        private double ProductivityIndex;

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

        public clsVogel(double reservoirPressure, double testBottomHolePressure, double testFlowRate,
            double? bubblePointPressure)
        {

            ReservoirPressure = reservoirPressure;
            BubblePointPressure = bubblePointPressure;
            TestBottomHolePressure = testBottomHolePressure;
            TestFlowRate = testFlowRate;


        }

        private void CalculateMaxFlowRate()
        {
            
            // A method to calcualte the max flow rate.

            double x = TestBottomHolePressure / ReservoirPressure; // a variable to store the pwf/ pr.
            MaxFlowRate = TestFlowRate / (1 - 0.2 * (x) - 0.8 * Math.Pow(x, 2)); // calculate the maximum flow rate qo(max) or AOF.

        }

        private void CalculateProductivityIndex()
        {

            // A method to calculate the productivity index.

            if (TestBottomHolePressure >= BubblePointPressure)
                // calculating J using linear productivity index equation: J = qo / (Pr - Pwf).
                ProductivityIndex =  clsIPRGeneralFunctions.ProductivityIndex(TestFlowRate, ReservoirPressure, TestBottomHolePressure);
            else
            {
                // calculating J using J = qo / (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]).
                double x = TestBottomHolePressure / (double)BubblePointPressure; // pwf / pb
                double y = ReservoirPressure - (double)BubblePointPressure +
                    ((double)BubblePointPressure / 1.8) * (1 - 0.2 * x - 0.8 * Math.Pow(x, 2)); // (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ])

                ProductivityIndex = TestFlowRate / y;

            }

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to generate the IPR data for a saturated reservoir using vogel's method.

            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>(); // a list to store the IPR data.

            // Calculate max flow rate:
            CalculateMaxFlowRate();

            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double y = pressure / ReservoirPressure; // a variable to store the pwf/ pr.

                double FlowRate = MaxFlowRate * (1 - 0.2 * (y) - 0.8 * Math.Pow(y, 2));

                dataRows.Add(new clsInFlowDataRow(pressure, FlowRate));


            }

            return dataRows;


        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir()
        {

            // A fuction to generate the IPR data for an undersaturated reservoir using vogel's method.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

            // Calculte Productivity Index:
            CalculateProductivityIndex();

            // Calcuating the bubble point flowrate by using the bubble point pressure as flowing pressure.
            double BubblePointFlowRate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex, ReservoirPressure, (double)BubblePointPressure);

            // Generating the IPR data:
            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double flowrate;

                if (pressure > BubblePointPressure)
                    // calculate the flowrate using qo = j(pr - pwf).
                    flowrate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex, ReservoirPressure, pressure);
                else
                {
                    // calculate the flowrate using qo = qb + (J * pb / 1.8) [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    double x = pressure / (double)BubblePointPressure;// pwf / pb
                    double y = 1 - 0.2 * x - 0.8 * Math.Pow(x, 2); // [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    flowrate = BubblePointFlowRate + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;


                }

                data.Add(new clsInFlowDataRow(pressure, flowrate));
            
            }

            return data;


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
