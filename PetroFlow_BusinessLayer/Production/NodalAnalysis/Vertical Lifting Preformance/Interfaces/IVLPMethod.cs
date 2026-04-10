using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces
{
    public interface IVLPMethod
    {

        public ValidationResult ReadDataInput(clsVLPDataInput dataInout);

        public double DeterminePressureGradient();

    }
}
