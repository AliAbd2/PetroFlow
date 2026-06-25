using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FlowRegime
{
    internal interface IFlowRegimeDetector
    {

        public SlipFlowRegime.enFlowRegime Detect(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);

    }
}
