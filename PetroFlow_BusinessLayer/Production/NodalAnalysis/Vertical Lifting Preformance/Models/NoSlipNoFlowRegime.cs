using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System.Diagnostics;
using System.Text;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethods
{
    public class NoSlipNoFlowRegime : IVLPModel
    {

        public double NoSlipMixtureDensity { get; set; }

        public double TotalMassFlowrate { get; set; }

        public double TubingInsideDiameter { get; set; }

        public double TwoPhaseFirctionFactor { get; set; }

        private readonly NoSlipNoFlowRegimeFrictionFactorCalculator _FrictionFactorCalculator;

        public NoSlipNoFlowRegime(NoSlipNoFlowRegimeFrictionFactorCalculator calculator)
        {
            _FrictionFactorCalculator = calculator;
        }

        public void ValidateInputData(VLPWorkingData inputData, ref NodalAnalysisValidationResult validationResult)
        {


            Validation.Missing(inputData.TotalMassFlowRate, "total mass flow rate");
            Validation.Missing(inputData.PipeInsideDiameter, "pipe inside diameter");
            Validation.Missing(inputData.GasDensity, "gas density");
            Validation.Missing(inputData.LiquidDensity, "liquid density");
            Validation.Missing(inputData.GasSuperficialVelocity, "gas superficial velocity");
            Validation.Missing(inputData.LiquidSuperficialVelocity, "liquid superficial velocity");

        }

        private (double liquidHoldup, double gasHoldup) DetermineNoSlipeHoldups(VLPWorkingData inputData)
        {

            double liquidHoldup, gasHoldup;
            double liquidSuperficialVelocity = inputData.LiquidSuperficialVelocity.Value;
            double gasSupeficialVelocity = inputData.GasSuperficialVelocity.Value;

            liquidHoldup = VLPGeneralHoldupCalculator.
                CalculateNoSlipLiquidHoldupByVelocity(liquidSuperficialVelocity,
                gasSupeficialVelocity);

            gasHoldup = 1 - liquidHoldup;

            return (liquidHoldup, gasHoldup);

        }

        private void DetermineNoSlipDensity(VLPWorkingData inputData)
        {

            (double liquidHoldup, double gasHoldup) noSlipHoldups = DetermineNoSlipeHoldups(inputData);

            double liquidDensity = inputData.LiquidDensity.Value;
            double gasDensity = inputData.GasDensity.Value;
            double liquidHoldup = noSlipHoldups.liquidHoldup;
            double gasHoldup = noSlipHoldups.gasHoldup;


            NoSlipMixtureDensity = VLPMixtureProperties
                .CalculateNoSlipDensity(liquidDensity, liquidHoldup,
                gasDensity, gasHoldup);

        }

        private void DetermineFrictonFactor(VLPWorkingData inputData, ref NodalAnalysisValidationResult validationResult)
        {

            VLPDerivedProperties derivedProperties = new();

            derivedProperties.NoSlipMixtureDensity = NoSlipMixtureDensity;

            TwoPhaseFirctionFactor =
                _FrictionFactorCalculator.CalculateFrictionFactor(
                    inputData, derivedProperties, ref validationResult);
        }

        public double DeterminePressureGradient(VLPWorkingData inputData, ref NodalAnalysisValidationResult validationResult)
        {

            double pressureGradient = 0;

            ValidateInputData(inputData, ref validationResult);

            TubingInsideDiameter = inputData.PipeInsideDiameter.Value;
            TotalMassFlowrate = inputData.TotalMassFlowRate.Value;

            DetermineNoSlipDensity(inputData);

            DetermineFrictonFactor(inputData, ref validationResult);

            double totalMassFlowrate = TotalMassFlowrate * UnitConversionConstants.NumberOfSecDay;

            // Pressure Gradient = pn + (f * w^2) / (2.9652 * 10^11 * p * d^5)

            double x = TwoPhaseFirctionFactor * totalMassFlowrate * totalMassFlowrate;
            double y = 2.9652e11 * NoSlipMixtureDensity * Math.Pow(TubingInsideDiameter, 5);

            pressureGradient = (NoSlipMixtureDensity + x / y) / 
                (UnitConversionConstants.NumberOfInchesFoot * UnitConversionConstants.NumberOfInchesFoot);

            return pressureGradient;

        }

    }
}
