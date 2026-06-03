using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Models;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT;
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

        NodalAnalysisValidationResult validationResult = new();

        DunsRos dunsRos = new();

        IPRInputData IPRinput = new();
        IPRinput.ReservoirPressure = 4000;
        IPRinput.BubblePointPressure = 2000;
        IPRinput.TestsData.Add(new InFlowDataRow(3000, 200));

        IPRGenerationSettings generationSettings = new(5, 1);
        IPRinput.GenerationSettings = generationSettings;

        Vogel vogel = new();

        InFlowPerformanceRelationship IPR = new(vogel, IPRinput);

        List<InFlowDataRow> IPRDataRows = IPR.GenerateIPR(ref validationResult);

        VLPInputData input = new();
        input.WellHeadPressure = 240;//
        input.PVT.FahrenheitTemperature = 200;
        input.GasViscosity = .018;
        input.LiquidViscosity = 18;
        input.TotalDepth = 6000;//
        input.PipeInsideDiameter = .2;//
        input.DepthStepSize = 100;
        input.FlowRateStepSize = 1;
        input.SurfaceTension = 30;
        input.PipeRelativeRoughness = 0.0006;//
        input.GravityAcceleration = PhysicsConstants.EarthAcceleration;
        input.MinimumPressure = 10;
        input.PVT.GasOilRatio = 800;
        input.PVT.GasSpecificGravity = 0.65;
        input.PVT.API = 25;
        input.PVT.PSIBubblePointPressure = 1500;
        input.MinimumFlowRate = 10;

        NoSlipNoFlowRegime PoettmannCarpeter = new(new PoettmannCarpenterFrictionFactor());
        NoSlipNoFlowRegime BaxendellThomas = new(new BaxendellThomasFrictionFactor());
        NoSlipNoFlowRegime FancherBrown = new(new FancherBrownFrictionFactor());

        SlipNoFlowRegime HagedornBrown = new(new HagedornBrownFrictionFactor(),
            new HagedornBrownHoldupCalculator());


        VerticalLiftingPreformance verticalLiftingPreformance = new(dunsRos, input);

        List<OutFlowDataRow> VLPdataRows = verticalLiftingPreformance.GenerateOutFlow(1000,
            ref validationResult);

        Console.WriteLine($"Qo      |       IPR      |       VLP");

        //for (int i = 0; i < VLPdataRows.Count; i++)
        //{

        //    Console.WriteLine($"{Math.Round(IPRDataRows[i].FlowRate)}       |" +
        //        $"       {Math.Round(IPRDataRows[i].BottomHolePressure)}" +
        //        $"      |       {Math.Round(VLPdataRows[i].Pressure)}");

        //}

        foreach(OutFlowDataRow row in VLPdataRows)
        {

            Console.WriteLine($"{Math.Round(row.FlowRate)}      |       {Math.Round(row.Pressure)}");

        }

        foreach (string warning in validationResult.Warnings)
        {

            Console.WriteLine(warning);

        }

        //VLPWorkingData Working = new();
        //Working.GasSuperficialVelocity = 4.09;
        //Working.LiquidSuperficialVelocity = 2.65;
        //Working.PipeInsideDiameter = .249;
        //Working.LiquidVelocityNumber = 6.02;
        //Working.GasVelocityNumber = 9.29;
        //Working.PipeRelativeRoughness = 0.0006;
        //Working.PipeDiameterNumber = 41.34;
        //Working.LiquidSurfaceTension = 30;
        //Working.Pressure = 720;
        //Working.LiquidViscosity = 18;
        //Working.GasViscosity = 0.018;
        //Working.LiquidDensity = 56.6;
        //Working.GasDensity = 2.84;
        //Working.LiquidViscosityNumber = 0.08;
        //Working.GravityAcceleration = 32.2;

        //double x = dunsRos.DeterminePressureGradient(Working, ref validationResult);

        //Console.WriteLine(x);



    }

}