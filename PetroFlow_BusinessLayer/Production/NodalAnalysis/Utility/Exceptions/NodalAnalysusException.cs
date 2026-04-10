using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    public abstract class NodalAnalysusException : Exception
    {

        protected NodalAnalysusException(string message) : base(message) { }


    }


}
