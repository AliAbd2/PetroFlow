using PetroFlow_BusinessLayer.General_Utility.Validation;
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

            Validation.IsNonNegative(input.LiquidVelocityNumber, "liquid velocity number");
            Validation.IsNonNegative(input.GasVelocityNumber, "gas velocity number");
            Validation.IsNonNegative(input.LiquidDensity, "liquid density");
            Validation.IsNonNegative(input.LiquidSurfaceTension, "liquid surface tension");

            if (derivedProperties?.FlowRegime == null)
                throw new MissingRequiredInputException(new ErrorMessage("Flow Regime Detection Error", 
                    "Flow regime is required for Duns & Ros holdup calculation."));

        }

        //============================
        // --- Bubble Flow Regime ---
        //============================

        private double _determineFirstSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated first slip velocity number (F1) may be unreliable."));


            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;
            double logNl7 = logNl6 * logNl;
            double logNl8 = logNl7 * logNl;

            double x = 0.07960693318590623 - 0.7582859318441545 * logNl - 0.5054462330299302 * logNl2
                + 1.9872221260923268 * logNl3 + 5.920270668948145 * logNl4 + 7.10211126536129 * logNl5
                + 4.2354609478746195 * logNl6 + 1.2344885844958777 * logNl7 + 0.14033985973556018 * logNl8;

            return Math.Pow(10, x);

        }

        private double _determineSecondSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated second slip velocity number (F2) may be unreliable."));

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

            double x = -0.09779253871430399 + 0.20461637178584866 * logNl + 1.8685355551760476 * logNl2
                - 0.26538040558679576 * logNl3 - 16.899016734225402 * logNl4 - 41.86824439478302 * logNl5
                - 49.30405393810165 * logNl6 - 32.29555433980717 * logNl7 - 12.002705613816044 * logNl8
                - 2.368986006643564 * logNl9 - 0.19306432300296328 * logNl10;

            return Math.Pow(10, x);

        }

        private double _determineThirdSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated third slip velocity number (F3) may be unreliable."));

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

            double x = 0.596183789607616 - 0.06189582571525779 * logNl - 0.2609535213344842 * logNl2
                + 0.6209715404585027 * logNl3 + 2.573941079218668 * logNl4 + 3.4162364292767164 * logNl5
                + 2.100603185883344 * logNl6 + 0.5057179338218712 * logNl7 - 0.07057382762893977 * logNl8
                - 0.05959975693496161 * logNl9 - 0.008417462424274678 * logNl10;

            return Math.Pow(10, x);

        }

        private double _determineFourthSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated fourth slip velocity number (F4) may be unreliable."));

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;
            double logNl5 = logNl4 * logNl;
            double logNl6 = logNl5 * logNl;

            return 57.984502635323324 + 2.05245663072945 * logNl + 1.8172530701422203 * logNl2
                - 17.45443895077694 * logNl3 - 28.92925210857443 * logNl4 - 12.380682434263594 * logNl5
                - 1.7220558769243466 * logNl6;

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

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Range Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated fifth slip velocity number (F5) may be unreliable."));

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

            double x = -1.0363169525162275 - 1.004404769642721 * logNl - 9.912779180798784 * logNl2
                - 15.895742771558664 * logNl3 + 54.70515128504116 * logNl4 + 285.7518947305839 * logNl5
                + 564.758052879776 * logNl6 + 628.1160498282394 * logNl7 + 431.21993284258303 * logNl8
                + 186.81786848025925 * logNl9 + 49.85321510774732 * logNl10 + 7.494865077345032 * logNl11
                + 0.4863652633065185 * logNl12;

            return Math.Pow(10, x);

        }


        private double _determineSixthSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Rang Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated sixth slip velocity number (F6) may be unreliable."));

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

            return 1.7500846493788171 + 3.37118526235901 * logNl + 25.09587333537104 * logNl2
                + 39.691055078043576 * logNl3 - 143.83675488757757 * logNl4 - 712.8254562746409 * logNl5
                - 1334.728637559747 * logNl6 - 1406.058432928918 * logNl7 - 918.8546588491067 * logNl8
                - 381.7661427954624 * logNl9 - 98.51136170259781 * logNl10 - 14.436857218950813 * logNl11
                - 0.9200040502977727 * logNl12;

        }

        private double _determineSeventhSlipVelocityNumber(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            double Nl = input.LiquidViscosityNumber.Value;

            if (Nl < 2e-3 || Nl > 2)
                validationResult.AddWarning(new ErrorMessage("Out of Rang Warning",
                "Liquid velocity number (Nl) is outside the recommended Duns & Ros range [0.002, 2]. " +
                "The calculated seventh slip velocity number (F7) may be unreliable."));

            double logNl = Math.Log10(Nl);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;

            double x = -1.6055824648455717 - 0.09995069462715704 * logNl + 0.15497397543611768 * logNl2
                 + 0.03259310792620898 * logNl3;

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
            double Nlv = input.LiquidVelocityNumber.Value;

            double x = 1 + F5;
            double y = Math.Pow(Ng, 0.982) + F6c;
            double z = 1 + F7 * Nlv;
            double z2 = z * z;

            return x * (y / z2);


        }

        private double _determineSlipVelocity(VLPWorkingData input,
            double dimensionlessSlipVelocity)
        {

            double liquidDensity = input.LiquidDensity.Value;
            double liquidSurfaceTensionlbsec2 = 
                input.LiquidSurfaceTension.Value * UnitConversionConstants.PoundsPerDyne;


            double x = liquidDensity / (liquidSurfaceTensionlbsec2 * PhysicsConstants.EarthAcceleration);
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

            double x = ((Vm - Vs) * (Vm - Vs)) + 4 * Vs * input.LiquidSuperficialVelocity.Value;
            double y = Math.Pow(x, 0.5);

            return (Vs - Vm + y) / (2 * Vs);

        }

    }
}
