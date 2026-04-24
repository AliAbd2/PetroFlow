using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor
{
    public abstract class SlipNoFlowRegimeFrictionFactorCalculator
    {

        public double CalculateFrictionFactor(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult)
        {
            Validate(input, noSlipMixtureDensity, ref validationResult);
            return Compute(input, noSlipMixtureDensity, ref validationResult);
        }

        protected abstract void Validate(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult);
        protected abstract double Compute(VLPDataInput input, double noSlipMixtureDensity,
            ref NodalAnalysisValidationResult validationResult);

    }
}
