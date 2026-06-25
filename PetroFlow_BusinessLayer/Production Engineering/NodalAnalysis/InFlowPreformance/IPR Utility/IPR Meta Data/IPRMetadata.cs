namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData
{
    public static class IPRMetadata
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

        // this flag for the UI to know what controls to enable
        [Flags]
        public enum IPRInputFields
        {


            None = 0,

            ReservoirPressure = 1 << 0,
            BubblePointPressure = 1 << 1,

            TestFlowEfficiency = 1 << 4,

            WellExponent = 1 << 5,

        }

        // this flag for the UI to know what controls to enable
        [Flags]
        public enum IPRFutureInputFields
        {

            None = 1<<0,

            FutureReservoirPressure = 1 << 1,
            FlowCoefficient = 1 << 2,

            PresentOilRelativePermeability = 1 << 3,
            PresentOilViscosity = 1 << 4,
            PresentOilFormationVolumeFactor = 1 << 5,

            FutureOilRelativePermeability = 1 << 6,
            FutureOilViscosity = 1 << 7,
            FutureOilFormationVolumeFactor = 1 << 8,


        }

        // this flag for the UI to know what controls to enable
        public enum IPRTestDataRequirement
        {
            None,
            SinglePoint,
            TwoPoints,
            MultiplePoints
        }

    }
}
