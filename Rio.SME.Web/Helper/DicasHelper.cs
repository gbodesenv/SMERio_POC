using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rio.SME.Web.Helper
{
    public static class DicasHelper
    {
        /// <summary>
        /// Cria uma label com a mensagem de dica na tela
        /// </summary>
        /// <param name="mensagem">A mensagem contida na label</param>
        /// <returns>HtmlString com o markup</returns>
        public static HtmlString LabelDica(string mensagem)
        {
            return new HtmlString(string.Format("<label class=\"labelOBS\">{0}</label>", mensagem));   
        }
    }
}