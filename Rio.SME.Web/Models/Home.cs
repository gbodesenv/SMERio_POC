using System;

namespace Rio.SME.Web.Models
{
    public class Home
    {
        public int ContadorTarefa { get; set; }
        public int ContadorNotificacao { get; set; }
        public int ContadorAgendamento { get; set; }
        public int ContadorProcesso { get; set; }
        public int ContadorCondicionante { get; set; }
        public int ContadorFiscalizacao { get; set; }
        public string CodigoTipoProcesso { get; set; }
        public string NumeroProcesso { get; set; }
        public int? NumeroInscricao { get; set; }
        public int? CodigoTipoInscricao { get; set; }
    }
}