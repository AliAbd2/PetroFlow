using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT;
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

        private static double _determineInSituGasFlowRate(double oilFlowRate,
            double gasFormationVolumeFactor, double gasLiquidRatio, double solutionGasOilRatio)
        {


            return oilFlowRate * gasFormationVolumeFactor
                * (gasLiquidRatio - solutionGasOilRatio) /
                UnitConversionConstants.NumberOfSecDay;

        }

        public static double DetermineGasSuperficialVelocity(double gasFlowRate, double pipeDiameter,
            double gasFormationVolumeFactor, double gasLiquidRatio, double solutionGasOilRatio)
        {

            if (solutionGasOilRatio >= gasLiquidRatio)
                return 0;

            double gasInSituFlowRate = _determineInSituGasFlowRate(gasFlowRate,
                gasFormationVolumeFactor, gasLiquidRatio, solutionGasOilRatio);

            double pipeArea = GeneralMathFunctions.CrircleArea(pipeDiameter);

            return gasInSituFlowRate / pipeArea;

        }

        public static double DetermineMassFlowRate(VLPWorkingData workingData, double flowRate,
            double oilFormationVolumeFactor, double gasFormationVolumeFactor)
        {

            double GLR = workingData.PVT.GasOilRatio.Value;
            double liquidDensity = workingData.LiquidDensity.Value;
            double Rs = VerticalLiftingPreformancePVT.SolutionGasOilRatioByVasquezBeggs(workingData.PVT);
            double gasDensity = workingData.GasDensity.Value;

            double liquidInSituFlowRate = _determineInSituLiquidFlowRate(flowRate,
                oilFormationVolumeFactor);

            double gasInSituFlowRate = _determineInSituGasFlowRate(flowRate,
                gasFormationVolumeFactor, GLR, Rs);

            double noSlipLiquidHoldup = liquidInSituFlowRate / (liquidInSituFlowRate + gasInSituFlowRate);

            double noSlipMixtureDensity = liquidDensity * noSlipLiquidHoldup + gasDensity * (1 - noSlipLiquidHoldup);

            return (flowRate + GLR * flowRate) / (noSlipMixtureDensity * UnitConversionConstants.NumberOfSecDay);

        }

        public static VLPWorkingData PrepareWorkingData(VLPInputData inputData)
        {

            VLPWorkingData workingData = new();

            workingData.PVT = inputData.PVT;
            workingData.GasViscosity = inputData.GasViscosity;
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
