using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Main_VLP_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Models;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance
{
    public class VerticalLiftingPreformance
    {

        private readonly IVLPModel _model;


        public VerticalLiftingPreformance(IVLPModel model)
        {

            _model = model;

        }

        private double _determinePressureAtFlowRate(VLPInputData input,  VLPWorkingData workingData, double flowRate,
            ref NodalAnalysisValidationResult validationResult)
        {

            if (input == null)
                throw new InvalidOperationException("The VLP input data has not been provided.");

            workingData.PVT.PSIPressure = input.WellHeadPressure;
            double pipeIsindeDiameter = workingData.PipeInsideDiameter.Value;
            double gasLiquidRatio = input.PVT.GasOilRatio.Value;


            for (double depth = 0; depth < input.TotalDepth.Value; 
                depth += input.DepthStepSize.Value)
            {

                double gasFormationVolumeFactor =
                VerticalLiftingPreformancePVT.GasFormationVolumeFactorInBurrel(workingData.PVT);
                double oilFormationVolumeFactor =
                    VerticalLiftingPreformancePVT.OilFormationVolumeFactorByVasquezBeggs(workingData.PVT);

                workingData.LiquidDensity =
                    VerticalLiftingPreformancePVT.OilDensity(workingData.PVT);

                workingData.GasDensity =
                    VerticalLiftingPreformancePVT.GasDensity(workingData.PVT);

                double Rs = VerticalLiftingPreformancePVT.SolutionGasOilRatioByVasquezBeggs(workingData.PVT);

                workingData.GasSuperficialVelocity =
                    VLPRawDataProcessor.DetermineGasSuperficialVelocity(flowRate,
                    pipeIsindeDiameter, gasFormationVolumeFactor, gasLiquidRatio, Rs);

                workingData.LiquidSuperficialVelocity =
                    VLPRawDataProcessor.DetermineLiquidSuperficialVelocity(flowRate,
                    pipeIsindeDiameter, oilFormationVolumeFactor);

                workingData.TotalMassFlowRate = VLPRawDataProcessor.DetermineMassFlowRate(workingData,
                    flowRate, oilFormationVolumeFactor, gasFormationVolumeFactor);


                workingData.LiquidVelocityNumber =
                    VLPDimensionlessNumbers.DetermineLiquidVelocityNumber(workingData);

                workingData.GasVelocityNumber =
                    VLPDimensionlessNumbers.DetermineGasVelocityNumber(workingData);

                workingData.LiquidViscosityNumber =
                    VLPDimensionlessNumbers.DetermineViscosityNumber(workingData);

                
                workingData.PipeDiameterNumber =
                    VLPDimensionlessNumbers.DeterminePipeDiameterNumber(workingData);

                double pressureGradient = _model.DeterminePressureGradient(workingData,
                    ref validationResult);

                workingData.PVT.PSIPressure += pressureGradient * input.DepthStepSize.Value;

            }

            return workingData.PVT.PSIPressure.Value;

        }

        public List<FlowDataRow> GenerateOutFlow(VLPInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            if (input == null)
                throw new InvalidOperationException("The VLP input data has not been provided.");

            List<FlowDataRow> outFlowData = new();

            VLPWorkingData workingData = VLPRawDataProcessor.PrepareWorkingData(input);

            double pressure = 0;

            double flowRateStepSize = input.FlowRateStepSize.Value;


            for (double flowRate = input.MinimumFlowRate.Value; flowRate < input.MaxFlowRate.Value;
                flowRate += flowRateStepSize)
            {


                pressure = _determinePressureAtFlowRate(input, workingData, flowRate,
                    ref validationResult);

                outFlowData.Add(new FlowDataRow(pressure, flowRate));

            }

            return outFlowData;

        }

        public List<FlowDataRow> GenerateOutFlowForInFlow(VLPInputData input, List<FlowDataRow> inFlowData,
            ref NodalAnalysisValidationResult validationResult)
        {

            if (input == null)
                throw new InvalidOperationException("The VLP input data has not been provided.");

            List<FlowDataRow> outFlowData = new();

            VLPWorkingData workingData = VLPRawDataProcessor.PrepareWorkingData(input);

            double pressure = 0;

            double flowRateStepSize = input.FlowRateStepSize.Value;


            foreach (double flowRate in inFlowData.Select(x => x.FlowRate))
            {

                pressure = _determinePressureAtFlowRate(input, workingData, flowRate,
                    ref validationResult);

                outFlowData.Add(new FlowDataRow(pressure, flowRate));

            }

            return outFlowData;

        }



    }
}
