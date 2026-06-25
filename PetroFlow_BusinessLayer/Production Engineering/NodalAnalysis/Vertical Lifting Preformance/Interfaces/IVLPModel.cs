using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;


namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces
{
    public interface IVLPModel
    {

        public void ValidateInputData(VLPWorkingData InputData,
            ref NodalAnalysisValidationResult validationResult);

        public double DeterminePressureGradient(VLPWorkingData InputData,
            ref NodalAnalysisValidationResult validationResult);

    }
}
