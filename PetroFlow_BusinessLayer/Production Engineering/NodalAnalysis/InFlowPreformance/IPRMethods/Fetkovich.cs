using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.Errors_and_Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    internal class Fetkovich : IPRMethodBase, IFuturePredictable
    {

        public override IPRMethodType MethodType => IPRMethodType.Fetkovich;

        public override string DisplayName => "Fetkovich";

        public override IPRMethodFeatures Features => 
            IPRMethodFeatures.Oil
            | IPRMethodFeatures.FuturePrediction
            | IPRMethodFeatures.VerticalWell;

        public override IPRRequirements InputRequirements => new()
        {

            Present = IPRInputFields.ReservoirPressure
            | IPRInputFields.BubblePointPressure
            | IPRInputFields.WellExponent,

            Future = IPRFutureInputFields.FutureReservoirPressure
            | IPRFutureInputFields.FlowCoefficient,

            TestData = IPRTestDataRequirement.MultiplePoints

        };

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
                            $"Well exponent is outside the recommended range" +
                            $" for the Fetkovich model ({MinRecommendedExponent}–{MaxRecommendedExponent})."));
                }


            }

            //=====================
            // --- Test Data ---
            //=====================
            if (input.TestsData == null)
                throw new MissingRequiredInputException(IPRErrorMessages.MissingTestData);

            if (input.TestsData.Count < 3 && !input.WellExponent.HasValue)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount("Fetkovich", 3));

            if (input.TestsData.Count < 1 &&
                input.WellExponent.HasValue)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount("Fetkovich", 1));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);

            if (input.TestsData.Count > 1 && input.WellExponent.HasValue)
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

        private (double WellExponent, double FlowCoefficient) 
            DetermineWellExponentFlowCoefficient(IPRInputData input)
        {
            // This is a method to determaine the Well Exponent and Flow Coefficitent for Fetckovich method.

            if (input.WellExponent.HasValue && input.FlowCoefficient.HasValue)
                return (input.WellExponent.Value, input.FlowCoefficient.Value);

            double reservoirPressure = input.ReservoirPressure!.Value;
            List<FlowDataRow> testsData = input.TestsData;

            double pr2 = reservoirPressure * reservoirPressure;

            double[] logX = testsData!.Select(x => Math.Log10(
                pr2 - x.BottomHolePressure * x.BottomHolePressure)).ToArray();
            double[] logY = testsData!.Select(y => Math.Log10(y.FlowRate)).ToArray();

            double slope = 0;
            double intercept = 0;

            (slope, intercept) = MathUtilities.LeastSquaresLineFit(logX, logY);

            double wellExponent = slope;

            double flowCoefficient = Math.Pow(10, intercept);

            return (wellExponent, flowCoefficient);
            
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

            double pwf = input.TestsData[0].BottomHolePressure;
            double flowRate = input.TestsData[0].FlowRate;

            double pr = input.ReservoirPressure!.Value;
            double pb = input.BubblePointPressure!.Value;


            // Case 1:
            if (isSaturated)
            {
                double pressureRatio = pwf / pr;
                double pressureRatio2 = pressureRatio * pressureRatio;
                double fetkovichTerm = Math.Pow(1 - pressureRatio2, wellExponent);
                productivityIndex = 2 * flowRate / (pr * fetkovichTerm);

            }

            // Case 2:
            else if (!isSaturated && pwf > pb)
                productivityIndex = IPRGeneralFunctions.ProductivityIndex(flowRate, pr, pwf);

            // Case 3:
            else
            {

                double pressureRatio = pwf / pb;
                double pressureRatio2 = pressureRatio * pressureRatio;
                double fetkovichTerm = Math.Pow(1 - pressureRatio2, wellExponent);
                double denominator = pr - pb 
                    + pb / 2 * fetkovichTerm;
                productivityIndex = flowRate / denominator;
            }


            return productivityIndex;

        }

        private List<FlowDataRow> GenerateIPR_SaturatedReservoir(IPRInputData input,
            double wellExponent, double productivityIndex)
        {

            // A method to generate the IPR data for saturated reservoir using Fetkovich method.

            List<FlowDataRow> dataRows = new List<FlowDataRow>();
            double flowRate = 0;

            double pr = input.ReservoirPressure!.Value;
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;


            for (double pressure = minimumPressure; 
                pressure <= pr; pressure += pressureStepSize)
            {
                // q = J * Pr / 2 * [ 1 - (Pwf / Pr)^2 ]^n

                double pressureRatio = pressure / pr;
                double pressureRatio2 = pressureRatio * pressureRatio;
                double fetkovichTerm = Math.Pow(1 - pressureRatio2, wellExponent); // [ 1 - (Pwf / Pr)^2 ]^n
                flowRate = productivityIndex * pr / 2 * fetkovichTerm;

                dataRows.Add(new FlowDataRow(pressure, flowRate));


            }

            return dataRows;


        }

        private List<FlowDataRow> GenerateIPR_UnderSaturatedReservoir(IPRInputData input,
            double wellExponent, double productivityIndex)
        {


            // A method to generate the IPR data for undersaturated reservoir using Fetkovich method.


            List<FlowDataRow> dataRows = new List<FlowDataRow>();
            double flowRate = 0;

            double pr = input.ReservoirPressure!.Value;
            double pb = input.BubblePointPressure!.Value;
            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;

            for (double pressure = minimumPressure;
                pressure <= pr; pressure += pressureStepSize)
            {

                if (pressure > pb)
                {
                    // q = J * (Pr - Pwf)
                    flowRate = IPRGeneralFunctions.LinearFlowRate(productivityIndex,
                        pr, pressure);

                }
                else
                {
                    // q = J * (Pr - Pb) + J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n

                    double pressureRatio = pressure / pb;
                    double pressureRatio2 = pressureRatio * pressureRatio;
                    double fetkovichTerm = Math.Pow(1 - pressureRatio2, wellExponent); // [ 1 - (Pwf / Pr)^2 ]^n
                    double y = productivityIndex * pb / 2 * fetkovichTerm; // J * Pb / 2 * [ 1 - (Pwf / Pb)^2 ]^n
                    flowRate = productivityIndex * (pr - pb) + y;

                }

                dataRows.Add(new FlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }

        protected override List<FlowDataRow> ComputeIPR(IPRInputData input)
        {

            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            bool isSaturated = IPRGeneralFunctions.IsSaturatedReservoir(input);

            (double wellExponent, _) = DetermineWellExponentFlowCoefficient(input);

            double productivityIndex = DetermineProductivityIndex(input,
                isSaturated, wellExponent);

            if (isSaturated)
                return GenerateIPR_SaturatedReservoir(input, wellExponent,
                    productivityIndex);
            else
                return GenerateIPR_UnderSaturatedReservoir(input, wellExponent,
                    productivityIndex);

        }

        public void ValidateFutureRawDataInput(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            Validation.IsGreaterThanZero(input.ReservoirPressure, "Reservoir Pressure");

            //=====================================
            // --- Future Reservoir Pressure ---
            //=====================================
            Validation.IsGreaterThanZero(input.FutureReservoirPressure, "Future Reservoir Pressure");

            Validation.IsGreaterThan(input.ReservoirPressure, input.FutureReservoirPressure,
                "Future Reservoir Pressure", "Reservoir Pressure");

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
                            $"Well exponent is outside the recommended range" +
                            $" for the Fetkovich model ({MinRecommendedExponent}–{MaxRecommendedExponent})."));
                }


            }

            //=====================
            // --- Test Data ---
            //=====================
            if (input.TestsData == null)
                throw new MissingRequiredInputException(IPRErrorMessages.MissingTestData);

            if (input.TestsData.Count < 3 
                && !input.WellExponent.HasValue
                && !input.FlowCoefficient.HasValue)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount("Fetkovich", 3));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);


        }

        private double DetermineFutureFlowCoefficient(IPRInputData input,
            double presentFlowCoefficient)
        {

            // A method to calcualte Future Flow Coefficient.

            // The Future Flow Corfficient can be calculated using the following equation:
            // CF = CP * (PRF / PrP)


            return presentFlowCoefficient * 
                (input.FutureReservoirPressure!.Value / input.ReservoirPressure!.Value);

        }

        public List<FlowDataRow> GenerateFutureIPR(IPRInputData input,
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

            ValidateFutureRawDataInput(input, ref validationResult);

            (double wellExponent, double flowCoefficient) = DetermineWellExponentFlowCoefficient(input);

            List<FlowDataRow> data = new List<FlowDataRow>();

            double futureFlowCoefficient = DetermineFutureFlowCoefficient(input,
                flowCoefficient);

            double minimumPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings!.PressureStepSize;

            double prf2 = input.FutureReservoirPressure!.Value * input.FutureReservoirPressure.Value;

            for (double pressure = minimumPressure; pressure <= input.FutureReservoirPressure.Value;
                pressure += pressureStepSize)
            {

                double fetkovichTerm = Math.Pow(prf2 - pressure * pressure, wellExponent);
                double flowrate = futureFlowCoefficient * fetkovichTerm;

                data.Add(new FlowDataRow(pressure, flowrate));

            }

            return data;


        }

    }
}
