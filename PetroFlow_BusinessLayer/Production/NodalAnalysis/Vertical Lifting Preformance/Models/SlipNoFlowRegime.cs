using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels
{
    public class SlipNoFlowRegime : IVLPModel
    {

        public double TotalMassFlowRate { get; set; }

        public double TwoPhaseFrictionFactor { get; set; }

        public double PipeInsideDiameter { get; set; }

        public double MixtureDensity { get; set; }

        public double LiquidHoldup { get; set; }

        private SlipNoFlowRegimeFrictionFactorCalculator _frictionFactorCalculator;

        private SlipNoFlowRegimeHoldupCalculator _holdupCalculator;

        public SlipNoFlowRegime(SlipNoFlowRegimeFrictionFactorCalculator frictionFactorCalculator,
            SlipNoFlowRegimeHoldupCalculator holdupCalculator)
        {

            _frictionFactorCalculator = frictionFactorCalculator;
            _holdupCalculator = holdupCalculator;


        }

        public void ValidateInputData(VLPDataInput input,
            ref NodalAnalysisValidationResult validationResult)
        {

            Validation.Missing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZero(input.LiquidDensity.Value, "liquid density");

            Validation.Missing(input.GasDensity, "gas density");
            Validation.GreaterThanZero(input.GasDensity.Value, "gas density");

            Validation.Missing(input.LiquidViscosity, "liquid viscosity");
            Validation.GreaterThanZero(input.LiquidViscosity.Value, "liquid viscosity");

            Validation.Missing(input.GasViscosity, "gas viscosity");
            Validation.GreaterThanZero(input.GasViscosity.Value, "gas viscosity");

            Validation.Missing(input.GravityAcceleration, "gravity acceleration");
            Validation.GreaterThanZero(input.GravityAcceleration.Value, "gravity acceleration");

            Validation.Missing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.GreaterThanZero(input.LiquidSuperficialVelocity.Value, "liquid superficial velocity");

            Validation.Missing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.GreaterThanZero(input.GasSuperficialVelocity.Value, "gas superficial velocity");



        }

        private void _determineLiquidHoldup(VLPDataInput InputData, 
            ref NodalAnalysisValidationResult validationResult)
        {

            LiquidHoldup = _holdupCalculator.CalculateLiquidHoldup(InputData, ref validationResult);

        }

        private void _determineMixtureDensity(VLPDataInput input)
        {

            MixtureDensity = input.LiquidDensity.Value * LiquidHoldup +
                input.GasDensity.Value * (1 - LiquidHoldup);

        }

        private double _determineElevationTerm(VLPDataInput input)
        {


            return ((input.GravityAcceleration.Value / PhysicsConstants.EarthAcceleration) * MixtureDensity) /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        private double _determineNoSlipMixtureDensity(VLPDataInput input)
        {

            double noSlipLiquidHoldup = VLPGeneralHoldupCalculator.
                CalculateNoSlipLiquidHoldupByVelocity(input.LiquidSuperficialVelocity.Value,
                input.GasSuperficialVelocity.Value);

            return input.LiquidDensity.Value * noSlipLiquidHoldup +
                input.GasDensity.Value * (1 - noSlipLiquidHoldup);

        }

        private double _determineSlipMixtureViscosity(VLPDataInput input)
        {

            double gasHoldup = 1 - LiquidHoldup;
            double liquidViscosity = input.LiquidViscosity.Value;
            double gasViscosity = input.GasViscosity.Value;

            return Math.Pow(liquidViscosity, LiquidHoldup) * Math.Pow(gasViscosity, gasHoldup);
            

        }

        private double _determineFrictionTerm(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {

            double frictionFactor = _frictionFactorCalculator.CalculateFrictionFactor(input,
                derivedProperties, ref validationResult);

            double frictionDensity = Math.Pow(derivedProperties.NoSlipMixtureDensity.Value, 2) / MixtureDensity;

            double totalVelocity = input.GasSuperficialVelocity.Value + input.LiquidSuperficialVelocity.Value;

            double x = frictionFactor * frictionDensity * Math.Pow(totalVelocity, 2);
            double y = 2 * PhysicsConstants.EarthAcceleration * input.PipeInsideDiameter.Value;

            return (x / y) /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        public double DeterminePressureGradient(VLPDataInput input, 
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
