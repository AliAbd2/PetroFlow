using PetroFlow_BusinessLayer.General_Utility.Exceptions;
using PetroFlow_BusinessLayer.General_Utility.Validation;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class InvalidParameterException : ExceptionBase
    {

        public InvalidParameterException(ErrorMessage errorMessage) : base(errorMessage) { }
    }
}
