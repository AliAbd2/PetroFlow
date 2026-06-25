using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FrictionFactor
{
    internal interface IFrictionFactorCalculator
    {

        public double ComputeFrictionFactor(VLPWorkingData input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);

    }
}
