using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance
{

    // A class to sotre the functions that are used in different IPR methods.

    public class clsIPRGeneralFunctions
    {

        public static double ProductivityIndex(double oilFlowRate, double reservoirPressure, double flowingPressure)
        {
            // A function to calculate the linear prodctivity index.

            return oilFlowRate / (reservoirPressure - flowingPressure);
        }

        public static double LinearFlowRate(double productivityIndex, double reservoirPressure, double flowingPressure)
        {
            // A function to calculate the oil flowrate from a linear prodctivity index.

            return productivityIndex * (reservoirPressure - flowingPressure);
        }

    }
}
