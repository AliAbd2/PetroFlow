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

            Validation.IsNonNegative(gasFlowRate, "Gas flow rate");

            Validation.IsGreaterThanZero(pipeArea, "Pipe area");

            return gasFlowRate / pipeArea;

        }

        public static double CalculateActualGasVelocity(double gasFlowRate, double pipeArea,
            double gasHoldup)
        {

            Validation.IsNonNegative(gasFlowRate, "Gas flow rate");

            Validation.IsGreaterThanZero(pipeArea, "Pipe area");

            Validation.IsGreaterThanZero(gasHoldup, "Gas holdup");

            Validation.IsInRange(gasHoldup, 0, 1, "Gas holdup");

            return gasFlowRate / (pipeArea * gasHoldup);

        }

        //=========================
        // --- Liquid Velocity ---
        //=========================
        public static double CalculateLiquidSuperficialVelocity(double liquidFlowRate, double pipeArea)
        {

            Validation.IsNonNegative(liquidFlowRate, "Liquid flow rate");

            Validation.IsGreaterThanZero(pipeArea, "Pipe area");

            return liquidFlowRate / pipeArea;

        }

        public static double CalculateActualLiquidVelocity(double liquidFlowRate, double pipeArea,
            double liquidHoldup)
        {

            Validation.IsNonNegative(liquidFlowRate, "Liquid flow rate");

            Validation.IsGreaterThanZero(pipeArea, "Pipe area");

            Validation.IsGreaterThanZero(liquidHoldup, "Liquid holdup");

            Validation.IsInRange(liquidHoldup, 0, 1, "Liquid holdup");

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

            Validation.IsInRange(gasHoldup, 0, 1, "Gas holdup");
            Validation.IsInRange(liquidHoldup, 0, 1, "Liquid holdup");
            Validation.IsGreaterThanZero(gasHoldup, "Gas holdup");
            Validation.IsGreaterThanZero(liquidHoldup, "Liquid holdup");

            return (gasSuperficialVelocity / gasHoldup) - (liquidSuperficialVelocity / liquidHoldup);

        }


        //==========================
        // --- Liquid Viscosity ---
        //==========================

        public static double CalculateLiquidViscosity(double oilViscosity, double waterViscosity,
            double oilFraction, double waterFraction)
        {

            Validation.IsGreaterThanZero(oilViscosity, "Oil viscosity");
            Validation.IsGreaterThanZero(waterViscosity, "Water viscosity");
            Validation.IsInRange(oilFraction, 0, 1, "Oil fraction");
            Validation.IsInRange(waterFraction, 0, 1, "Water fraction");

            return oilViscosity * oilFraction + waterViscosity * waterFraction;

        }

        //=============================
        // --- Two-Phase Viscosity ---
        //=============================

        public static double CalculateNoSlipMixtureViscosity(double liquidViscosity, double gasViscosity,
            double liquidNoSlipHoldup, double gasNoSlipHoldup)
        {

            Validation.IsGreaterThanZero(liquidViscosity, "Liquid viscosity");
            Validation.IsGreaterThanZero(gasViscosity, "Gas viscosity");
            Validation.IsInRange(liquidNoSlipHoldup, 0, 1, "Liquid no slip holdup");
            Validation.IsInRange(gasNoSlipHoldup, 0, 1, "Gas no slip holdup");

            return liquidViscosity * liquidNoSlipHoldup + gasViscosity * gasNoSlipHoldup;

        }


        public static double CalculateMixtureViscosity(double liquidViscosity, double gasViscosity,
            double liquidHoldup, double gasHoldup)
        {

            Validation.IsGreaterThanZero(liquidViscosity, "Liquid viscosity");
            Validation.IsGreaterThanZero(gasViscosity, "Gas viscosity");
            Validation.IsInRange(liquidHoldup, 0, 1, "Liquid holdup");
            Validation.IsInRange(gasHoldup, 0, 1, "Gas holdup");


            return Math.Pow(liquidViscosity, liquidHoldup) * Math.Pow(gasViscosity, gasHoldup);

        }

    }
}
