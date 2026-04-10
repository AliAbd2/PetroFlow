using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class InvalidParameterException : NodalAnalysusException
    {

        public InvalidParameterException(string message) : base(message) { }

    }
}
