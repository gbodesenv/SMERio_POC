
using Rio.SME.Web.Generico;
using System.Linq;
using System.Web.Mvc;

namespace Rio.SME.Web.Controllers
{
    using AutoMapper;
    using Domain.Contracts.Services;
    using Domain.Enums;
    using Domain.Filters;
    using Rio.SME.Domain.Entities;
    using Rio.SME.Web.Filters;
    using System;

    [Autenticacao]
    public class AgrupadoresController : BaseController
    {
        #region Propriedades

        private readonly IAgrupadorService _agrupadorService;

        #endregion Propriedades

        #region Construtor

        public AgrupadoresController(IAgrupadorService agrupadorService)
        {
            _agrupadorService = agrupadorService;
        }
        #endregion Construtor

        #region Listar

        [GenericoVoltarFilter]
        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.ListarAgrupadorArea, Evento = Autenticacao.TipoEvento.Selecionar)]
        public ActionResult Listar()
        {
            return View();
        }


        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.ListarAgrupadorArea, Evento = Autenticacao.TipoEvento.Selecionar)]
        public ActionResult ListarGrid(AgrupadorFilter filtro)
        {
            var listaAgrupador = _agrupadorService.ListarGrid(filtro);

            var result = from agrupador in listaAgrupador
                         select new
                         {
                             Codigo = agrupador.Id,
                             Nome = agrupador.Nome,
                             Ativo = agrupador.IndicadorAtivo
                         };

            return ResponseResult(success: true, showMessage: false, content: new
            {
                iTotalRecords = listaAgrupador.Count(),
                iTotalDisplayRecords = filtro.TotalRecords,
                aaData = result
            });
        }

        #endregion Listar

        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.ListarAgrupadorArea, Evento = Autenticacao.TipoEvento.Selecionar)]
        public ActionResult Buscar(int id)
        {
            var dicionario = _agrupadorService.Listar(new AgrupadorFilter { Id = id }).FirstOrDefault();

            Mapper.CreateMap<Domain.Entities.Agrupador, Models.Agrupador>();
            var agrupadorAreaMap = Mapper.Map<Models.Agrupador>(dicionario);

            return ResponseResult(success: true, showMessage: false, content: agrupadorAreaMap);
        }


        #region Salvar

        #region Views Inserir/Editar

        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.IncluirAgrupadorArea, Evento = Autenticacao.TipoEvento.Inserir)]
        [GenericoVoltarFilter]
        public ActionResult Inserir()
        {
            return View("InserirEditar");
        }


        //
        // GET: /DicionarioArea/Editar/5
        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.EditarAgrupadorArea, Evento = Autenticacao.TipoEvento.Visualizar)]
        [GenericoVoltarFilter]
        public ActionResult Editar(int id)
        {
            ViewBag.Editar = true;
            ViewBag.Id = id;
            return View("InserirEditar");
        }

        #endregion Views Inserir/Editar


        [HttpPost]
        //[CommitedTransaction]
        [Autenticacao(PermissaoAcesso = FuncionalidadeSeguranca.IncluirAgrupadorArea, Evento = Autenticacao.TipoEvento.Inserir)]
        public ActionResult Salvar(Agrupador entidade)
        {
            try
            {
                _agrupadorService.Salvar(entidade);

                base.Commit();
                base.RetirarURLPilha(entidade);

                return ResponseResult(success: true, showMessage: true, url: Url.Action("Editar", new { id = entidade.Id }).Normalize());
            }
            catch (Exception ex)
            {
                return ResponseResult(success: false, showMessage: true, messageError: ex.Message);
            }
        }

        #endregion Salvar 
        
        
    }
}