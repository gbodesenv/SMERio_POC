 using Rio.SME.Domain.Contracts.Entities;
using Rio.SME.Domain.Util.ExtensionMethods;
using Rio.SME.Domain.Validation;
using System;

namespace Rio.SME.Domain.Entities
{
    public class Agrupador : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool IndicadorAtivo { get; set; }
        public bool Excluido { get; set; }

        public void Validate()
        {

            var validationResultsManager = new ValidationResultsManager();

            //// Required
            if (Nome.IsNullOrEmpty())
                validationResultsManager.AddValidationResultNotValid(String.Format(Resources.MensagensValidacao.CampoObrigatorio, "Nome"));
           
            //// Optional
            if (validationResultsManager.HasError)
                validationResultsManager.ThrowBusinessValidationError();
        }

        // Clona os dados da entidade
        public object Clone()
        {
            var entidade = new Agrupador();
            entidade.Nome = this.Nome;
            return entidade;
        }

        public enum CampoOrdenacaoAgrupador
        {
            Id,
            Nome            
        }
    }
}
