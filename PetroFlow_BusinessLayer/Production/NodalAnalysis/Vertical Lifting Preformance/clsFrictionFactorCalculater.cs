using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance
{
    public static class clsFrictionFactorCalculater
    {

        public static double CalcuateReynoldsNumber(double density, double viscosity,
            double velocity, double pipeDiameter)
        {

            return (density * velocity * pipeDiameter) / viscosity;

        }


        public static double DrewKooMcAdams(double density, double viscosity,
            double velocity, double pipeDiameter)
        {

            double ReynoldsNumber = CalcuateReynoldsNumber(density, viscosity, velocity, pipeDiameter);

            return 0.0056 + 0.5 * Math.Pow(ReynoldsNumber, -0.32);


        }


        public static double Blasius(double density, double viscosity,
            double velocity, double pipeDiameter)
        {

            double ReynoldsNumber = CalcuateReynoldsNumber(density, viscosity, velocity, pipeDiameter);

            return 0.316 * Math.Pow(ReynoldsNumber, -0.25);

        }


        public static double Nikuradse()
        {

            return -1;

        }

    }
}
