using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
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

    public class clsVogel : IPRMethodBase
    {

        public clsVogel()
        {

        }

        protected override void ValidateRawData(IPRInputData inputData,
            ref NodalAnalysisValidationResult validationResult)
        {

            //=============================
            // --- Reservoir Pressure ---
            //=============================
            if (inputData.ReservoirPressure == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Reservoir pressure has not been provided.");

            if (inputData.ReservoirPressure <= 0)
                throw new InvalidParameterException(
                    "Invalid reservoir pressure: A positive value greater than zero is required.");

            //=====================
            // --- Test Data ---
            //=====================
            if (inputData.TestsData == null)
                throw new MissingRequiredInputException(
                    "Cannot generate IPR: Test data has not been provided.");

            if (inputData.TestsData.Count < 1)
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
                    "Invalid test data: One or more bottom hole pressures are greater than reservoir pressure.");

            if (inputData.TestsData.Count > 1)
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


        }

        private double CalculateMaxFlowRate(IPRInputData inputData)
        {
            
            // A method to calcualte the max flow rate.

            double x = inputData.TestsData[0].BottomHolePressure / inputData.ReservoirPressure.Value; // a variable to store the pwf/ pr.
            return inputData.TestsData[0].FlowRate / (1 - 0.2 * (x) - 0.8 * Math.Pow(x, 2)); // calculate the maximum flow rate qo(max) or AOF.

        }

        private double CalculateProductivityIndex(IPRInputData input)
        {

            // A method to calculate the productivity index.

            if (input.BubblePointPressure == null || 
                input.TestsData[0].BottomHolePressure >= input.BubblePointPressure.Value)
                // calculating J using linear productivity index equation: J = qo / (Pr - Pwf).
                return IPRGeneralFunctions.ProductivityIndex(input.TestsData[0].FlowRate,
                    input.ReservoirPressure.Value, input.TestsData[0].BottomHolePressure);
            else
            {
                // calculating J using J = qo / (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]).
                double x = input.TestsData[0].BottomHolePressure / input.BubblePointPressure.Value; // pwf / pb
                double y = input.ReservoirPressure.Value - input.BubblePointPressure.Value +
                    (input.BubblePointPressure.Value / 1.8) * (1 - 0.2 * x - 0.8 * Math.Pow(x, 2)); // (pr - pb + pb / 1.8 [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ])

               return input.TestsData[0].FlowRate / y;

            }

        }

        private List<InFlowDataRow> GenerateIPR_SaturatedReservoir(IPRInputData input)
        {

            // A method to generate the IPR data for a saturated reservoir using vogel's method.

            List<InFlowDataRow> dataRows = new(); // a list to store the IPR data.

            // Calculate max flow rate:
            double MaxFlowRate = CalculateMaxFlowRate(input);

            double ReservoirPressure = input.ReservoirPressure.Value;

            for (double pressure = input.GenerationSettings.MinimumPressure;
                pressure <= ReservoirPressure; 
                pressure += input.GenerationSettings.PressureStepSize)
            {

                double y = pressure / ReservoirPressure; // a variable to store the pwf/ pr.

                double FlowRate = MaxFlowRate * (1 - 0.2 * (y) - 0.8 * Math.Pow(y, 2));

                dataRows.Add(new InFlowDataRow(pressure, FlowRate));


            }

            return dataRows;


        }

        private List<InFlowDataRow> GenerateIPR_UnderSaturatedReservoir(IPRInputData input)
        {

            // A fuction to generate the IPR data for an undersaturated reservoir using vogel's method.

            List<InFlowDataRow> data = new();

            // Calculte Productivity Index:
            double ProductivityIndex = CalculateProductivityIndex(input);

            double BubblePointPressure = input.BubblePointPressure.Value;
            double ReservoirPressure = input.ReservoirPressure.Value;

            // Calcuating the bubble point flowrate by using the bubble point pressure as flowing pressure.
            double BubblePointFlowRate = IPRGeneralFunctions.LinearFlowRate(ProductivityIndex,
                ReservoirPressure, BubblePointPressure);

            // Generating the IPR data:
            for (double pressure = input.GenerationSettings.MinimumPressure; pressure <= ReservoirPressure;
                pressure += input.GenerationSettings.PressureStepSize)
            {

                double flowrate;

                if (pressure > input.BubblePointPressure.Value)
                    // calculate the flowrate using qo = j(pr - pwf).
                    flowrate = IPRGeneralFunctions.LinearFlowRate(ProductivityIndex, ReservoirPressure, pressure);
                else
                {
                    // calculate the flowrate using qo = qb + (J * pb / 1.8) [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    double x = pressure / (double)BubblePointPressure;// pwf / pb
                    double y = 1 - 0.2 * x - 0.8 * Math.Pow(x, 2); // [ 1 - 0.2(pwf / pb) - 0.8(pwf / pb)^2 ]

                    flowrate = BubblePointFlowRate + (ProductivityIndex * (double)BubblePointPressure / 1.8) * y;


                }

                data.Add(new InFlowDataRow(pressure, flowrate));
            
            }

            return data;


        }


        protected override List<InFlowDataRow> ComputeIPR(IPRInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            bool IsSaturated;
            // Indicates whether the reservoir is saturated (i.e., Pr ≤ Pb).
            // If the bubble point pressure is not provided, the reservoir is treated as saturated.
            if (input.BubblePointPressure.HasValue)
                IsSaturated = input.ReservoirPressure.Value <=
                    input.BubblePointPressure.Value;
            else
                IsSaturated = true;




            if (IsSaturated)
                return GenerateIPR_SaturatedReservoir(input);
            else
                return GenerateIPR_UnderSaturatedReservoir(input);

        }


    }

}
