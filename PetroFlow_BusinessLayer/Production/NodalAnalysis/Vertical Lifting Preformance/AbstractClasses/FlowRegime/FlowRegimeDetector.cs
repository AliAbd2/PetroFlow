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

        public SlipFlowRegime.enFlowRegime DetecteFlowRegime(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {
            ValidateRawData(input, derivedProperties, ref validationResult);
            return Detecte(input, derivedProperties, ref validationResult);
        }

        protected abstract void ValidateRawData(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);
        protected abstract SlipFlowRegime.enFlowRegime Detecte(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);

    }
}
