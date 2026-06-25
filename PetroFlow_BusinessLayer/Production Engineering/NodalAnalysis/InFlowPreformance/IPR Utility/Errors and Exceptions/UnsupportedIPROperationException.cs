using PetroFlow_BusinessLayer.General_Utility.Exceptions;
using PetroFlow_BusinessLayer.General_Utility.Validation;

namespace PetroFlow_BusinessLayer.Production.NodalAnalysis.Utility.Exceptions
{
    internal class UnsupportedIPROperationException : ExceptionBase
    {

        public UnsupportedIPROperationException(ErrorMessage errorMessage) : base(errorMessage) { }

    }
}
