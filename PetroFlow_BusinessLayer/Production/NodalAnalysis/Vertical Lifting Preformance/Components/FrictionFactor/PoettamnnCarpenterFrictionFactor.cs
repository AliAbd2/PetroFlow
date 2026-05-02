using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods
{
    public class PoettmannCarpenterFrictionFactor : NoSlipNoFlowRegimeFrictionFactorCalculator
    {

        public PoettmannCarpenterFrictionFactor()
        {

        }

        protected override void ValidateRawData(VLPWorkingData input, VLPDerivedProperties derivedProperties,
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

        private void ValidateDerivedData(ref double reynoldsNumberNumerator,
            double totalSuperficialVelocity, ref NodalAnalysisValidationResult validationResult)
        {

            Validation.GreaterThanZero(reynoldsNumberNumerator, "Reynold number numerator");
            Validation.GreaterThanZero(totalSuperficialVelocity, "total superficial velocity");

            reynoldsNumberNumerator = Math.Clamp(reynoldsNumberNumerator, 2, 100);

        }

        protected override double ComputeFrictionFactor(VLPWorkingData input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            double totalSuperficialVelocity = DetermineTotalSuperficialVelocity
                (input.LiquidSuperficialVelocity.Value, input.GasSuperficialVelocity.Value);

            double reynoldsNumberNumerator = DeterminereynoldsNumberNumerator(totalSuperficialVelocity,
                derivedProperties.NoSlipMixtureDensity.Value, input.PipeInsideDiameter.Value);

            ValidateDerivedData(ref reynoldsNumberNumerator, totalSuperficialVelocity, ref validationResult);

            // The Poettmann and Carpenter friction factor will be calculated using a correlation 
            // made by regression analysis using python 
            // the equation that was founded by the regression: 
            // f = 10^(2.2326 - 3.4118 * log(ρvd) + 0.60488 * (log(ρvd))^2)

            double logReynoldsNumberNumerator = Math.Log10(reynoldsNumberNumerator);
            double logReynoldsNumberNumerator2 = logReynoldsNumberNumerator * logReynoldsNumberNumerator;
            double x = 2.232567621431371 - 3.411827278182862 * logReynoldsNumberNumerator + 0.6048752861775446 * logReynoldsNumberNumerator2;

            return Math.Pow(10, x);

        }

    } 
}
