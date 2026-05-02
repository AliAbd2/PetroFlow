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

        protected override void Validate(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.NonNegative(input.GasSuperficialVelocity.Value, "gas superficial velocity");

            Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.NonNegative(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            Validation.Missing(derivedProperties.NoSlipMixtureDensity, "no slip mixture density");
            Validation.GreaterThanZero(derivedProperties.NoSlipMixtureDensity.Value, "no slip mixture density");

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
        private double DeterminereynoldsNumberNumerator(double totalSuperficialVelocity,
            double noSlipMixtureDensity, double pipeInsideDiameter)
        {

            return totalSuperficialVelocity * pipeInsideDiameter * noSlipMixtureDensity;

        }

        private void ValidateDerivedData(double reynoldsNumberNumerator,
            double totalSuperficialVelocity, ref NodalAnalysisValidationResult validationResult)
        {

            Validation.GreaterThanZero(reynoldsNumberNumerator, "Reynold number numerator");
            Validation.GreaterThanZero(totalSuperficialVelocity, "total superficial velocity");

            if (reynoldsNumberNumerator < 2 || reynoldsNumberNumerator > 80)
                validationResult.Warnings.Add($"The calculated Reynolds number numerator ({reynoldsNumberNumerator}) is outside "
                    + $"Fancher and Brown range [2, 100].");

        }

        private double DetermineFrictionFactorGLR1500(double logreynoldsNumberNumerator)
        {

            double x = logreynoldsNumberNumerator;
            double x2 = logreynoldsNumberNumerator * logreynoldsNumberNumerator;
            double x3 = x * x2;

            double y = 1.5346 - 3.9199 * x + 1.9493 * x2 - 0.42924 * x3;

            return Math.Pow(10, y);

        }

        private double DeterminFrictionFactorGLR2250(double logreynoldsNumberNumerator)
        {
            double y = 0.1661 - 1.1962 * logreynoldsNumberNumerator;
            return Math.Pow(10, y);
        }

        private double DeterminFrictionFactorGLR3000(double logreynoldsNumberNumerator)
        {
            double y = - 0.12005 - 1.2971 * logreynoldsNumberNumerator;
            return Math.Pow(10, y);
        }

        protected override double Compute(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            double totalSuperficialVelocity = DetermineTotalSuperficialVelocity
                (input.LiquidSuperficialVelocity.Value, input.GasSuperficialVelocity.Value);

            double reynoldsNumberNumerator = DeterminereynoldsNumberNumerator(totalSuperficialVelocity,
                derivedProperties.NoSlipMixtureDensity.Value, input.PipeInsideDiameter.Value);

            ValidateDerivedData(reynoldsNumberNumerator, totalSuperficialVelocity, ref validationResult);

            double logreynoldsNumberNumerator = Math.Log10(reynoldsNumberNumerator);
            double GLR = input.GasLiquidRatio.Value;

            if (GLR > 1500 && GLR < 2250)
            {

                double friction1500 = DetermineFrictionFactorGLR1500(logreynoldsNumberNumerator);
                double friction2250 = DeterminFrictionFactorGLR2250(logreynoldsNumberNumerator);

                double logFriction1500 = Math.Log10(friction1500);
                double logFriction2250 = Math.Log10(friction2250);

                double y = GeneralMathFunctions.LinearInterpolate(GLR, 1500, logFriction1500, 2250, logFriction2250);

                return Math.Pow(10, y);

            }

            if (GLR > 2250 && GLR < 3000)
            {

                double friction2250 = DeterminFrictionFactorGLR2250(logreynoldsNumberNumerator);
                double friction3000 = DeterminFrictionFactorGLR3000(logreynoldsNumberNumerator);

                double logFriction2250 = Math.Log10(friction2250);
                double logFriction3000 = Math.Log10(friction3000);

                double y = GeneralMathFunctions.LinearInterpolate(GLR, 2250, logFriction2250, 3000, logFriction3000);

                return Math.Pow(10, y);

            }

            if (GLR <= 1500)
                return DetermineFrictionFactorGLR1500(logreynoldsNumberNumerator);

            if (GLR == 2250)
                return DeterminFrictionFactorGLR2250(logreynoldsNumberNumerator);



            return DeterminFrictionFactorGLR3000(logreynoldsNumberNumerator);


        }

    }
}
