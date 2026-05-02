using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations
{
    public static class VLPDimensionlessNumbers
    {

        public static double DetermineLiquidVelocityNumber(VLPWorkingData input)
        {

            if (input.LiquidVelocityNumber.HasValue)
                return input.LiquidVelocityNumber.Value;

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.LiquidSurfaceTension, "liquid surface tension");
            Validation.GreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.GreaterThanZero(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 1.938 * input.LiquidSuperficialVelocity.Value * Math.Pow(x, .25);

        }

        public static double DetermineGasVelocityNumber(VLPWorkingData input)
        {

            if (input.GasVelocityNumber.HasValue)
                return input.GasVelocityNumber.Value;

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.LiquidSurfaceTension, "liquid surface tension");
            Validation.GreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.GreaterThanZero(input.GasSuperficialVelocity.Value, "gas superficial velocity");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 1.938 * input.GasSuperficialVelocity.Value * Math.Pow(x, .25);

        }

        public static double DeterminePipeDiameterNumber(VLPWorkingData input)
        {

            if (input.PipeDiameterNumber.HasValue)
                return input.PipeDiameterNumber.Value;

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.LiquidSurfaceTension, "liquid surface tension");
            Validation.GreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.Missing(input.PipeInsideDiameter, "pipe inside diameter");
            Validation.GreaterThanZero(input.PipeInsideDiameter.Value, "pipe inside diameter");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 120.872 * input.PipeInsideDiameter.Value * Math.Pow(x, .5);

        }

        public static double DetermineViscosityNumber(VLPWorkingData input)
        {

            if (input.LiquidViscosityNumber.HasValue)
                return input.LiquidViscosityNumber.Value;

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.LiquidSurfaceTension, "liquid surface tension");
            Validation.GreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.Missing(input.LiquidViscosity, "liquid viscosity");
            Validation.GreaterThanZero(input.LiquidViscosity.Value, "liquid viscosity");

            double x = 1 / (input.LiquidDensity.Value * Math.Pow(input.LiquidSurfaceTension.Value, 3));
            double liquidViscosityNumber = 0.15726 * input.LiquidViscosity.Value * Math.Pow(x, .25);

            return liquidViscosityNumber;

        }

    }
}
