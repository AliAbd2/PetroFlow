using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT
{

    // This is temporary class 

    public class VerticalLiftingPreformancePVT
    {

        public static double GasFormationVolumeFactorInCuFoot(PVTDataInput input)
        {

            double prssureStandardConditions = input.StandardPSIPressure.Value;
            double TemperatureStanderConditions = input.StandardRankineTemperature.Value;
            double temperatureInR = input.RankineTemperature.Value;
            double pressure = input.PSIPressure.Value;

            double Z = ZFactorCalculator.CalculateZFactor(input);

            double standerCondtionRatio = prssureStandardConditions / TemperatureStanderConditions;

            return standerCondtionRatio * Z * temperatureInR / pressure;

        }

        public static double GasFormationVolumeFactorInBurrel(PVTDataInput input)
        {

            return GasFormationVolumeFactorInCuFoot(input) 
                / UnitConversionConstants.NumberOfFeetInBurrel;

        }

        public static double GasDensity(PVTDataInput input)
        {

            double gasSpecificGravity = input.GasSpecificGravity.Value;
            double pressure = input.PSIPressure.Value;
            double temperatureInR = input.RankineTemperature.Value;

            double Z = ZFactorCalculator.CalculateZFactor(input);

            double C = 2.7;

            return C * gasSpecificGravity * pressure / (Z * temperatureInR);

        }

        public static double OilDensity(PVTDataInput input)
        {

            double oilSpecificGravity = input.OilSpecificGravity.Value;
            double gasSpecificGravity = input.GasSpecificGravity.Value;
            double solutionGasOilRatio = 0;

            if (input.SolutionGasOilRatio.HasValue)
                solutionGasOilRatio = input.SolutionGasOilRatio.Value;
            else
                solutionGasOilRatio = SolutionGasOilRatioByStanding(input);

            double oilFormationVolumeFactor = OilFormationVolumeFactorByVasquezBeggs(input);

            double x = PhysicsConstants.PoundBerSTBWaterDensity * oilSpecificGravity
                + PhysicsConstants.PoundBerSCFAirDensity * gasSpecificGravity * solutionGasOilRatio;

            double y = UnitConversionConstants.NumberOfFeetInBurrel * oilFormationVolumeFactor;

            return x / y;

        }

        public static double SolutionGasOilRatioByStanding(PVTDataInput input)
        {

            double pressure = input.PSIPressure.Value;
            double api = input.API.Value;
            double temperatureInF = input.FahrenheitTemperature.Value;
            double gasSpecificGravity = input.GasSpecificGravity.Value;

            double x = (pressure * Math.Pow(10, 0.0125 * api)) / (18 * Math.Pow(10, 0.00091 * temperatureInF));
            double y = Math.Pow(x, 1.0/ 0.83);

            return gasSpecificGravity * y;


        }

        public static double SolutionGasOilRatioByVasquezBeggs(PVTDataInput input)
        {


            double pressure = input.PSIPressure.Value;
            double temperature = input.FahrenheitTemperature.Value;
            double gasGravity = input.GasSpecificGravity.Value;
            double api = input.API.Value;


            // There is a correction for gas gravity is the separator condition is available.
            if (input.FahrenheitSeparatorTemperature.HasValue &&
                input.PSISeparatorPressure.HasValue)
            {

                gasGravity = gasGravity * (1.0 + (5.912e-5 * api
                    * temperature * Math.Log10(pressure / 114.7)));

            }

            double C1 = 0;
            double C2 = 0;
            double C3 = 0;

            if (api <= 30)
            {

                C1 = 0.0362;
                C2 = 1.0937;
                C3 = 25.724;

            }
            else
            {

                C1 = 0.0178;
                C2 = 1.187;
                C3 = 23.931;

            }

            return C1 * gasGravity * Math.Pow(pressure, C2) 
                * Math.Exp(C3 * api / (temperature + UnitConversionConstants.FahrenheitToRankin));


        }

        public static double SaturatedOilFormationVolumeFactorByStanding(PVTDataInput input)
        {

            double gasSpecificGravity = input.GasSpecificGravity.Value;
            double oilSpecificGravity = input.OilSpecificGravity.Value;
            double temperatureInF = input.FahrenheitTemperature.Value;
            double Rs = 0;

            if (input.SolutionGasOilRatio.HasValue)
                Rs = input.SolutionGasOilRatio.Value;
            else
                Rs = SolutionGasOilRatioByStanding(input);

            double C1 = 1.25;

            double F = Rs * Math.Sqrt(gasSpecificGravity / oilSpecificGravity) + (C1 * temperatureInF);

            double C2 = 0.972;
            double C3 = 0.000147;
            double C4 = 1.175;

            return C2 + C3 * Math.Pow(F, C4);

        }

        private static double _SaturatedOilFormationVolumeFactorByVasquezBeggs(PVTDataInput input)
        {

            double temperature = input.FahrenheitTemperature.Value;
            double pressure = input.PSIPressure.Value;
            double api = input.API.Value;
            double gasGravity = input.GasSpecificGravity.Value;
            double Rs = 0;

            if (input.SolutionGasOilRatio.HasValue)
                Rs = input.SolutionGasOilRatio.Value;
            else
                Rs = SolutionGasOilRatioByVasquezBeggs(input);

            double C1 = 0;
            double C2 = 0;
            double C3 = 0;

            if (api <= 30)
            {

                C1 = 4.677e-4;
                C2 = 1.1751e-5;
                C3 = -1.811e-8;

            }
            else
            {

                C1 = 4.670e-4;
                C2 = 1.1e-5;
                C3 = 1.337e-9;

            }

            double x = C1 * Rs;
            double y = C2 * (temperature - 60) * (api / gasGravity);
            double z = C3 * Rs * (temperature - 60) * (api / gasGravity);

            return 1 + x + y + z;

        }

        private static double _UnderSaturatedOilFormationVolumeFactorByVasquezBeggs(PVTDataInput input)
        {

            double pressure = input.PSIPressure.Value;
            double bubblePointPressure = input.PSIBubblePointPressure.Value;

            PVTDataInput bubblePointData = new();
            bubblePointData.PSIPressure = input.PSIBubblePointPressure;
            bubblePointData.FahrenheitTemperature = input.FahrenheitTemperature;
            bubblePointData.API = input.API;
            bubblePointData.SolutionGasOilRatio = input.SolutionGasOilRatio;
            bubblePointData.GasOilRatio = input.GasOilRatio;
            bubblePointData.GasSpecificGravity = input.GasSpecificGravity;
            bubblePointData.FahrenheitSeparatorTemperature = input.FahrenheitSeparatorTemperature;
            bubblePointData.PSISeparatorPressure = input.PSISeparatorPressure;

            double BubblePointOilFormationVolumeFactor =
                _SaturatedOilFormationVolumeFactorByVasquezBeggs(bubblePointData);

            double oilCompressibility = OilCompressibilityByVasquezBegges(input);

            return BubblePointOilFormationVolumeFactor * Math.Exp(
                oilCompressibility * (bubblePointPressure - pressure));



        }

        public static double OilFormationVolumeFactorByVasquezBeggs(PVTDataInput input)
        {

            if (input.PSIPressure.Value > input.PSIBubblePointPressure.Value)
                return _UnderSaturatedOilFormationVolumeFactorByVasquezBeggs(input);
            else
                return _SaturatedOilFormationVolumeFactorByVasquezBeggs(input);

        }

        public static double OilCompressibilityByVasquezBegges(PVTDataInput input)
        {

            double pressure = input.PSIPressure.Value;
            double temperature = input.FahrenheitTemperature.Value;
            double gasGravity = input.GasSpecificGravity.Value;
            double api = input.API.Value;
            double Rs = 0;

            if (input.SolutionGasOilRatio.HasValue)
                Rs = input.SolutionGasOilRatio.Value;
            else
                Rs = SolutionGasOilRatioByVasquezBeggs(input);

            // There is a correction for gas gravity is the separator condition is available.
            if (input.FahrenheitSeparatorTemperature.HasValue &&
                input.PSISeparatorPressure.HasValue)
            {

                gasGravity = gasGravity * (1.0 + (5.912e-5 * api
                    * temperature * Math.Log10(pressure / 114.7)));

            }

            double C1 = 5;
            double C2 = 17.2;
            double C3 = -1180;
            double C4 = 12.61;
            double C5 = -1433;
            double C6 = 1e5;

            double x = C1 * Rs + C2 * temperature + C3 * gasGravity
                + C4 * api + C5;

            return x / (pressure * C6);

        }

        public static double OilViscosityByBeggsRobinson(PVTDataInput input)
        {

             double api = input.API.Value;
            double temperature = input.FahrenheitTemperature.Value;
            double Rs = 0;

            if (input.SolutionGasOilRatio.HasValue)
                Rs = input.SolutionGasOilRatio.Value;
            else
                Rs = SolutionGasOilRatioByStanding(input);

            double Z = 3.0324 - 0.0203 * api;
            double Y = Math.Pow(10, Z);
            double X = Y * Math.Pow(temperature, -1.163);
            double deadOilViscosity = Math.Pow(10, X) - 1;

            double A = 10.715 * Math.Pow((Rs + 150), -0.515);
            double B = 5.44 * Math.Pow((Rs + 150), -0.338);

            return A * Math.Pow(deadOilViscosity, B);

        }

    }
}
