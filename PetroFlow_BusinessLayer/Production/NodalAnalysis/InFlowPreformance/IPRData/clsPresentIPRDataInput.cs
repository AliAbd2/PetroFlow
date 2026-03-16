using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class clsPresentIPRDataInput
    {

        public double? ReservoirPressure { get; set; }

        public double? BubblePointPressure { get; set; }

        public List<clsInFlowDataRow>? TestsData { get; set; }

        public double? TestFlowEfficiency { get; set; }

        public double? WellExponent { get; set; }
        public double? FlowCoefficient { get; set; }

        public clsPresentIPRDataInput(double? reservoirPressure, double? bubblePointPressure,
            List<clsInFlowDataRow>? testData, double? testFlowEfficiency, double? wellExponent, 
            double? flowCoefficient)
        {

            ReservoirPressure = reservoirPressure;
            BubblePointPressure = bubblePointPressure;
            TestsData = testData;
            TestFlowEfficiency = testFlowEfficiency;
            WellExponent = wellExponent;
            FlowCoefficient = flowCoefficient;

        }

    }
}
