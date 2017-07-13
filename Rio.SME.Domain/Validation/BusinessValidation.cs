namespace Rio.SME.Domain.Validation
{
    public abstract class BusinessValidation
    {
        protected bool InstanceThrowError { get; private set; }
        public string EntityName { get; private set; }

        protected BusinessValidation()
        {
            this.ValidationResultsManager = new ValidationResultsManager();
        }

        protected BusinessValidation(string entityName, bool instanceThrowError)
        {
            this.InstanceThrowError = instanceThrowError;
            this.EntityName = entityName;

            this.ValidationResultsManager = new ValidationResultsManager();
        }

        #region Validation Results Methods

        protected ValidationResultsManager ValidationResultsManager { get; set; }

        protected virtual void ThrowBusinessValidationError()
        {
            this.ValidationResultsManager.ThrowBusinessValidationError();
        }

        protected virtual void AddValidationResultNotValid(string errorMessage, string propertyName)
        {
            this.ValidationResultsManager.ValidationResults.Add(
                new BusinessValidationResult(false, errorMessage, this.EntityName, propertyName));
        }

        protected virtual void AddValidationResultValid(string propertyName)
        {
            this.ValidationResultsManager.ValidationResults.Add(
                new BusinessValidationResult(true, string.Empty, this.EntityName, propertyName));
        }

        protected virtual void ClearValidationResults()
        {
            this.ValidationResultsManager.ValidationResults.Clear();
        }

        #endregion Validation Results Methods
    }
}