using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethod.VLPFrictionFactorMethods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethods;

class Program
{

    static void Main(string[] args)
    {

        VLPDataInput vLPDataInput = new();
        vLPDataInput.GasSuperficialVelocity = 4.09;
        vLPDataInput.LiquidSuperficialVelocity = 2.65;
        vLPDataInput.PipeInsideDiameter = 0.249;
        vLPDataInput.GasDensity = 2.84;
        vLPDataInput.LiquidDesnity = 56.6;
        vLPDataInput.TotalMassFlowRate = 7.87;
        vLPDataInput.GasLiquidRatio = 1500;

        FancherBrownFrictionFactor poettamnnCarpenterFrictionFactor = new();

        NoSlipNoFlowRegime calculator = new(poettamnnCarpenterFrictionFactor);

        NodalAnalysisValidationResult validationResult = new();

        double pressureGradient = calculator.DeterminePressureGradient(vLPDataInput, ref validationResult);


        Console.WriteLine(pressureGradient);

    }

}