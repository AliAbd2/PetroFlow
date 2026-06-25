using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT
{
    public class ZFactorCalculator
    {

        private static double _calculateA(double pseudoreducedTemperature)
        {


            // constants used in calculating A
            double c1 = 1.39;
            double c2 = 0.92;
            double c3 = 0.36;
            double c4 = 0.101;

            return c1 * Math.Pow((pseudoreducedTemperature - c2), 0.5)
                - c3 * pseudoreducedTemperature - c4;

        }

        private static double _calculateB(double pseudoreducedTemperature,
            double pseudoreducedPressure)
        {

            // constants used in calculating B
            double c1 = 0.62;
            double c2 = 0.23;
            double c3 = 0.066;
            double c4 = 0.86;
            double c5 = 0.037;
            double c6 = 0.32;
            double c7 = 20.723;

            double x = pseudoreducedPressure * (c1 - c2 * pseudoreducedTemperature);
            double y = pseudoreducedPressure * pseudoreducedPressure *
                (c3 / (pseudoreducedTemperature - c4) - c5);
            double z = (c6 * Math.Pow(pseudoreducedPressure, 6)) / Math.Exp(c7 * (pseudoreducedTemperature - 1));

            return x + y + z;

        }

        private static double _calculateC(double pseudoreducedTemperature)
        {

            double c1 = 0.132;
            double c2 = 0.32;

            return c1 - c2 * Math.Log10(pseudoreducedTemperature);

        }

        private static double _calculateD(double pseudoreducedTemperature)
        {

            double c1 = 0.715;
            double c2 = 1.128;
            double c3 = 0.42;

            double x = c1 - c2 * pseudoreducedTemperature +
                c3 * pseudoreducedTemperature * pseudoreducedTemperature;

            return Math.Exp(x);

        }

        public static double CalculateZFactor(PVTDataInput input)
        {

            double gasSpecificGravity = input.GasSpecificGravity.Value;
            double temperatureinR = input.RankineTemperature.Value;
            double pressure = input.PSIPressure.Value;

            double pseudocriticalTemperature = 170.5 + 307.3 * gasSpecificGravity;
            double pseduocriticalPressure = 709.6 - 58.7 * gasSpecificGravity;

            double pseudoreducedTemperature = temperatureinR / pseudocriticalTemperature;
            double pseudoreducedPressure = pressure / pseduocriticalPressure;


            double A = _calculateA(pseudoreducedTemperature);
            double B = _calculateB(pseudoreducedTemperature, pseudoreducedPressure);
            double C = _calculateC(pseudoreducedTemperature);
            double D = _calculateD(pseudoreducedTemperature);

            return A + (1 - A) * Math.Exp(-B) + C * Math.Pow(pseudoreducedPressure, D);

        }

    }
}
