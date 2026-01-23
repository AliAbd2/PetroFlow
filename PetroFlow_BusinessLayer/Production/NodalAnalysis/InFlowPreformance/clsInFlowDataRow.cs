using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    // A class to represent a inflow preformance data row.

    public class clsInFlowDataRow
    {

        public double BottomHolePressure;

        public double FlowRate;

        public clsInFlowDataRow(double bottomHolePressure, double flowRate)
        {

            BottomHolePressure = bottomHolePressure;
            FlowRate = flowRate;

        }



    }
}
