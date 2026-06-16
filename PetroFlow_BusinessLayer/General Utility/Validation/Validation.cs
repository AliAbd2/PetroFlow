using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation
{
    public static class Validation
    {
        private static double RequireValue(double? value, string name)
        {
            return value ?? throw new MissingRequiredInputException(
                ValidationMessages.MissingRequiredData(name));
        }

        public static void IsInRange(
            double? value,
            double min,
            double max,
            string name)
        {
            if (min > max)
                throw new ArgumentException(
                    "Minimum cannot be greater than maximum.");

            double actualValue = RequireValue(value, name);

            if (actualValue < min || actualValue > max)
            {
                throw new InvalidParameterException(
                    ValidationMessages.MustBeInRange(
                        name,
                        min,
                        max));
            }
        }

        public static void IsNonNegative(
            double? value,
            string name)
        {
            double actualValue = RequireValue(value, name);

            if (actualValue < 0)
            {
                throw new InvalidParameterException(
                    ValidationMessages.MustBeNonNegative(name));
            }
        }

        public static void IsGreaterThanZero(
            double? value,
            string name)
        {
            double actualValue = RequireValue(value, name);

            if (actualValue <= 0)
            {
                throw new InvalidParameterException(
                    ValidationMessages.MustBeGreaterThanZero(name));
            }
        }

        public static void IsGreaterThan(
            double? greaterValue,
            double? smallerValue,
            string greaterValueName,
            string smallerValueName)
        {
            double actualGreaterValue =
                RequireValue(greaterValue, greaterValueName);

            double actualSmallerValue =
                RequireValue(smallerValue, smallerValueName);

            if (actualGreaterValue <= actualSmallerValue)
            {
                throw new InvalidParameterException(
                    ValidationMessages.MustBeGreaterThan(
                        greaterValueName,
                        smallerValueName));
            }
        }

    }

}
