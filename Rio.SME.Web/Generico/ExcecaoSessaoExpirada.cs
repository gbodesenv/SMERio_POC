using System;

namespace Rio.SME.Web.Generico
{
    class ExcecaoSessaoExpirada : Exception
    {
        public ExcecaoSessaoExpirada()
        {
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Response.Redirect(@"~/Home/Index?ReturnUrl=" +
                                                                 System.Web.HttpUtility.UrlEncode(
                                                                     System.Web.HttpContext.Current.Request.RawUrl));
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

    }
}
