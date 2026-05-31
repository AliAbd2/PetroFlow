using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.AbstractClasses.FlowRegime;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPGeneralEquations;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Components.FlowRegime
{
    public class DunsRosFlowRegimeDetector : FlowRegimeDetector
    {

        protected override void ValidateRawData(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult, VLPDerivedProperties? derivedProperties = null)
        {


        }

        private (double L1, double L2) _determineFlowRegimeBoundaries(VLPWorkingData input, 
            ref NodalAnalysisValidationResult validationResult)
        {

            double pipeDiameterNumber = VLPDimensionlessNumbers.DeterminePipeDiameterNumber(input);

            if (pipeDiameterNumber < 15 || pipeDiameterNumber > 300)
                validationResult.AddWarning("The pipe diameter number is out of range:" +
                    "The result of the second flow regime number (L2) may be unrealistic.");

            if (pipeDiameterNumber < 10 || pipeDiameterNumber > 300)
                validationResult.AddWarning("The pipe diameter number is out of range:" +
                    "The result of the first flow regime number (L1) may be unrealistic.");

            double logNd = Math.Log10(pipeDiameterNumber);
            double logNd2 = logNd * logNd;
            double logNd3 = logNd2 * logNd;
            double logNd4 = logNd3 * logNd;
            double logNd5 = logNd4 * logNd;
            double logNd6 = logNd5 * logNd;
            double logNd7 = logNd6 * logNd;
            double logNd8 = logNd7 * logNd;
            double logNd9 = logNd8 * logNd;

            double L1 = 23024.39446181188 - 138274.28844552726 * logNd
                + 364552.88310498727 * logNd2 - 553587.6879890925 * logNd3
                + 533487.6168031659 * logNd4 - 338308.65823780524 * logNd5
                + 141169.54448784262 * logNd6 - 37380.75720064985 * logNd7
                + 5700.498108021158 * logNd8 - 381.5451059140905 * logNd9;

            double L2 = 20308.653571829287 - 100576.85337603833 * logNd
                + 218402.06639436766 * logNd2 - 272721.4148037198 * logNd3
                + 215618.15052347022 * logNd4 - 111805.08314814913 * logNd5
                + 37969.502595677055 * logNd6 - 8128.437255489505 * logNd7
                + 992.8347113443087 * logNd8 - 52.524268155160826 * logNd9;

            return (L1, L2);

        }

        protected override SlipFlowRegime.enFlowRegime Detect(VLPWorkingData input,
            ref NodalAnalysisValidationResult validationResult,
            VLPDerivedProperties? derivedProperties = null)
        {

            double liquidVelocityNumber = VLPDimensionlessNumbers.DetermineLiquidVelocityNumber(input);
            double gasVelocityNumber = VLPDimensionlessNumbers.DetermineGasVelocityNumber(input);

            double Ls = 50 + 36 * liquidVelocityNumber;
            double Lm = 75 + 84 * Math.Pow(liquidVelocityNumber, .75);

            (double L1, double L2) = _determineFlowRegimeBoundaries(input, ref validationResult);

            if (0 <= gasVelocityNumber && gasVelocityNumber <= (L1 + L2 * liquidVelocityNumber))
                return SlipFlowRegime.enFlowRegime.BubbleFlow;

            if ((L1 + L2 * liquidVelocityNumber) <= gasVelocityNumber && gasVelocityNumber <= Ls)
                return SlipFlowRegime.enFlowRegime.SlugFlow;

            if (Ls < gasVelocityNumber && gasVelocityNumber < Lm)
                return SlipFlowRegime.enFlowRegime.TransitionFlow;

            if (Lm < gasVelocityNumber)
                return SlipFlowRegime.enFlowRegime.MistFlow;


            // This need better solution later :).
            validationResult.AddWarning("Flow regime can't be detected, Bubble flow is assumed.");

            return SlipFlowRegime.enFlowRegime.BubbleFlow;

            
        }
        

    }
}
