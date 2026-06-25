using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // A class to store the functions that are used in different IPR methods.

    public class IPRGeneralFunctions
    {

        public static double ProductivityIndex(double oilFlowRate, double reservoirPressure, double flowingPressure)
        {
            // A function to calculate the linear productivity index.

            return oilFlowRate / (reservoirPressure - flowingPressure);
        }

        public static double LinearFlowRate(double productivityIndex, double reservoirPressure, double flowingPressure)
        {
            // A function to calculate the oil flow rate from a linear productivity index.

            return productivityIndex * (reservoirPressure - flowingPressure);
        }

        public static double CalculateVogelTerm(
            double pressureRatio)
        {

            double vogelLinearCoefficient = 0.2;
            double vogelQuadraticCoefficient = 0.8;

            return 1
                - vogelLinearCoefficient * pressureRatio
                - vogelQuadraticCoefficient * pressureRatio * pressureRatio;
        }

        public static bool IsSaturatedReservoir(IPRInputData input)
        {

            return
                !input.BubblePointPressure.HasValue 
                || input.ReservoirPressure!.Value <= input.BubblePointPressure.Value;

        }

    }
}
