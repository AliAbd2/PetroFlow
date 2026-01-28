using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class exInvalidIPRParameterException : exIPRException
    {

        public exInvalidIPRParameterException(string message) : base(message) { }

    }
}
