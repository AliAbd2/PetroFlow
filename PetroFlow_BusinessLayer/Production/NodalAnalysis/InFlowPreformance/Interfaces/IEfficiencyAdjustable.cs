using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{
    public interface IEfficiencyAdjustable
    {

        void GenerateWithEfficiency(double efficiency);

    }
}
