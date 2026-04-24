using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods
{
    public class FancherBrownFrictionFactor : NoSlipNoFlowRegimeFrictionFactorCalculator
    {

        public FancherBrownFrictionFactor()
        {

        }

        protected override void Validate(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult)
        {

            Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.NonNegative(input.GasSuperficialVelocity.Value, "gas superficial velocity");

            Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.NonNegative(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            Validation.GreaterThanZero(noSlipMixtureDensity, "no slip mixture density");

            Validation.Missing(input.PipeInsideDiameter, "pipe inside diameter");
            Validation.GreaterThanZero(input.PipeInsideDiameter.Value, "pipe inside diameter");

            Validation.Missing(input.GasLiquidRatio, "gas liquid ratio");
            Validation.GreaterThanZero(input.GasLiquidRatio.Value, "gas liquid ratio");

        }

        private double DetermineTotalSuperficialVelocity(double liquidSuperficialVelocity,
            double gasSuperficialVelocity)
        {
            return liquidSuperficialVelocity + gasSuperficialVelocity;
        }
        private double DetermineReynoldNumberNumerator(double totalSuperficialVelocity,
            double noSlipMixtureDensity, double pipeInsideDiameter)
        {

            return totalSuperficialVelocity * pipeInsideDiameter * noSlipMixtureDensity;

        }

        private void ValidateDerivedData(double reynoldNumberNumerator,
            double totalSuperficialVelocity, ref NodalAnalysisValidationResult validationResult)
        {

            Validation.GreaterThanZero(reynoldNumberNumerator, "Reynold number numerator");
            Validation.GreaterThanZero(totalSuperficialVelocity, "total superficial velocity");

            if (reynoldNumberNumerator < 2 || reynoldNumberNumerator > 80)
                validationResult.Warnings.Add($"The calculated Reynolds number numerator ({reynoldNumberNumerator}) is outside "
                    + $"Fancher and Brown range [2, 100].");

        }

        private double DeterminFrictionFactorGLR1500(double logReynoldNumberNumerator)
        {

            double x = logReynoldNumberNumerator;
            double x2 = logReynoldNumberNumerator * logReynoldNumberNumerator;
            double x3 = x * x2;

            double y = 1.5346 - 3.9199 * x + 1.9493 * x2 - 0.42924 * x3;

            return Math.Pow(10, y);

        }

        private double DeterminFrictionFactorGLR2250(double logReynoldNumberNumerator)
        {
            double y = 0.1661 - 1.1962 * logReynoldNumberNumerator;
            return Math.Pow(10, y);
        }

        private double DeterminFrictionFactorGLR3000(double logReynoldNumberNumerator)
        {
            double y = - 0.12005 - 1.2971 * logReynoldNumberNumerator;
            return Math.Pow(10, y);
        }

        protected override double Compute(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult)
        {

            double totalSuperficialVelocity = DetermineTotalSuperficialVelocity
                (input.LiquidSuperficialVelocity.Value, input.GasSuperficialVelocity.Value);

            double reynoldNumberNumerator = DetermineReynoldNumberNumerator(totalSuperficialVelocity,
                noSlipMixtureDensity, input.PipeInsideDiameter.Value);

            ValidateDerivedData(reynoldNumberNumerator, totalSuperficialVelocity, ref validationResult);

            double logReynoldNumberNumerator = Math.Log10(reynoldNumberNumerator);
            double GLR = input.GasLiquidRatio.Value;

            if (GLR > 1500 && GLR < 2250)
            {

                double friction1500 = DeterminFrictionFactorGLR1500(logReynoldNumberNumerator);
                double friction2250 = DeterminFrictionFactorGLR2250(logReynoldNumberNumerator);

                double logFriction1500 = Math.Log10(friction1500);
                double logFriction2250 = Math.Log10(friction2250);

                double y = GeneralMathFunctions.LinearInterpolate(GLR, 1500, logFriction1500, 2250, logFriction2250);

                return Math.Pow(10, y);

            }

            if (GLR > 2250 && GLR < 3000)
            {

                double friction2250 = DeterminFrictionFactorGLR2250(logReynoldNumberNumerator);
                double friction3000 = DeterminFrictionFactorGLR3000(logReynoldNumberNumerator);

                double logFriction2250 = Math.Log10(friction2250);
                double logFriction3000 = Math.Log10(friction3000);

                double y = GeneralMathFunctions.LinearInterpolate(GLR, 2250, logFriction2250, 3000, logFriction3000);

                return Math.Pow(10, y);

            }

            if (GLR <= 1500)
                return DeterminFrictionFactorGLR1500(logReynoldNumberNumerator);

            if (GLR == 2250)
                return DeterminFrictionFactorGLR2250(logReynoldNumberNumerator);



            return DeterminFrictionFactorGLR3000(logReynoldNumberNumerator);


        }

    }
}
