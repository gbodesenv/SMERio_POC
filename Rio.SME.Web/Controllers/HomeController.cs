using AutoMapper;
using Rio.SME.Domain.Contracts.Services;
using Rio.SME.Domain.Entities.DTO;
using Rio.SME.Web.Filters;
using Rio.SME.Web.Generico;
using Rio.SME.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Rio.SME.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Propriedades

        private IUsuarioService _usuarioService;

        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        #endregion

        #region Index

        public ActionResult Index(string returnUrl)
        {
            ViewBag.Versao = Versionamento.BuscarUltimaModificacao();
            ViewBag.Titulo = "SME";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        #endregion

        #region Inicial

        /// <summary>
        /// Get da página inicial
        /// </summary>
        /// <returns>A página</returns>
        [GenericoVoltarFilter]
        [HttpGet]
        public ActionResult Inicial()
        {
            return View();
        }

        #endregion
            
        #region Autenticação

        [HttpPost]
        public ActionResult Timeout(string returnUrl)
        {
            if (HttpContext.Session != null)
                HttpContext.Session.Remove("UsuarioLogado");

            HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            HttpContext.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();

            return ResponseResult(success: true, showMessage: false,
                url: Url.Action("Index", "Home") + "?ReturnUrl=" + returnUrl);
        }

        /// <summary>
        /// Método get da página de usuário sem permissão
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NaoAutorizado()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLogin user, string returnUrl)
        {
            var coreSSO = new Rio.SME.CoreSSO.Main();
            coreSSO.Autenticar();
            Session["UsuarioLogado"] = coreSSO._UsuarioDTO;
            return ResponseResult(success: true, url: Url.Action("Listar", "Agrupadores"));           
        }

        /// <summary>
        /// Método get para efetuar o logoff da aplicação
        /// </summary>
        /// <returns>Direciona para a página inicial</returns>
        [HttpPost]
        public ActionResult Deslogar()
        {
            if (HttpContext.Session != null) HttpContext.Session.Remove("UsuarioLogado");

            HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            HttpContext.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();

            return ResponseResult(success: true, showMessage: false, url: Url.Action("Index", "Home"));
        }

        #endregion

        public ActionResult Erro()
        {
            return View();
        }
        
        /// <summary>
        /// Método para buscar o usuario logado para ser usado no JS
        /// </summary>
        /// <returns></returns>
        public ActionResult BuscaUsuarioLogado()
        {
            var UsuarioLogadoDTO = (UsuarioWeb)Session["UsuarioLogado"];

            Mapper.CreateMap<UsuarioWeb, Models.Usuario>();
            var usuarioLogadoMap = Mapper.Map<Models.Usuario>(UsuarioLogadoDTO);

            return ResponseResult(true, content: usuarioLogadoMap);
        }
              
    }
}