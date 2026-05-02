using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Main_VLP_Classes
{
    internal class VLPRawDataProcessor
    {

        private static double _determineInSituLiquidFlowRate(double LiquidFlowRate,
            double oilFormationVolumeFactor)
        {

            return (LiquidFlowRate * oilFormationVolumeFactor) /
                UnitConversionConstants.NumberOfSecDay; ;

        }

        public static double DetermineLiquidSuperficialVelocity(double liquidFlowRate, double pipeDiameter,
            double oilFormationVolumeFactor)
        {

            double liquidInSituFlowRate = _determineInSituLiquidFlowRate(liquidFlowRate,
                oilFormationVolumeFactor);

            double pipeArea = GeneralMathFunctions.CrircleArea(pipeDiameter);

            return liquidInSituFlowRate / pipeArea;

        }

        private static double _determineInSituGasFlowRate(double gasFlowRate,
            double gasFormationVolumeFactor, double gasLiquidRatio)
        {

            return (gasFlowRate * gasFormationVolumeFactor * gasLiquidRatio) /
                UnitConversionConstants.NumberOfSecDay; ;

        }

        public static double DetermineGasSuperficialVelocity(double gasFlowRate, double pipeDiameter,
            double gasFormationVolumeFactor, double gasLiquidRatio)
        {

            double gasInSituFlowRate = _determineInSituGasFlowRate(gasFlowRate,
                gasFormationVolumeFactor, gasLiquidRatio);

            double pipeArea = GeneralMathFunctions.CrircleArea(pipeDiameter);

            return gasInSituFlowRate / pipeArea;

        }

        public static double DetermineMassFlowRate(VLPWorkingData workingData, double flowRate,
            double oilFormationVolumeFactor, double gasFormationVolumeFactor)
        {

            double GLR = workingData.GasLiquidRatio.Value;
            double liquidDensity = workingData.LiquidDensity.Value;
            double gasDensity = workingData.GasDensity.Value;

            double liquidInSituFlowRate = _determineInSituLiquidFlowRate(flowRate,
                oilFormationVolumeFactor);

            double gasInSituFlowRate = _determineInSituGasFlowRate(flowRate,
                gasFormationVolumeFactor, GLR);

            double noSlipLiquidHoldup = liquidInSituFlowRate / (liquidInSituFlowRate + gasInSituFlowRate);

            double noSlipMixtureDensity = liquidDensity * noSlipLiquidHoldup + gasDensity * (1 - noSlipLiquidHoldup);

            return (flowRate + GLR * flowRate) / (noSlipMixtureDensity * UnitConversionConstants.NumberOfSecDay);

        }

        public static VLPWorkingData PrepareWorkingData(VLPInputData inputData)
        {

            VLPWorkingData workingData = new();

            workingData.GasDensity = inputData.GasDensity;
            workingData.GasLiquidRatio = inputData.GasLiquidRatio;
            workingData.Temperature = inputData.Temperature;
            workingData.GasViscosity = inputData.GasViscosity;
            workingData.LiquidDensity = inputData.liquidDensity;
            workingData.GasViscosity = inputData.GasViscosity;
            workingData.LiquidViscosity = inputData.LiquidViscosity;
            workingData.PipeInsideDiameter = inputData.PipeInsideDiameter;
            workingData.LiquidSurfaceTension = inputData.SurfaceTension;
            workingData.GravityAcceleration = inputData.GravityAcceleration;
            workingData.PipeRelativeRoughness = inputData.PipeRelativeRoughness;


            return workingData;

        }

    }
}
