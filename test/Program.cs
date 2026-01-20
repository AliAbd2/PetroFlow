using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;

class Program
{

    static void Main(string[] args)
    {

        List<clsInFlowDataRow> data =  clsStanding.GenerateIPR_SaturatedReservoir(2085, 0.7, 202, 1765, 1.3);

        Console.WriteLine("Pwf      Qo");

        foreach (clsInFlowDataRow dataRow in data)
        {

            Console.WriteLine(dataRow.FlowingPressure + "   " + dataRow.FlowRate);

        }


    }

}