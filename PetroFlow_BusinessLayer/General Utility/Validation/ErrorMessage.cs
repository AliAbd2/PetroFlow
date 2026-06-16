using System;
using System.Collections.Generic;
using System.Text;

namespace PetroFlow_BusinessLayer.General_Utility.Validation
{
    public sealed record ErrorMessage
    {

        public string Title { get; }
        public string Message { get; }

        public ErrorMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }

    }
}
