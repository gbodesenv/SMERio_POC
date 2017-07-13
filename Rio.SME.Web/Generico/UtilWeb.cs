using AutoMapper;
using FluentScheduler;
using Microsoft.AspNet.SignalR;
using Rio.SME.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Usuario = Rio.SME.Web.DTO.Usuario;

namespace Rio.SME.Web.Generico
{
    public static class UtilWeb
    {
        /// <summary>
        /// Retorna o usuario logado para uso no projeto Web
        /// </summary>
        public static Usuario UsuarioLogado
        {
            [DebuggerStepThrough()]
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["UsuarioLogado"] != null)
                    return (Usuario)HttpContext.Current.Session["UsuarioLogado"];
                else
                    throw new ExcecaoSessaoExpirada();
            }
        }

        /// <summary>
        /// Retorna o Usuario logado para uso nos serviços
        /// </summary>
        //public static Domain.DTO.Usuario UsuarioLogadoDomain
        //{
        //    //[DebuggerStepThrough()]
        //    get
        //    {
        //        Mapper.CreateMap<Usuario, Domain.DTO.Usuario>();
        //        return Mapper.Map<Domain.DTO.Usuario>(UsuarioLogado);
        //    }
        //}

        //public static string MontarNomeUnidadeGestaoProcesso(ICollection<ProcessoUnidadeSituacao> processoUnidadeSituacaoLista,
        //    List<UnidadeGestoraPMV> listaUnidadeGestora)
        //{
        //    var listaUnidades = (from item in listaUnidadeGestora
        //                         where (from p in processoUnidadeSituacaoLista
        //                                where p.NumeroUnidadePMV == item.Codigo
        //                                select p).Any()
        //                         select item).ToList();

        //    string nomeUnidadesGestao = string.Join("; ", listaUnidades.Select(p => p.Nome));

        //    return nomeUnidadesGestao;
        //}

        /// <summary>
        /// Retorna o nome da unidade.
        /// </summary>
        /// <param name="listaUnidadeGestora">A lista de unidades gestora.</param>
        /// <param name="codigoUnidadeGestora">O codigo da unidade gestora.</param>
        /// <returns>Caso encontre a unidade, retorna o nome dela, caso contrário, vazio</returns>
        //public static string RetornarNomeUnidade(List<UnidadeGestoraPMV> listaUnidadeGestora, int codigoUnidadeGestora)
        //{
        //    var unidadeGestora = listaUnidadeGestora.FirstOrDefault(p => p.Codigo == codigoUnidadeGestora);
        //    return unidadeGestora == null ? String.Empty : unidadeGestora.Nome;
        //}

        public static string MontarHtmlAttribute(object dataAttribute)
        {
            string stringHtmlAttributes = string.Empty;

            foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(dataAttribute))
            {
                stringHtmlAttributes += string.Format("{0}=\"{1}\" ", property.Name.Replace('_', '-').Replace("@", ""), property.GetValue(dataAttribute));
            }

            return stringHtmlAttributes;
        }

        /// <summary>
        /// Método responsável por gerar as notificações para o usuário
        /// </summary>
        /// <param name="listaNotificacao">Lista contendo todas as notificações geradas no service</param>
        //public static void GerarNotificacaoUsuario(List<Notificacao> listaNotificacao)
        //{
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    if (listaNotificacao.Any())
        //    {

        //        foreach (var notificacao in listaNotificacao.Where(x => x.DataInicioOrigem.HasValue == false))
        //        {
        //            hubContext.Clients.Group(notificacao.NumeroResponsavel.ToString())
        //                .addNewMessageToPage(notificacao.Conteudo, notificacao.Id);
        //        }

        //        foreach (var notificacaoScheduler in listaNotificacao.Where(x => x.DataInicioOrigem.HasValue))
        //        {
        //            JobManager.AddJob(() => hubContext.Clients.Group(notificacaoScheduler.NumeroResponsavel.ToString())
        //                .addNewMessageToPage(notificacaoScheduler.Conteudo, notificacaoScheduler.Id),
        //                schedule => schedule.ToRunOnceAt(notificacaoScheduler.DataInicioOrigem.Value.Date));

        //        }
        //    }
        //}

    }
}