using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FlowRegime
{
    public abstract class FlowRegimeDetector
    {

        public SlipFlowRegime.enFlowRegime DetectFlowRegime(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {
            ValidateRawData(input, ref validationResult, derivedProperties);
            return Detect(input, ref validationResult, derivedProperties);
        }

        protected abstract void ValidateRawData(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);
        protected abstract SlipFlowRegime.enFlowRegime Detect(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);

    }
}
