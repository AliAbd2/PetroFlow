using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    internal class Vogel : IPRMethodBase
    {

        public override IPRMethodType MethodType => IPRMethodType.Vogel;

        public override string DisplayName => "Vogel";

        public override IPRMethodFeatures Features => 
            IPRMethodFeatures.Oil
            | IPRMethodFeatures.VerticalWell;

        const double VogelDivisor = 1.8;

        protected override void ValidateRawData(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            Validation.IsGreaterThanZero(input.ReservoirPressure, "Reservoir Pressure");

            //=====================
            // --- Test Data ---
            //=====================
            if (input.TestsData == null)
                throw new MissingRequiredInputException(IPRErrorMessages.MissingTestData);

            if (input.TestsData.Count < 1)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount(DisplayName, 1));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);

            if (input.TestsData.Count > 1)
                validationResult.AddWarning(
                    IPRErrorMessages.OnlyOneTestDataPointWillBeUsedWarning);

            //================================
            // --- Bubble Point Pressure ---
            //================================
            if (input.BubblePointPressure == null)
                validationResult.AddWarning(
                    IPRErrorMessages.BubblePointPressureNotProvidedWarning);
            else
                Validation.IsGreaterThanZero(input.BubblePointPressure, "Bubble Point Pressure");
        }

        private double CalculateMaxFlowRate(IPRInputData input)
        {

            // A method to calcualte the max flow rate.

            double pr = input.ReservoirPressure!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure; ;

            double pressureRatio = pwf / pr;

            return qTest / IPRGeneralFunctions.CalculateVogelTerm(pressureRatio);

        }

        private double CalculateProductivityIndex(IPRInputData input)
        {

            // Calculates the productivity index used by the
            // Vogel undersaturated reservoir formulation.

            double pr = input.ReservoirPressure!.Value;
            double pb = input.BubblePointPressure!.Value;
            double qTest = input.TestsData!.First().FlowRate;
            double pwf = input.TestsData!.First().BottomHolePressure;

            if (pwf >= input.BubblePointPressure.Value)
                // calculating J using linear productivity index equation: J = qo / (Pr - Pwf).
                return IPRGeneralFunctions.ProductivityIndex(qTest,
                    pr, pwf);
            else
            {
                // calculating J using J = qo / (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]).
                double pressureRatio = pwf / pb;

                double bubblePointVogelTerm = pr
                    - pb
                    + (pb / VogelDivisor) 
                    * IPRGeneralFunctions.CalculateVogelTerm(pressureRatio);

               return qTest / bubblePointVogelTerm;

            }

        }

        private List<FlowDataRow> GenerateIPR_SaturatedReservoir(IPRInputData input)
        {

            // A method to generate the IPR data for a saturated reservoir using vogel's method.

            List<FlowDataRow> dataRows = new(); // a list to store the IPR data.

            // Calculate max flow rate:
            double maxFlowRate = CalculateMaxFlowRate(input);

            double pr = input.ReservoirPressure!.Value;
            double minPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;


            for (double pressure = minPressure;
                pressure <= pr; 
                pressure += pressureStepSize)
            {

                double pressureRatio = pressure / pr;

                double flowRate = maxFlowRate
                    * IPRGeneralFunctions.CalculateVogelTerm(pressureRatio);

                dataRows.Add(new FlowDataRow(pressure, flowRate));


            }

            return dataRows;


        }

        private List<FlowDataRow> GenerateIPR_UndersaturatedReservoir(IPRInputData input)
        {

            // A fuction to generate the IPR data for an undersaturated reservoir using vogel's method.

            List<FlowDataRow> data = new();

            // Calculte Productivity Index:
            double productivityIndex = CalculateProductivityIndex(input);

            double pb = input.BubblePointPressure!.Value;
            double pr = input.ReservoirPressure!.Value;
            double minPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;

            // Calcuating the bubble point flowrate by using the bubble point pressure as flowing pressure.
            double bubblePointFlowRate = IPRGeneralFunctions.LinearFlowRate(productivityIndex,
                pr, pb);

            // Generating the IPR data:
            for (double pressure = minPressure; pressure <= pr;
                pressure += pressureStepSize)
            {

                double flowrate;

                if (pressure > pb)
                    // calculate the flowrate using qo = j(pr - pwf).
                    flowrate = IPRGeneralFunctions.LinearFlowRate(productivityIndex, pr, pressure);
                else
                {
                    // calculate the flowrate using qo = qb + (J * pb / 1.8) [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    double pressureRatio = pressure / pb;

                    double vogelTerm = IPRGeneralFunctions.CalculateVogelTerm(pressureRatio); 

                    flowrate = bubblePointFlowRate
                        + (productivityIndex
                        * pb / VogelDivisor) 
                        * vogelTerm;


                }

                data.Add(new FlowDataRow(pressure, flowrate));
            
            }

            return data;


        }

        protected override List<FlowDataRow> ComputeIPR(IPRInputData input)
        {

            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            bool isSaturated =
                !input.BubblePointPressure.HasValue ||
                input.ReservoirPressure!.Value <= input.BubblePointPressure.Value;

            if (isSaturated)
                return GenerateIPR_SaturatedReservoir(input);
            else
                return GenerateIPR_UndersaturatedReservoir(input);

        }

    }

}
