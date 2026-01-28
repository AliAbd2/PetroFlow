using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;

class Program
{

    static void Main(string[] args)
    {
        List<clsInFlowDataRow> inputData = new List<clsInFlowDataRow>();

        inputData.Add(new clsInFlowDataRow(3170, 263));
        inputData.Add(new clsInFlowDataRow(2897, 383));
        inputData.Add(new clsInFlowDataRow(2150, 640));

        clsFetkovich fetkovich = new clsFetkovich(3600, inputData);

        List<clsInFlowDataRow> data = new List<clsInFlowDataRow>();

        fetkovich.GenerateIPR();

        data = fetkovich.GeneratedData;

        Console.WriteLine("Pwf      Qo");

        foreach (clsInFlowDataRow dataRow in data)
        {

            Console.WriteLine(dataRow.BottomHolePressure + "   " + dataRow.FlowRate);

        }


    }

}