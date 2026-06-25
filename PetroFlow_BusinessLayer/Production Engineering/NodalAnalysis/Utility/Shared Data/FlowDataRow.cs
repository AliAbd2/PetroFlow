using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility
{
    public class FlowDataRow
    {

        public double BottomHolePressure { get; set; }
        public double FlowRate { get; set; }

        public FlowDataRow(double bottomHolePressure, double flowrate)
        {

            BottomHolePressure = bottomHolePressure;
            FlowRate = flowrate;

        }

    }
}
