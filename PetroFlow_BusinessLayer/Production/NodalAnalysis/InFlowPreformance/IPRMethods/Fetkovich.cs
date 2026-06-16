using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
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
    //   • Δlog(qo) / Δlog(Pr² − Pwf²) when two test points are available
    //   • Least-squares linear regression when three or more test points exist
    //
    // After determining n, the class calculates the productivity index (J)
    // using Fetkovich’s formulations for:
    //   • Saturated reservoirs
    //   • Under-saturated reservoirs (with or without test points above Pb)
    //
    // Finally, the class generates IPR data over a specified pressure range
    // using the calculated productivity index and exponent.
    //
    // The class supports both saturated and under-saturated reservoirs,
    // with optional bubble-point pressure input.

    public class Fetkovich : IPRMethodBase, IFuturePredictable
    {



        public Fetkovich()
        {

  

        }

        protected override void ValidateRawData(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            Validation.IsGreaterThanZero(input.ReservoirPressure, "Reservoir Pressure");

            //=========================
            // --- Well Exponent ---
            //========================
            if (input.WellExponent.HasValue)
            {

                const double MinRecommendedExponent = 0.568;
                const double MaxRecommendedExponent = 1.0;

                if (input.WellExponent < MinRecommendedExponent ||
                    input.WellExponent > MaxRecommendedExponent)
                {
                    validationResult.AddWarning(
                        new ErrorMessage(
                            "Outside Recommended Range",
                            $"Well exponent is outside the recommended range for the Fetkovich model ({MinRecommendedExponent}–{MaxRecommendedExponent})."));
                }


            }

            //=====================
            // --- Test Data ---
            //=====================
            if (input.TestsData == null)
                throw new MissingRequiredInputException(IPRErrorMessages.MissingTestData);

            if (input.TestsData.Count < 3 && input.WellExponent == null)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount("Fetkovich", 3));

            if (input.TestsData.Count < 1 &&
                input.WellExponent != null &&
                input.PresentFlowCoefficient == null)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount("Fetkovich", 1));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);

            if (input.TestsData.Count > 1 && input.WellExponent == null)
                validationResult.AddWarning(IPRErrorMessages.OnlyOneTestDataPointWillBeUsedWarning);


            //================================
            // --- Bubble Point Pressure ---
            //================================
            if (input.BubblePointPressure == null)
            {
                validationResult.AddWarning(IPRErrorMessages.BubblePointPressureNotProvidedWarning);
            }
            else
            {

                Validation.IsGreaterThanZero(input.BubblePointPressure, "Bubble Point Pressure");

            }


        }

        private (double wellExponent, double flowCorfficient)
            DetermineSlopeAndFlowCoefficient(IPRInputData input)
        {
            // This is a method to determaine the slope.

            // Least Squares formulas:
            // slope = (n * sum(x*y) - sum(x) * sum(y)) / (n * sum(x^2) - sum(x)^2)
            // where n is the number of records.

            if (input.WellExponent.HasValue && input.FlowCoefficient.HasValue)
                return (input.WellExponent.Value,
                    input.FlowCoefficient.Value);

            double ReservoirPressure = input.ReservoirPressure.Value;
            List<InFlowDataRow> TestsData = input.TestsData;

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

            double WellExponent = m;

            double PresentFlowCoefficient = 1 / Math.Pow(10, intercept);

            return (WellExponent, PresentFlowCoefficient);
            
        }

        private double DetermineProductivityIndex(IPRInputData input, bool isSaturated,
            double wellExponent)
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

            double productivityIndex = 0;

            double bottomHolePressure = input.TestsData[0].BottomHolePressure;
            double flowRate = input.TestsData[0].FlowRate;

            double ReservoirPressure = input.ReservoirPressure.Value;
            double BubblePointPressure = input.BubblePointPressure.Value;


            // Case 1:
            if (isSaturated)
            {
                double x = Math.Pow((bottomHolePressure / ReservoirPressure), 2); // (Pwf / Pr)^2
                double y = ReservoirPressure * Math.Pow(1 - x, wellExponent); // (Pr * [ 1 - (Pwf / Pr)^2 ]^n)
                productivityIndex = 2 * flowRate / y; 

            }

            // Case 2:
            else if (!isSaturated && bottomHolePressure > BubblePointPressure)
                productivityIndex = flowRate / (ReservoirPressure - bottomHolePressure);

            // Case 3:
            else
            {

                double x = Math.Pow(bottomHolePressure / BubblePointPressure, 2); // (Pwf / Pb)^2
                double y = Math.Pow(1 - x, wellExponent); // [1 - (Pwf / Pb)^2]^n
                double z = (ReservoirPressure - (double)BubblePointPressure) +
                    (double)BubblePointPressure / 2 * y;
                productivityIndex = flowRate / z;
            }


            return productivityIndex;

        }

        private List<InFlowDataRow> GenerateIPR_SaturatedReservoir(IPRInputData input,
            double wellExponent, double ProductivityIndex)
        {

            // A method to generate the IPR data for saturated reservoir using Fetkovich method.


            List<InFlowDataRow> dataRows = new List<InFlowDataRow>();
            double flowRate = 0;

            double ReservoirPressure = input.ReservoirPressure.Value;
            IPRGenerationSettings GenerationSettings = input.GenerationSettings;


            for (double pressure = GenerationSettings.MinimumPressure; 
                pressure <= ReservoirPressure; pressure += GenerationSettings.PressureStepSize)
            {
                // q = J * Pr / 2 * [ 1 - (Pwf / Pr)^d2 * n ]

                double y = Math.Pow(pressure / ReservoirPressure, 2);
                double x = Math.Pow(1 - y, wellExponent); // [ 1 - (Pwf / Pr)^d2 * n ]
                flowRate = ProductivityIndex * ReservoirPressure / 2 * x;

                dataRows.Add(new InFlowDataRow(pressure, flowRate));


            }

            return dataRows;


        }

        private List<InFlowDataRow> GenerateIPR_UnderSaturatedReservoir(IPRInputData input,
            double wellExponent, double ProductivityIndex)
        {


            // A method to generate the IPR data for undersaturated reservoir using Fetkovich method.


            List<InFlowDataRow> dataRows = new List<InFlowDataRow>();
            double flowRate = 0;

            double ReservoirPressure = input.ReservoirPressure.Value;
            double BubblePointPressure = input.BubblePointPressure.Value;
            IPRGenerationSettings GenerationSettings = input.GenerationSettings;

            for (double pressure = GenerationSettings.MinimumPressure;
                pressure <= ReservoirPressure; pressure += GenerationSettings.PressureStepSize)
            {

                if (pressure > BubblePointPressure)
                {
                    // q = J * (Pr - Pwf)
                    flowRate = IPRGeneralFunctions.LinearFlowRate(ProductivityIndex,
                        ReservoirPressure, pressure);

                }
                else
                {
                    // q = J * (Pr - Pb) + J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n

                    double x = Math.Pow(1 - Math.Pow(pressure / BubblePointPressure, 2), wellExponent); //[ 1 - (Pwf / Pb)^2 ]^n
                    double y = ProductivityIndex * (double)BubblePointPressure / 2 * x; // J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n
                    flowRate = ProductivityIndex * (ReservoirPressure - (double)BubblePointPressure) + y;

                }

                dataRows.Add(new InFlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }

        protected override List<InFlowDataRow> ComputeIPR(IPRInputData input)
        {

            bool IsSaturated;
            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            if (input.BubblePointPressure.HasValue)
                IsSaturated = input.ReservoirPressure.Value <=
                    input.BubblePointPressure.Value;
            else
                IsSaturated = true;

            (double wellExponent, double flowCorfficient) FetkovichCoefficients =
                DetermineSlopeAndFlowCoefficient(input);

            double productivityIndex = DetermineProductivityIndex(input,
                IsSaturated, FetkovichCoefficients.wellExponent);

            if (IsSaturated)
                return GenerateIPR_SaturatedReservoir(input, FetkovichCoefficients.wellExponent,
                    productivityIndex);
            else
                return GenerateIPR_UnderSaturatedReservoir(input, FetkovichCoefficients.wellExponent,
                    productivityIndex);

        }

        public void ValidateFutureRawDataInput(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {


            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            Validation.IsGreaterThanZero(input.FutureReservoirPressure, "Future Reservoir Pressure");

            Validation.IsGreaterThan(input.ReservoirPressure, input.FutureReservoirPressure,
                "Future Reservoir Pressure", "Reservoir Pressure");

        }

        private double DetermineFutureFlowCoefficient(IPRInputData input,
            double PresentFlowCoefficient)
        {

            // A method to calcualte Future Flow Coefficient.

            // The Future Flow Corfficient can be calculated using the following equation:
            // CF = CP * (PRF / PrP)


            return PresentFlowCoefficient * 
                (input.FutureReservoirPressure.Value / input.ReservoirPressure.Value);

        }

        public List<InFlowDataRow> GenerateFutureIPR(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
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

            (double wellExponent, double flowCorfficient) FetkovichCoefficients =
                DetermineSlopeAndFlowCoefficient(input);

            ValidateFutureRawDataInput(input, ref validationResult);

            List<InFlowDataRow> Data = new List<InFlowDataRow>();

            double futureFlowCoefficient = DetermineFutureFlowCoefficient(input, 
                FetkovichCoefficients.flowCorfficient);
            IPRGenerationSettings GenerationSettings = input.GenerationSettings;


            for (double pressure = GenerationSettings.MinimumPressure; pressure <= input.FutureReservoirPressure.Value;
                pressure += GenerationSettings.PressureStepSize)
            {

                double PRF2 = input.FutureReservoirPressure.Value * input.FutureReservoirPressure.Value;
                double x = Math.Pow((PRF2 - pressure * pressure), FetkovichCoefficients.wellExponent);
                double flowrate = futureFlowCoefficient * x;

                Data.Add(new InFlowDataRow(pressure, flowrate));

            }

            return Data;


        }

    }
}
