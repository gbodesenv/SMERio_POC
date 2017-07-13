using Rio.SME.Web.DTO;
using System;
using System.Web;
using System.Web.Mvc;

namespace Rio.SME.Web.Generico
{
    public class Autenticacao : AuthorizeAttribute
    {
        public string PermissaoAcesso { get; set; }
        public bool Autenticado { get; set; }
        public bool Autorizado { get; set; }
        public TipoEvento Evento { get; set; }

        public enum TipoEvento
        {
            Inserir,
            Excluir,
            Editar,
            Selecionar,
            Visualizar
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Usuário Logado
            if (httpContext.Session["UsuarioLogado"] != null)
            {
                Autenticado = true;

                // Permissão de Acesso
                if (!String.IsNullOrEmpty(PermissaoAcesso))
                {
                    Autorizado = (new Seguranca()).VerificaPermissao(PermissaoAcesso);
                }
                else
                {
                    Autorizado = true;
                }
            }
            else
            {
                Autenticado = false;
            }

            // Não está logado ou não possui permissão retorna falso
            if (!(Autenticado && Autorizado))
                return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Usuário não está logado
            if (!Autenticado)
            {
                //base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "Index",
                            ReturnUrl = filterContext.HttpContext.Request.RawUrl
                        })
                    );

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.HttpContext.Response.End();
                }
            }
            // Usuário não possui permissão
            else if (!Autorizado)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "NaoAutorizado",
                            area = ""
                        })
                    );

                //if ajax request set status code and end responcse
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}