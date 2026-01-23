using System;
using PetroFlow_BusinessLayer;
using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance;

class Program
{

    static void Main(string[] args)
    {

        clsStanding standing = new clsStanding(4000, 1200, 378, 0.7, 2000);

        List<clsInFlowDataRow> data = standing.GenerateIPR();

        Console.WriteLine("Pwf      Qo");

        foreach (clsInFlowDataRow dataRow in data)
        {

            Console.WriteLine(dataRow.BottomHolePressure + "   " + dataRow.FlowRate);

        }


    }

}