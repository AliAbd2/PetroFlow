using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{
    public class InFlowPreformanceRelationship
    {

        private IIPRMethod _method;

        private IPRInputData _iPRInput;

        public InFlowPreformanceRelationship(IIPRMethod method, 
            IPRInputData input)
        {

            _method = method;
            _iPRInput = input;

        }




    }
}
