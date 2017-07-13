using System;
using System.Web.Mvc;
using Rio.SME.Domain.Util.ExtensionMethods;

namespace Rio.SME.Web.Helper
{
    public static class BarraHelper
    {
        public static Barra HelperBarra(this HtmlHelper helper, String titulo, Lupa objLupa = null, string UrlIcone = null, string id = "", 
            object dataAttributes = null, bool iconeAjuda = false, string codigoMensagemAjuda = null)
        {
            if(id.IsNullOrEmpty())
                id = Guid.NewGuid().ToString();

            return new Barra(helper, titulo, objLupa, UrlIcone, id, dataAttributes, iconeAjuda, codigoMensagemAjuda);
        }
    }
}