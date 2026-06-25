using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.Holdup
{
    internal interface IHoldupCalculator
    {
        public double ComputeLiquidHoldup(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);

    }
}
