using System;


namespace Rio.SME.Web.Models
{
    public class Usuario
    {
        public bool Autenticado { get; set; }
        public String MsgRetorno { get; set; }
        public string Matricula { get; set; }
        public String Nome { get; set; }
        public string CodigoUnidadeGestora { get; set; }
        public string NomeUnidadeGestora { get; set; }
    }
}
