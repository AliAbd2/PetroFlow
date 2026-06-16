using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations
{
    public static class VLPGeneralHoldupCalculator
    {

        static public double CalculateLiquidHoldupByLiquidVolume(double volumeOfLiquid, double volumeOfPipeSegment)
        {

            Validation.IsGreaterThanZero(volumeOfPipeSegment, "Volume of pipe segment");

            Validation.IsNonNegative(volumeOfLiquid, "Volume of liquid");

            Validation.IsGreaterThan(volumeOfPipeSegment, volumeOfLiquid,
                "Volume of pipe segment", "Volume of liquid");

            double liquidHoldup = volumeOfLiquid / volumeOfPipeSegment;

            return liquidHoldup;
        }

        static public double CalculateLiquidHoldupByGasHoldup(double gasHoldup)
        {

            Validation.IsInRange(gasHoldup, 0, 1, "Gas holdup");

            double liquidHoldup = 1 - gasHoldup;

            return liquidHoldup;

        }

        static public double CalculateGasHoldupByGasVolume(double volumeOfGas, double volumeOfPipeSegment)
        {

            Validation.IsNonNegative(volumeOfGas, "Gas volume");

            Validation.IsGreaterThanZero(volumeOfPipeSegment, "Volume of pipe segment");

            Validation.IsGreaterThan(volumeOfPipeSegment, volumeOfGas,
                "Volume of pipe segment", "Volume of gas");

            double gasHoldup = volumeOfGas / volumeOfPipeSegment;

            return gasHoldup;


        }

        static public double CalculateGasHoldupByLiquidHoldup(double liquidHoldup)
        {

            Validation.IsInRange(liquidHoldup, 0, 1, "Liquid holdup");

            return 1 - liquidHoldup;
        }

        static public double CalculateNoSlipLiquidHoldupByLiquidVolume(double liquidFlowrate, double gasFlowrate)
        {

            Validation.IsNonNegative(liquidFlowrate, "Liquid flow rate");

            Validation.IsNonNegative(gasFlowrate, "Gas flow rate");

            double TotalFlowrate = liquidFlowrate + gasFlowrate;

            Validation.IsGreaterThanZero(TotalFlowrate, "Total flow rate");

            return liquidFlowrate / TotalFlowrate;
        }

        static public double CalculateNoSlipLiquidHoldupByVelocity(double superficialLiquidVelocity,
            double superficialGasVelocity)
        {

            double totalVelosity = superficialGasVelocity + superficialLiquidVelocity;

            Validation.IsGreaterThanZero(totalVelosity, "Total superficial velocity");

            return superficialLiquidVelocity / totalVelosity;

        }

        static public double CalculateNoSlipLiquidHoldupByGasHoldup(double noSlipGasHoldup)
        {

            Validation.IsInRange(noSlipGasHoldup, 0, 1, "No slip gas holdup");

            return 1 - noSlipGasHoldup;
        }

        static public double CalculateNoSlipGasHoldupByGasFlowrate(double gasFlowrate, double liquidFlowrate)
        {

            Validation.IsNonNegative(liquidFlowrate, "Liquid flowrate");

            Validation.IsNonNegative(gasFlowrate, "Gas flowrate");

            double TotalFlowrate = liquidFlowrate + gasFlowrate;

            Validation.IsGreaterThanZero(TotalFlowrate, "Total flowrate");

            return gasFlowrate / TotalFlowrate;
        }

        static public double CalculateNoSlipGasHoldupByLiquidHoldup(double noSlipLiquidHoldup)
        {

            Validation.IsInRange(noSlipLiquidHoldup, 0, 1, "No slip liquid holdup");

            return 1 - noSlipLiquidHoldup;
        }
        
        static public double CalculateNoSlipGasHoldupByVelocity(double superficialLiquidVelocity,
            double superficialGasVelocity)
        {


            double totalVelosity = superficialGasVelocity + superficialLiquidVelocity;

            Validation.IsGreaterThanZero(totalVelosity, "Total superficial velocity");

            return superficialGasVelocity / totalVelosity;

        }

    }
}
