using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Main_Classes
{
    public class NodalAnalysisDataRow
    {

        public double FlowRate { get; set; }

        public double IPRBottomHolePressure { get; set; }

        public double VLPBottomHolePressure { get; set; }

        public NodalAnalysisDataRow(double flowRate, double iprBottomHolePressure, double vlpBottomHolePressure)
        {

            FlowRate = flowRate;
            IPRBottomHolePressure = iprBottomHolePressure;
            VLPBottomHolePressure = vlpBottomHolePressure;

        }

    }
}
