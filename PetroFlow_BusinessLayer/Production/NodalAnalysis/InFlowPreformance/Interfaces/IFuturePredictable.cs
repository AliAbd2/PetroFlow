using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{
    public interface IFuturePredictable
    {

        clsValidationResult ValidateFutureInput(clsFutureIPRDataInput futureDataInput);

        void GenerateFutureIPR(clsFutureIPRDataInput futureDataInput);

    }
}
