using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{
    public interface IFuturePredictable
    {

        void GenerateFutureIPR(double futureReservoirPressure,
            double futureOilRelativePermeability, double futureOilFormationVolumeFactor, double FutureOilViscosity);

    }
}
