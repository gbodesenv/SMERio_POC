using AutoMapper;
using Rio.SME.Domain.Contracts.Services;
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
            return View();
//            if (ModelState.IsValid)
//            {
//                try
//                {
//#if AmbienteNT
//                    if (user.login.RemoveNonNumbers().Length == 0)
//                    {
//                        var lista = _funcionarioService.RetornarFuncionario(2).Where(x => x.Nome.IndexOf(user.login, StringComparison.OrdinalIgnoreCase) >= 0);
//                        var lista2 = _funcionarioService.RetornarFuncionario(1).Where(x => x.Nome.IndexOf(user.login, StringComparison.OrdinalIgnoreCase) >= 0);
//                        var lista3 = _funcionarioService.RetornarFuncionario(3).Where(x => x.Nome.IndexOf(user.login, StringComparison.OrdinalIgnoreCase) >= 0);

//                        if (lista.Any())
//                            user.login = lista.First().Matricula;
//                        else if (lista2.Any())
//                            user.login = lista2.First().Matricula;
//                        else if (lista3.Any())
//                            user.login = lista3.First().Matricula;

//                        user.password = "teste";
//                    }
//#endif
//                    var funcionario = _funcionarioService.AutenticarFuncionario(user.login, user.password);
//                    if (funcionario != null && !String.IsNullOrEmpty(funcionario.Nome))
//                    {
//                        if (funcionario.UnidadeGestora.Count >= 0)
//                        {
//                            DTO.Usuario usuario = new DTO.Usuario
//                            {
//                                Autenticado = true,
//                                Nome = funcionario.Nome,
//                                Senha = user.password,
//                                Matricula = funcionario.Matricula,
//                                CodigoUnidadeGestora = funcionario.UnidadeGestora[0].Codigo.ToString(),
//                                NomeUnidadeGestora = funcionario.UnidadeGestora[0].Nome,
//                                GruposPermissao = funcionario.GrupoPermissao,//.Where(x => !x.Nome.Equals(EnumHelper.GetEnumDescription<GrupoPermissaoEnum>(GrupoPermissaoEnum.JuntaRecursoAdministrativo.ToString()))).ToList(),
//                                CodigoPessoa = funcionario.CodigoPessoa
//                            };

//                            var funcionalidades = _funcionalidadeService.RetornarFuncionalidades(user.login, user.password, Convert.ToInt32(usuario.CodigoUnidadeGestora));

//                            usuario.Funcionalidades = funcionalidades;
//                            usuario.UnidadesGestoras = funcionario.UnidadeGestora;

//                            var grupoPrincipal = Utils.RetornarCargoPrincipal(funcionario.GrupoPermissao);
//                            usuario.CodigoCargoPrincipal = grupoPrincipal == null ? 0 : grupoPrincipal.Codigo;
//                            usuario.NomeCargoPrincipal = grupoPrincipal == null ? string.Empty : grupoPrincipal.Nome;

//                            Session["UsuarioLogado"] = usuario;

//                            if (String.IsNullOrEmpty(returnUrl))
//                                return ResponseResult(success: true, url: Url.Action("Inicial", "Home"));

//                            return ResponseResult(success: true, url: returnUrl);
//                        }
//                    }
//                    return ResponseResult(false, true, null, null, MensagensValidacao.ServicoSegurancaSemPermissao);
//                }
//                catch
//                {
//                    return ResponseResult(success: false, showMessage: true, messageError: MensagensErro.ErroServicoSeguranca);
//                }
//            }
//            return ResponseResult(success: false);
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
            var UsuarioLogadoDTO = (DTO.Usuario)Session["UsuarioLogado"];

            Mapper.CreateMap<DTO.Usuario, Models.Usuario>();
            var usuarioLogadoMap = Mapper.Map<Models.Usuario>(UsuarioLogadoDTO);

            return ResponseResult(true, content: usuarioLogadoMap);
        }

        [HttpGet]
        public ActionResult Navix()
        {
            return View();
        }
    }
}