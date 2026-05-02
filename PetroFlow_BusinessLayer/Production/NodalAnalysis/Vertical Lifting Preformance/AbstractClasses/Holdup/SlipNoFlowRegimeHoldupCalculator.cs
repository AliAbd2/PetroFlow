using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses
{
    public abstract class SlipNoFlowRegimeHoldupCalculator
    {

        public double CalculateLiquidHoldup(VLPDataInput input,
            ref NodalAnalysisValidationResult validationResult)
        {
            ValidateRawInput(input, ref validationResult);
            return ComputeLiquidHoldup(input, ref validationResult);
        }

        protected abstract void ValidateRawInput(VLPDataInput input,
            ref NodalAnalysisValidationResult validationResult);
        protected abstract double ComputeLiquidHoldup(VLPDataInput input,
            ref NodalAnalysisValidationResult validationResult);

    }
}
