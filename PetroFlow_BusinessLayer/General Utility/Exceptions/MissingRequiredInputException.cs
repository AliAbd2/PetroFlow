using PetroFlow_BusinessLayer.General_Utility.Exceptions;
using PetroFlow_BusinessLayer.General_Utility.Validation;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.Exceptions
{
    internal class MissingRequiredInputException : ExceptionBase
    {

        public MissingRequiredInputException(ErrorMessage errorMessage) : base(errorMessage) { }

    }
}
