using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.PVT
{
    public class PVTDataInput
    {
        
        public double? PSIPressure { get; set; }

        public double? PSIBubblePointPressure { get; set; }

        public double? FahrenheitTemperature { get; set; }

        public double? RankineTemperature { get
            {
                if (FahrenheitTemperature.HasValue)
                    return FahrenheitTemperature + 460;
                else
                    return null;
            }
        }

        public double? API { get; set; }

        public double? GasSpecificGravity { get; set; }

        public double? OilSpecificGravity { get
            {
                return 141.5 / (131.5 + API);
            }
            }

        public double? PSISeparatorPressure { get; set; }

        public double? FahrenheitSeparatorTemperature { get; set; }

        public double? RankineSeparatorTemperature { get
            {
                if (FahrenheitTemperature.HasValue)
                    return FahrenheitTemperature + 460;
                else
                    return null;
            }
        }

        public double? StandardPSIPressure { get; set; } = PhysicsConstants.StandardPSIPressure;

        public double? StandardFahrenheitTemperature { get; set; } = PhysicsConstants.StandarFahrenheitTemperature;

        public double? StandardRankineTemperature { get
            {
                return StandardFahrenheitTemperature + 460;
            }
            }

        public double? OilFormationVolumeFactor { get; set; }

        public double? GasFromationVolumeFactor { get; set; }

        public double? GasOilRatio { get; set; }

        public double? SolutionGasOilRatio { get; set; }

    }
}
