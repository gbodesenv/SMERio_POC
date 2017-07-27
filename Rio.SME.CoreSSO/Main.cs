using MSTech.CoreSSO.BLL;
using MSTech.CoreSSO.Entities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Rio.SME.Domain.Entities.DTO;

namespace Rio.SME.CoreSSO
{

    public class Main
    {
        public UsuarioWeb _UsuarioDTO { get; set; }

        public Main()
        {
            _UsuarioDTO = HttpContext.Current.Session["UsuarioLogado"] == null ?
                usuarioMock()
                   :
                HttpContext.Current.Session["UsuarioLogado"] as UsuarioWeb;
        }

        public void Autenticar()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity;
                if (identity != null)
                {
                    // Recupera Ticket de autenticação gravado em Cookie
                    FormsIdentity id = identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Carrega usuário na session através do ticket de authenticação
                    _UsuarioDTO.Usuario = new SYS_Usuario
                    {
                        ent_id = new Guid(UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Entidade))
                           ,
                        usu_login = UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Login)
                    };
                    SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(_UsuarioDTO.Usuario);

                    // Carrega grupo na session através do ticket de autenticação
                    string gru_id = UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Grupo);
                    if (!string.IsNullOrEmpty(gru_id))
                        _UsuarioDTO.Grupo = SYS_GrupoBO.GetEntity(new SYS_Grupo { gru_id = new Guid(gru_id) });
                    else
                    {
                        // Carrega grupos do usuário
                        IList<SYS_Grupo> list = SYS_GrupoBO.GetSelectBySis_idAndUsu_id(
                                _UsuarioDTO.Usuario.usu_id
                                , ApplicationWEB.SistemaID);

                        // Verifica se foi carregado os grupos do usuário
                        if (list.Count > 0)
                            // Seleciona o primeiro grupo do usuário logado para carregar na Session
                            _UsuarioDTO.Grupo = list[0];
                    }
                    // Carrega o cid_id na session referente a entidade do usuário autenticado
                    Guid ent_id = _UsuarioDTO.Usuario.ent_id;
                    Guid ene_id = SYS_EntidadeEnderecoBO.Select_ene_idBy_ent_id(ent_id);

                    SYS_EntidadeEndereco entityEntidadeEndereco = new SYS_EntidadeEndereco { ent_id = ent_id, ene_id = ene_id };
                    SYS_EntidadeEnderecoBO.GetEntity(entityEntidadeEndereco);

                    END_Endereco entityEndereco = new END_Endereco { end_id = entityEntidadeEndereco.end_id };
                    END_EnderecoBO.GetEntity(entityEndereco);

                    // Carrega nome ou login na session do usuário autenticado
                    PES_Pessoa entityPessoa = new PES_Pessoa { pes_id = _UsuarioDTO.Usuario.pes_id };
                    PES_PessoaBO.GetEntity(entityPessoa);
                    _UsuarioDTO.Nome = string.IsNullOrEmpty(entityPessoa.pes_nome) ? _UsuarioDTO.Usuario.usu_login : entityPessoa.pes_nome;
                }
            }
        }

        private Domain.Entities.DTO.UsuarioWeb usuarioMock()
        {
            return new UsuarioWeb()
            {
                Autenticado = true,
                Nome = "Gabriel Oliveira",
                Senha = String.Empty,
                Matricula = "0000000000",
                CodigoUnidadeGestora = String.Empty,
                NomeUnidadeGestora = "Porto Alegre",
                CodigoPessoa = 0,
                NomeCargoPrincipal = "Desenvolvedor .NET"
                //,
                //SistemaWebCoreSSO = sistemaCoreSSOMock()
            };
        }

        private Domain.Entities.DTO.SistemaWebCoreSSO sistemaCoreSSOMock()
        {
            return new SistemaWebCoreSSO()
            {
                //Armazena titulo geral do sistema definido nos parâmetros do CoreSSO
                Titulo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TITULO_GERAL),
                //Armazena URL do sistema administrativo definido nos parâmetros do CoreSSO
                UrlCoreSSO = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_ADMINISTRATIVO),
                //Armazena URL do cliente definido nos parâmetros do CoreSSO
                UrlInstituicao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_CLIENTE),
                //Armazena mensagem de copyright definido nos parâmetros do CoreSSO
                MensagemCopyright = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.MENSAGEM_COPYRIGHT),
                //Armazena contato do help desk definido nos parâmetros do CoreSSO
                helpDeskContato = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.HELP_DESK_CONTATO)
            };
        }
    }
}
