using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{
    public class InFlowPerformanceRelationship
    {


        private readonly IPRMethodBase _method;

        private readonly IPRInputData _input;

        public InFlowPerformanceRelationship(IPRMethodBase method,
            IPRInputData input)
        {

            _method = method;
            _input = input;

        }


        public List<InFlowDataRow> GenerateIPR(
            ref NodalAnalysisValidationResult validationResult)
        {

            return _method.GenerateIPR(_input, ref validationResult);

        }

        public List<InFlowDataRow> GenerateFutureIPR(IPRInputData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            if (_method is IFuturePredictable futureMethod)
            {

                return futureMethod.GenerateFutureIPR(input,
                    ref validationResult);

            }

            throw new UnsupportedIPROperationException(
                "The selected method does not support future IPR generation.");

        }

    }
}
