using Rio.SME.Web.App_Start;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Optimization;

namespace Rio.SME.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/libs/jquery-2.2.3.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/util").Include(
                        "~/Scripts/libs/notificacao.js",
                        "~/Scripts/libs/menu.horizontal.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                        "~/Scripts/libs/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/utilvalidation").Include(
                        "~/Scripts/libs/util-validation.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libs/bootstrap.js",
                      "~/Scripts/libs/jquery.smartmenus.js",
                      "~/Scripts/libs/jquery.smartmenus.bootstrap.js",
                      "~/Scripts/libs/bootstrap-tabcollapse.js",
                      "~/Scripts/libs/bootstrap-dropdown.js",
                      "~/Scripts/libs/bootstrap-tab.js",
                      "~/Scripts/libs/bootstrap-tabdrop.js",
                      "~/Scripts/libs/respond.js",
                      "~/Scripts/libs/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/libs/jquery.validate*",
                        "~/Scripts/libs/validacao.pt-BR.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/site.css",
                        "~/Content/jquery.datepicker.css",
                        "~/Content/geral.css",
                        "~/Content/interna.css",
                        "~/Content/font-awesome.min.css",
                        //"~/Content/menu.horizontal.css",
                        //"~/Content/menu.responsivo.css",
                        //"~/Content/modal.css",
                        "~/Content/tabs.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                "~/Scripts/libs/jquery.dataTables.js",
                "~/Scripts/libs/dataTables.bootstrap.js",
                "~/Scripts/libs/jquery.dataTables.columnFilter.js",
                "~/Scripts/libs/dataTables.responsive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/autoComplete").Include(
                "~/Scripts/libs/jquery-ui-1.9.2.custom.js"));

            bundles.Add(new StyleBundle("~/Content/gridCss").Include(
                // "~/Content/css/dataTables.bootstrap.css",
                "~/Content/css/jquery.dataTables.css",
                "~/Content/css/responsive.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/autoCompleteCss").Include(
                "~/Content/jquery-ui-1.9.2.custom.css"));

            bundles.Add(new StyleBundle("~/Content/galeria").Include(
               "~/Content/galeria.css"));

            JSModelBundle jsModelBundle = new JSModelBundle("~/bundles/JsModel");
            //jsModelBundle.ModelList.Add(typeof(NomeDaClasse));
            bundles.Add(jsModelBundle);

            #region Cadastrar Scripts Genérico

            List<string> list = new List<string>(Directory.GetDirectories(HttpRuntime.AppDomainAppPath + "\\Scripts")).Where(s => !s.EndsWith("tinymce")).ToList();

            list.ForEach(s =>
            {
                var directory = new DirectoryInfo(s);
                string directoryName = directory.Name;
                var files = new List<string>(Directory.GetFiles(s));
                files.ForEach(f =>
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    bundles.Add(new ScriptBundle("~/bundles/" + directoryName.ToLower() + "/" + fileNameWithoutExtension).Include(
                        "~/Scripts/" + directoryName + "/" + fileNameWithoutExtension + ".js"));
                });

                var folders = directory.GetDirectories().ToList();

                if (folders.Count() > 0)
                {
                    foreach (var item in folders)
                    {
                        foreach (var file in item.GetFiles())
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.Name);

                            bundles.Add(new ScriptBundle("~/bundles/" + directoryName + "/" + item.Name + "/" + fileName).Include(
                                    "~/Scripts/" + directoryName + "/" + item.Name + "/" + fileName + ".js"));
                        }
                    }
                }

            });

            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}