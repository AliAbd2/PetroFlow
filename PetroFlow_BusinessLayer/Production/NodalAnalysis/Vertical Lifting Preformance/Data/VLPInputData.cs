using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data
{

    /// <summary>
    /// Represents the raw input data provided by the user.
    /// This data may come in different unit systems and is not guaranteed to be validated or normalized.
    /// It serves as the entry point for the calculation pipeline before any processing or unit conversion.
    /// </summary>
    public class VLPInputData
    {

        public double? WellHeadPressure { get; set; }

        public double? Temperature { get; set; }

        public double? GasLiquidRatio { get; set; }

        public double? TotalDepth { get; set; }

        public double? GravityAcceleration { get; set; } 

        public double? OilFormationVolumeFactor { get; set; }

        public double? GasFormationVolumeFactor { get; set; }

        //=================
        // --- Density ---
        //=================

        public double? OilDensity { get; set; }

        public double? WaterDensity { get; set; }

        public double? liquidDensity { get; set; }

        public double?  GasDensity { get; set; }

        //===================
        // --- Viscosity ---
        //===================

        public double? OilViscosity { get; set; }

        public double? WaterViscosity { get; set; }

        public double? LiquidViscosity { get; set; }

        public double? GasViscosity { get; set; }

        //=========================
        // --- Pipe Properties ---
        //=========================

        public double? PipeInsideDiameter { get; set; }

        public double? PipeRelativeRoughness { get; set; }

        //=========================
        // --- Surface Tension ---
        //=========================

        public double? SurfaceTension { get; set; }

        //============================
        // --- Summation Settings ---
        //============================

        public double? DepthStepSize { get; set; }

        public double? MaximumFlowRate { get; set; }

        public double? FlowRateStepSize { get; set; }

        public double? MinimumPressure { get; set; }


    }
}
