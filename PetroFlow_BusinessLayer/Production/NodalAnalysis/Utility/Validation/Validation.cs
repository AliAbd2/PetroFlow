using OpenTK.Input;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation
{
    public static class Validation
    {

        public static void Range(double value, double min, double max, string name)
        {
            if (min > max)
                throw new ArgumentException("Minimum cannot be greater than maximum.");

            if (value < min || value > max)
                throw new InvalidParameterException($"{name} must be between {min} and {max}.");
        }

        public static void NonNegative(double value, string name)
        {
            if (value < 0)
                throw new InvalidParameterException($"{name} must be non-negative.");
        }

        public static void GreaterThanZero(double value, string name)
        {
            if (value <= 0)
                throw new InvalidParameterException($"{name} must be greater than zero.");
        }

        public static void GreaterThan(double greaterValue, double smallerValue,
            string greaterValueName, string smallerValueName)
        {
            if (greaterValue <= smallerValue)
                throw new InvalidParameterException($"{greaterValueName} must be greater than {smallerValueName}.");
        }

        public static void LessThan(double smallerValue, double greaterValue,
            string smallerValueName, string greaterValueName)
        {

            if (smallerValue >= greaterValue)
                throw new InvalidParameterException($"{smallerValueName} must be smaller than {greaterValueName}.");

        }

        public static void SumApproximatelyOne(double value1, double value2, 
            string name1, string name2, double tolerance = 1e-6)
        {

            Range(value1, 0, 1, name1);
            Range(value2, 0, 1, name2);

            double total = value1 + value2;

            if (Math.Abs(total - 1) > tolerance)
                throw new InvalidParameterException(
                    $"The sum of {name1} and {name2} must be approximately 1 (within tolerance {tolerance}).");

        }

        public static void Missing(double? value, string name)
        {

            if (value == null)
                throw new MissingRequiredInputException($"Missing Required Data, Please provide {name}.");

        }

    }
}
