using PetroFlow_BusinessLayer.General_Utility.Validation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility
{

    // this is a class to store all the error messages related to the IPR utility.
    // This will help to maintain consistency in the error messages and make it easier to update them in the future.

    public static class IPRErrorMessages
    {
        //============================
        // --- Test Data Errors ---
        //============================

        public static readonly ErrorMessage MissingTestData =
            new(
                "Missing Test Data",
                "Cannot generate IPR: Test data has not been provided."
            );

        public static ErrorMessage InvalidTestDataCount(
            string method,
            int requiredNumber)
        {
            return new ErrorMessage(
                "Invalid Test Data",
                $"Invalid test data: {method} requires {requiredNumber} test data points."
            );
        }

        public static readonly ErrorMessage InvalidTestDataFlowRate =
            new(
                "Invalid Test Data",
                "Invalid test data: One or more flow rates are zero or negative."
            );

        public static readonly ErrorMessage InvalidTestDataBottomHolePressure =
            new(
                "Invalid Test Data",
                "Invalid test data: One or more bottom hole pressures are zero or negative."
            );

        public static readonly ErrorMessage InvalidTestDataBottomHolePressureGreaterThanReservoirPressure =
            new(
                "Invalid Test Data",
                "Invalid test data: One or more bottom hole pressures are greater than reservoir pressure."
            );

        public static readonly ErrorMessage OnlyOneTestDataPointWillBeUsedWarning =
            new(
                "Test Data Warning",
                "Multiple test data rows were provided. Only the first row will be used."
            );

        //=======================================
        // --- Bubble Point Pressure Errors ---
        //=======================================

        public static readonly ErrorMessage BubblePointPressureNotProvidedWarning =
            new(
                "Bubble Point Pressure Warning",
                "Bubble point pressure has not been provided. Reservoir will be assumed saturated."
            );

    }

}
