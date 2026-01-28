using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
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

        private double Slope;

        private double PresentFlowCoefficient;

        private double ProductivityIndex;

        private double PressureStepSize;

        private double MinimumPressure;

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

            CurvePlotSetting = new clsCurvePlotSettings();

        }
        public clsValidationResult SetInputData(Dictionary<enIPRData, object> inputData)
        {
            clsValidationResult validationResult = new();

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            if (!inputData.TryGetValue(enIPRData.ReservoirPressure, out var pressureObj))
                throw new exMissingRequiredInputException(
                    "Cannot generate IPR: Reservoir pressure has not been provided.");

            if (pressureObj is not double reservoirPressure)
                throw new exInvalidIPRParameterException(
                    "Invalid reservoir pressure: Expected a numeric value.");

            if (reservoirPressure <= 0)
                throw new exInvalidIPRParameterException(
                    "Invalid reservoir pressure: A positive value greater than zero is required.");

            //=====================
            // --- Test Data ---
            //=====================

            if (!inputData.TryGetValue(enIPRData.TestData, out var testDataObj))
                throw new exMissingRequiredInputException(
                    "Cannot generate IPR: Test data has not been provided.");

            if (testDataObj is not List<clsInFlowDataRow> rows)
                throw new exInvalidIPRParameterException(
                    "Invalid test data: Expected a list of InFlow data rows.");

            if (rows.Count == 0)
                throw new exInvalidIPRParameterException(
                    "Invalid test data: At least one test data row is required.");

            if (rows.Any(x => x.FlowRate <= 0))
                throw new exInvalidIPRParameterException(
                    "Invalid test data: One or more flow rates are zero or negative.");

            if (rows.Any(x => x.BottomHolePressure <= 0))
                throw new exInvalidIPRParameterException(
                    "Invalid test data: One or more bottom hole pressures are zero or negative.");

            //================================
            // --- Bubble Point Pressure ---
            //================================

            double? bubblePointPressure = null;

            if (!inputData.TryGetValue(enIPRData.BubblePointPressure, out var bubbleObj) || bubbleObj == null)
            {
                validationResult.Warnings.Add(
                    "Bubble point pressure was not provided. Reservoir will be assumed saturated.");
            }
            else
            {
                if (bubbleObj is not double bp)
                    throw new exInvalidIPRParameterException(
                        "Invalid bubble point pressure: Expected a numeric value.");

                if (bp <= 0)
                    throw new exInvalidIPRParameterException(
                        "Invalid bubble point pressure: A positive value greater than zero is required.");

                bubblePointPressure = bp;
            }

            ReservoirPressure = reservoirPressure;
            TestsData = rows;
            BubblePointPressure = bubblePointPressure;

            return validationResult;
        }

        private void DetermineslopeIntercept()
        {
            // This is a method to determaine the slope.
            // the method to determine the slope is depends on the number of tests:
            // if there is one test only then the slop is assumed to be = 1.
            // if two tests are availble then use slope = delta y / delta x formula.
            // if three or more tests are available then use the least squares method.

            if (TestsData.Any(t =>
                t.FlowRate <= 0 ||
                ReservoirPressure * ReservoirPressure <= t.BottomHolePressure * t.BottomHolePressure))
            {
                throw new InvalidOperationException(
                    "Invalid test data: logarithm arguments must be positive.");
            }

            // detemain the number of recoreds.
            int n = TestsData.Count;

            double pr2 = ReservoirPressure * ReservoirPressure;

            if (n == 1)
            {
                Slope = 1;
                return;
            }

            //if (n == 2)
            //{

            //    double flowrate1 = TestsData[0].FlowRate;
            //    double flowrate2 = TestsData[1].FlowRate;

            //    double pressure1 = TestsData[0].BottomHolePressure;
            //    double pressure2 = TestsData[1].BottomHolePressure;

            //    double deltaLogQ =
            //        Math.Log10(flowrate2) - Math.Log10(flowrate1);

            //    double deltaLogX =
            //        Math.Log10(pr2 - pressure2 * pressure2) -
            //        Math.Log10(pr2 - pressure1 * pressure1);

            //    Slope = deltaLogQ / deltaLogX;



            //    return;

            //}

            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // where n is the number of records.

            List<double> logX = TestsData.Select(x => Math.Log10(
                pr2 - x.BottomHolePressure * x.BottomHolePressure)).ToList();
            List<double> logY = TestsData.Select(y => Math.Log10(y.FlowRate)).ToList();

            double sumX = logX.Sum();
            double sumY = logY.Sum();
            double sumX2 = logX.Sum(x => x * x);
            double sumXY = logX.Zip(logY, (x, y) => x * y).Sum();

            double m = (n * sumXY - sumX * sumY)
                     / (n * sumX2 - sumX * sumX);

            double intercept = (sumY - Slope * sumX) / n;

            Slope = m;

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


            if (flowRate <= 0 || bottomHolePressure <= 0 || ReservoirPressure <= 0)
                throw new InvalidOperationException("Invalid pressures or flow rate.");

            if (!IsSaturated && BubblePointPressure < 0)
                throw new InvalidOperationException("Bubble point pressure must be positive.");



            // Case 1:
            if (IsSaturated)
            {
                double x = Math.Pow((bottomHolePressure / ReservoirPressure), 2); // (Pwf / Pr)^2
                double y = ReservoirPressure * Math.Pow(1 - x, Slope); // (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
                productivityIndex = 2 * flowRate / y; 

            }

            // Case 2:
            else if (!IsSaturated && bottomHolePressure > BubblePointPressure)
                productivityIndex = flowRate / (ReservoirPressure - bottomHolePressure);

            // Case 3:
            else
            {

                double x = Math.Pow((bottomHolePressure / (double)BubblePointPressure), 2); // (Pwf / Pb)^2
                double y = Math.Pow(1 - x, Slope); // [1 - (Pwf / Pb)^2]^n
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

            for (double pressure = MinimumPressure; 
                pressure <= ReservoirPressure; pressure += PressureStepSize)
            {
                // q = J * Pr / 2 * [ 1 - (Pwf / Pr)^d2 * n ]

                double x = 1 - Math.Pow(pressure / ReservoirPressure, 2 * Slope); // [ 1 - (Pwf / Pr)^d2 * n ]
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

            DetermineProductivityIndex();

            for (double pressure = MinimumPressure;
                pressure <= ReservoirPressure; pressure += PressureStepSize)
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

                    double x = Math.Pow((1 - Math.Pow((pressure / (double)BubblePointPressure), 2)), Slope); //[ 1 - (Pwf / Pb)^2 ]^n
                    double y = ProductivityIndex * (double)BubblePointPressure / 2 * x; // J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n
                    flowRate = ProductivityIndex * (ReservoirPressure - (double)BubblePointPressure) + y;

                }

                dataRows.Add(new clsInFlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }

        public void GenerateIPR()
        {

            if (IsSaturated)
                GeneratedData = GenerateIPR_SaturatedReservoir();
            else
                GeneratedData = GenerateIPR_UnderSaturatedReservoir();

        }

        private double DetermineFutureFlowCoefficient(double futureReservoirPressure)
        {

            // A method to calcualte Future Flow Coefficient.

            // The Future Flow Corfficient can be calculated using the following equation:
            // CF = CP * (PRF / PrP)

            DetermineslopeIntercept();

            return PresentFlowCoefficient * (futureReservoirPressure / ReservoirPressure);

        }

        public void GenerateFutureIPR(double futureReservoirPressure,
            double futureOilRelativePermeability, double futureOilFormationVolumeFactor, double FutureOilViscosity)
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

            List<clsInFlowDataRow> Data = new List<clsInFlowDataRow>();

            double futureFlowCoefficient = DetermineFutureFlowCoefficient(futureReservoirPressure);


            for (double pressure = MinimumPressure; pressure <= futureReservoirPressure;
                pressure += PressureStepSize)
            {

                double PRF2 = futureReservoirPressure * futureReservoirPressure;
                double x = Math.Pow((PRF2 - pressure * pressure), Slope);
                double flowrate = futureFlowCoefficient * x;

                Data.Add(new clsInFlowDataRow(pressure, flowrate));

            }

            GeneratedData = Data;


        }

    }
}
