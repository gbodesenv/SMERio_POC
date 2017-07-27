using NLog;
using Rio.SME.Domain.Exceptions;
using Rio.SME.Domain.Resources;
using Rio.SME.Web.App_Start;
using Rio.SME.Web.Generico;
using SimpleInjector.Integration.Web.Mvc;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Rio.SME.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();

            // Dependency Injector
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(new Infra.IOC.StartIOC().Container));

            // Areas
            AreaRegistration.RegisterAllAreas();

            // Configs
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
            AutoMapperConfig.Configure();
        }

        protected void Application_BeginRequest()
        {
#if !AmbienteNT
            Func<HttpRequest, bool> checkProfileOn = delegate(HttpRequest req)
            {
                return req.QueryString.AllKeys.Any(x => x == "MostrarMiniProfilerSigavix") ||
                       (req.UrlReferrer != null &&
                        req.UrlReferrer.Query.Contains("MostrarMiniProfilerSigavix=true"));
            };
#else
            Func<HttpRequest, bool> checkProfileOn = (req) => true;
#endif

            if (!checkProfileOn(HttpContext.Current.Request))
                return;

            HttpContext.Current.Items["MostrarMiniProfiler"] = true;

            MiniProfiler.Start();
            MiniProfiler.Settings.Results_Authorize = checkProfileOn;
            MiniProfiler.Settings.Results_List_Authorize = checkProfileOn;
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception erro = Server.GetLastError();

                // ServiceException já possui log e já foi dado o rollback, basta tratar o retorno
                // NotFoundException e ValidationException não necessitam log, são apenas descritivos
                // Demais exceptions são erros no Web e devem ser logados / tratados localmente
                if (erro is NotFoundException)
                {
                    ConfigurarRetornoRequest(erro.Message);
                }
                else if (erro is ValidationException)
                {
                    ConfigurarRetornoRequest(erro.Message);
                }
                else if (erro is ServiceException)
                {
                    // retornar mensagem padrão
                    ConfigurarRetornoRequest(MensagensErro.ErroWeb);
                }
                else
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Log(LogLevel.Error, erro, MensagensErro.ErroWeb);

                    // retornar mensagem padrão
                    ConfigurarRetornoRequest(MensagensErro.ErroWeb);
                }
            }
            catch
            {
                // retornar mensagem padrão
                ConfigurarRetornoRequest(MensagensErro.ErroWeb);
            }
        }

        private void ConfigurarRetornoRequest(string mensagemErro)
        {
            if (new HttpRequestWrapper(Request).IsAjaxRequest())
            {
                Context.ClearError();
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = 200;
                Context.Response.Write(
                    new JavaScriptSerializer().Serialize(
                        new
                        {
                            success = false,
                            showMessage = true,
                            message = mensagemErro
                        })
                );
            }
            else
            {
                Context.Response.Redirect(@"~/Home/Erro");
            }
        }
    }
}