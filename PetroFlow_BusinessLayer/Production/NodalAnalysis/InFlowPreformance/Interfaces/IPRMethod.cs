using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{

    public abstract class IPRMethodBase
    {

        public List<InFlowDataRow> GenerateIPR(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult)
        {

            ValidateRawData(inputData, ref validationResult);

            return ComputeIPR(inputData, ref validationResult);

        }

        protected abstract void ValidateRawData(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult);

        protected abstract List<InFlowDataRow> ComputeIPR(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult);


    }
}
