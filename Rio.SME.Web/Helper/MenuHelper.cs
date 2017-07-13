using System;
using System.Web.Mvc;

namespace Rio.SME.Web.Helper
{
    public static class MenuHelper
    {
        public static Menu HelperMenu(this HtmlHelper helper, String titulo)
        {
            return new Menu(titulo);
        }
    }
}