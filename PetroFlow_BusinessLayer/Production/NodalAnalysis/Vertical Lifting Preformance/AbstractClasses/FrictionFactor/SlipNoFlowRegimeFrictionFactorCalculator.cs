using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor
{
    public abstract class SlipNoFlowRegimeFrictionFactorCalculator
    {

        public double CalculateFrictionFactor(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult)
        {
            ValidateRawData(input, derivedProperties, ref validationResult);
            return ComputeFrictionFactor(input, derivedProperties, ref validationResult);
        }

        protected abstract void ValidateRawData(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);
        protected abstract double ComputeFrictionFactor(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);

    }
}
