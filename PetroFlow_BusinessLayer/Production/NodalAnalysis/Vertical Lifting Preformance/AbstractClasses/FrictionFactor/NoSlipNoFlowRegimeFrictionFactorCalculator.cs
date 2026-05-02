using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces
{
    public abstract class NoSlipNoFlowRegimeFrictionFactorCalculator
    {

        public double CalculateFrictionFactor(VLPDataInput input, VLPDerivedProperties derivedProperties, 
            ref NodalAnalysisValidationResult validationResult)
        {
            Validate(input, derivedProperties, ref validationResult);
            return Compute(input, derivedProperties, ref validationResult);
        }

        protected abstract void Validate(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);
        protected abstract double Compute(VLPDataInput input, VLPDerivedProperties derivedProperties,
            ref NodalAnalysisValidationResult validationResult);

    }
}
