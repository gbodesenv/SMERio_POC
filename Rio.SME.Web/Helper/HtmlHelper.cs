using System.Web.Mvc;

namespace Rio.SME.Web.Helper
{
    public static class Html
    {
        public static bool IsAmbienteNT(this HtmlHelper htmlHelper)
        {
#if AmbienteNT
            return true;
#else
            return false;
#endif
        }
    }
}