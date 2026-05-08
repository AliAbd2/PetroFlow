using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData
{
    public class IPRGenerationSettings
    {
        public double PressureStepSize { get; set; } = 10;
        public double MinimumPressure { get; set; } = 0;


        public IPRGenerationSettings(double stepSize, double minPressure)
        {
            if (stepSize <= 0)
                throw new InvalidParameterException(
                    "Pressure step size must be greater than zero.");

            if (minPressure < 0)
                throw new InvalidParameterException(
                    "Minimum pressure cannot be negative.");

            PressureStepSize = stepSize;
            MinimumPressure = minPressure;
        }


    }
}
