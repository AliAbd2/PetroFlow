using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Methods;

class Program
{

    static void Main(string[] args)
    {

        List<clsInFlowDataRow> testsData = new List<clsInFlowDataRow>();
        testsData.Add(new clsInFlowDataRow(1780, 282));


        clsFetkovich fetkovich = new clsFetkovich(2100 , testsData);

        List<clsInFlowDataRow> data = fetkovich.GenerateIPR();

        Console.WriteLine("Pwf      Qo");

        foreach (clsInFlowDataRow dataRow in data)
        {

            Console.WriteLine(dataRow.BottomHolePressure + "   " + dataRow.FlowRate);

        }


    }

}