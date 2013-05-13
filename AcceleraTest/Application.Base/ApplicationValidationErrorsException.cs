using System;
using System.Collections.Generic;
using Infraestructure.Validation.Base;

namespace Application.Base
{
    public class ApplicationValidationErrorsException : Exception
    {
        public IList<Error> ValidationErrors { get; private set; }

        public ApplicationValidationErrorsException(IList<Error> validationErrors)
            : base("Validation Error")
        {
            ValidationErrors = validationErrors;
        }

        public ApplicationValidationErrorsException(Error error)
            : base("Validation Error")
        {
            ValidationErrors = new List<Error> { error };
        }
    }
}
