using PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Vertical_Lifting_Preformance.VLPData
{

    /// <summary>
    /// Represents intermediate properties calculated during specific model evaluations.
    /// These properties are derived from the working data and are typically flow-regime or 
    /// method-specific. They are passed between calculation components to avoid redundant computations.
    /// </summary>

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

        public SlipFlowRegime.enFlowRegime FlowRegime { get; set; }

    }
}
