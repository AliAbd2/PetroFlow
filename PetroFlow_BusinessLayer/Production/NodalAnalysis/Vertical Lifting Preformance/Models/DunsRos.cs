using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FlowRegime;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.Holdup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.FlowRegime;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.FrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.Holdup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Models
{
    public class DunsRos : SlipFlowRegime
    {


        private readonly FlowRegimeDetector _regimeDetector;

        private readonly SlipFlowRegimeFrictionFactorCalculator _frictionFactorCalculator;

        private readonly SlipFlowRegimeHoldupCalculator _holdupCalculator;

        public DunsRos()
        {

            _regimeDetector = new DunsRosFlowRegimeDetector();
            _frictionFactorCalculator = new DunsRosFrictionFactor();
            _holdupCalculator = new DunsRosHoldupCalculator();

        }

        public override void ValidateInputData(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            Validation.GreaterThanZeroNotMissing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZeroNotMissing(input.GasDensity, "gas density");
            Validation.GreaterThanZeroNotMissing(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.GreaterThanZeroNotMissing(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.GreaterThanZeroNotMissing(input.LiquidVelocityNumber, "liquid velocity number");
            Validation.GreaterThanZeroNotMissing(input.GasVelocityNumber, "gas velocity number");
            Validation.GreaterThanZeroNotMissing(input.GravityAcceleration, "gravity acceleration");
            Validation.GreaterThanZeroNotMissing(input.PipeInsideDiameter, "pipe inside diameter");
            Validation.GreaterThanZeroNotMissing(input.Pressure, "pressure");


        }

        private enFlowRegime _detectFlowRegime(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            return _regimeDetector.DetectFlowRegime(input, ref validationResult);

        }

        private double _determineLiquidHoldup(VLPWorkingData input, enFlowRegime flowRegime,
            ref NodalAnalysisValidationResult validationResult)
        {

            VLPDerivedProperties derivedProperties = new();
            derivedProperties.FlowRegime = flowRegime;

            return _holdupCalculator.CalculateLiquidHoldup(input, 
                ref validationResult, derivedProperties);

        }

        private double _determineSlipTwoPhaseDensity(VLPWorkingData input, enFlowRegime flowRegime, 
            double? liquidHoldup = null)
        {

            double liquidDensity = input.LiquidDensity.Value;
            double gasDensity = input.GasDensity.Value;


            switch (flowRegime)
            {

                case enFlowRegime.BubbleFlow:
                    return liquidDensity * liquidHoldup.Value + gasDensity * (1 - liquidHoldup.Value);
                case enFlowRegime.SlugFlow:
                    return liquidDensity * liquidHoldup.Value + gasDensity * (1 - liquidHoldup.Value);

                default:
                    throw new InvalidParameterException("Unsupported flow regime");


            }

        }

        private double _determineNoSlipTowPhaseDensity(VLPWorkingData input)
        {

            double liquidDensity = input.LiquidDensity.Value;
            double gasDensity = input.GasDensity.Value;
            double Vsl = input.LiquidSuperficialVelocity.Value;
            double Vgl = input.GasSuperficialVelocity.Value;
            double Vm = Vsl + Vgl;

            return liquidDensity * (Vsl / Vm) + gasDensity * (Vgl / Vm);

        }

        private double _determineSlipElevationTerm(VLPWorkingData input, enFlowRegime flowRegime,
            ref NodalAnalysisValidationResult validationResult, double liquidHoldup)
        {
            double twoPhaseDensity;


            switch (flowRegime)
            {
                case enFlowRegime.BubbleFlow:
                    twoPhaseDensity = _determineSlipTwoPhaseDensity(input, flowRegime, liquidHoldup);
                    break;
                case enFlowRegime.SlugFlow:
                    twoPhaseDensity = _determineSlipTwoPhaseDensity(input, flowRegime, liquidHoldup);
                    break;
                default:
                    throw new InvalidParameterException("Unsupported flow regime");


            }

            return twoPhaseDensity * (input.GravityAcceleration.Value / PhysicsConstants.EarthAcceleration);

        }

        private double _determineNoSlipElevationTerm(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {
            double twoPhaseDensity;

            twoPhaseDensity = _determineNoSlipTowPhaseDensity(input);

            return twoPhaseDensity * (input.GravityAcceleration.Value / PhysicsConstants.EarthAcceleration);

        }

        private double _determineFrictionFactor(VLPWorkingData input, enFlowRegime flowRegime,
            ref NodalAnalysisValidationResult validationResult)
        {

            VLPDerivedProperties derivedProperties = new();
            derivedProperties.FlowRegime = flowRegime;

            return _frictionFactorCalculator.CalculateFrictionFactor(input,
                derivedProperties, ref validationResult);

        }

        private double _determineFrictionTerm(VLPWorkingData input, enFlowRegime flowRegime,
            ref NodalAnalysisValidationResult validationResult)
        {

            double frictionFactor = _determineFrictionFactor(input, flowRegime, 
                ref validationResult);

            double liquidDensity = input.LiquidDensity.Value;
            double gasDensity = input.GasDensity.Value;
            double Vsl = input.LiquidSuperficialVelocity.Value;
            double Vsg = input.GasSuperficialVelocity.Value;
            double Vm = Vsl + Vsg;
            double d = input.PipeInsideDiameter.Value;


            if (flowRegime == enFlowRegime.BubbleFlow ||
                flowRegime == enFlowRegime.SlugFlow)
            {

                double x = frictionFactor * liquidDensity * Vsl * Vm;
                double y = 2 * PhysicsConstants.EarthAcceleration * d;

                return x / y;

            }

            if (flowRegime == enFlowRegime.MistFlow)
            {

                double x = frictionFactor * gasDensity * Vsg * Vsg;
                double y = 2 * PhysicsConstants.EarthAcceleration * d;

                return x / y;

            }

            throw new InvalidParameterException("Unsupported flow regime");

        }

        private double _determineAccelerationTerm(VLPWorkingData input)
        {
            double Vsl = input.LiquidSuperficialVelocity.Value;
            double Vsg = input.GasSuperficialVelocity.Value;
            double Vm = Vsl + Vsg;
            double pressure = input.Pressure.Value;


            double NoSlipeTwoPhaseDensity = _determineNoSlipTowPhaseDensity(input);

            double x = Vm * Vsg * NoSlipeTwoPhaseDensity;
            double y = PhysicsConstants.EarthAcceleration * pressure;

            return x / y;

        }

        private double _determinePressureGradientBubbleFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double liquidHoldup = _determineLiquidHoldup(input,
                enFlowRegime.BubbleFlow, ref validationResult);

            double pressureGradient = _determineSlipElevationTerm(input, enFlowRegime.BubbleFlow,
                ref validationResult,  liquidHoldup) + _determineFrictionTerm(input,
                enFlowRegime.BubbleFlow, ref validationResult);


            return pressureGradient /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        private double _determinePressureGradientSlugFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {
            double liquidHoldup = _determineLiquidHoldup(input,
                enFlowRegime.SlugFlow, ref validationResult);

            double pressureGradient = _determineSlipElevationTerm(input, enFlowRegime.SlugFlow,
                ref validationResult, liquidHoldup) + _determineFrictionTerm(input,
                enFlowRegime.SlugFlow, ref validationResult);


            return pressureGradient /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        private double _determinePressureGradientMistFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double x = _determineNoSlipElevationTerm(input, ref validationResult) 
                + _determineFrictionTerm(input, enFlowRegime.MistFlow, ref validationResult);

            double y = 1 - _determineAccelerationTerm(input);

            if (y <= 0)
                throw new InvalidParameterException("Duns and Ros acceleration factor should be less than 1 and positive.");

            double pressureGradient = x / y;

            return pressureGradient /
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

        }

        private double _determinePressureGradientTransitionFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nlv = input.LiquidVelocityNumber.Value;
            double Ngv = input.GasVelocityNumber.Value;
            double Ls = 50 + 36 * Nlv;
            double Lm = 75 + 84 * Math.Pow(Nlv, .75);

            double denominator = (Lm - Ls);

            if (Math.Abs(denominator) < 1e-6)
                throw new InvalidParameterException("Invalid transition region: Lm equals Ls.");

            double A = (Lm - Ngv) / denominator;

            if (A < 0 || A > 1)
                throw new InvalidParameterException(
                    "Invalid transition flow region: the calculated interpolation factor is outside the valid range. " +
                    "Please verify the liquid and gas velocity numbers.");

            double B = 1 - A;

            double pressureGradient = A * _determinePressureGradientSlugFlow(input, ref validationResult)
                + B * _determinePressureGradientMistFlow(input, ref validationResult);

            return pressureGradient;

        }

        public override double DeterminePressureGradient(VLPWorkingData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            enFlowRegime flowRegime = _detectFlowRegime(input, ref validationResult);

            switch(flowRegime)
            {

                case enFlowRegime.BubbleFlow:
                    return _determinePressureGradientBubbleFlow(input, ref validationResult);
                case enFlowRegime.SlugFlow:
                    return _determinePressureGradientSlugFlow(input, ref validationResult);
                case enFlowRegime.MistFlow:
                    return _determinePressureGradientMistFlow(input, ref validationResult);
                case enFlowRegime.TransitionFlow:
                    return _determinePressureGradientTransitionFlow(input, ref validationResult);

                default:
                    throw new InvalidParameterException("Unsupported flow regime");

            }

        }

    }
}
