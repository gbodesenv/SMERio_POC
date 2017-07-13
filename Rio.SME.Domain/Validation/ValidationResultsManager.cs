using System.Collections.Generic;

namespace Rio.SME.Domain.Validation
{
    using Exceptions;

    public class ValidationResultsManager
    {
        public List<BusinessValidationResult> ValidationResults { get; set; }

        public bool HasError { get { return ValidationResults.Count > 0; } }

        public ValidationResultsManager()
        {
            this.ValidationResults = new List<BusinessValidationResult>();
        }

        public virtual void ThrowBusinessValidationError()
        {
            if (this.ValidationResults.Count == 0)
                return;

            string validationMessage = string.Empty;

            List<BusinessValidationResult> errors = new List<BusinessValidationResult>();

            foreach (BusinessValidationResult vr in this.ValidationResults)
            {
                if (vr.IsValid)
                    continue;

                errors.Add(vr);

                if (validationMessage.Length > 0)
                    validationMessage += "\n";

                if (vr.EntityName != string.Empty)
                    validationMessage += vr.EntityName + " - ";
                if (vr.PropertyName != string.Empty)
                    validationMessage += vr.PropertyName + " - ";

                validationMessage += vr.ErrorMessage;
            }

            if (errors.Count > 0)
                throw new ValidationException(validationMessage, errors);
        }

        public virtual void AddValidationResultNotValid(string errorMessage, string entityName = "", string propertyName = "")
        {
            this.ValidationResults.Add(new BusinessValidationResult(
                false, errorMessage, entityName, propertyName));
        }

        public virtual void AddValidationResultValid(string entityName, string propertyName)
        {
            this.ValidationResults.Add(new BusinessValidationResult(true, string.Empty, entityName, propertyName));
        }

        public virtual void ClearValidationResults()
        {
            this.ValidationResults = new List<BusinessValidationResult>();
        }
    }
}