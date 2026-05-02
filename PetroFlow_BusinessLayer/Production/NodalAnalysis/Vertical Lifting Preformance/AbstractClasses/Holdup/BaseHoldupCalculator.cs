using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.Holdup
{
    public abstract class BaseHoldupCalculator
    {

        public double CalculateLiquidHoldup(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {
            ValidateRawInput(input, ref validationResult, derivedProperties);
            return ComputeLiquidHoldup(input, ref validationResult, derivedProperties);
        }

        protected abstract void ValidateRawInput(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);
        protected abstract double ComputeLiquidHoldup(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null);

    }
}
