using Rio.SME.Domain.Util.ExtensionMethods;
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Rio.SME.Web.Helper
{
    public static class TextAreaHelper
    {
        public static HtmlString TextAreaCount(this HtmlHelper htmlHelper, string idTextArea, string value, object htmlAttributes)
        {
            string idContador = idTextArea + "Count";

            string textArea = TextAreaExtensions.TextArea(htmlHelper, idTextArea, value, htmlAttributes).ToHtmlString();

            string labelContador = LabelExtensions.Label(htmlHelper, idContador, new { @id = idContador, @data_for_id = idTextArea, @class = "caracteres" }).ToHtmlString();

            return new HtmlString(textArea + labelContador);
        }

        public static HtmlString TextAreaCountFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            string idTextArea = "";
            if (htmlAttributes.ToDictionary().Keys.Contains("id"))
                idTextArea = htmlAttributes.ToDictionary()["id"].ToString();
            else
                idTextArea = ((MemberExpression)((LambdaExpression)expression).Body).Member.Name;

            string idContador = idTextArea + "Count";

            string textArea = TextAreaExtensions.TextAreaFor(htmlHelper, expression, htmlAttributes).ToHtmlString();

            string labelContador = LabelExtensions.Label(htmlHelper, idContador, new { @id = idContador, @data_for_id = idTextArea, @class = "caracteres" }).ToHtmlString();

            return new HtmlString(textArea + labelContador);
        }
    }
}