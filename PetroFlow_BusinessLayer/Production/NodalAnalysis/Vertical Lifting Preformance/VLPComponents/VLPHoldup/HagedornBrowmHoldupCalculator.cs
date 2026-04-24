using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPAbstractClasses;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPComponents.VLPHoldup
{
    internal class HagedornBrowmHoldupCalculator : SlipNoFlowRegimeHoldupCalculator
    {

        private double _liquidVelocityNumber;

        private double _gasVelocityNumber;

        private double _pipeDiameterNumber;

        private double _liquidViscosityNumber;

        public HagedornBrowmHoldupCalculator()
        {

        }

        private double _determineLiquidVelocityNumber(double liquidSuperficialVelocity,
            double liquidDensity, double surfaceTension)
        {

            double x = liquidDensity / surfaceTension;

            return 1.938 * liquidSuperficialVelocity * Math.Pow(x, .25);

        }

        private double _determineGasVelocityNumber(double gasSuperficialVelocity,
            double liquidDensity, double surfaceTension)
        {

            double x = liquidDensity / surfaceTension;

            return 1.938 * gasSuperficialVelocity * Math.Pow(x, .25);

        }

        private double _determinePipeDiameterNumber(double pipeDiameter,
            double liquidDensity, double surfaceTension)
        {

            double x = liquidDensity / surfaceTension;
            return 120.872 * pipeDiameter * Math.Pow(x, .5);

        }

        private double _determineViscosityNumber(double liquidViscosity,
            double liquidDensity, double surfaceTension)
        {

            double x = 1 / (liquidDensity * Math.Pow(surfaceTension, 3));
            double liquidViscosityNumber = 0.15726 * liquidViscosity * Math.Pow(x, .25);

            return liquidViscosityNumber;

        }

        private double _determineHoldupFactor(double liquidVelocityNumber, double gasVelocityNumber,
            double pressure, double correctedViscoityNumber, double pipeDiameterNumber)
        {

            double x = (liquidVelocityNumber / Math.Pow(gasVelocityNumber, .575));
            double y = Math.Pow(pressure / PhysicsConstants.StanderConditionPressure, .1);
            double z = correctedViscoityNumber / pipeDiameterNumber;

            double a = x * y * z;

            double loga = Math.Log10(a);
            double loga2 = loga * loga;
            double loga3 = loga2 * loga;
            double loga4 = loga3 * loga;
            double loga5 = loga4 * loga;
            double loga6 = loga5 * loga;
            double loga7 = loga6 * loga;


            return 110.77 + 208.73 * loga + 165.16 * loga2 +
                70.384 * loga3 + 17.45 * loga4 + 2.524 * loga5 + 0.19775 * loga6 + 0.0064915 * loga7;


        }

        private void _determineViscosityNumberCorrected(double liquidViscosityNumber)
        {

            double logNl = Math.Log10(liquidViscosityNumber);
            double logNl2 = logNl * logNl;
            double logNl3 = logNl2 * logNl;
            double logNl4 = logNl3 * logNl;

            double y = -1.9838 - 0.66417 * logNl - 1.4314 * logNl2 - 0.6686 * logNl3 - 0.098641 * logNl4;

            _liquidViscosityNumber = Math.Pow(10, y);

        }

        protected override void ValidateRawInpu(VLPDataInput input, ref NodalAnalysisValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        protected override double ComputeLiquidHoldup(VLPDataInput input, ref NodalAnalysisValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

    }
}
