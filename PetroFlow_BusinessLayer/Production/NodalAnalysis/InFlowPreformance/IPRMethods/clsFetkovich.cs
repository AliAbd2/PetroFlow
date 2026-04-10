using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // This class implements Fetkovich’s method for generating the
    // Inflow Performance Relationship (IPR) for oil wells.
    //
    // Fetkovich showed that oil-well inflow behavior can be expressed as:
    //
    //     qo = C · (Pr² − Pwf²)ⁿ
    //
    // where:
    //     qo  = oil flow rate
    //     Pr  = reservoir pressure
    //     Pwf = flowing bottom-hole pressure
    //     C   = flow coefficient
    //     n   = flow exponent related to well and reservoir characteristics
    //
    // By applying a base-10 logarithmic transformation:
    //
    //     log(qo) = log(C) + n · log(Pr² − Pwf²)
    //
    // the equation becomes linear, allowing the exponent n to be
    // determined from production test data.
    //
    // This class determines the exponent n using:
    //   • n = 1 when only one test point is available
    //   • Δlog(qo) / Δlog(Pr² − Pwf²) when two test points are available
    //   • Least-squares linear regression when three or more test points exist
    //
    // After determining n, the class calculates the productivity index (J)
    // using Fetkovich’s formulations for:
    //   • Saturated reservoirs
    //   • Undersaturated reservoirs (with or without test points above Pb)
    //
    // Finally, the class generates IPR data over a specified pressure range
    // using the calculated productivity index and exponent.
    //
    // The class supports both saturated and undersaturated reservoirs,
    // with optional bubble-point pressure input.

    public class clsFetkovich : IIPRMethod, IFuturePredictable
    {
        public string Name { get; set; }

        public enIPRMethodType MethodType { get { return enIPRMethodType.Fetkovich; } }

        public double ReservoirPressure { get; set; }

        public double? BubblePointPressure { get; set; }

        public List<clsInFlowDataRow> TestsData { get; set; }

        public List<clsInFlowDataRow> GeneratedData { get; set; }

        public clsCurvePlotSettings CurvePlotSetting { get; set; }

        public bool IsInputValid { get; set; }

        public clsIPRGenerationSettings GenerationSettings { get; set; }

        private double? WellExponent;

        private double? PresentFlowCoefficient;

        private double ProductivityIndex;

        // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
        // If the bubble point pressure is not provided, the reservoir is treated as saturated.
        private bool IsSaturated
        {
            get
            {

                if (BubblePointPressure == null)
                    return true;

                return ReservoirPressure <= BubblePointPressure;

            }
        }

        public clsFetkovich()
        {

            WellExponent = null;
            PresentFlowCoefficient = null;
            CurvePlotSetting = new clsCurvePlotSettings();

        }

        public ValidationResult SetInputData(clsPresentIPRDataInput inputData)
        {
            ValidationResult validationResult = new();

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            if (inputData.ReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Reservoir pressure has not been provided.");

            if (inputData.ReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid reservoir pressure: A positive value greater than zero is required.");

            //=========================
            // --- Well Exponent ---
            //========================
            if (inputData.WellExponent != null)
            {

                if (inputData.WellExponent < 0.568 ||
                    inputData.WellExponent > 1)
                    validationResult.Warnings.Add(
                        "Well exponent is outside the recommended range for the Fetkovich model (0.568–1.0).");


            }

            //==========================
            // --- Flow Coefficient ---
            //==========================
            if (inputData.FlowCoefficient != null)
                PresentFlowCoefficient = inputData.FlowCoefficient;


            //=====================
            // --- Test Data ---
            //=====================
            if (inputData.TestsData == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Test data has not been provided.");

            if (inputData.TestsData.Count < 3 && inputData.WellExponent == null)
                throw new InvalidParameterException(
                    "Invalid test data: At least three test data rows is required.");

            if (inputData.TestsData.Count < 1 && 
                inputData.WellExponent != null && 
                PresentFlowCoefficient == null)
                throw new InvalidParameterException(
                    "Invalid test data: At least one test data row is required.");

            if (inputData.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more flow rates are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(
                    "Invalid test data: One or more bottom hole pressures are zero or negative.");

            if (inputData.TestsData.Any(x => x.BottomHolePressure >= inputData.ReservoirPressure))
                throw new InvalidParameterException(
                    "Bottom-hole pressure must be less than reservoir pressure.");

            if (inputData.TestsData.Count > 1 && inputData.WellExponent == null)
                validationResult.Warnings.Add(
                    "Multiple test data rows were provided. Only the first row will be used.");


            //================================
            // --- Bubble Point Pressure ---
            //================================
            if (inputData.BubblePointPressure == null)
            {
                validationResult.Warnings.Add(
                    "Bubble point pressure was not provided. Reservoir will be assumed saturated.");
            }
            else
            {

                if (inputData.BubblePointPressure <= 0)
                    throw new InvalidParameterException(
                        "Invalid bubble point pressure: A positive value greater than zero is required.");

                if (inputData.BubblePointPressure.Value > inputData.ReservoirPressure.Value)
                {
                    validationResult.Warnings.Add(
                        "Bubble point pressure is greater than reservoir pressure. Reservoir will behave as saturated.");
                }

            }

            ReservoirPressure = inputData.ReservoirPressure.Value;
            TestsData = inputData.TestsData;
            BubblePointPressure = inputData.BubblePointPressure;
            WellExponent = inputData.WellExponent;

            IsInputValid = true;

            return validationResult;
        }

        private void DetermineslopeIntercept()
        {
            // This is a method to determaine the slope.

            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // where n is the number of records.

            if (WellExponent != null)
                return;

            double pr2 = ReservoirPressure * ReservoirPressure;
            int n = TestsData.Count();

            List<double> logX = TestsData.Select(x => Math.Log10(
                pr2 - x.BottomHolePressure * x.BottomHolePressure)).ToList();
            List<double> logY = TestsData.Select(y => Math.Log10(y.FlowRate)).ToList();

            double sumX = logX.Sum();
            double sumY = logY.Sum();
            double sumX2 = logX.Sum(x => x * x);
            double sumXY = logX.Zip(logY, (x, y) => x * y).Sum();

            double m = (n * sumXY - sumX * sumY)
                     / (n * sumX2 - sumX * sumX);

            double intercept = (sumY - m * sumX) / n;

            WellExponent = m;

            PresentFlowCoefficient = 1 / Math.Pow(10, intercept);

            
        }

        private void DetermineProductivityIndex()
        {
            // A method to calculate the productivity index for fetkovich method.
            // There is three cases:
            // 1. if the reservoir is saturated then:
            // J = 2 * q / (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
            // 2. if the reservoir is undersaturated and there is a test point 
            // bottom hole pressure > bubble point then:
            // J = q / (Pr - Pwf)
            // 3. if the rservoir is undersaturated and there is not a test point
            // bottom hole pressure > bubble point then:
            // J = q / ((Pr - Pwf) + Pb / 2 *([1 - (Pwf / Pb)^2]^n)

            DetermineslopeIntercept();

            double productivityIndex = 0;

            double bottomHolePressure = TestsData[0].BottomHolePressure;
            double flowRate = TestsData[0].FlowRate;



            // Case 1:
            if (IsSaturated)
            {
                double x = Math.Pow((bottomHolePressure / ReservoirPressure), 2); // (Pwf / Pr)^2
                double y = ReservoirPressure * Math.Pow(1 - x, WellExponent.Value); // (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
                productivityIndex = 2 * flowRate / y; 

            }

            // Case 2:
            else if (!IsSaturated && bottomHolePressure > BubblePointPressure)
                productivityIndex = flowRate / (ReservoirPressure - bottomHolePressure);

            // Case 3:
            else
            {

                double x = Math.Pow(bottomHolePressure / BubblePointPressure.Value, 2); // (Pwf / Pb)^2
                double y = Math.Pow(1 - x, WellExponent.Value); // [1 - (Pwf / Pb)^2]^n
                double z = (ReservoirPressure - (double)BubblePointPressure) +
                    (double)BubblePointPressure / 2 * y;
                productivityIndex = flowRate / z;
            }


            ProductivityIndex = productivityIndex;

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to generate the IPR data for saturated reservoir using Fetkovich method.


            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>();
            double flowRate = 0;

            DetermineProductivityIndex();

            for (double pressure = GenerationSettings.MinimumPressure; 
                pressure <= ReservoirPressure; pressure += GenerationSettings.PressureStepSize)
            {
                // q = J * Pr / 2 * [ 1 - (Pwf / Pr)^d2 * n ]

                double x = 1 - Math.Pow(pressure / ReservoirPressure, 2 * WellExponent.Value); // [ 1 - (Pwf / Pr)^d2 * n ]
                flowRate = ProductivityIndex * ReservoirPressure / 2 * x;

                dataRows.Add(new clsInFlowDataRow(pressure, flowRate));


            }

            return dataRows;


        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir()
        {


            // A method to generate the IPR data for undersaturated reservoir using Fetkovich method.


            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>();
            double flowRate = 0;

            if (WellExponent == null)
                DetermineProductivityIndex();

            for (double pressure = GenerationSettings.MinimumPressure;
                pressure <= ReservoirPressure; pressure += GenerationSettings.PressureStepSize)
            {

                if (pressure > BubblePointPressure)
                {
                    // q = J * (Pr - Pwf)
                    flowRate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex,
                        ReservoirPressure, pressure);

                }
                else
                {
                    // q = J * (Pr - Pb) + J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n

                    double x = Math.Pow((1 - Math.Pow(pressure / BubblePointPressure.Value, 2)), WellExponent.Value); //[ 1 - (Pwf / Pb)^2 ]^n
                    double y = ProductivityIndex * (double)BubblePointPressure / 2 * x; // J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n
                    flowRate = ProductivityIndex * (ReservoirPressure - (double)BubblePointPressure) + y;

                }

                dataRows.Add(new clsInFlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }

        public void GenerateIPR()
        {

            if (!IsInputValid)
                throw new InvalidOperationException("Invalid operation: " +
                    "Calculation method was called before input data was set. Call SetInputData() first.");

            if (GenerationSettings.MinimumPressure > ReservoirPressure)
                throw new InvalidParameterException("Minimum pressure must be less than the reservoir pressure.");

            if (IsSaturated)
                GeneratedData = GenerateIPR_SaturatedReservoir();
            else
                GeneratedData = GenerateIPR_UnderSaturatedReservoir();

        }

        public ValidationResult ValidateFutureInput(clsFutureIPRDataInput futureDataInput)
        {

            ValidationResult validationResult = new();

            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            if (futureDataInput.FutureReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Future Reservoir pressure has not been provided.");

            if (futureDataInput.FutureReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure: A positive value greater than zero is required.");

            if (futureDataInput.FutureReservoirPressure > ReservoirPressure)
                throw new InvalidParameterException(
                    "Invalid future reservoir pressure:" +
                    " future reservoir pressure must be less than present reservoir pressure.");

            //===================================
            // --- Present Flow Coefficient ---
            //===================================
            if (PresentFlowCoefficient == null)
                throw new MissingRequiredInputException(
                    "Cannot generate Future IPR: Present flow Coefficinet has not been provided.");


            return validationResult;

        }

        private double DetermineFutureFlowCoefficient(double futureReservoirPressure)
        {

            // A method to calcualte Future Flow Coefficient.

            // The Future Flow Corfficient can be calculated using the following equation:
            // CF = CP * (PRF / PrP)

            DetermineslopeIntercept();

            return PresentFlowCoefficient.Value * (futureReservoirPressure / ReservoirPressure);

        }

        public void GenerateFutureIPR(clsFutureIPRDataInput InputData)
        {

            // A Method to generate future IPR using Vetkovich's method.

            // The method proposed by Fetkovich to construct future
            // IPR's consists of adjusting the flow coefficient C for 
            // changes in f(Pr).
            // Fetkovich assumed that f(Pr) was a linear function of Pr,
            // and therefore, the value of C can be adjusted as

            // CF = CP * (PRF / PrP)

            // and then the future IPR's can thus be Generated from:
            // qo(F) = CF (PRF^2 - Pwf^2)^n

            ValidateFutureInput(InputData);

            List<clsInFlowDataRow> Data = new List<clsInFlowDataRow>();

            double futureFlowCoefficient = DetermineFutureFlowCoefficient(InputData.FutureReservoirPressure.Value);


            for (double pressure = GenerationSettings.MinimumPressure; pressure <= InputData.FutureReservoirPressure.Value;
                pressure += GenerationSettings.PressureStepSize)
            {

                double PRF2 = InputData.FutureReservoirPressure.Value * InputData.FutureReservoirPressure.Value;
                double x = Math.Pow((PRF2 - pressure * pressure), WellExponent.Value);
                double flowrate = futureFlowCoefficient * x;

                Data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            GeneratedData = Data;


        }

    }
}
