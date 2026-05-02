using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels
{

    public abstract class SlipFlowRegime : IVLPModel
    {

        public enum enFlowRegime { BubbleFlow, SlugFlow, TransitionFlow, MistFlow }

        public abstract void ValidateInputData(VLPWorkingData InputData,
            ref NodalAnalysisValidationResult validationResult);

        public abstract double DeterminePressureGradient(VLPWorkingData InputData,
            ref NodalAnalysisValidationResult validationResult);

    }
}
