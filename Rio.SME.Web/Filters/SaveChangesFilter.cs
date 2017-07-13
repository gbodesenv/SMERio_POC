using Rio.SME.Domain.Contracts.Data.Global;
using Rio.SME.Web.Generico;
using System.Web.Mvc;

namespace Rio.SME.Web.Filters
{
    public class SaveChangesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Caso não tenha erro anterior no contexto, tenta salvar as alterações
            if (filterContext.Exception == null)
            {
                var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
                unitOfWork.Commit();/*UtilWeb.UsuarioLogado.CodigoPessoa);*/
            }

            base.OnActionExecuted(filterContext);
        }
    }
}