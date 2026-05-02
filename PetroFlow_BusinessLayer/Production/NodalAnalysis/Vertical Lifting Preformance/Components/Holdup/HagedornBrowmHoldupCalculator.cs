using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPHoldup
{
    public class HagedornBrownHoldupCalculator : SlipNoFlowRegimeHoldupCalculator
    {

        public HagedornBrownHoldupCalculator()
        {

        }

        protected override void ValidateRawInput(VLPWorkingData input, 
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {

            if (input.LiquidVelocityNumber.HasValue)
                Validation.GreaterThanZero(input.LiquidVelocityNumber.Value, "liquid velocity number");
            else
            {
                Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
                Validation.GreaterThanZero(input.LiquidSuperficialVelocity.Value,
                    "liquid superficial velocity");
            }


            if (input.GasVelocityNumber.HasValue)
                Validation.GreaterThanZero(input.GasVelocityNumber.Value, "gas velocity number");
            else
            {

                Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
                Validation.GreaterThanZero(input.GasSuperficialVelocity.Value,
                    "gas superficial velocity");

            }

            if (input.PipeDiameterNumber.HasValue)
                Validation.GreaterThanZero(input.PipeDiameterNumber.Value, "pipe diameter number");
            else
            {

                Validation.Missing(input.PipeInsideDiameter, "pipe inside diameter");
                Validation.GreaterThanZero(input.PipeInsideDiameter.Value, 
                    "pipe inside diameter");

            }

            if (input.LiquidViscosityNumber.HasValue)
                Validation.GreaterThanZero(input.LiquidViscosityNumber.Value, "liquid viscosity number");
            else
            {
                Validation.Missing(input.LiquidViscosity, "liquid viscosity");
                Validation.GreaterThanZero(input.LiquidViscosity.Value, "liquid viscosity");

            }


            if (!input.LiquidVelocityNumber.HasValue || !input.GasVelocityNumber.HasValue ||
                !input.PipeDiameterNumber.HasValue || !input.LiquidViscosityNumber.HasValue)
            {

                Validation.Missing(input.LiquidDensity, "liquid density");
                Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

                Validation.Missing(input.LiquidSurfaceTension, "liquid surface tension");
                Validation.GreaterThanZero(input.LiquidSurfaceTension.Value,
                    "liquid surface tension");

            }

            Validation.Missing(input.Pressure, "pressure");
            Validation.GreaterThanZero(input.Pressure.Value, "pressure");

        }

        private double _determineLiquidVelocityNumber(VLPWorkingData input)
        {

            return VLPDimensionlessNumbers.DetermineLiquidVelocityNumber(input);

        }

        private double _determineGasVelocityNumber(VLPWorkingData input)
        {

            return VLPDimensionlessNumbers.DetermineGasVelocityNumber(input);

        }

        private double _determinePipeDiameterNumber(VLPWorkingData input)
        {

            return VLPDimensionlessNumbers.DeterminePipeDiameterNumber(input);

        }

        private double _determineViscosityNumber(VLPWorkingData input)
        {

            return VLPDimensionlessNumbers.DetermineViscosityNumber(input);

        }

        private double _determineViscosityNumberCorrected(double liquidViscosityNumber,
            ref NodalAnalysisValidationResult validationResult)
        {

            if (liquidViscosityNumber < 0.002 || liquidViscosityNumber > .5)
                validationResult.Warnings.Add("Liquid Viscosity Number is out of range [0.002, 0.5], " +
                    "Unrealistic result is expected");

            double logNl = Math.Log10(liquidViscosityNumber);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;

            double y = -1.9838 - 0.66417 * logNl - 1.4314 * logNl2 - 0.6686 * logNl3 - 0.098641 * logNl4;

            return Math.Pow(10, y);

        }

        private double _determineHoldupFactor(double pressure, double liquidVelocityNumber,
            double gasVelocityNumber, double pipeDiameterNumber, 
            double correctedViscosityNumber , ref NodalAnalysisValidationResult validationResult)
        {

            double x = (liquidVelocityNumber / Math.Pow(gasVelocityNumber, .575));
            double y = Math.Pow(pressure / PhysicsConstants.StandardConditionPressure, .10);
            double z = correctedViscosityNumber / pipeDiameterNumber;

            double a = x * y * z;

            if (a < 2e-6 || a > 5e-3)
                validationResult.Warnings.Add("The factor that is used to determine" +
                    " holdup factor is out of range [2e-6, 5e-3], Unrealistic result is expected");


            double loga = Math.Log10(a);
            double loga2 = loga * loga;
            double loga3 = loga2 * loga;
            double loga4 = loga3 * loga;
            double loga5 = loga4 * loga;
            double loga6 = loga5 * loga;
            double loga7 = loga6 * loga;

            // Polynomial regression coefficients derived from digitized
            // Hagedorn-Brown chart data using Python regression analysis.

            return 110.77380416266402 
                + (208.73448813543106 * loga ) 
                + (165.16418602777244 * loga2)
                + (70.38374553033373 * loga3) 
                + (17.45043646297661 * loga4) 
                + (2.523953063395804 * loga5) 
                + (0.1977522753048504 * loga6) 
                + (0.006491450202389615 * loga7);


        }

        private double _determineHoldupCorrectionFactor(double gasVelocityNumber,
            double liquidViscosityNumber, double pipeDiameterNumber, 
            ref NodalAnalysisValidationResult validationResult)
        {

            double x = (gasVelocityNumber * Math.Pow(liquidViscosityNumber, .38)) /
                Math.Pow(pipeDiameterNumber, 2.14);

            if (x < 0.1 || x > 0.9)
                validationResult.Warnings.Add("The factor that is used to correct" +
                    " holdup is out of range [0.1, 0.9], Unrealistic result is expected");

            if (x < 0.1)
                return 1;

            if (x > 0.9)
                return 1.82;

            double x2 = x * x;
            double x3 = x2 * x;
            double x4 = x3 * x;
            double x5 = x4 * x;
            double x6 = x5 * x;

            return 1.771 - 157.53 * x + 1.0642e4 * x2 - 2.9088e5 * x3 + 3.97e6 * x4 -
                2.6775e7 * x5 + 7.0863e7 * x6;

        }

        protected override double ComputeLiquidHoldup(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {

            double liquidVelocityNumber = _determineLiquidVelocityNumber(input);
            double gasVelocityNumber = _determineGasVelocityNumber(input);
            double liquidViscosityNumber = _determineViscosityNumber(input);
            double pipeDiameterNumber = _determinePipeDiameterNumber(input);
            double correctedLiquidViscosityNumber = _determineViscosityNumberCorrected(
                liquidViscosityNumber, ref validationResult);

            double holdupFactor = _determineHoldupFactor(input.Pressure.Value,
                liquidVelocityNumber, gasVelocityNumber, pipeDiameterNumber, correctedLiquidViscosityNumber,
                ref validationResult);

            double holdupCorrectionFactor = _determineHoldupCorrectionFactor(gasVelocityNumber,
                liquidViscosityNumber, pipeDiameterNumber, ref validationResult);

            return holdupFactor * holdupCorrectionFactor;


        }

    }
}
