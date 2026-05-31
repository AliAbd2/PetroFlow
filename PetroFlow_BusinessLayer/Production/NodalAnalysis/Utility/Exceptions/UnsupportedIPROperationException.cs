using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Exceptions
{
    internal class UnsupportedIPROperationException : NodalAnalysusException
    {

        public UnsupportedIPROperationException(string message) : base(message) { }

    }
}
