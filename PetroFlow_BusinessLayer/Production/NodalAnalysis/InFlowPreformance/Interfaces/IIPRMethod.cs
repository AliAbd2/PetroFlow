using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions_and_Validation;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
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

        List<clsInFlowDataRow> TestsData { get; set; }

        List<clsInFlowDataRow> GeneratedData { get; set; } 

        clsCurvePlotSettings CurvePlotSetting { get; set; }

        bool IsInputValid { get; set; }

        clsValidationResult SetInputData(Dictionary<enIPRData, object> inputData);

        void GenerateIPR();


    }

}
