using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class MissingRequiredInputException : NodalAnalysusException
    {

        public MissingRequiredInputException(string message) : base(message) { }

    }
}
