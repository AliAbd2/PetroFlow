using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;

class Program
{

    static void Main(string[] args)
    {

        List<clsInFlowDataRow> testsData = new List<clsInFlowDataRow>();
        testsData.Add(new clsInFlowDataRow(3170, 263));
        testsData.Add(new clsInFlowDataRow(2897, 383));
        testsData.Add(new clsInFlowDataRow(2440, 497));
        testsData.Add(new clsInFlowDataRow(2150, 640));


        clsJonesBlountGlaze jonesBlountGlaze = new clsJonesBlountGlaze(3600, testsData);

        List<clsInFlowDataRow> data = jonesBlountGlaze.GenerateIPR();

        Console.WriteLine("Pwf      Qo");

        foreach (clsInFlowDataRow dataRow in data)
        {

            Console.WriteLine(dataRow.BottomHolePressure + "   " + dataRow.FlowRate);

        }


    }

}