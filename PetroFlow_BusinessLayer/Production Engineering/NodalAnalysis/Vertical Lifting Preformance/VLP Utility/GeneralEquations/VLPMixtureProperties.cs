using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations
{
    public static class VLPMixtureProperties
    {

        //==================
        // --- Density ---
        //==================

        static public double CalculateGasDensityByGasLaw(double pressure, double gasGravity,
            double gasCompressibilityFactor, double temperature)
        {

            Validation.IsGreaterThanZero(gasCompressibilityFactor, "Gas compressibility factor (Z-Factor)");

            Validation.IsGreaterThanZero(temperature, "Temperature");

            Validation.IsGreaterThanZero(pressure, "Pressure");

            Validation.IsGreaterThanZero(gasGravity, "Gas gravity");

            return (2.7 * pressure * gasGravity) / (gasCompressibilityFactor * temperature);

        }

        static public double CalculateOilDensity(double oilGravity, double gasGravity, double gasOilRatio,
            double oilFormationVolumeFactor)
        {

            Validation.IsGreaterThanZero(oilGravity, "Oil gravity");

            Validation.IsNonNegative(gasGravity, "Gas gravity");

            Validation.IsNonNegative(gasOilRatio, "Gas oil ratio");

            Validation.IsGreaterThanZero(oilFormationVolumeFactor, "Oil formation volume factor");

            return (350.4 * oilGravity + 0.0764 * gasGravity * gasOilRatio) / (5.615 * oilFormationVolumeFactor);

        }

        public static double CalculateNoSlipLiquidDensity(double oilDensity, double oilFraction, double waterDensity,
            double waterFraction)
        {

            Validation.IsNonNegative(oilDensity, "Oil density");
            Validation.IsNonNegative(waterDensity, "Water density");
            Validation.IsInRange(oilFraction, 0, 1, "Oil fraction");
            Validation.IsInRange(waterFraction, 0, 1, "Oil fraction");


            return oilDensity * oilFraction + waterDensity * waterFraction;
        }

        public static double CalculateSlipDensity(double liquidDensity, double liquidholdup,
            double gasDensity, double gasholdup)
        {

            Validation.IsNonNegative(liquidDensity, "Liquid density");
            Validation.IsNonNegative(gasDensity, "Gas density");
            Validation.IsInRange(liquidholdup, 0, 1, "Liquid holdup");
            Validation.IsInRange(gasholdup, 0, 1, "Gas holdup");

            return liquidDensity * liquidholdup + gasDensity * gasholdup;
        }

        public static double CalculateNoSlipDensity(double liquidDensity, double noSlipLiquidholdup,
            double gasDensity, double noSlipGasholdup)
        {

            Validation.IsNonNegative(liquidDensity, "Liquid density");

            Validation.IsNonNegative(gasDensity, "Gas density");

            Validation.IsInRange(noSlipGasholdup, 0, 1, "No slip gas holdup");

            Validation.IsInRange(noSlipLiquidholdup, 0, 1, "No slip liquid holdup");

            return liquidDensity * noSlipLiquidholdup + gasDensity * noSlipGasholdup;
        }

        public static double CalculateFrictionLossDensity(double liquidDensity, double noSlipLiquidHoldup,
            double liquidHoldup, double gasDensity, double noSlipGasHoldup, double gasHoldup)
        {

            Validation.IsGreaterThanZero(liquidHoldup, "Liquid holdup");

            Validation.IsGreaterThanZero(gasHoldup, "Gas holdup");

            Validation.IsNonNegative(liquidDensity, "Liquid density");

            Validation.IsNonNegative(gasDensity, "Gas density");

            Validation.IsInRange(liquidHoldup, 0, 1, "Liquid holdup");

            Validation.IsInRange(gasHoldup, 0, 1, "Gas holdup");

            Validation.IsInRange(noSlipLiquidHoldup, 0, 1, "No slip liquid holdup");

            Validation.IsInRange(noSlipGasHoldup, 0, 1, "No slip gas holdup");

            return (liquidDensity * noSlipLiquidHoldup * noSlipLiquidHoldup) / liquidHoldup +
                (gasDensity * noSlipGasHoldup * noSlipGasHoldup) / gasHoldup;
        }


        //===================
        // --- Fraction ---
        //===================

        public static double CalculateOilFraction(double oilFlowrate, double waterFlowrate)
        {

            Validation.IsNonNegative(oilFlowrate, "Oil flow rate");

            Validation.IsNonNegative(waterFlowrate, "Water flow rate");

            double TotalFlowrate = oilFlowrate + waterFlowrate;

            Validation.IsGreaterThanZero(TotalFlowrate, "Total Flow rate");

            return oilFlowrate / (oilFlowrate + waterFlowrate);
        }

        public static double CalculateOilFraction(double waterOilRatio, double oilFormationVolumeFactor,
            double waterFormationVolumeFactor)
        {

            Validation.IsNonNegative(waterOilRatio, "Water oil ratio");

            Validation.IsNonNegative(waterFormationVolumeFactor, "Water formation volume factor");

            Validation.IsGreaterThanZero(oilFormationVolumeFactor, "Oil formation volume factor");

            return 1 / (1 + waterOilRatio * (waterFormationVolumeFactor / oilFormationVolumeFactor));
        }

        public static double CalculateOilFractionByWaterFraction(double waterFraction)
        {

            Validation.IsInRange(waterFraction, 0, 1, "Water fraction");

            return 1 - waterFraction;
        }

        public static double CalculateWaterFractionByWaterFlowrate(double waterFlowrate, double oilFlowrate)
        {

            Validation.IsNonNegative(waterFlowrate, "Water flow rate");

            Validation.IsNonNegative(oilFlowrate, "Oil flow rate");

            double TotalFlowrate = oilFlowrate + waterFlowrate;

            Validation.IsGreaterThanZero(TotalFlowrate, "Total Flow rate");

            return waterFlowrate / (oilFlowrate + waterFlowrate);
        }

        public static double CalculateWaterFraction(double oilFraction)
        {

            Validation.IsInRange(oilFraction, 0, 1, "Oil fraction");

            return 1 - oilFraction;
        }

        //================================
        // --- Liquid Surface Tension ---
        //================================

        public static double CalculateLiquidSurfaceTension(double oilSurfaceTension, double waterSurfaceTension,
            double oilFraction, double waterFraction)
        {

            Validation.IsInRange(oilFraction, 0, 1, "Oil fraction");
            Validation.IsInRange(waterFraction, 0, 1, "Water fraction");



            return oilSurfaceTension * oilFraction + waterSurfaceTension * waterFraction;

        }


    }
}
