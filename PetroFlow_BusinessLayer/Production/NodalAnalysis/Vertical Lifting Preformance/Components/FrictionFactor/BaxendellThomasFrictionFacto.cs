using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods
{
    public class BaxendellThomasFrictionFacto : NoSlipNoFlowRegimeFrictionFactorCalculator
    {

        public BaxendellThomasFrictionFacto()
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

            if (reynoldsNumberNumerator < 2 || reynoldsNumberNumerator > 100)
                validationResult.Warnings.Add($"The calculated Reynolds number numerator ({reynoldsNumberNumerator}) is outside "
                    + $"Baxendell and Thomas range [2, 100].");

        }

        protected override double Compute(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            double totalSuperficialVelocity = DetermineTotalSuperficialVelocity
                (input.LiquidSuperficialVelocity.Value, input.GasSuperficialVelocity.Value);

            double reynoldsNumberNumerator = DeterminereynoldsNumberNumerator(totalSuperficialVelocity,
                derivedProperties.NoSlipMixtureDensity.Value, input.PipeInsideDiameter.Value);

            ValidateDerivedData(reynoldsNumberNumerator, totalSuperficialVelocity, ref validationResult);

            // The Baxendell and Thomas friction factor will be calculated using a correlation 
            // made by regression analysis using python 
            // the equation that was founded by the regression: 
            // f = 10^(2.3345 - 3.5464 * log(ρvd) + 0.34663 * (log(ρvd)))^2 + 0.21188 * (log(ρvd))^3)

            double logReynoldsNumberNumerator = Math.Log10(reynoldsNumberNumerator);
            double logReynoldsNumberNumerator2 = logReynoldsNumberNumerator * logReynoldsNumberNumerator;
            double logReynoldsNumberNumerator3 = logReynoldsNumberNumerator2 * logReynoldsNumberNumerator;
            double x = 2.3345 - 3.5464 * logReynoldsNumberNumerator + 0.34663 * logReynoldsNumberNumerator2 +
                0.21188 * logReynoldsNumberNumerator3;

            return Math.Pow(10, x);

        }


    }
}
