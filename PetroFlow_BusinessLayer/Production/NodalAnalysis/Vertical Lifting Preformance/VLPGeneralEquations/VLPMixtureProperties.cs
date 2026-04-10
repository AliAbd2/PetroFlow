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

            Validation.GreaterThanZero(gasCompressibilityFactor, "Gas compressibility factor (Z-Factor)");

            Validation.GreaterThanZero(temperature, "Temperature");

            Validation.GreaterThanZero(pressure, "Pressure");

            Validation.GreaterThanZero(gasGravity, "Gas gravity");

            return (2.7 * pressure * gasGravity) / (gasCompressibilityFactor * temperature);

        }

        static public double CalculateOilDensity(double oilGravity, double gasGravity, double gasOilRatio,
            double oilFormationVolumeFactor)
        {

            Validation.GreaterThanZero(oilGravity, "Oil gravity");

            Validation.NonNegative(gasGravity, "Gas gravity");

            Validation.NonNegative(gasOilRatio, "Gas oil ratio");

            Validation.GreaterThanZero(oilFormationVolumeFactor, "Oil formation volume factor");

            return (350.4 * oilGravity + 0.0764 * gasGravity * gasOilRatio) / (5.615 * oilFormationVolumeFactor);

        }

        public static double CalculateNoSlipLiquidDensity(double oilDensity, double oilFraction, double waterDensity,
            double waterFraction)
        {

            Validation.NonNegative(oilDensity, "Oil density");
            Validation.NonNegative(waterDensity, "Water density");
            Validation.Range(oilFraction, 0, 1, "Oil fraction");
            Validation.Range(waterFraction, 0, 1, "Oil fraction");


            return oilDensity * oilFraction + waterDensity * waterFraction;
        }

        public static double CalculateSlipDensity(double liquidDensity, double liquidholdup,
            double gasDensity, double gasholdup)
        {

            Validation.NonNegative(liquidDensity, "Liquid density");
            Validation.NonNegative(gasDensity, "Gas density");
            Validation.Range(liquidholdup, 0, 1, "Liquid holdup");
            Validation.Range(gasholdup, 0, 1, "Gas holdup");

            return liquidDensity * liquidholdup + gasDensity * gasholdup;
        }

        public static double CalculateNoSlipDensity(double liquidDensity, double noSlipLiquidholdup,
            double gasDensity, double noSlipGasholdup)
        {

            Validation.NonNegative(liquidDensity, "Liquid density");

            Validation.NonNegative(gasDensity, "Gas density");

            Validation.Range(noSlipGasholdup, 0, 1, "No slip gas holdup");

            Validation.Range(noSlipLiquidholdup, 0, 1, "No slip liquid holdup");

            return liquidDensity * noSlipLiquidholdup + gasDensity * noSlipGasholdup;
        }

        public static double CalculateFrictionLossDensity(double liquidDensity, double noSlipLiquidHoldup,
            double liquidHoldup, double gasDensity, double noSlipGasHoldup, double gasHoldup)
        {

            Validation.GreaterThanZero(liquidHoldup, "Liquid holdup");

            Validation.GreaterThanZero(gasHoldup, "Gas holdup");

            Validation.NonNegative(liquidDensity, "Liquid density");

            Validation.NonNegative(gasDensity, "Gas density");

            Validation.Range(liquidHoldup, 0, 1, "Liquid holdup");

            Validation.Range(gasHoldup, 0, 1, "Gas holdup");

            Validation.Range(noSlipLiquidHoldup, 0, 1, "No slip liquid holdup");

            Validation.Range(noSlipGasHoldup, 0, 1, "No slip gas holdup");

            return (liquidDensity * noSlipLiquidHoldup * noSlipLiquidHoldup) / liquidHoldup +
                (gasDensity * noSlipGasHoldup * noSlipGasHoldup) / gasHoldup;
        }


        //===================
        // --- Fraction ---
        //===================

        public static double CalculateOilFraction(double oilFlowrate, double waterFlowrate)
        {

            Validation.NonNegative(oilFlowrate, "Oil flow rate");

            Validation.NonNegative(waterFlowrate, "Water flow rate");

            double TotalFlowrate = oilFlowrate + waterFlowrate;

            Validation.GreaterThanZero(TotalFlowrate, "Total Flow rate");

            return oilFlowrate / (oilFlowrate + waterFlowrate);
        }

        public static double CalculateOilFraction(double waterOilRatio, double oilFormationVolumeFactor,
            double waterFormationVolumeFactor)
        {

            Validation.NonNegative(waterOilRatio, "Water oil ratio");

            Validation.NonNegative(waterFormationVolumeFactor, "Water formation volume factor");

            Validation.GreaterThanZero(oilFormationVolumeFactor, "Oil formation volume factor");

            return 1 / (1 + waterOilRatio * (waterFormationVolumeFactor / oilFormationVolumeFactor));
        }

        public static double CalculateOilFractionByWaterFraction(double waterFraction)
        {

            Validation.Range(waterFraction, 0, 1, "Water fraction");

            return 1 - waterFraction;
        }

        public static double CalculateWaterFractionByWaterFlowrate(double waterFlowrate, double oilFlowrate)
        {

            Validation.NonNegative(waterFlowrate, "Water flow rate");

            Validation.NonNegative(oilFlowrate, "Oil flow rate");

            double TotalFlowrate = oilFlowrate + waterFlowrate;

            Validation.GreaterThanZero(TotalFlowrate, "Total Flow rate");

            return waterFlowrate / (oilFlowrate + waterFlowrate);
        }

        public static double CalculateWaterFraction(double oilFraction)
        {

            Validation.Range(oilFraction, 0, 1, "Oil fraction");

            return 1 - oilFraction;
        }

        //================================
        // --- Liquid Surface Tension ---
        //================================

        public static double CalculateLiquidSurfaceTension(double oilSurfaceTension, double waterSurfaceTension,
            double oilFraction, double waterFraction)
        {

            Validation.Range(oilFraction, 0, 1, "Oil fraction");
            Validation.Range(waterFraction, 0, 1, "Water fraction");
            Validation.SumApproximatelyOne(oilFraction, waterFraction, 
                "Oil fraction", "Water fraction");


            return oilSurfaceTension * oilFraction + waterSurfaceTension * waterFraction;

        }


    }
}
