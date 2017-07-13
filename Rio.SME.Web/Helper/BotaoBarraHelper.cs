using System;
using System.Web;

namespace Rio.SME.Web.Helper
{
    public class BotaoBarraHelper : IHtmlString
    {
        public TipoBotao AcaoBotao { get; set; }
        public string IdBotao { get; set; }
        public string TituloBotao { get; set; }
        public string HRefBotao { get; set; }
        public string CodPermissao { get; set; }
        public string LegendaBotao { get; set; }

        public enum TipoBotao
        {
            Inserir,
            Excluir,
            Editar,
            Selecionar,
            Voltar,
            Fechar
        }

        public static BotaoBarraHelper MontarBotao(string codPermissao, TipoBotao tipoBotao, string idBotao, string tituloBotao, string hrefBotao, string legendaBotao)
        {
            return new BotaoBarraHelper()
            {
                CodPermissao = codPermissao,
                HRefBotao = hrefBotao,
                IdBotao = idBotao,
                AcaoBotao = tipoBotao,
                TituloBotao = tituloBotao,
                LegendaBotao = legendaBotao
            };
        }

        public string MontaHtml()
        {
            String htmlBotao = "";

            if (VerificaPermissao())
            {
                String li = "<li><a href='{0}' id='{1}' alt='{2}' title='{4}' {3}><span>{2}</span></a></li>";
                htmlBotao = String.Format(li, (String.IsNullOrEmpty(HRefBotao) ? "javascript:void(0);" : HRefBotao), IdBotao, TituloBotao, AcaoBotao == TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(LegendaBotao) ? TituloBotao : LegendaBotao);
            }
            else
                htmlBotao = "";

            return htmlBotao;
        }

        public bool VerificaPermissao()
        {
            switch (AcaoBotao)
            {
                case TipoBotao.Inserir:
                    return true;
                case TipoBotao.Excluir:
                    return true;
                case TipoBotao.Editar:
                    return true;
                case TipoBotao.Selecionar:
                    return true;
                case TipoBotao.Voltar:
                    return true;
                case TipoBotao.Fechar:
                    return true;
                default:
                    return false;
            }
        }

        #region IHtmlString Members

        public string ToHtmlString()
        {
            return MontaHtml();
        }

        #endregion
    }
}
