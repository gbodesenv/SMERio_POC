using System;
using System.Collections.Generic;

namespace Rio.SME.Domain.Exceptions
{
    using Validation;

    [Serializable]
    public class ValidationException : Exception
    {
        public List<BusinessValidationResult> ValidationResults { get; private set; }

        public ValidationException(string message, List<BusinessValidationResult> listValidationResults)
            : base(message)
        {
            this.ValidationResults = listValidationResults;
        }

        public ValidationException(string message)
            : base(message)
        {
            this.ValidationResults = new List<BusinessValidationResult>();
        }
    }
}