using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    // A class to represent a inflow preformance data row.

    public class clsInFlowDataRow
    {

        public double FlowingPressure { get; set; }
        public double FlowRate { get; set; }

        public double FlowEfficiency { get; set; }


        public clsInFlowDataRow(double flowingPressure, double flowRate)
        {

            FlowingPressure = flowingPressure;
            FlowRate = flowRate;

        }

        public clsInFlowDataRow(double flowingPressure, double flowRate, double flowEfficiency)
        {

            FlowingPressure = flowingPressure;
            FlowRate = flowRate;
            FlowEfficiency = flowEfficiency; 

        }

    }
}
