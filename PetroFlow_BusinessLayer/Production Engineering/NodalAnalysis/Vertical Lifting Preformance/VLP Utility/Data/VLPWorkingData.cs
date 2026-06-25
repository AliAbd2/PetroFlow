using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData
{

    /// <summary>
    /// Represents the processed and normalized data used internally by the calculation engine.
    /// This data is prepared by the orchestrator from the raw input and includes derived values 
    /// such as phase flow rates and superficial velocities.
    /// It is the primary data structure consumed by VLP and APR calculation methods.
    /// </summary>

    public class VLPWorkingData
    {

        //=================
        // --- General ---
        //=================

        public double? PipeInsideDiameter { get; set; } // ft

        public double? PipeRelativeRoughness { get; set; }

        public double? GravityAcceleration { get; set; } // ft / sec^2

        //====================
        // --- Flow Rate ----
        //====================

        public double? TotalMassFlowRate { get; set; } // lbm / sec


        //==================
        // --- Density ----
        //==================

        public double? LiquidDensity { get; set; } // lbm / cu ft

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

        public double? GasViscosity { get; set; } // cp

        //=========================
        // --- Surface Tension ---
        //=========================

        public double? LiquidSurfaceTension { get; set; } // dynes / cm

        //=======================================
        // --- Slip No Flow Regime constants ---
        //=======================================
        public double? LiquidVelocityNumber { get; set; }

        public double? GasVelocityNumber { get; set; }

        public double? PipeDiameterNumber { get; set; }

        public double? LiquidViscosityNumber { get; set; }

        public PVTDataInput PVT { get; set; } = new();

    }
}
