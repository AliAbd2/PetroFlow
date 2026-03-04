using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class clsIPRGenerationSettings
    {
        public double PressureStepSize { get; set; } = 10;
        public double MinimumPressure { get; set; } = 0;


        public clsIPRGenerationSettings(double stepSize, double minPressure)
        {
            if (stepSize <= 0)
                throw new exInvalidIPRParameterException(
                    "Pressure step size must be greater than zero.");

            if (minPressure < 0)
                throw new exInvalidIPRParameterException(
                    "Minimum pressure cannot be negative.");

            PressureStepSize = stepSize;
            MinimumPressure = minPressure;
        }


    }
}
