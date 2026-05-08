using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData
{
    public class VLPDataInput
    {

        //=================
        // --- General ---
        //=================
        public double? Pressure { get; set; } // psi

        public double? Temperature { get; set; } // F

        public double? GasLiquidRatio { get; set; } // SCF / STB

        public double? PipeInsideDiameter { get; set; } // ft

        public double? PipeRelativeRoughness { get; set; }

        public double? GravityAcceleration { get; set; } // ft / sec^2

        //====================
        // --- Flow Rate ----
        //====================

        public double? TotalMassFlowRate { get; set; } // lbm / sec

        public double? LiquidFlowRate { get; set; } // cu ft / day

        public double? OilFlowRate { get; set; } // cu ft / day

        public double? WaterFlowRate { get; set; } // cu ft / day

        public double? GasFlowRate { get; set; } // cu ft / day

        //==================
        // --- Density ----
        //==================

        public double? LiquidDensity { get; set; } // lbm / cu ft

        public double? OilDensity { get; set; } // lbm / cu ft

        public double? WaterDensity { get; set; } // lbm / cu ft

        public double? GasDensity { get; set; } // lbm / cu ft

        //==================
        // --- Velocity ---
        //==================

        public double? LiquidSuperficialVelocity { get; set; } // ft / sec

        public double? GasSuperficialVelocity { get; set; } // ft / sec

        //===================
        // --- Viscosity ---
        //===================

        public double? LiquidViscosity { get; set; } // cp

        public double? OilViscosity { get; set; } // cp

        public double? WaterViscosity { get; set; } // cp

        public double? GasViscosity { get; set; } // cp

        //=========================
        // --- Surface Tension ---
        //=========================

        public double? SurfaceTension { get; set; } // dynes / cm

        public double? LiquidSurfaceTension { get; set; } // dynes / cm

        public double? OilSurfaceTension { get; set; } // dynes / cm

        public double? WaterSurfaceTension { get; set; } // dynes / cm

        public double? GasSurfaceTension { get; set; } // dynes / cm

        //===================
        // --- Fractions ---
        //===================

        public double? OilFraction { get; set; }

        public double? WaterFraction { get; set; }

        //=======================================
        // --- Slip No Flow Regime constants ---
        //=======================================
        public double? LiquidVelocityNumber { get; set; }

        public double? GasVelocityNumber { get; set; }

        public double? PipeDiameterNumber { get; set; }

        public double? LiquidViscosityNumber { get; set; }

    }
}
