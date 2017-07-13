namespace Rio.SME.Domain.Validation
{
    public class BusinessValidationResult
    {
        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }
        public string EntityName { get; private set; }

        public BusinessValidationResult(bool isValid, string errorMessage,
            string entityName, string propertyName)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
            this.PropertyName = propertyName;
            this.EntityName = entityName;
        }

        public BusinessValidationResult(bool isValid, string errorMessage, string entityName)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
            this.EntityName = entityName;
            this.PropertyName = string.Empty;
        }

        public BusinessValidationResult(bool isValid, string errorMessage)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
            this.EntityName = string.Empty;
            this.PropertyName = string.Empty;
        }
    }
}