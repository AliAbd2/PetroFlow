using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.Holdup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels
{
    internal class SlipNoFlowRegime : IVLPModel
    {

        public double TotalMassFlowRate { get; set; }

        public double TwoPhaseFrictionFactor { get; set; }

        public double PipeInsideDiameter { get; set; }

        public double MixtureDensity { get; set; }

        public double LiquidHoldup { get; set; }

        private IFrictionFactorCalculator _frictionFactorCalculator;

        private IHoldupCalculator _holdupCalculator;

        public SlipNoFlowRegime(IFrictionFactorCalculator frictionFactorCalculator,
            IHoldupCalculator holdupCalculator)
        {

            _frictionFactorCalculator = frictionFactorCalculator;
            _holdupCalculator = holdupCalculator;


        }

        public void ValidateInputData(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {





        }

        private void _determineLiquidHoldup(VLPWorkingData InputData, 
            ref NodalAnalysisValidationResult validationResult)
        {

            LiquidHoldup = _holdupCalculator.ComputeLiquidHoldup(InputData, ref validationResult);

        }

        private void _determineMixtureDensity(VLPWorkingData input)
        {

            MixtureDensity = input.LiquidDensity.Value * LiquidHoldup +
                input.GasDensity.Value * (1 - LiquidHoldup);

        }

        private double _determineElevationTerm(VLPWorkingData input)
        {


            return ((input.GravityAcceleration.Value / PhysicsConstants.EarthAcceleration) * MixtureDensity) /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        private double _determineNoSlipMixtureDensity(VLPWorkingData input)
        {

            double noSlipLiquidHoldup = VLPGeneralHoldupCalculator.
                CalculateNoSlipLiquidHoldupByVelocity(input.LiquidSuperficialVelocity.Value,
                input.GasSuperficialVelocity.Value);

            return input.LiquidDensity.Value * noSlipLiquidHoldup +
                input.GasDensity.Value * (1 - noSlipLiquidHoldup);

        }

        private double _determineSlipMixtureViscosity(VLPWorkingData input)
        {

            double gasHoldup = 1 - LiquidHoldup;
            double liquidViscosity = input.LiquidViscosity.Value;
            double gasViscosity = input.GasViscosity.Value;

            return Math.Pow(liquidViscosity, LiquidHoldup) * Math.Pow(gasViscosity, gasHoldup);
            

        }

        private double _determineFrictionTerm(VLPWorkingData input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            double frictionFactor = _frictionFactorCalculator.ComputeFrictionFactor(input,
                derivedProperties, ref validationResult);

            double frictionDensity = Math.Pow(derivedProperties.NoSlipMixtureDensity.Value, 2) / MixtureDensity;

            double totalVelocity = input.GasSuperficialVelocity.Value + input.LiquidSuperficialVelocity.Value;

            double x = frictionFactor * frictionDensity * Math.Pow(totalVelocity, 2);
            double y = 2 * PhysicsConstants.EarthAcceleration * input.PipeInsideDiameter.Value;

            return (x / y) /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        public double DeterminePressureGradient(VLPWorkingData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            VLPDerivedProperties derivedProperties = new();

            ValidateInputData(input, ref validationResult);

            _determineLiquidHoldup(input, ref validationResult);
            _determineMixtureDensity(input);
            double elvationTerm = _determineElevationTerm(input);

            derivedProperties.NoSlipMixtureDensity = _determineNoSlipMixtureDensity(input);
            derivedProperties.SlipMixtureViscosity =  _determineSlipMixtureViscosity(input);
            double frictionTerm = _determineFrictionTerm(input, derivedProperties, 
                ref validationResult);


            return elvationTerm + frictionTerm;

        }


    }
}
