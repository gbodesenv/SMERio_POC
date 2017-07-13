using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rio.SME.Web.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public bool Excluido { get; set; }
        public int? IdProcessoFiscalizacao { get; set; }
        public int? IdRelatorioVistoria { get; set; }
        public int? NumeroUnidadeGestaoPMV { get; set; }
        public int? NumeroResponsavelPMV { get; set; }
        public string DataCadastro { get; set; }
        public string DataImagem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string NomeImagem { get; set; }

        public string NumeroRelatorioVistoria { get; set; }
    }
}