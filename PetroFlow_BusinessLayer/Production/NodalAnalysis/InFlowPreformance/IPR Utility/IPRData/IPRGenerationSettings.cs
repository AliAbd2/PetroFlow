using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
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

            Validation.IsGreaterThanZero(stepSize, "Pressure Step Size");

            Validation.IsNonNegative(minPressure, "Minimum Pressure");

            PressureStepSize = stepSize;
            MinimumPressure = minPressure;
        }


    }
}
