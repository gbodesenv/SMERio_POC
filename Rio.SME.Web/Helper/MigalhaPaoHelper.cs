using System.Web.Mvc;

namespace Rio.SME.Web.Helper
{
    public static class MigalhaPaoHelper
    {
        /// <summary>
        /// Método para Criar Migalha de Pão
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <param name="titulos">Lista de Títulos da Migalha de Pão</param>
        /// <returns></returns>
        public static MvcHtmlString MigalhaPao<TModel>(this HtmlHelper<TModel> helper, params string[] titulos)
        {
            string html = "";
            string imgPath = UrlHelper.GenerateContentUrl(@"~/content/themes/images/icobreadcrumbseta.png", helper.ViewContext.HttpContext);
            foreach (var titulo in titulos)
            {
                html += string.Format("<img src='{0}' class='icobreadcrumbseta' alt=''><label>{1}</label>", imgPath, titulo);
            }
            return MvcHtmlString.Create(html);
        }
    }
}