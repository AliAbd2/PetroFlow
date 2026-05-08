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

        public IPRGenerationSettings GenerationSettings { get; set; }

        public double? FutureReservoirPressure { get; set; }

        public double? PresentOilRelativePermeability { get; set; }

        public double? PresentOilViscosity { get; set; }

        public double? PresentOilFormationVolumeFactor { get; set; }

        public double? FutureOilRelativePermeability { get; set; }

        public double? FutureOilViscosity { get; set; }

        public double? FutureOilFormationVolumeFactor { get; set; }

        public double? NewFlowEfficiency { get; set; }

        public double? PresentFlowCoefficient { get; set; }

    }
}
