using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPFrictionFactor
{
    public class HagedornBrownFrictionFactor : IFrictionFactorCalculator
    {

        private double _calculateReynoldsNumber(VLPWorkingData input, VLPDerivedProperties derivedProperties)
        {

            double noSlipMixtureDensity = derivedProperties.NoSlipMixtureDensity.Value;

            double x = noSlipMixtureDensity * (input.GasSuperficialVelocity.Value +
                input.LiquidSuperficialVelocity.Value) * input.PipeInsideDiameter.Value;

            return (x / derivedProperties.SlipMixtureViscosity.Value) * 1488;

        }

        public double ComputeFrictionFactor(VLPWorkingData input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            double reynoldsNumber = _calculateReynoldsNumber(input, derivedProperties);
            double relativeRoughness = input.PipeRelativeRoughness.Value;

            return SinglePhaseFrictionFactorCalculator.Jain(relativeRoughness, reynoldsNumber);
           

        }


    }
}
