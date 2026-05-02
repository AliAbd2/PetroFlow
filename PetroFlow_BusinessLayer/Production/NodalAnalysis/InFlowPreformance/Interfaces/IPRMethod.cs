using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.ShearedData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Interfaces
{

    public enum enIPRMethodType { Vogel, Standing, Fetkovich, Jones }

    public enum enIPRData { ReservoirPressure, BubblePointPressure, TestData, TestFlowEfficiency, 
    FutureReservoirPressur, OilRelativePermeability, OilFromationVolumeFactor, OilViscosity,
        FutureOilRelativePermeability, FutureOilFromationVolumeFactor, FutureOilViscosity, CurveName, CurvePlotSettings
    }
    public interface IIPRMethod
    {
        string Name { get; set; }

        enIPRMethodType MethodType { get; }

        double ReservoirPressure { get; set; }

        double? BubblePointPressure { get; set; }

        List<InFlowDataRow> TestsData { get; set; }

        List<InFlowDataRow> GeneratedData { get; set; } 

        CurvePlotSettings CurvePlotSetting { get; set; }

        bool IsInputValid { get; set; }

        clsIPRGenerationSettings GenerationSettings { get; set; }

        NodalAnalysisValidationResult SetInputData(IPRInputData dataInput);

        void GenerateIPR();


    }

}
