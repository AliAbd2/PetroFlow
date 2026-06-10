using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis
{
    public class NodalAnalysisInputData
    {

        public IPRInputData IPRInputData;

        public VLPInputData VLPInputData;

        public NodalAnalysisInputData()
        {

            IPRInputData = new();

            VLPInputData = new();

        }

    }
}
