using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.General_Utility.Validation
{
    internal class ValidationMessages
    {

        public static ErrorMessage MissingRequiredData(
            string parameterName)
        {
            return new ErrorMessage(
                "Missing Required Data",
                $"Please provide {parameterName}.");
        }

        public static ErrorMessage MustBeGreaterThanZero(
            string parameterName)
        {
            return new ErrorMessage(
                "Invalid Parameter",
                $"{parameterName} must be greater than zero.");
        }

        public static ErrorMessage MustBeNonNegative(
            string parameterName)
        {
            return new ErrorMessage(
                "Invalid Parameter",
                $"{parameterName} must be non-negative.");
        }

        public static ErrorMessage MustBeInRange(
            string parameterName,
            double min,
            double max)
        {
            return new ErrorMessage(
                "Invalid Parameter",
                $"{parameterName} must be between {min} and {max}.");
        }

        public static ErrorMessage MustBeGreaterThan(
            string greaterParameter,
            string smallerParameter)
        {
            return new ErrorMessage(
                "Invalid Parameter",
                $"{greaterParameter} must be greater than {smallerParameter}.");
        }

    }
}
