using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
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

        double ReservoirPressure;
        
        double? BubblePointPressure;

        double TestFlowEfficiency;

        double TestMaxFlowRate;

        bool IsSaturated
        {
            get
            {
                if (BubblePointPressure < ReservoirPressure || BubblePointPressure == null)
                    return false;
                else
                    return true;
            }
        }

        public clsStanding(double reservoirPressure, double testBottomHolePressure, 
            double testFlowRate, double testFlowEfficiency, double? bubblePointPressure)
        {

            ReservoirPressure = reservoirPressure;
            BubblePointPressure = bubblePointPressure;
            TestFlowEfficiency = testFlowEfficiency;


            // Calculate the maximum flowrate at FE = 1(e.g. zero skin factor) using the following equation:
            // qo(max)(FE = 1) = qo / [ 1.8 * FE * (1 - (pwf / pr)) - 0.8 * (FE^2) * (1 - (pwf / pr))^2 ]
            double x = 1 - testBottomHolePressure / reservoirPressure; // 1 - pwf / pr
            double y = 1.8 * testFlowEfficiency * (x) - 0.8 * Math.Pow(testFlowEfficiency, 2) * Math.Pow(x, 2);
            double TestMaxFlowRate = testFlowRate / y;



        }

        List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to Generate the IPR for a saturatedReservoir.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>(); // A list to store the data.

            // Standing's equation to generate the IPR has a limit and only valid if:
            // pwf >= pr(1 - (1 / FE )) so we need to add a minimum value of pwf
            int minumumBottomHolePressure = 0;
            // and since the minimum bottom hole pressure will shift from 0 we need to calculate the qo (max) using:
            // qo(max) = qo(max)
            if (TestFlowEfficiency > 1)
            {

                minumumBottomHolePressure = (int)Math.Floor(ReservoirPressure * (1 - (1 / TestFlowEfficiency))) + 1;


            }



            return data;


        }

        //public static List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir(double reservoirPressure,
        //    double bubblePointPressure, double testFlowEfficiency, double testFlowrate, double testBottomHolePressure)
        //{

        //    if (testBottomHolePressure >= bubblePointPressure)
        //    {

        //        double J = clsIPRGeneralFunctions.ProductivityIndex(testFlowrate, reservoirPressure, testBottomHolePressure);



        //    }

        //}


    }
}
