using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class exMissingRequiredInputException : exIPRException
    {

        public exMissingRequiredInputException(string message) : base(message) { }

    }
}
