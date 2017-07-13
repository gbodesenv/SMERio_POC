using System;

namespace Rio.SME.Web.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        public int? NumeroProcessoSIPAD { get; set; }
        public string CodigoProcessoSIPAD { get; set; }
        public DateTime DataProtocolo { get; set; }
        public string Instancia { get; set; }
        public string TipoRecurso { get; set; }
        public string DescricaoTempestividade { get; set; }
        public string Tempestividade { get; set; }
        public string NumeroProcessoSIGAVIX { get; set; }
        public string Requerente { get; set; }
        public string RequerenteCPFCNPJ { get; set; }
        public string ResponsavelProcessoSIGAVIX { get; set; }
        public string ResponsavelProcessoSIGAVIXCPFCNPJ { get; set; }
        public int NumeroUnidadeGestaoPMV { get; set; }
        public string Situacao { get; set; }
        public string Informacao { get; set; }
        public int NumeroObjetoEstado { get; set; }

        public int IdRecursoDefesa { get; set; }
        public string DataCadastroRecursoDefesa { get; set; }
        public string AutorRecursoDefesa { get; set; }
        public int NumeroUnidadeGestaoRecursoDefesa { get; set; }
        public string NomeUnidadeGestaoRecursoDefesa { get; set; }
        public int NumeroPessoaRecursoDefesa { get; set; }
        public int IdAnexoRecursoDefesa { get; set; }
        public string AnexoNomeRecursoDefesa { get; set; }
        public string DescricaoDefesa { get; set; }

        public int IdRecursoAnalise { get; set; }
        public string DataAnalise { get; set; }
        public string AutorRecursoAnalise { get; set; }
        public int NumeroUnidadeGestaoRecursoAnalise { get; set; }
        public string NomeUnidadeGestaoRecursoAnalise { get; set; }
        public int NumeroPessoaRecursoAnalise { get; set; }
        public int IdAnexoRecursoAnalise { get; set; }
        public string AnexoNomeRecursoAnalise { get; set; }
        public string DescricaoAnalise { get; set; }
    }
}