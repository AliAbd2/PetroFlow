using System;
using System.Collections.Generic;
using System.Text;

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

    public static class clsVogel
    {
        /// <summary>
        /// Generates inflow performance relationship (IPR) data for a saturated reservoir using Vogel's method.
        /// </summary>
        /// <remarks>This method applies Vogel's empirical IPR equation to estimate the deliverability of
        /// a saturated oil reservoir. The returned list contains one entry for each integer pressure value from zero to
        /// the specified reservoir pressure. Ensure that all input parameters use consistent units.</remarks>
        /// <param name="reservoirPressure">The reservoir pressure, in the same units as testFlowingPressure. Must be greater than zero.</param>
        /// <param name="testFlowRate">The measured flow rate at the test flowing pressure, in consistent units (e.g., STB/day or m³/day). Must be
        /// greater than or equal to zero.</param>
        /// <param name="testFlowingPressure">The flowing bottomhole pressure at which the test flow rate was measured, in the same units as
        /// reservoirPressure. Must be greater than or equal to zero and less than or equal to reservoirPressure.</param>
        /// <returns>A list of inflow data rows representing calculated flow rates at various bottomhole pressures from zero up
        /// to the reservoir pressure.</returns>
        static List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir(double reservoirPressure,
            double testFlowRate, double testFlowingPressure)
        {

            // A function to generate the IPR data for an saturated reservoir using vogel's method.

            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>(); // a list to store the IPR data.

            double x = testFlowingPressure / reservoirPressure; // a variable to store the pwf/ pr.
            double AOF = testFlowRate / (1 - 0.2 * (x) - 0.8 * Math.Pow(x, 2)); // calculate the maximum flow rate qo(max) or AOF.

            for (int pressure = 0; pressure <= reservoirPressure; pressure++)
            {

                double y = pressure / reservoirPressure; // a variable to store the pwf/ pr.

                double FlowRate = AOF * (1 - 0.2 * (y) - 0.8 * Math.Pow(y, 2));

                dataRows.Add(new clsInFlowDataRow(pressure, FlowRate));


            }

            return dataRows;


        }

        /// <summary>
        /// Generates inflow performance relationship (IPR) data for an undersaturated oil reservoir using Vogel's
        /// method.
        /// </summary>
        /// <remarks>This method applies Vogel's IPR correlation for undersaturated reservoirs, using the
        /// provided test data to estimate the productivity index and generate the full IPR curve. The calculation
        /// assumes single-phase oil flow and does not account for gas breakthrough or multiphase effects. All input
        /// parameters should use consistent units.</remarks>
        /// <param name="reservoirPressure">The reservoir pressure, in the same units as the flowing and bubble point pressures. Must be greater than
        /// zero.</param>
        /// <param name="bubblePointPressure">The bubble point pressure of the reservoir fluid, in the same units as the reservoir pressure. Must be less
        /// than or equal to the reservoir pressure.</param>
        /// <param name="testFlowRate">The measured oil flow rate at the test flowing pressure, in consistent volumetric units. Must be greater
        /// than or equal to zero.</param>
        /// <param name="testFlowingPressure">The flowing bottomhole pressure at which the test flow rate was measured, in the same units as the reservoir
        /// and bubble point pressures. Must be greater than or equal to zero.</param>
        /// <returns>A list of inflow data rows, each containing a flowing bottomhole pressure and the corresponding calculated
        /// flow rate, representing the IPR curve for the specified reservoir conditions.</returns>
        static List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir(double reservoirPressure,
            double bubblePointPressure, double testFlowRate, double testFlowingPressure)
        {

            // A fuction to generate the IPR data for saturated reservoir using vogel's method.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

            double J;

            if (testFlowingPressure >= bubblePointPressure)
                // calculating J using linear productivity index equation: J = qo / (Pr - Pwf).
                J = clsIPRGeneralFunctions.ProductivityIndex(testFlowRate, reservoirPressure, testFlowingPressure);
            else
            {
                // calculating J using J = qo / (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]).
                double x = testFlowingPressure / bubblePointPressure; // pwf / pb
                double y = reservoirPressure - bubblePointPressure +
                    (bubblePointPressure / 1.8) * (1 - 0.2 * x - 0.8 * Math.Pow(x, 2)); // (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ])

                J = testFlowRate / y;

            }

            // Calcuating the bubble point flowrate by using the bubble point pressure as flowing pressure.
            double BubblePointFlowRate = clsIPRGeneralFunctions.LinearFlowRate(J, reservoirPressure, bubblePointPressure);

            // Generating the IPR data:
            for (int pressure = 0; pressure <= reservoirPressure; pressure++)
            {

                double flowrate;

                if (pressure > bubblePointPressure)
                    // calculate the flowrate using qo = j(pr - pwf).
                    flowrate = clsIPRGeneralFunctions.LinearFlowRate(J, reservoirPressure, pressure);
                else
                {
                    // calculate the flowrate using qo = qb + (J * pb / 1.8) [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    double x = pressure / bubblePointPressure;// pwf / pb
                    double y = 1 - 0.2 * x - 0.8 * Math.Pow(x, 2); // [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    flowrate = BubblePointFlowRate + (J * bubblePointPressure / 1.8) * y;


                }

                data.Add(new clsInFlowDataRow(pressure, flowrate));
            
            }

            return data;


        }


        public static List<clsInFlowDataRow> GeneratIPR(double reservoirPressure, 
            double testFlowRate, double testFlowingPressure, double? bubblePointPressure = null)
        {

            if (bubblePointPressure == null || bubblePointPressure >= reservoirPressure)
                return GenerateIPR_SaturatedReservoir(reservoirPressure, testFlowRate, testFlowingPressure);
            else
                return GenerateIPR_UnderSaturatedReservoir(reservoirPressure, (double)bubblePointPressure,
                    testFlowRate, testFlowingPressure);

        }


    }

}
