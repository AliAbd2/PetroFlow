using PetroFlow_BusinessLayer.General_Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // This class implements the Jones, Blount, and Glaze method for generating the
    // Inflow Performance Relationship (IPR) for oil wells.
    //
    // Their method is simple: they rearrange the following equation:
    //
    // qo = (0.00708 * ko * h * (Pr - Pwf)) / (μo * Bo * [ln(0.472 * re / rw) + S])
    //
    // into the following form:
    //
    // Pr - Pwf = A * qo + B * qo²
    //
    // By dividing both sides by qo:
    //
    // (Pr - Pwf) / qo = A + B * qo
    //
    // which represents a straight-line equation.
    //
    // Where:
    // A = intercept
    // B = slope
    //
    // Using the least-squares method, the constants A and B are determined.
    // The oil flow rate (qo) can then be calculated using:
    //
    // qo = (-A + √(A² + 4B(Pr - Pwf))) / (2B)
    internal class JonesBlountGlaze : IPRMethodBase
    {

        public override IPRMethodType MethodType => IPRMethodType.Jones_Blount_Glaze;

        public override string DisplayName => "Jones-Blount-Glaze";

        public override IPRMethodFeatures Features =>
            IPRMethodFeatures.Oil
            | IPRMethodFeatures.VerticalWell;

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

            if (input.TestsData.Count < 2)
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataCount(DisplayName, 2));

            if (input.TestsData.Any(x => x.FlowRate <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataFlowRate);

            if (input.TestsData.Any(x => x.BottomHolePressure <= 0))
                throw new InvalidParameterException(IPRErrorMessages.InvalidTestDataBottomHolePressure);

            if (input.TestsData.Any(x => x.BottomHolePressure >= input.ReservoirPressure))
                throw new InvalidParameterException(
                    IPRErrorMessages.InvalidTestDataBottomHolePressureGreaterThanReservoirPressure);


        }

        private (double slope, double intercept) DetermineSlopeIntercept(IPRInputData inputData)
        {

            // A method to Determine Slope(B) and Intercept(A) using least squares method.

            List<FlowDataRow> testsData = inputData.TestsData!.ToList();
            double pr = inputData.ReservoirPressure!.Value;

            double[] q = testsData.Select(x => x.FlowRate).ToArray();
            double[] piTerm = testsData.Select(x =>
            (pr - x.BottomHolePressure) / x.FlowRate).ToArray();

            (double slope, double intercept) = MathUtilities.LeastSquaresLineFit(q, piTerm);

            return (slope, intercept);


        }

        protected override List<FlowDataRow> ComputeIPR(IPRInputData input)
        {

            // A method to generate the IPR using Jones, Blount, and Glaze method.


            // This method will assume Bottom-Hole Flowing Pressure and calcualte Flow Rate Using:
            // qo = (-A + √(A² + 4B(Pr - Pwf))) / (2B)
            // Where:
            // qo : Oil Flow Rate.
            // Pr : Reservoir Pressure.
            // Pwf: Bottom Hole Flowing Pressure.
            // A  : Intercept.
            // B  : Slope.


            List<FlowDataRow> dataRows = new List<FlowDataRow>();

            double flowRate;

            (double slope, double intercept) = DetermineSlopeIntercept(input);

            const double MinSlopeTolerance = 1e-10;

            if (Math.Abs(slope) < MinSlopeTolerance)
            {
                throw new InvalidParameterException(
                    new ErrorMessage(
                        "Invalid Jones-Blount-Glaze Parameters",
                        "The calculated slope (B) is too close to zero. " +
                        "This results in an undefined Jones-Blount-Glaze equation and " +
                        "may indicate insufficient variation in the test data."
                    ));
            }

            double pr = input.ReservoirPressure!.Value;

            double minPressure = input.GenerationSettings!.MinimumPressure;
            double pressureStepSize = input.GenerationSettings.PressureStepSize;


            for (double pressure = minPressure; pressure <= pr;
                pressure += pressureStepSize)
            {

                double numerator = - intercept 
                    + Math.Sqrt(intercept * intercept + 4 * slope * (pr - pressure));

                flowRate = numerator / (2 * slope);

                dataRows.Add(new FlowDataRow(pressure, flowRate));

            }

            return dataRows;

        }


    }
}
