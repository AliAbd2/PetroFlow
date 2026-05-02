using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance;
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

        VLPDataInput input = new();
        input.Pressure = 720;
        input.Temperature = 128;
        input.PipeInsideDiameter = 0.249;
        input.GasViscosity = .018;
        input.LiquidViscosity = 18;
        input.GasSuperficialVelocity = 4.09;
        input.LiquidSuperficialVelocity = 2.65;
        input.PipeRelativeRoughness = .0006;
        input.LiquidDensity = 56.6;
        input.GasDensity = 2.84;
        input.LiquidVelocityNumber = 6.02;
        input.GasVelocityNumber = 9.29;
        input.LiquidViscosityNumber = 0.08;
        input.PipeDiameterNumber = 41.34;
        input.GravityAcceleration = PhysicsConstants.EarthAcceleration;


        SlipNoFlowRegimeFrictionFactorCalculator frictionFactorCalculator = new HagedornBrownFrictionFactor();
        SlipNoFlowRegimeHoldupCalculator holdupCalculator = new HagedornBrownHoldupCalculator();

        SlipNoFlowRegime calculator = new SlipNoFlowRegime(frictionFactorCalculator,
            holdupCalculator);

        NodalAnalysisValidationResult validationResult = new();


        double pressureGradient = calculator.DeterminePressureGradient(input, ref validationResult);


        Console.WriteLine(pressureGradient);

    }

}