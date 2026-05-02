using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FlowRegime;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.FlowRegime
{
    public class DunsRosFlowRegimeDetector : FlowRegimeDetector
    {

        protected override void ValidateRawData(VLPDataInput input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        protected override SlipFlowRegime.enFlowRegime Detecte(VLPDataInput input, VLPDerivedProperties derivedProperties, ref NodalAnalysisValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
        

    }
}
