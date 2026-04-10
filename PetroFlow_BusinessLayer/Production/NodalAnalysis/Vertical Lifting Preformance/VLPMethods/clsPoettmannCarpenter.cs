using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPMethods
{
    public class clsPoettmannCarpenter : INoSlipNoFlowRegime
    {

        public double NoSlipDensity { get; set; }

        public double TotalMassFlowrate { get; set; }

        public double TubingInsideDiameter { get; set; }

        public double TwoPhaseFirctionFactor { get; set; }

        private void DetermineNoSlipDensity()
        {



        }

    }
}
