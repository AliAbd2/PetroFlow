using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.FrictionFactor
{
    internal class DunsRosFrictionFactor : SlipFlowRegimeFrictionFactorCalculator
    {

        protected override void ValidateRawData(VLPWorkingData input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            Validation.IsNonNegative(input.LiquidDensity, "liquid density");
            Validation.IsNonNegative(input.LiquidSuperficialVelocity, "liquid superficial velocity");
            Validation.IsNonNegative(input.PipeInsideDiameter, "pipe inside diameter");
            Validation.IsNonNegative(input.LiquidViscosity, "liquid viscosity");
            Validation.IsNonNegative(input.PipeRelativeRoughness, "pipe relative roughness");
            Validation.IsNonNegative(input.PipeDiameterNumber, "pipe diameter number");
            Validation.IsNonNegative(input.GasSuperficialVelocity, "gas superficial velocity");
            Validation.IsNonNegative(input.LiquidSurfaceTension, "liquid surface tension");
            Validation.IsNonNegative(input.GasDensity, "gas density");

        }

        //============================
        // --- Bubble & Slug Flow ---
        //============================
        private double _determineLiquidReynoldsNumber(VLPWorkingData input)
        {

            // this Reynold's number is used in Bubble Flow regime.

            double liquidDensity = input.LiquidDensity.Value;
            double liquidSuperficialVelocity = input.LiquidSuperficialVelocity.Value;
            double pipeInsideDiameter = input.PipeInsideDiameter.Value;
            double liquidViscoity = input.LiquidViscosity.Value * UnitConversionConstants.CentipoiseToLbPerFtSec;

            return (liquidDensity * liquidSuperficialVelocity * pipeInsideDiameter) / liquidViscoity;

        }

        private double _determineFirstDimensionlessFrictionFactorCoefficient(VLPWorkingData input
            ,double liquidReynoldsNumber)
        {

            if (liquidReynoldsNumber < 2000)
                return SinglePhaseFrictionFactorCalculator.LaminarFrictionFactor(liquidReynoldsNumber);

            return SinglePhaseFrictionFactorCalculator.Jain(input.PipeRelativeRoughness.Value
                , liquidReynoldsNumber);

        }

        private double _determineSecondDimensionlessFrictionFactorCoefficient(VLPWorkingData input,
            double firstDimensionlessFrictionFactor, NodalAnalysisValidationResult validationResult)
        {

            double poweredDiameterNumber = Math.Pow(input.PipeDiameterNumber.Value, 2.0/3.0);
            double velocityRatio = input.GasSuperficialVelocity.Value / input.LiquidSuperficialVelocity.Value;
            double x = firstDimensionlessFrictionFactor * velocityRatio * poweredDiameterNumber;


            if (x < 1e-3 || x > 1e+3)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                    "The parameter used to calculate the second Duns & Ros dimensionless friction factor " +
                    "is outside the recommended range [0.001, 1000]. The calculated friction factor may be unreliable."));

            if (x == 0)
                return 1;

            double logx = Math.Log10(x);
            double logx2 = logx * logx;
            double logx3 = logx2 * logx;
            double logx4 = logx3 * logx;
            double logx5 = logx4 * logx;
            double logx6 = logx5 * logx;
            double logx7 = logx6 * logx;
            double logx8 = logx7 * logx;
            double logx9 = logx8 * logx;
            double logx10 = logx9 * logx;
            double logx11 = logx10 * logx;
            double logx12 = logx11 * logx;

            double y = - 0.09811627423250424 - 0.3279867989935618 * logx
                - 0.19960816879660434 * logx2 + 0.13105168486648253 * logx3
                + 0.08108681669613091 * logx4 - 0.053364332149608396 * logx5
                - 0.019936454999999523 * logx6 + 0.01171603383383939 * logx7
                + 0.0028592480725095097 * logx8 - 0.0012098232363160644 * logx9
                - 0.00021315706678953078 * logx10 + 4.666595592622835e-5 * logx11
                + 6.3046754878448574e-6 * logx12;

            return Math.Pow(10, y);


        }

        private double _determineThirdDimensionlessFrictionFactorCoefficient(VLPWorkingData input,
            double firstDimensionlessFrictionFactor)
        {

            double x = input.GasSuperficialVelocity.Value / (50 * input.LiquidSuperficialVelocity.Value);

            return 1 + firstDimensionlessFrictionFactor * Math.Pow(x, 0.5);


        }

        private double _determineFrictionFactorBubbleFlow(VLPWorkingData input,
            VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            double liquidReynoldsNumber = _determineLiquidReynoldsNumber(input);

            double f1 = _determineFirstDimensionlessFrictionFactorCoefficient(input,
                liquidReynoldsNumber);
            double f2 = _determineSecondDimensionlessFrictionFactorCoefficient(input,
                f1, validationResult);
            double f3 = _determineThirdDimensionlessFrictionFactorCoefficient(input, 
                f1);

            return f1 * (f2 / f3);

        }


        //====================
        // --- Mist Flow ---
        //====================

        private double _determineWeberNumber(VLPWorkingData input)
        {

            double gasDensity = input.GasDensity.Value;
            double gasSuperficialVelocity2 = input.GasSuperficialVelocity.Value *
                input.GasSuperficialVelocity.Value;
            double pipeRelativeRoughness = input.PipeRelativeRoughness.Value;
            double liquidSurfaceTension = input.LiquidSurfaceTension.Value;

            return (gasDensity * gasSuperficialVelocity2 * pipeRelativeRoughness) / liquidSurfaceTension;

        }

        private double _determineModifiedLiquidViscosityNumber(VLPWorkingData input)
        {

            double liquidViscosity2 = input.LiquidViscosity.Value * input.LiquidViscosity.Value;
            double liquidDensity = input.LiquidDensity.Value;
            double liquidSurfaceTension = input.LiquidSurfaceTension.Value;
            double pipeRelativeRoughness = input.PipeRelativeRoughness.Value;

            return liquidViscosity2 / (liquidDensity * liquidSurfaceTension * pipeRelativeRoughness);

        }

        private double _determineModifiedPipeRelativeRoughness(VLPWorkingData input)
        {

            double weberNumber = _determineWeberNumber(input);
            double modifiedLiquidViscosityNumber = _determineModifiedLiquidViscosityNumber(input);

            double relativeRoughnessDimensionlessNumber = weberNumber * modifiedLiquidViscosityNumber;

            double gasDensity = input.GasDensity.Value;
            double gasSuperficialVelocity2 = input.GasSuperficialVelocity.Value *
                input.GasSuperficialVelocity.Value;
            double pipeInsideDiameter = input.PipeInsideDiameter.Value;
            double liquidSurfaceTension = input.LiquidSurfaceTension.Value;

            double y = gasDensity * gasSuperficialVelocity2 * pipeInsideDiameter;

            if (relativeRoughnessDimensionlessNumber <= .0005)
            {

                double x = .0749 * liquidSurfaceTension;

                return x / y;

            }
           else
            {

                double x = .3713 * liquidSurfaceTension;

                return x / y * Math.Pow(relativeRoughnessDimensionlessNumber, .302);

            }


        }

        private double _determineFrictionFactorMistFlow(VLPWorkingData input)
        {

            double modifiedRoughness = _determineModifiedPipeRelativeRoughness(input);

            double a = 4 * Math.Log10(0.23 * modifiedRoughness);
            double b = 1 / (a * a);
            double c = 0.067 * Math.Pow(modifiedRoughness, 1.73);

            return (b + c) * 4;
            
        }

        protected override double ComputeFrictionFactor(VLPWorkingData input,
            VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {

            if (derivedProperties.FlowRegime == SlipFlowRegime.enFlowRegime.TransitionFlow)
                throw new InvalidOperationException("Duns and Ros Has no friction factor logic for Transition flow regime.");

            switch (derivedProperties.FlowRegime)
            {

                case SlipFlowRegime.enFlowRegime.BubbleFlow:
                    return _determineFrictionFactorBubbleFlow(input, derivedProperties, 
                        ref validationResult);
                case SlipFlowRegime.enFlowRegime.SlugFlow:
                    return _determineFrictionFactorBubbleFlow(input, derivedProperties,
                        ref validationResult);
                case SlipFlowRegime.enFlowRegime.MistFlow:
                    return _determineFrictionFactorMistFlow(input);
                default:
                    throw new InvalidOperationException("Could not detect Flow Rwgime");


            }

        }

    }
}
