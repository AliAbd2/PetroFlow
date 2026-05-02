using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.Holdup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.Holdup
{
    internal class DunsRosHoldupCalculator : SlipFlowRegimeHoldupCalculator
    {

        protected override void ValidateRawInput(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {

            Validation.GreaterThanZeroNotMissing(input.LiquidVelocityNumber, "liquid velocity number");
            Validation.GreaterThanZeroNotMissing(input.GasVelocityNumber, "gas velocity number");
            Validation.GreaterThanZeroNotMissing(input.LiquidDensity, "liquid density");
            Validation.GreaterThanZeroNotMissing(input.LiquidSurfaceTension, "liquid surface tension");

            if (derivedProperties?.FlowRegime == null)
                throw new MissingRequiredInputException("Flow regime is required for Duns & Ros holdup calculation.");

        }

        //============================
        // --- Bubble Flow Regime ---
        //============================

        private double _determineFirstSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated first slip velocity number (F1) may be unreliable.");


            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;

            double x = 0.079607 - 0.75829 * logNl - 0.50545 * logNl2
                + 1.9872 * logNl3 + 5.9203 * logNl4 + 7.1021 * logNl5
                + 4.2355 * logNl6 + 1.2345 * logNl7 + 0.14034 * logNl8;

            return Math.Pow(10, x);

        }

        private double _determineSecondSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated second slip velocity number (F2) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;
            double logNl9 = logNl8 * logNl;
            double logNl10 = logNl9 * logNl;

            double x = -0.097793 + 0.20462 * logNl + 1.8685 * logNl2
                - 0.2653 * logNl3 - 16.899 * logNl4 - 41.868 * logNl5
                - 49.304 * logNl6 - 32.296 * logNl7 - 12.003 * logNl8
                - 2.360 * logNl9 - 0.19306 * logNl10;

            return Math.Pow(10, x);

        }

        private double _determineThirdSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated third slip velocity number (F3) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;
            double logNl9 = logNl8 * logNl;
            double logNl10 = logNl9 * logNl;

            double x = 0.59618 - 0.061896 * logNl - 0.26095 * logNl2
                + 0.62097 * logNl3 + 2.5739 * logNl4 + 3.4162 * logNl5
                + 2.1006 * logNl6 + 0.50572 * logNl7 - 0.070574 * logNl8
                - 0.0596 * logNl9 - 0.0084175 * logNl10;

            return Math.Pow(10, x);

        }

        private double _determineFourthSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated fourth slip velocity number (F4) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;

            return 57.985 + 2.0535 * logNl + 1.8173 * logNl2
                - 17.454 * logNl3 - 28.929 * logNl4 - 12.381 * logNl5
                - 1.7221 * logNl6;

        }

        private double _correctThirdSlipVelocityNumber(VLPWorkingData input,
            double thirdVelocityNumber, double fourthVelocityNumber)
        {

            return thirdVelocityNumber - 
                (fourthVelocityNumber / input.PipeDiameterNumber.Value);

        }

        private double _determineDimensionlessSlipVelocityBubbleFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double F1 = _determineFirstSlipVelocityNumber(input,
                ref validationResult);
            double F2 = _determineSecondSlipVelocityNumber(input,
                ref validationResult);
            double F3 = _determineThirdSlipVelocityNumber(input,
                ref validationResult);
            double F4 = _determineFourthSlipVelocityNumber(input,
                ref validationResult);
            double F3c = _correctThirdSlipVelocityNumber(input,
                F3, F4);


            double x = input.GasVelocityNumber.Value / (1 + input.LiquidVelocityNumber.Value);
            double x2 = x * x;

            return F1 + F2 * input.LiquidVelocityNumber.Value +
                F3c * x2;

        }

        //==========================
        // --- Slug Flow Regime ---
        //==========================

        private double _determineFifthSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated fifth slip velocity number (F5) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;
            double logNl9 = logNl8 * logNl;
            double logNl10 = logNl9 * logNl;
            double logNl11 = logNl10 * logNl;
            double logNl12 = logNl11 * logNl;

            double x = -1.0363 - 1.0044 * logNl - 9.9128 * logNl2
                - 15.896 * logNl3 + 54.705 * logNl4 + 285.75 * logNl5
                + 564.76 * logNl6 + 628.12 * logNl7 + 431.22 * logNl8
                + 186.82 * logNl9 + 49.853 * logNl10 + 7.4949 * logNl11
                + 0.48637 * logNl12;

            return Math.Pow(10, x);

        }


        private double _determineSixthSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated sixth slip velocity number (F6) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;
            double logNl9 = logNl8 * logNl;
            double logNl10 = logNl9 * logNl;
            double logNl11 = logNl10 * logNl;
            double logNl12 = logNl11 * logNl;

            return 1.7501 + 3.3712 * logNl + 25.096 * logNl2
                + 39.691 * logNl3 - 143.84 * logNl4 - 712.83 * logNl5
                - 1334.7 * logNl6 - 1406.1 * logNl7 - 918.85 * logNl8
                - 381.77 * logNl9 - 98.511 * logNl10 - 14.437 * logNl11
                - 0.92 * logNl12;

        }

        private double _determineSeventhSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

             double Nl = input.LiquidVelocityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.Warnings.Add(
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated seventh slip velocity number (F7) may be unreliable.");

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;

            double x = -1.6056 - 0.099951 * logNl + 0.15497 * logNl2
                 + 0.032593 * logNl3;

            return Math.Pow(10, x);
        }

        private double _correctSixthSlipVelocityNumber(VLPWorkingData input, 
            double sixthVelocityNumber)
        {

            return 0.029 * input.PipeDiameterNumber.Value + sixthVelocityNumber;

        }

        private double _determineDimensionlessSlipVelocitySlugFlow(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double F5 = _determineFifthSlipVelocityNumber(input, ref validationResult);
            double F6 = _determineSixthSlipVelocityNumber(input, ref validationResult);
            double F6c = _correctSixthSlipVelocityNumber(input, F6);
            double F7 = _determineSeventhSlipVelocityNumber(input, ref validationResult);
            double Ng = input.GasVelocityNumber.Value;
            double Nl = input.LiquidVelocityNumber.Value;

            double x = 1 + F5;
            double y = Math.Pow(Ng, 0.982) + F6c;
            double z = 1 + F7 * Nl;
            double z2 = z * z;

            return x * (y / z2);


        }

        private double _determineSlipVelocity(VLPWorkingData input,
            double dimensionlessSlipVelocity)
        {

            double liquidDensity = input.LiquidDensity.Value;
            double liquidSurfaceTension = input.LiquidSurfaceTension.Value;

            double x = liquidDensity / (liquidSurfaceTension * PhysicsConstants.EarthAcceleration);
            double y = Math.Pow(x, 0.25);

            return dimensionlessSlipVelocity / y;

        }

        protected override double ComputeLiquidHoldup(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {
            double S = 0;

            if (derivedProperties.FlowRegime == SlipFlowRegime.enFlowRegime.BubbleFlow)
                S = _determineDimensionlessSlipVelocityBubbleFlow(input, 
                    ref validationResult);

            if (derivedProperties.FlowRegime == SlipFlowRegime.enFlowRegime.SlugFlow)
                S = _determineDimensionlessSlipVelocitySlugFlow(input, 
                    ref validationResult);

            if (derivedProperties.FlowRegime == SlipFlowRegime.enFlowRegime.MistFlow)
                throw new InvalidOperationException("Duns and Ros Has no holdup logic for Mist flow regime.");

            if (derivedProperties.FlowRegime == SlipFlowRegime.enFlowRegime.TransitionFlow)
                throw new InvalidOperationException("Duns and Ros Has no holdup logic for Transition flow regime.");

            double Vs = _determineSlipVelocity(input, S);
            double Vm = input.GasSuperficialVelocity.Value + input.LiquidSuperficialVelocity.Value;

            double x = (Vm - Vs) * (Vm - Vs) + 4 * Vs * input.LiquidSuperficialVelocity.Value;
            double y = Math.Pow(x, 0.5);

            return ((Vs - Vm) + y) / (2 * Vs);

        }

    }
}
