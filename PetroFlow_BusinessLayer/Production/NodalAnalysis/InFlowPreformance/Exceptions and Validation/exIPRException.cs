using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    public abstract class exIPRException : Exception
    {

        protected exIPRException(string message) : base(message) { }


    }


}
