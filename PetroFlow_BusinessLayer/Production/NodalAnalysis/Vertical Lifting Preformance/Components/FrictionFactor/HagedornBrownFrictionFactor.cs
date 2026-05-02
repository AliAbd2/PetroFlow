using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPFrictionFactor
{
    public class HagedornBrownFrictionFactor : SlipNoFlowRegimeFrictionFactorCalculator
    {

        public HagedornBrownFrictionFactor()
        {

        }

        protected override void ValidateRawData(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.GreaterThanZero(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.GreaterThanZero(input.GasSuperficialVelocity.Value, "gas superficial velocity");

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.GasDensity, "gas density");
            Validation.GreaterThanZero(input.GasDensity.Value, "gas density");

            Validation.Missing(input.PipeInsideDiameter, "pipe inside diameter");
            Validation.GreaterThanZero(input.PipeInsideDiameter.Value, "pipe inside diameter");

            Validation.Missing(input.PipeRelativeRoughness, "pipe relative roughness");
            Validation.GreaterThanZero(input.PipeRelativeRoughness.Value, "pipe relative roughness");

            Validation.Missing(derivedProperties.SlipMixtureViscosity, "slip mixture viscosity");
            Validation.GreaterThanZero(derivedProperties.SlipMixtureViscosity.Value, "slip mixture viscosity");

            Validation.Missing(derivedProperties.NoSlipMixtureDensity, "no slip mixture density");
            Validation.GreaterThanZero(derivedProperties.NoSlipMixtureDensity.Value, "no slip mixture density");

        }

        private double _calculateReynoldsNumber(VLPDataInput input, VLPDerivedProperties derivedProperties)
        {

            double noSlipMixtureDensity = derivedProperties.NoSlipMixtureDensity.Value;

            double x = noSlipMixtureDensity * (input.GasSuperficialVelocity.Value +
                input.LiquidSuperficialVelocity.Value) * input.PipeInsideDiameter.Value;

            return (x / derivedProperties.SlipMixtureViscosity.Value) * 1488;

        }

        protected override double ComputeFrictionFactor(VLPDataInput input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            double reynoldsNumber = _calculateReynoldsNumber(input, derivedProperties);
            double relativeRoughness = input.PipeRelativeRoughness.Value;

            return SinglePhaseFrictionFactorCalculator.Jain(relativeRoughness, reynoldsNumber);
           

        }


    }
}
