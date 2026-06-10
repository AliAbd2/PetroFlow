using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis
{
    public class NodalAnalysis
    {

        private IPRMethodBase _IPRMEthod;

        private IVLPModel _VLPModel;

        public NodalAnalysis(IPRMethodBase iPRMethod, IVLPModel vLPModel)
        {

            _IPRMEthod = iPRMethod;
            _VLPModel = vLPModel;

        }


    }
}
