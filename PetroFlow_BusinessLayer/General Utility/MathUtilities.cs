using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis
{
    internal abstract class MathUtilities
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

        public static (double Slope, double Intercept) LeastSquaresLineFit(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Input arrays must have the same length.");

            if (x.Length < 2)
                throw new ArgumentException("At least two points are required.");

            int n = x.Length;

            double sumX = x.Sum();
            double sumY = y.Sum();
            double sumX2 = x.Sum(x => x * x);
            double sumXY = x.Zip(y, (x, y) => x * y).Sum();

            double denominator = n * sumX2 - sumX * sumX;

            if (Math.Abs(denominator) < 1e-12)
                throw new InvalidOperationException(
                    "Cannot calculate regression because all X values are identical.");

            double slope = (n * sumXY - sumX * sumY) / denominator;

            double intercept = (sumY - slope * sumX) / n;

            return (slope, intercept);
        }

    }
}
