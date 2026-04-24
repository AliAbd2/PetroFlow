using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData
{
    public class VLPDataInput
    {

        public double? Pressure { get; set; }

        public double? Temperature { get; set; }

        public double? TotalMassFlowRate { get; set; }

        public double? LiquidFlowRate { get; set; }

        public double? LiquidSuperficialVelocity { get; set; }

        public double? LiquidDesnity { get; set; }

        public double? LiquidViscosity { get; set; }

        public double? GasFlowRate { get; set; }

        public double? GasSuperficialVelocity { get; set; }

        public double? GasDensity { get; set; }

        public double? GasViscosity { get; set; }

        public double? GasLiquidRatio { get; set; }

        public double? GasLiquidSurfaceTension { get; set; }

        public double? PipeInsideDiameter { get; set; }

        public double? PipeRelativeRoughness { get; set; }

        public double? LiquidVelocityNumber { get; set; }

        public double? GasVelocityNumber { get; set; }

        public double? PipeDiameterNumber { get; set; }

        public double? LiquidViscosityNumber { get; set; }

    }
}
