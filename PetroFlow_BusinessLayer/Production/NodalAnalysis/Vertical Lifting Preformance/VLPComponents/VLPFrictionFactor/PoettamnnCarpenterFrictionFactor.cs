using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods
{
    public class PoettamnnCarpenterFrictionFactor : NoSlipNoFlowRegimeFrictionFactorCalculator
    {

        public PoettamnnCarpenterFrictionFactor()
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

            if (reynoldNumberNumerator < 2 || reynoldNumberNumerator > 100)
                validationResult.Warnings.Add($"The calculated Reynolds number numerator ({reynoldNumberNumerator}) is outside "
                    + $"Poettmann & Carpenter range [2, 100].");

        }

        protected override double Compute(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult)
        {

            double totalSuperficialVelocity = DetermineTotalSuperficialVelocity
                (input.LiquidSuperficialVelocity.Value, input.GasSuperficialVelocity.Value);

            double reynoldNumberNumerator = DetermineReynoldNumberNumerator(totalSuperficialVelocity,
                noSlipMixtureDensity, input.PipeInsideDiameter.Value);

            ValidateDerivedData(reynoldNumberNumerator, totalSuperficialVelocity, ref validationResult);

            // The Poettmann and Carpenter friction factor will be calculated using a correlation 
            // made by regression analysis using python 
            // the equation that was founded by the regression: 
            // f = 10^(2.2326 - 3.4118 * log(ρvd) + 0.60488 * (log(ρvd))^2)

            double logRynoldNumberNumerator = Math.Log10(reynoldNumberNumerator);
            double logRynoldNumberNumerator2 = logRynoldNumberNumerator * logRynoldNumberNumerator;
            double x = 2.2326 - 3.4118 * logRynoldNumberNumerator + 0.60488 * logRynoldNumberNumerator2;

            return Math.Pow(10, x);

        }

    } 
}
