using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class InFlowDataRow
    {

        public double BottomHolePressure { get; set; }
        public double FlowRate { get; set; }

        public InFlowDataRow(double bottomHolePressure, double flowrate)
        {

            BottomHolePressure = bottomHolePressure;
            FlowRate = flowrate;

        }

    }
}
