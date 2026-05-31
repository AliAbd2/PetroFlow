using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
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

        private readonly VLPInputData _inputData;

        public VerticalLiftingPreformance(IVLPModel model, VLPInputData inputData)
        {

            _model = model;
            _inputData = inputData;

        }

        private double _determinePressureAtFlowRate(VLPWorkingData workingData, double flowRate,
            ref NodalAnalysisValidationResult validationResult)
        {

            workingData.PVT.PSIPressure = _inputData.WellHeadPressure;
            double pipeIsindeDiameter = workingData.PipeInsideDiameter.Value;
            double gasLiquidRatio = _inputData.PVT.GasOilRatio.Value;


            for (double depth = 0; depth < _inputData.TotalDepth.Value; 
                depth +=_inputData.DepthStepSize.Value)
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

                workingData.PVT.PSIPressure += pressureGradient * _inputData.DepthStepSize.Value;

            }

            return workingData.PVT.PSIPressure.Value;

        }

        public List<OutFlowDataRow> GenerateOutFlow(double maxFlowRate, 
            ref NodalAnalysisValidationResult validationResult)
        {

            List<OutFlowDataRow> outFlowData = new();

            VLPWorkingData workingData = VLPRawDataProcessor.PrepareWorkingData(_inputData);

            double pressure = 0;

            double flowRateStepSize = _inputData.FlowRateStepSize.Value;


            for (double flowRate = _inputData.MinimumFlowRate.Value; flowRate < maxFlowRate;
                flowRate += flowRateStepSize)
            {


                pressure = _determinePressureAtFlowRate(workingData, flowRate,
                    ref validationResult);

                outFlowData.Add(new OutFlowDataRow(pressure, flowRate));

            }

            return outFlowData;

        }


        public List<OutFlowDataRow> GenerateOutFlowForInFlow(List<InFlowDataRow> inFlowData,
            ref NodalAnalysisValidationResult validationResult)
        {

            List<OutFlowDataRow> outFlowData = new();

            VLPWorkingData workingData = VLPRawDataProcessor.PrepareWorkingData(_inputData);

            double pressure = 0;

            double flowRateStepSize = _inputData.FlowRateStepSize.Value;


            foreach (double flowRate in inFlowData.Select(x => x.FlowRate))
            {

                pressure = _determinePressureAtFlowRate(workingData, flowRate,
                    ref validationResult);

                outFlowData.Add(new OutFlowDataRow(pressure, flowRate));

            }

            return outFlowData;

        }



    }
}
