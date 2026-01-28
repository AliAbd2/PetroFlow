using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Transactions;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods
{

    // A class to store Vogel's equations to solve the IPR using Vogel's method.

    // Vogel's method can be applied under the following characteristics:
    /*
     * 1. vertical oil well.
     * 2. One stabilized Test (qo, Pwf).
     * 3. Zero skin factor.
     * 4. saturated and unsaturated Reservoir.
     * 
     */

    public class clsVogel : IIPRMethod
    {

        public string Name { get; set; }

        public enIPRMethodType MethodType { get { return enIPRMethodType.Vogel; } }

        public double ReservoirPressure { get; set; }

        public double? BubblePointPressure { get; set; }

        public List<clsInFlowDataRow> TestsData { get; set; }

        public List<clsInFlowDataRow> GeneratedData { get; set; }

        public clsCurvePlotSettings CurvePlotSetting { get; set; }

        public bool IsInputValid { get; set; }

        private double MaxFlowRate;

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

        public clsVogel()
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

            IsInputValid = true;

            return validationResult;
        }

        private void CalculateMaxFlowRate()
        {
            
            // A method to calcualte the max flow rate.

            double x = TestsData[0].BottomHolePressure / ReservoirPressure; // a variable to store the pwf/ pr.
            MaxFlowRate = TestsData[0].FlowRate / (1 - 0.2 * (x) - 0.8 * Math.Pow(x, 2)); // calculate the maximum flow rate qo(max) or AOF.

        }

        private void CalculateProductivityIndex()
        {

            // A method to calculate the productivity index.

            if (BubblePointPressure == null || TestsData[0].BottomHolePressure >= BubblePointPressure)
                // calculating J using linear productivity index equation: J = qo / (Pr - Pwf).
                ProductivityIndex =  clsIPRGeneralFunctions.ProductivityIndex(TestsData[0].FlowRate, ReservoirPressure, TestsData[0].BottomHolePressure);
            else
            {
                // calculating J using J = qo / (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]).
                double x = TestsData[0].BottomHolePressure / (double)BubblePointPressure; // pwf / pb
                double y = ReservoirPressure - (double)BubblePointPressure +
                    ((double)BubblePointPressure / 1.8) * (1 - 0.2 * x - 0.8 * Math.Pow(x, 2)); // (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ])

                ProductivityIndex = TestsData[0].FlowRate / y;

            }

        }

        private List<clsInFlowDataRow> GenerateIPR_SaturatedReservoir()
        {

            // A method to generate the IPR data for a saturated reservoir using vogel's method.

            List<clsInFlowDataRow> dataRows = new List<clsInFlowDataRow>(); // a list to store the IPR data.

            // Calculate max flow rate:
            CalculateMaxFlowRate();

            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double y = pressure / ReservoirPressure; // a variable to store the pwf/ pr.

                double FlowRate = MaxFlowRate * (1 - 0.2 * (y) - 0.8 * Math.Pow(y, 2));

                dataRows.Add(new clsInFlowDataRow(pressure, FlowRate));


            }

            return dataRows;


        }

        private List<clsInFlowDataRow> GenerateIPR_UnderSaturatedReservoir()
        {

            // A fuction to generate the IPR data for an undersaturated reservoir using vogel's method.

            List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

            // Calculte Productivity Index:
            CalculateProductivityIndex();

            // Calcuating the bubble point flowrate by using the bubble point pressure as flowing pressure.
            double BubblePointFlowRate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex, ReservoirPressure, (double)BubblePointPressure);

            // Generating the IPR data:
            for (int pressure = 0; pressure <= ReservoirPressure; pressure++)
            {

                double flowrate;

                if (pressure > BubblePointPressure)
                    // calculate the flowrate using qo = j(pr - pwf).
                    flowrate = clsIPRGeneralFunctions.LinearFlowRate(ProductivityIndex, ReservoirPressure, pressure);
                else
                {
                    // calculate the flowrate using qo = qb + (J * pb / 1.8) [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    double x = pressure / (double)BubblePointPressure;// pwf / pb
                    double y = 1 - 0.2 * x - 0.8 * Math.Pow(x, 2); // [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    flowrate = BubblePointFlowRate + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;


                }

                data.Add(new clsInFlowDataRow(pressure, flowrate));
            
            }

            return data;


        }

        public void GenerateIPR()
        {

            if (!IsInputValid)
                throw new InvalidOperationException("Invalid operation: " +
                    "Calculation method was called before input data was set. Call SetInputData() first.");

            if (IsSaturated)
                GeneratedData = GenerateIPR_SaturatedReservoir();
            else
                GeneratedData = GenerateIPR_UnderSaturatedReservoir();

        }


    }

}
