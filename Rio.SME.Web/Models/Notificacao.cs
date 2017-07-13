using System;

namespace Rio.SME.Web.Models
{
    public class Notificacao
    {
        public int Id { get; set; }
        public int? IdObjetoEventoNotificacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Conteudo { get; set; }
        public string SitNotificacao { get; set; }
        public bool Excluido { get; set; }
        public DateTime? DataInicio { get; set; }

        public int NumeroResponsavel { get; set; }
        public bool IndicadorLido { get; set; }
        public string Mensagem { get; set; }

        public string NaoLidoClass { get; set; }

        //campos filtro
        public string NumeracaoOrigem { get; set; }
        public DateTime DataInicioOrigem { get; set; }
        public int IdOrigem { get; set; }
        public int IdEvento { get; set; }
    }
}
