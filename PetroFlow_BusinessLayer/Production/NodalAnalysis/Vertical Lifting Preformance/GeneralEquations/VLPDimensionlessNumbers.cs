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

            Validation.IsGreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.IsGreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.IsGreaterThanZero(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 1.938 * input.LiquidSuperficialVelocity.Value * Math.Pow(x, .25);

        }

        public static double DetermineGasVelocityNumber(VLPWorkingData input)
        {

            if (input.GasVelocityNumber.HasValue)
                return input.GasVelocityNumber.Value;

            Validation.IsGreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.IsGreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.IsGreaterThanZero(input.GasSuperficialVelocity.Value, "gas superficial velocity");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 1.938 * input.GasSuperficialVelocity.Value * Math.Pow(x, .25);

        }

        public static double DeterminePipeDiameterNumber(VLPWorkingData input)
        {

            if (input.PipeDiameterNumber.HasValue)
                return input.PipeDiameterNumber.Value;

            Validation.IsGreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.IsGreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.IsGreaterThanZero(input.PipeInsideDiameter.Value, "pipe inside diameter");

            double x = input.LiquidDensity.Value / input.LiquidSurfaceTension.Value;

            return 120.872 * input.PipeInsideDiameter.Value * Math.Pow(x, .5);

        }

        public static double DetermineViscosityNumber(VLPWorkingData input)
        {

            if (input.LiquidViscosityNumber.HasValue)
                return input.LiquidViscosityNumber.Value;

            Validation.IsGreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.IsGreaterThanZero(input.LiquidSurfaceTension.Value, "liquid surface tension");

            Validation.IsGreaterThanZero(input.LiquidViscosity.Value, "liquid viscosity");

            double x = 1 / (input.LiquidDensity.Value * Math.Pow(input.LiquidSurfaceTension.Value, 3));
            double liquidViscosityNumber = 0.15726 * input.LiquidViscosity.Value * Math.Pow(x, .25);

            return liquidViscosityNumber;

        }

    }
}
