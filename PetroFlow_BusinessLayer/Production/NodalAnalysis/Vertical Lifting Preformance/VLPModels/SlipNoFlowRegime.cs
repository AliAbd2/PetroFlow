using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses.VLPFrictionFactor;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels
{
    public class SlipNoFlowRegime : IVLPModel
    {

        public double TotalMassFlowRate { get; set; }

        public double TwoPhaseFrictionFactor { get; set; }

        public double PipeInsideDiameter { get; set; }

        public double LiquidHoldupBasedDensity { get; set; }

        private SlipNoFlowRegimeFrictionFactorCalculator _frictionFactorCalculator;

        private SlipNoFlowRegimeHoldupCalculator _holdupCalculator;

        public SlipNoFlowRegime(SlipNoFlowRegimeFrictionFactorCalculator frictionFactorCalculator,
            SlipNoFlowRegimeHoldupCalculator holdupCalculator)
        {

            _frictionFactorCalculator = frictionFactorCalculator;
            _holdupCalculator = holdupCalculator;

        }

        public NodalAnalysisValidationResult ValidateInputData(VLPDataInput InputData)
        {

        }

        public double DeterminePressureGradient(VLPDataInput InputData, ref NodalAnalysisValidationResult validationResult)
        {

        }


    }
}
