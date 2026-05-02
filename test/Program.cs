using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Models;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPHoldup;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;

class Program
{

    static void Main(string[] args)
    {

        VLPInputData input = new();
        input.WellHeadPressure = 240;
        input.Temperature = 128;
        input.PipeInsideDiameter = 0.249;
        input.GasViscosity = .018;
        input.LiquidViscosity = 18;
        input.TotalDepth = 8000;
        input.GasLiquidRatio = 500;
        input.GasDensity = 2.84;
        input.liquidDensity = 56.6;
        input.PipeInsideDiameter = .249;
        input.DepthStepSize = 10;
        input.GasFormationVolumeFactor = .6;
        input.OilFormationVolumeFactor = 1.25;
        input.FlowRateStepSize = 100;
        input.SurfaceTension = 30;
        input.PipeRelativeRoughness = 0.0006;
        input.GravityAcceleration = PhysicsConstants.EarthAcceleration;
        input.MinimumPressure = 100;

        NoSlipNoFlowRegime PoettmannCarpeter = new(new PoettmannCarpenterFrictionFactor());
        NoSlipNoFlowRegime BaxendellThomas = new(new BaxendellThomasFrictionFactor());
        NoSlipNoFlowRegime FancherBrown = new(new FancherBrownFrictionFactor());

        SlipNoFlowRegime HagedornBrown = new(new HagedornBrownFrictionFactor(),
            new HagedornBrownHoldupCalculator());

        DunsRos dunsRos = new();

        VerticalLiftingPreformance verticalLiftingPreformance = new(dunsRos, input);

        NodalAnalysisValidationResult validationResult = new();

        List<OutFlowDataRow> dataRows = verticalLiftingPreformance.GenerateOutFlow(4000, 
            ref validationResult);



        foreach (OutFlowDataRow  row in dataRows)
        {


            Console.WriteLine(Math.Round(row.Pressure) + "   " + row.FlowRate);

        }


        foreach (string warning in validationResult.Warnings)
        {

            Console.WriteLine(warning);

        }

    }

}