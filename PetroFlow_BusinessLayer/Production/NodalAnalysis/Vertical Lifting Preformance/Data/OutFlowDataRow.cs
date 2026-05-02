using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data
{
    public class OutFlowDataRow
    {

        public double Pressure { get; set; }
        public double FlowRate { get; set; }

        public OutFlowDataRow(double pressure, double flowrate)
        {

            Pressure = pressure;
            FlowRate = flowrate;

        }

    }
}
