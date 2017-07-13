using Rio.SME.Domain.Contracts.Data.Global;
using System.Web.Mvc;

namespace Rio.SME.Web.Filters
{
    public class CommitedTransaction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Nova Instancia é criada para cada Request HTTP
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();

            unitOfWork.BeginTransaction();
            
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Nova Instancia é criada para cada Request HTTP
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();

            try
            {
                if (filterContext.Exception == null && filterContext.Result is JsonResult)
                {
                    var result = (JsonResult)filterContext.Result;
                    dynamic dynResult = result.Data;

                    if (dynResult.success)
                        unitOfWork.CommitTransaction();
                    else
                        unitOfWork.RollbackTransaction();
                }
                else if (filterContext.Exception != null && !filterContext.ExceptionHandled)
                {
                    unitOfWork.RollbackTransaction();
                }
            }
            catch
            {
                unitOfWork.RollbackTransaction();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}