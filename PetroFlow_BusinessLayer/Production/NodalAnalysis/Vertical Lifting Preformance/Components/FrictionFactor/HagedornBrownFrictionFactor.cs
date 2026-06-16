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

        protected override void ValidateRawData(VLPWorkingData input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {



        }

        private double _calculateReynoldsNumber(VLPWorkingData input, VLPDerivedProperties derivedProperties)
        {

            double noSlipMixtureDensity = derivedProperties.NoSlipMixtureDensity.Value;

            double x = noSlipMixtureDensity * (input.GasSuperficialVelocity.Value +
                input.LiquidSuperficialVelocity.Value) * input.PipeInsideDiameter.Value;

            return (x / derivedProperties.SlipMixtureViscosity.Value) * 1488;

        }

        protected override double ComputeFrictionFactor(VLPWorkingData input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            double reynoldsNumber = _calculateReynoldsNumber(input, derivedProperties);
            double relativeRoughness = input.PipeRelativeRoughness.Value;

            return SinglePhaseFrictionFactorCalculator.Jain(relativeRoughness, reynoldsNumber);
           

        }


    }
}
