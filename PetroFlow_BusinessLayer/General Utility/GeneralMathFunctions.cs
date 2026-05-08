using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis
{
    internal abstract class GeneralMathFunctions
    {

        public static double LinearInterpolate(
            double x, double x1, double y1, double x2, double y2)
        {
            if (x2 == x1)
                throw new ArgumentException("x1 and x2 cannot be equal.");

            return y1 + (x - x1) * (y2 - y1) / (x2 - x1);
        }

        public static double CrircleArea(double diameter)
        {

            return Math.PI * 0.25 * diameter * diameter;

        }

    }
}
