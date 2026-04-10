using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations
{
    public static class VLPGeneralFlowEquations
    {

        //======================
        // --- Gas Velocity ---
        //======================

        public static double CalculateGasSuperficialVelocity(double gasFlowRate, double pipeArea)
        {

            Validation.NonNegative(gasFlowRate, "Gas flow rate");

            Validation.GreaterThanZero(pipeArea, "Pipe area");

            return gasFlowRate / pipeArea;

        }

        public static double CalculateActualGasVelocity(double gasFlowRate, double pipeArea,
            double gasHoldup)
        {

            Validation.NonNegative(gasFlowRate, "Gas flow rate");

            Validation.GreaterThanZero(pipeArea, "Pipe area");

            Validation.GreaterThanZero(gasHoldup, "Gas holdup");

            Validation.Range(gasHoldup, 0, 1, "Gas holdup");

            return gasFlowRate / (pipeArea * gasHoldup);

        }

        //=========================
        // --- Liquid Velocity ---
        //=========================
        public static double CalculateLiquidSuperficialVelocity(double liquidFlowRate, double pipeArea)
        {

            Validation.NonNegative(liquidFlowRate, "Liquid flow rate");

            Validation.GreaterThanZero(pipeArea, "Pipe area");

            return liquidFlowRate / pipeArea;

        }

        public static double CalculateActualLiquidVelocity(double liquidFlowRate, double pipeArea,
            double liquidHoldup)
        {

            Validation.NonNegative(liquidFlowRate, "Liquid flow rate");

            Validation.GreaterThanZero(pipeArea, "Pipe area");

            Validation.GreaterThanZero(liquidHoldup, "Liquid holdup");

            Validation.Range(liquidHoldup, 0, 1, "Liquid holdup");

            return liquidFlowRate / (pipeArea * liquidHoldup);

        }


        //============================
        // --- Two-Phase Velocity ---
        //============================

        public static double CalculateMixtureSuperficialVelocity(double liquidSuperficialVelocity, 
            double gasSuperficialVelocity)
        {

            return liquidSuperficialVelocity + gasSuperficialVelocity;

        }

        public static double CalculateSlipVelocityByActualVelocities(double gasActualVelocity, double liquidActualVelocity)
        {

            return gasActualVelocity - liquidActualVelocity;

        }

        public static double CalculateSlipVelocityBySuperficialVelocity(double gasSuperficialVelocity,
            double liquidSuperficialVelocity, double gasHoldup, double liquidHoldup)
        {

            Validation.Range(gasHoldup, 0, 1, "Gas holdup");
            Validation.Range(liquidHoldup, 0, 1, "Liquid holdup");
            Validation.GreaterThanZero(gasHoldup, "Gas holdup");
            Validation.GreaterThanZero(liquidHoldup, "Liquid holdup");
            Validation.SumApproximatelyOne(liquidHoldup, gasHoldup,
                "Liquid holdup", "Gas holdup");

            return (gasSuperficialVelocity / gasHoldup) - (liquidSuperficialVelocity / liquidHoldup);

        }


        //==========================
        // --- Liquid Viscosity ---
        //==========================

        public static double CalculateLiquidViscosity(double oilViscosity, double waterViscosity,
            double oilFraction, double waterFraction)
        {

            Validation.GreaterThanZero(oilViscosity, "Oil viscosity");
            Validation.GreaterThanZero(waterViscosity, "Water viscosity");
            Validation.Range(oilFraction, 0, 1, "Oil fraction");
            Validation.Range(waterFraction, 0, 1, "Water fraction");
            Validation.SumApproximatelyOne(oilFraction, waterFraction,
                "Oil fraction", "Water fraction");

            return oilViscosity * oilFraction + waterViscosity * waterFraction;

        }

        //=============================
        // --- Two-Phase Viscosity ---
        //=============================

        public static double CalculateNoSlipMixtureViscosity(double liquidViscosity, double gasViscosity,
            double liquidNoSlipHoldup, double gasNoSlipHoldup)
        {

            Validation.GreaterThanZero(liquidViscosity, "Liquid viscosity");
            Validation.GreaterThanZero(gasViscosity, "Gas viscosity");
            Validation.Range(liquidNoSlipHoldup, 0, 1, "Liquid no slip holdup");
            Validation.Range(gasNoSlipHoldup, 0, 1, "Gas no slip holdup");
            Validation.SumApproximatelyOne(liquidNoSlipHoldup, gasNoSlipHoldup,
                "Liquid no slip holdup", "Gas no slip holdup");

            return liquidViscosity * liquidNoSlipHoldup + gasViscosity * gasNoSlipHoldup;

        }


        public static double CalculateMixtureViscosity(double liquidViscosity, double gasViscosity,
            double liquidHoldup, double gasHoldup)
        {

            Validation.GreaterThanZero(liquidViscosity, "Liquid viscosity");
            Validation.GreaterThanZero(gasViscosity, "Gas viscosity");
            Validation.Range(liquidHoldup, 0, 1, "Liquid holdup");
            Validation.Range(gasHoldup, 0, 1, "Gas holdup");
            Validation.SumApproximatelyOne(liquidHoldup, gasHoldup,
                "Liquid holdup", "Gas holdup");


            return Math.Pow(liquidViscosity, liquidHoldup) * Math.Pow(gasViscosity, gasHoldup);

        }

    }
}
