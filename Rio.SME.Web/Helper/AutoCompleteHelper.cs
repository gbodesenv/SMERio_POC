using Rio.SME.Web.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Rio.SME.Web.Helper
{
    public static class AutoCompleteHelper
    {

        #region MontarHtmlAutoComplete
        /// <summary>
        /// Método de chamada do MontarHtmlAutoComplete: responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete.)
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametroMetodoBuscar">String contendo o parametro de busca do método</param>
        /// <param name="disabled">Booleano que bloqueia campo e não carrega scripts de AutoComplete</param>
        /// <returns>HTMLString com o script p/ carregamento do AutoComplete & input que irá ativar seu AutoComplete</returns>
        public static HtmlString AutoComplete(IController controllerName, string id, string urlMetodoBuscar, string parametroMetodoBuscar, bool disabled = false)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, parametroMetodoBuscar, disabled);
        }

        /// <summary>
        /// Método de chamada do MontarHtmlAutoComplete: responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete.)
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametroMetodoBuscar">String contendo o parametro de busca do método</param>
        /// <param name="minLength">Inteiro com o número mínimo de caracteres no campo texto para realizar a chamada do AutoComplete.</param>
        /// <returns>HTMLString com o script p/ carregamento do AutoComplete & input que irá ativar seu AutoComplete</returns>
        public static HtmlString AutoComplete(IController controllerName, string id, string urlMetodoBuscar, string parametroMetodoBuscar, int minLength)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, parametroMetodoBuscar, false, minLength);
        }

        /// <summary>
        /// Método de chamada do MontarHtmlAutoComplete: responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete.)
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametroMetodoBuscar">String contendo o parametro de busca do método</param>
        /// <param name="disabled">Booleano que bloqueia campo e não carrega scripts de AutoComplete</param>
        /// <param name="minLength">Inteiro com o número mínimo de caracteres no campo texto para realizar a chamada do AutoComplete.</param>
        /// <returns></returns>
        public static HtmlString AutoComplete(IController controllerName, string id, string urlMetodoBuscar, string parametroMetodoBuscar, bool disabled, int minLength)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, parametroMetodoBuscar, disabled, minLength);
        }

        /// <summary>
        /// Método de chamada do MontarHtmlAutoComplete: responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete.)
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametroMetodoBuscar">String contendo o parametro de busca do método</param>
        /// <param name="disabled">Booleano que bloqueia campo e não carrega scripts de AutoComplete</param>
        /// <param name="minLength">Inteiro com o número mínimo de caracteres no campo texto para realizar a chamada do AutoComplete.</param>
        /// <param name="obj">Objeto com os atributos html</param>
        /// <param name="objetoValor">Boleano para autocomplete com texto e valor</param>
        /// <param name="idInputHidden">Nome identificador do campo hidden</param>
        /// <param name="dataBindAttribute">Objeto de DataBind do campo hidden</param>
        /// <returns></returns>
        public static HtmlString AutoComplete(IController controllerName, string id, string urlMetodoBuscar, string parametroMetodoBuscar, bool disabled, int minLength, object obj = null,
            bool objetoValor = false, string idInputHidden = "", string dataBindAttribute = null)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, parametroMetodoBuscar, disabled, minLength, obj, objetoValor, idInputHidden, dataBindAttribute);
        }

        /// <summary>
        /// Método de chamada do MontarHtmlAutoComplete: responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete.)
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametrosMetodoBuscar">Array de String contendo o parametro de busca do método</param>
        /// <param name="disabled">Booleano que bloqueia campo e não carrega scripts de AutoComplete</param>
        /// <param name="minLength">Inteiro com o número mínimo de caracteres no campo texto para realizar a chamada do AutoComplete.</param>
        /// <param name="obj">Objeto com os atributos html</param>
        /// <param name="objetoValor">Boleano para autocomplete com texto e valor</param>
        /// <param name="idInputHidden">Nome identificador do campo hidden</param>
        /// <param name="dataBindAttribute">Objeto de DataBind do campo hidden</param>
        /// <returns></returns>
        public static HtmlString AutoComplete(IController controllerName, string id, string urlMetodoBuscar, string[] parametrosMetodoBuscar, bool disabled, int minLength, object obj = null,
            bool objetoValor = false, string idInputHidden = "", string dataBindAttribute = null)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, parametrosMetodoBuscar, disabled, minLength, obj, objetoValor, idInputHidden, dataBindAttribute);
        }
        #endregion

        #region Private - MontarHtmlAutoComplete
        /// <summary>
        /// Método responsável por montar um input contendo AutoComplete; utilizando a Controller e método a ser utilizado p/ carregar o AutoComplete. 
        /// O objeto a ser retornado do método deve ser obrigatóriamente JSON.
        /// </summary>
        /// <param name="controllerName">Objeto Controller a ser utilizado</param>
        /// <param name="id">String contendo o Id do Input a ser criado</param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser</param>
        /// <param name="parametroMetodoBuscar">String contendo o parametro de busca do método</param>
        /// <param name="disabled">Boleano que indica se o campo deve vir DESABILITADO </param>
        /// <param name="minLength">Inteiro contendo o tamanho mínimo p/ realizar a busca do JSON</param>
        /// <param name="obj">Objeto com os atributos html</param>
        /// <returns>HTMLString com o script p/ carregamento do AutoComplete & input que irá ativar seu AutoComplete</returns>
        private static HtmlString MontarHtmlAutoComplete(IController controllerName, string id,
            string urlMetodoBuscar, string parametroMetodoBuscar, bool disabled, int minLength = 2,
            object obj = null, bool objetoValor = false, string idInputHidden = "", string dataBindAttribute = null)
        {
            return MontarHtmlAutoComplete(controllerName, id, urlMetodoBuscar, new string[] { parametroMetodoBuscar }, disabled, minLength, obj, objetoValor, idInputHidden, dataBindAttribute);
        }

        private static HtmlString MontarHtmlAutoComplete(IController controllerName, string id,
            string urlMetodoBuscar, string[] parametrosMetodoBuscar, bool disabled, int minLength = 2,
            object obj = null, bool objetoValor = false, string idInputHidden = "", string dataBindAttribute = null)
        {
            try
            {
                string string_htmlAttributes = UtilWeb.MontarHtmlAttribute(obj);

                ValidaParametros(controllerName, urlMetodoBuscar);

                string htmlAutoCompleteRetorno = string.Empty;

                if (!disabled)
                {
                    string jquerySelector = "$('#" + id + @"')";
                    string metodoResponse = string.Empty;

                    if (objetoValor)
                        metodoResponse = @"response($.map(result.content, function (item) {
                                                    return {
                                                        label: item.Text,
                                                        value: item.Value
                                                    };
                                            }));";
                    else
                        metodoResponse = "response(result.content);";

                    htmlAutoCompleteRetorno += MontarScriptAutoComplete(jquerySelector, minLength, urlMetodoBuscar, parametrosMetodoBuscar, metodoResponse, idInputHidden);
                }

                htmlAutoCompleteRetorno += string.Concat("<input id='", idInputHidden, "' type='hidden'", @"data-bind=""", dataBindAttribute, "\"" + "/>");
                htmlAutoCompleteRetorno += @"<input id='" + id + @"' type='text' " + (disabled ? "disabled" : "") + " " + string_htmlAttributes + " />";

                return new HtmlString(htmlAutoCompleteRetorno);
            }
            catch
            {
                throw;
            }
        }

        private static string MontarScriptAutoComplete(string jquerySelector, int minLength, string urlMetodoBuscar, string[] parametrosMetodoBuscar, string metodoResponse, string idInputHidden = null)
        {
            string data = string.Empty;
            if (parametrosMetodoBuscar.Length > 1)
            {
                foreach (var param in parametrosMetodoBuscar)
                {
                    data += param;
                }
            }
            else
                data = parametrosMetodoBuscar[0] + " : request.term";

            return @"<script type='text/javascript'>
                        $(function () { "
                            + jquerySelector + @".autocomplete({
                                minLength: " + minLength + @",
                                source: function (request, response) {
                                    $.ajax({
                                        url: '" + urlMetodoBuscar + @"',
                                        dataType: 'json',
                                        data: {"
                                            + data +
                                        @"},
                                        success: function (result) {
                                            App.Util.TratarRetorno(result, function () {
                                                " + metodoResponse + @"
                                            });                                                                                    
                                        }
                                    });
                                },
                                select: function (e, ui) {
                                    "
                                     + (string.IsNullOrEmpty(idInputHidden)
                                    ? string.Empty : @"$('#" + idInputHidden + @"').val(ui.item.value).trigger('change'); /* bugfix knockout */
                                    ") + jquerySelector + @".val(ui.item.label);
                                    " + @" 
                                    return false;
                                },
                            });

                            " + jquerySelector + @".attr('autocomplete', 'on');

                        });

                </script>";
        }
        #endregion

        #region Auxiliares

        /// <summary>
        /// Método responsável por validar parametros a serem passados p/ realizar a busca do AutoComplete.
        /// </summary>
        /// <param name="controllerName">Objeto Controller que possui o método de busca os objetos JSON a serem populados no AutoComplete. </param>
        /// <param name="urlMetodoBuscar">String contento a URL do Método (/controller/método) a ser.</param>
        /// <returns>Boleano Verdadeiro (True) se passou por todas validações com sucesso, caso contrario retorna uma exceção.</returns>
        private static bool ValidaParametros(IController controllerName, string urlMetodoBuscar)
        {
            if (String.IsNullOrEmpty(urlMetodoBuscar))
            {
                throw new Exception("A Url do método de busca não pode ser nula/vazia.");
            }
            else
            {
                var metodoBuscar = urlMetodoBuscar.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                MethodInfo methods = controllerName.GetType().GetMethod(metodoBuscar[metodoBuscar.Length - 1]);

                if (methods != null && methods.ReturnType != typeof(JsonResult))
                {
                    throw new Exception("Tipo de retorno inválido para uma função de AutoComplete. Utilize JsonResult como retorno.");
                }
            }
            return true;
        }
        #endregion

    }
}