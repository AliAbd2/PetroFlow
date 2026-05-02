using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData
{
    public class VLPDerivedProperties
    {

        //================================
        // --- No Slip No Flow Regime ---
        //================================

        public double? NoSlipMixtureDensity { get; set; }

        //================================
        // --- Slip No Flow Regime ---
        //================================

        public double? SlipMixtureDensity { get; set; }

        public double? SlipMixtureViscosity { get; set; }

        //================================
        // --- Slip and Flow Regime ---
        //================================

    }
}
