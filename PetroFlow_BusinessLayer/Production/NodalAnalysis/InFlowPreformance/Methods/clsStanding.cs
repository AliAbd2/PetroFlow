using System;
using System.Collections.Generic;
using System.Text;
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
    // methods that calculate the effect of changing FE, unlike Vogel's class,
    // which consists of static methods only.

    public class clsStanding
    {

        private double ReservoirPressure;
        
        private double? BubblePointPressure;

        private double TestFlowEfficiency;

        private double TestMaxFlowRate
        {
            get
            {

                // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
                // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]

                double x = 1 - TestBottomHolePressure / ReservoirPressure; // 1 - pwf / pr
                double y = 1.8 * TestFlowEfficiency * (x) - 0.8 * Math.Pow(TestFlowEfficiency, 2) * Math.Pow(x, 2);

                return TestFlowRate / y;

            }
        }

        private double TestBottomHolePressure;

        private double TestFlowRate;

        private double ProductivityIndex
        {
            get
            {

                // Calculate the productivity index:
                if (TestBottomHolePressure >= BubblePointPressure)
                    return clsIPRGeneralFunctions.ProductivityIndex(TestFlowRate,
                        ReservoirPressure, TestBottomHolePressure);
                else
                {

                    double x = 1 - TestBottomHolePressure / (double)BubblePointPressure;// (1 - pwf / pb)
                    double y = 1.8 * x - 0.8 * (TestFlowEfficiency) * Math.Pow(x, 2);
                    return TestFlowRate /
                        ((ReservoirPressure - (double)BubblePointPressure) + ((double)BubblePointPressure / 1.8 * y));
                }

            }
        }

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

        public clsStanding(double reservoirPressure, double testBottomHolePressure,
            double testFlowRate, double testFlowEfficiency, double? bubblePointPressure = null)
        {

            ReservoirPressure = reservoirPressure;
            BubblePointPressure = bubblePointPressure;
            TestFlowEfficiency = testFlowEfficiency;
            TestBottomHolePressure = testBottomHolePressure;
            TestFlowRate = testFlowRate;

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to Generate the IPR for a saturated reservoir.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            int minimumBottomHolePressure = 0;
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (TestFlowEfficiency > 1)
            {

                minimumBottomHolePressure = (int)Math.Floor(ReservoirPressure * (1 - (1 / TestFlowEfficiency))) + 1;

                double maxFlowRate = TestMaxFlowRate * (0.624 + 0.376 * TestFlowEfficiency);

                // adding the qo max to the data.
                data.Add(new clsInFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (int pressure = minimumBottomHolePressure; pressure <= ReservoirPressure; pressure++)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (TestFlowEfficiency) * x - 0.8 * Math.Pow(TestFlowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestMaxFlowRate * y;

                data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            return data;


        }

        private List<clsInFlowDataRow> ChangeFlowEfficiency_SaturatedReservoir(double newFlowEfficiency)
        {

            // A method to generate the IPR for flow Efficiency different from the test flow Efficiency.


            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            int minimumBottomHolePressure = 0;
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)(from test) * (0.624 + 0.376 * FE)
            if (newFlowEfficiency > 1)
            {

                minimumBottomHolePressure = (int)Math.Floor(ReservoirPressure * (1 - (1 / newFlowEfficiency))) + 1;

                double maxFlowRate = TestMaxFlowRate * (0.624 + 0.376 * newFlowEfficiency);

                // adding the qo max to the data.
                data.Add(new clsInFlowDataRow(0, maxFlowRate));

            }

            // Generating the IPR:
            for (int pressure = minimumBottomHolePressure; pressure <= ReservoirPressure; pressure++)
            {

                // the equation is: qo = qo(max) * [ 1.8 * (FE) * (1 - (pwf/pr)) - 0.8 * (FE)^2 * (1-(pwf/pr))^2 ]
                double x = 1 - pressure / ReservoirPressure;// (1 - (pwf/pr))
                double y = 1.8 * (newFlowEfficiency) * x - 0.8 * Math.Pow(newFlowEfficiency, 2) * Math.Pow(x, 2);
                double flowrate = TestMaxFlowRate * y;

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
        public List<clsInFlowDataRow> GenerateIPR()
        {

            if (IsSaturated)
                return GenerateIPR_SaturatedReservoir();
            else
                return GenerateIPR_UnderSaturatedReservoir();

        }

        /// <summary>
        /// Updates the flow efficiency of the reservoir model and returns the resulting inflow data rows.
        /// </summary>
        /// <param name="newFlowEfficiency">The new flow efficiency value to apply to the reservoir. Must be a positive number.</param>
        /// <returns>A list of <see cref="clsInFlowDataRow"/> objects representing the inflow data after the flow efficiency
        /// change.</returns>
        public List<clsInFlowDataRow> ChangeFlowEfficiency(double newFlowEfficiency)
        {

            if (IsSaturated)
                return ChangeFlowEfficiency_SaturatedReservoir(newFlowEfficiency);
            else
                return ChangeFlowEfficiency_UnderSaturatedReservoir(newFlowEfficiency);

        }

    }

}
