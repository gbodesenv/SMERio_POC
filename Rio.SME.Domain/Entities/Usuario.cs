namespace Rio.SME.Domain.Entities
{       
    using Validation;
    using Util.ExtensionMethods;
    using System;
    using Rio.SME.Domain.Contracts.Entities;

    public class Usuario : IEntity
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public bool Excluido { get; set; }

        public void Validate()
        {

            var validationResultsManager = new ValidationResultsManager();

            //// Required
            if (Nome.IsNullOrEmpty())
                validationResultsManager.AddValidationResultNotValid(String.Format(Resources.MensagensValidacao.CampoObrigatorio,"Nome"));
            if (Email.IsNullOrEmpty())
                validationResultsManager.AddValidationResultNotValid(String.Format(Resources.MensagensValidacao.CampoObrigatorio, "E-mail"));
            if (Senha.IsNullOrEmpty())
                validationResultsManager.AddValidationResultNotValid(String.Format(Resources.MensagensValidacao.CampoObrigatorio, "Senha"));
            if (Telefone.IsNullOrEmpty())
                validationResultsManager.AddValidationResultNotValid(String.Format(Resources.MensagensValidacao.CampoObrigatorio, "Telefone"));

            //// Optional
            if (validationResultsManager.HasError)
                validationResultsManager.ThrowBusinessValidationError();
        }

        // Clona os dados da entidade
        public object Clone()
        {
            var entidade = new Usuario();
            entidade.Nome = this.Nome;
            return entidade;
        }
    }

}
