using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance
{
    public static class SinglePhaseFrictionFactorCalculator
    {

        public static double CalcuateReynoldsNumber(double density, double viscosity,
            double velocity, double pipeDiameter)
        {

            return (density * velocity * pipeDiameter) / viscosity;

        }


        public static double LaminarFrictionFactor(double reynoldsNumbe)
        {

            return 64 / reynoldsNumbe;

        }

        public static double DrewKooMcAdams(double reynoldsNumber)
        {

            return 0.0056 + 0.5 * Math.Pow(reynoldsNumber, -0.32);

        }


        public static double Blasius(double reynoldsNumber)
        {

            return 0.316 * Math.Pow(reynoldsNumber, -0.25);

        }


        public static double Colebrook(double relativeRoughness, double reynoldsNumber, 
            double error = 1e-6)
        {

            double initialFriction = DrewKooMcAdams(reynoldsNumber);
            double friction = -1;

            while (Math.Abs(initialFriction - friction) > error)
            {

                double x = 2 * relativeRoughness +
                    (18.7 / (reynoldsNumber * Math.Pow(initialFriction, .5)));

                double y = 1.74 - 2 * Math.Log10(x);

                double z = 1 / 2;

                friction = z * z;


            }

            return friction;

        }


        public static double Jain(double relativeRoughness, double reynoldsNumber)
        {

            double x = relativeRoughness + (21.25 / Math.Pow(reynoldsNumber, 0.9));

            double y = 1.14 - 2 * Math.Log10(x);

            return Math.Pow(1 / y, 2);

        }

    }
}
