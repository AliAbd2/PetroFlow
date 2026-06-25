using System;
using System.Collections.Generic;
using System.Text;
using static PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.IPRData.IPRMetadata;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPR_Utility.Errors_and_Exceptions
{
    public class IPRRequirements
    {

        public IPRInputFields Present { get; init; }

        public IPRFutureInputFields Future { get; init; }

        public IPRTestDataRequirement TestData { get; init; }

    }
}
