using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class clsFutureIPRDataInput
    {

        public double? FutureReservoirPressure { get; set; }
         
        public double? PresentOilRelativePermeability { get; set; }

        public double? PresentOilViscosity { get; set; }

        public double? PresentOilFormationVolumeFactor { get; set; }

        public double? FutureOilRelativePermeability { get; set; }

        public double? FutureOilViscosity { get; set; }

        public double? FutureOilFormationVolumeFactor { get; set; }

        public clsFutureIPRDataInput(double? futureReservoirPressure, double? presentOilRelativePermeability, 
            double? presentOilViscosity, double? presentOilFormationVolumeFactor, double? futureOilRelativePermeability,
            double? futureOilViscosity, double? futureOilFormationVolumeFactor)
        {

            FutureReservoirPressure = futureReservoirPressure;
            PresentOilRelativePermeability = presentOilRelativePermeability;
            PresentOilViscosity = presentOilViscosity;
            PresentOilFormationVolumeFactor = presentOilFormationVolumeFactor;
            FutureOilRelativePermeability = futureOilRelativePermeability;
            FutureOilViscosity = futureOilViscosity;
            FutureOilFormationVolumeFactor = futureOilFormationVolumeFactor;
                
        }

    }
}
