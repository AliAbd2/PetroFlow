using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Main_Classes;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Data;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis
{
    public class NodalAnalysis
    {

        private InFlowPerformanceRelationship? _IPR;

        private VerticalLiftingPreformance? _VLP;

        public NodalAnalysis(IPRMethodBase? iPRMethod, IVLPModel? vLPModel)
        {

            _IPR = new(iPRMethod);
            _VLP = new(vLPModel);


        }

        public List<FlowDataRow> GenerateIPR(IPRInputData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            return _IPR.GenerateIPR(input, ref validationResult);

        }

        public List<FlowDataRow> GenerateFututreIPR(IPRInputData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            return _IPR.GenerateFutureIPR(input, ref validationResult);

        }


        public List<FlowDataRow> GenerateVLP(VLPInputData input,
            ref NodalAnalysisValidationResult validationResult)
        {

            return _VLP.GenerateOutFlow(input, ref validationResult);

        }

        public List<FlowDataRow> GeneraterVLPFromIPR(VLPInputData input, List<FlowDataRow> iprData,
            ref NodalAnalysisValidationResult validationResult)
        {

            return _VLP.GenerateOutFlowForInFlow(input, iprData, ref validationResult);

        }


        public static FlowDataRow GetOperatingPoint(List<FlowDataRow> IPR, List<FlowDataRow> VLP)
        {

            double previousDifference = IPR[0].BottomHolePressure - VLP[0].BottomHolePressure;

            for (int index = 1; index < IPR.Count; index++)
            {
                double currentDifference =
                    IPR[index].BottomHolePressure - VLP[index].BottomHolePressure;


                if (currentDifference == 0)
                {
                    return new FlowDataRow(
                        IPR[index].BottomHolePressure,
                        IPR[index].FlowRate);
                }


                if (Math.Sign(previousDifference) != Math.Sign(currentDifference))
                {
                    double q1 = IPR[index - 1].FlowRate;
                    double q2 = IPR[index].FlowRate;

                    double fraction =
                        Math.Abs(previousDifference) /
                        (Math.Abs(previousDifference) + Math.Abs(currentDifference));

                    double operatingFlowRate =
                        q1 + fraction * (q2 - q1);

                    double operatingPressure =
                        IPR[index - 1].BottomHolePressure +
                        fraction *
                        (IPR[index].BottomHolePressure -
                         IPR[index - 1].BottomHolePressure);

                    return new FlowDataRow(
                        operatingPressure,
                        operatingFlowRate);
                }

                previousDifference = currentDifference;
            }

            throw new InvalidOperationException(
                "There is no Operating Point: the IPR and VLP curves do not intersect.");
        }


    }
}
