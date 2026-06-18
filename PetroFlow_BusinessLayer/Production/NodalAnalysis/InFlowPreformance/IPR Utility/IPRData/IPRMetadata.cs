

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData
{
    public abstract class IPRMetadata
    {

        public enum IPRMethodType
        {

            LinearProductivityIndex,
            Vogel,
            Standing,
            Fetkovich,
            Jones_Blount_Glaze

        }

        public enum IPRScenarioType
        {
            Present,
            Future
        }

        [Flags]
        public enum IPRMethodFeatures
        {
            None = 0,

            Oil = 1 << 0,
            Gas = 1 << 1,

            FuturePrediction = 1 << 2,

            VerticalWell = 1 << 3
        }

    }
}
