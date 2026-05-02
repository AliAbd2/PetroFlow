using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class IPRInputData
    {

        public double? ReservoirPressure { get; set; }

        public double? BubblePointPressure { get; set; }

        public List<InFlowDataRow>? TestsData { get; set; }

        public double? TestFlowEfficiency { get; set; }

        public double? WellExponent { get; set; }
        public double? FlowCoefficient { get; set; }

        public IPRInputData(double? reservoirPressure, double? bubblePointPressure,
            List<InFlowDataRow>? testData, double? testFlowEfficiency, double? wellExponent, 
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
