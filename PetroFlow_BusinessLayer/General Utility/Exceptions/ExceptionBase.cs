using PetroFlow_BusinessLayer.General_Utility.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.General_Utility.Exceptions
{
    public abstract class ExceptionBase : Exception
    {

        public string Title { get; }

        public ExceptionBase(ErrorMessage errorMessage) : base(errorMessage?.Message)
        {

            ArgumentNullException.ThrowIfNull(errorMessage);

            Title = errorMessage.Title;
        }

    }
}
