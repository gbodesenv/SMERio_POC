﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSTech.CoreSSO.BLL;
using MSTech.CoreSSO.Entities;

namespace Rio.SME.CoreSSO
{

    public class MotherPage : MSTech.Web.WebProject.MotherPage
    {
        #region Propriedades

        public new SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[MSTech.Web.WebProject.ApplicationWEB.SessSessionWEB];
            }
            set
            {
                Session[MSTech.Web.WebProject.ApplicationWEB.SessSessionWEB] = value;
            }
        }

        /// <summary>
        /// Retorna o tema que está sendo utilizado.
        /// </summary>
        public string NomeTemaAtual
        {
            get
            {
                return Theme;
            }
        }

        /// <summary>
        /// Retorna o tema atual.
        /// </summary>
        public TemaPadraoSite TemaAtual
        {
            get
            {
                return (TemaPadraoSite)Enum.Parse(typeof(TemaPadraoSite), Theme, true);
            }
        }

        /// <summary>
        /// Retorna o módulo que o usuário está acessando (carregado no evento OnLoad da página),
        /// de acordo com a Url.
        /// </summary>
        public SYS_Modulo Modulo
        {
            get
            {
                if (HttpContext.Current.Session[SYS_Modulo.SessionName] != null)
                    return (SYS_Modulo) HttpContext.Current.Session[SYS_Modulo.SessionName];

                return new SYS_Modulo();
            }
        }

        #endregion Propriedades

        #region Eventos Page Life Cycle

        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
            if (this.Page.EnableTheming)
            {
                // Carrega o Theme dinamicamente quando configurado no web.config, devido a pré-compilação.
                System.Web.Configuration.PagesSection pagesSection = System.Configuration.ConfigurationManager.GetSection("system.web/pages") as System.Web.Configuration.PagesSection;
                if ((pagesSection != null))
                    Page.Theme = pagesSection.Theme;
            }
            else
            {
                this.Page.Theme = String.Empty;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("DirVirtual"))
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "DirVirtual", "<script type='text/javascript'>var diretorioVirtual ='" + Page.ResolveClientUrl("~") + "App_Themes/" + NomeTemaAtual + "/images/" + "';</script>");

            base.OnInit(e);

            #region Trace

            // ***** TRACE *****
            // Write a trace message.
            // Grava um as informações do trace no IIS que fica disponível na página raiz do site trace.axd
            // Precisa estar habilitado no web.config da aplicação
            if (Trace.IsEnabled)
            {
                try
                {
                    Trace.Write("__SessionWEB", __SessionWEB == null ? "NULL" : "OK");
                    //Trace.Write("__SessionWEB._AreaAtual", __SessionWEB._AreaAtual == null ? "NULL" : "OK");
                }
                catch (Exception ex)
                {
                    Trace.Write("Trace MotherPage", "OnInit", ex);
                }
            }

            #endregion Trace

            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryCore));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryUI));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryScrollTo));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.StylesheetToggle));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.Util));

                // Adição de javascript para correção de bugs do IE10 e utlilidades do sistema.
                sm.Scripts.Add(new ScriptReference("~/Includes/jsUtilGestao.js"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            string dirIncludes = ResolveUrl("~/Includes/");

            Page.Header.Controls.Add(UtilBO.SetStyleHeader(dirIncludes, "altoContraste.css", true));
            //Inicia tag que checa se o browser é IE6
            LiteralControl ifIE6 = new LiteralControl("<!--[if IE 6]>");
            Page.Header.Controls.Add(ifIE6);
            //Adiciona css para IE6
            Page.Header.Controls.Add(UtilBO.SetStyleHeader(dirIncludes, "cssIE6.css", false));
            //Fecha tag que checa se o browser é IE6
            LiteralControl endifIE6 = new LiteralControl("<![endif]-->");
            Page.Header.Controls.Add(endifIE6);

            if (__SessionWEB.__UsuarioWEB.Grupo != null)
            {
                // Verifica as permissões do grupo apenas na primeira vez que o usuário acessar a tela.
                if (HttpContext.Current.Request.CurrentExecutionFilePath.CompareTo(HttpContext.Current.Session[__SessionWEB.__UsuarioWEB.Grupo.gru_id.ToString()] ?? string.Empty) != 0)
                {
                    SYS_Modulo modulo;
                    // [Carla] Alterações para melhoria de performance.
                    // Método do Core que retorna as 2 entidades juntas no select (SYS_GrupoPermissao e SYS_Modulo).
                    __SessionWEB.__UsuarioWEB.GrupoPermissao = SYS_GrupoBO.GetGrupoPermissao_Grupo_By_Url
                        (ApplicationWEB.SistemaID, __SessionWEB.__UsuarioWEB.Grupo.gru_id
                         , HttpContext.Current.Request.CurrentExecutionFilePath, out modulo);
                    
                    HttpContext.Current.Session[SYS_Modulo.SessionName] = modulo;
                    HttpContext.Current.Session[__SessionWEB.__UsuarioWEB.Grupo.gru_id.ToString()] = HttpContext.Current.Request.CurrentExecutionFilePath;
                }
            }

            Title = MSTech.Web.WebProject.ApplicationWEB._TituloDasPaginas;

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Adiciona Init.js, que carrega na tela todas as funções declaradas dos outros scripts.
            // Carregar sempre por último - depois de todos os outros Js da página.
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
                sm.Scripts.Add(new ScriptReference(ArquivoJS.Init));
        }

        #endregion Eventos Page Life Cycle

        #region Métodos

        /// <summary>
        /// Redireciona para a página informada.
        /// </summary>
        /// <param name="url">Url da página para redirecionar</param>
        protected void RedirecionarPagina(string url)
        {
            Response.Redirect(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// Grava o erro, e seta mensagem de erro com a operação no label informado.
        /// </summary>
        /// <param name="ex">Excessão disparada</param>
        /// <param name="label">Label para mostrar a mensagem</param>
        /// <param name="operacao">Operação que originou o erro</param>
        public void TrataErro(Exception ex, Label label, string operacao)
        {
            ApplicationWEB._GravaErro(ex);
            label.Text = UtilBO.GetErroMessage("Erro ao tentar " + operacao + ".",
                                               UtilBO.TipoMensagem.Erro);
        }

        /// <summary>
        /// Método de validação de campos data para ser usado em Validators.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidarData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                SqlDateTime d = SqlDateTime.Parse(DateTime.Parse(args.Value).ToString("MM/dd/yyyy"));
                args.IsValid = true;
            }
            catch
            {
                args.IsValid = false;
            }
        }

        /// <summary>
        /// Retorna se o usuário está autenticado no sistema, verificando a SessionWEB e o FormsIdentity
        /// </summary>
        /// <returns>True - está autenticado | False - não está autenticado</returns>
        protected bool UserIsAuthenticated()
        {
            bool ret;
            if (!UsuarioWebIsValid())
            {
                GetFormsIdentityLoadSession();

                // Verifica se a SessionWeb foi carregada
                ret = UsuarioWebIsValid();
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Verifica se a Session do usuário está nula,
        /// se estiver verifica o FormsIdentity e carrega a Session
        /// </summary>
        private void GetFormsIdentityLoadSession()
        {
            try
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
                        __SessionWEB.__UsuarioWEB.Usuario = new SYS_Usuario
                           {
                               ent_id = new Guid(UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Entidade))
                               ,
                               usu_login = UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Login)
                           };
                        SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(__SessionWEB.__UsuarioWEB.Usuario);

                        // Carrega grupo na session através do ticket de autenticação
                        string gru_id = UtilBO.GetNameFormsAuthentication(ticket.Name, UtilBO.TypeName.Grupo);
                        if (!string.IsNullOrEmpty(gru_id))
                            __SessionWEB.__UsuarioWEB.Grupo = SYS_GrupoBO.GetEntity(new SYS_Grupo { gru_id = new Guid(gru_id) });
                        else
                        {
                            // Carrega grupos do usuário
                            IList<SYS_Grupo> list = SYS_GrupoBO.GetSelectBySis_idAndUsu_id(
                                    __SessionWEB.__UsuarioWEB.Usuario.usu_id
                                    , ApplicationWEB.SistemaID);

                            // Verifica se foi carregado os grupos do usuário
                            if (list.Count > 0)
                            {
                                // Seleciona o primeiro grupo do usuário logado para carregar na Session
                                __SessionWEB.__UsuarioWEB.Grupo = list[0];
                                 
                                // Este trecho guarda todas as permissões do usuário caso seja necessário visualizar
                                //DataTable dtUaPermissao = ESC_EscolaBO.RetornaUAPermissaoUsuarioGrupo(__SessionWEB.__UsuarioWEB.Usuario.usu_id, ApplicationWEB._EntidadeID, __SessionWEB.__UsuarioWEB.Grupo.gru_id);
                                //if (dtUaPermissao.Rows.Count > 0)
                                //{
                                //    int esc_id;
                                //    Int32.TryParse(dtUaPermissao.Rows[0]["esc_id"].ToString(), out esc_id);
                                //    __SessionWEB.__UsuarioWEB.Esc_idPermissao = esc_id;

                                //    //Caso não tenha escola, significa que o usuário possui permissão de Gestão
                                //    if (esc_id == 0)
                                //    {
                                //        if (!string.IsNullOrEmpty(dtUaPermissao.Rows[0]["uad_idSuperior"].ToString()))
                                //            __SessionWEB.__UsuarioWEB.Uad_idSuperiorPermissao = new Guid(dtUaPermissao.Rows[0]["uad_idSuperior"].ToString());
                                //    }
                                //}
                            }
                        }
                        // Carrega o cid_id na session referente a entidade do usuário autenticado
                        Guid ent_id = __SessionWEB.__UsuarioWEB.Usuario.ent_id;
                        Guid ene_id = SYS_EntidadeEnderecoBO.Select_ene_idBy_ent_id(ent_id);

                        SYS_EntidadeEndereco entityEntidadeEndereco = new SYS_EntidadeEndereco { ent_id = ent_id, ene_id = ene_id };
                        SYS_EntidadeEnderecoBO.GetEntity(entityEntidadeEndereco);

                        END_Endereco entityEndereco = new END_Endereco { end_id = entityEntidadeEndereco.end_id };
                        END_EnderecoBO.GetEntity(entityEndereco);

                        // Carrega nome ou login na session do usuário autenticado
                        PES_Pessoa entityPessoa = new PES_Pessoa { pes_id = __SessionWEB.__UsuarioWEB.Usuario.pes_id };
                        PES_PessoaBO.GetEntity(entityPessoa);
                        __SessionWEB.UsuarioLogado = string.IsNullOrEmpty(entityPessoa.pes_nome) ? __SessionWEB.__UsuarioWEB.Usuario.usu_login : entityPessoa.pes_nome;

                        LoadSessionSistema();
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
            }
        }

        /// <summary>
        /// Verifica se a Session do usuário está carregada
        /// </summary>
        /// <returns>True - está carregada | False - não está carregada</returns>
        protected bool UsuarioWebIsValid()
        {
            if (__SessionWEB.__UsuarioWEB.Usuario != null)
                return (__SessionWEB.__UsuarioWEB.Usuario.usu_id != Guid.Empty);
            return false;
        }

        /// <summary>
        /// Configura a Session com os dados do sistema
        /// </summary>
        protected void LoadSessionSistema()
        {
            // Armazena o nome do sistema atual na Session
            SYS_Sistema entitySistema = new SYS_Sistema { sis_id = ApplicationWEB.SistemaID };
            SYS_SistemaBO.GetEntity(entitySistema);
            __SessionWEB.TituloSistema = entitySistema.sis_nome;

            // Armazena o caminho do logo do sistema atual
            __SessionWEB.UrlLogoCabecalho = entitySistema.sis_urlLogoCabecalho;

            // Armazena caminho do logo e url do cliente na entidade logada (se existir)
            SYS_SistemaEntidade entitySistemaEntidade = new SYS_SistemaEntidade { sis_id = ApplicationWEB.SistemaID, ent_id = __SessionWEB.__UsuarioWEB.Usuario.ent_id };
            SYS_SistemaEntidadeBO.GetEntity(entitySistemaEntidade);

            if (!string.IsNullOrEmpty(entitySistemaEntidade.sen_urlCliente))
                __SessionWEB.UrlInstituicao = entitySistemaEntidade.sen_urlCliente;

            if (!string.IsNullOrEmpty(entitySistemaEntidade.sen_logoCliente))
                __SessionWEB.LogoCliente = entitySistemaEntidade.sen_logoCliente;
            else
                __SessionWEB.LogoCliente = "LOGO_CLIENTE.png";
        }

        /// <summary>
        /// Percorre o CheckBoxList passado, e checa os items em que o Value for encontrado dentro
        /// da lista de ids passada.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="chkList"></param>
        public void ChecarItensLista(List<string> ids, CheckBoxList chkList)
        {
            foreach (ListItem item in chkList.Items)
            {
                ListItem listItem = item;

                // Seleciona o item, caso encontrado na lista.
                item.Selected = (ids.Exists(s => s.Equals(listItem.Value, StringComparison.OrdinalIgnoreCase)));
            }
        }

        //protected void ConfigureReportViewer(ReportViewer reportViewer, string reportName)
        //{
        //    Uri urlReport = new Uri(ApplicationWEB.ReportURL);
        //    reportViewer.ServerReport.ReportServerUrl = urlReport;
        //    reportViewer.ServerReport.ReportServerCredentials = new SiteReportServerCredentials();
        //    reportViewer.ServerReport.ReportPath = String.Concat(ApplicationWEB.ReportPath, reportName);
        //}

        #endregion Métodos

        #region Classes

        //public sealed class SiteReportServerCredentials : IReportServerCredentials
        //{
        //    #region IReportServerCredentials Members

        //    public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        //    {
        //        authCookie = null;
        //        userName = null;
        //        password = null;
        //        authority = null;

        //        return false;
        //    }

        //    public WindowsIdentity ImpersonationUser
        //    {
        //        get
        //        {
        //            return null;
        //        }
        //    }

        //    public System.Net.ICredentials NetworkCredentials
        //    {
        //        get
        //        {
        //            SymmetricAlgorithm decript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
        //            //Get Credentials
        //            string userName = ApplicationWEB.ReportUserName;
        //            string pass = decript.Decrypt(ApplicationWEB.ReportPwd);
        //            string domain = ApplicationWEB.ReportDomain;
        //            //Retorna credencial de acesso ao usuário
        //            return new System.Net.NetworkCredential(userName, pass, domain);
        //        }
        //    }

        //    #endregion IReportServerCredentials Members
        //}

        //public sealed class SiteReportServerParametros
        //{
        //    private string[][] arrayParametrosAsp;
        //    private string[][] arrayParametrosRdl;

        //    public SiteReportServerParametros(string _paramsAsp, string _paramsRdl)
        //    {
        //        string[] auxArray = _paramsAsp.Split('&');
        //        arrayParametrosAsp = new string[auxArray.Length][];
        //        for (int i = 0; i < auxArray.Length; i++)
        //        {
        //            arrayParametrosAsp[i] = auxArray[i].Split('=');
        //        }

        //        auxArray = _paramsRdl.Split('&');
        //        arrayParametrosRdl = new string[auxArray.Length][];
        //        for (int i = 0; i < auxArray.Length; i++)
        //        {
        //            arrayParametrosRdl[i] = auxArray[i].Split('=');
        //        }
        //    }

        //    /// <summary>
        //    /// Retorna o Valor de um parâmetro qualquer (asp ou rdl)
        //    /// </summary>
        //    /// <param name="parametro">nome do parametro</param>
        //    /// <returns></returns>
        //    public string Value(string parametro)
        //    {
        //        for (int i = 0; i < (arrayParametrosAsp.Length); i++)
        //        {
        //            if (arrayParametrosAsp[i][0] == parametro)
        //                return arrayParametrosAsp[i][1];
        //        }
        //        for (int i = 0; i < (arrayParametrosRdl.Length); i++)
        //        {
        //            if (arrayParametrosRdl[i][0] == parametro)
        //                return arrayParametrosRdl[i][1];
        //        }
        //        throw new Exception("parâmetro não existente");
        //    }

        //    /// <summary>
        //    /// Retorna vetor de ReportParameter
        //    /// a partir dos parametros Rdl
        //    /// </summary>
        //    /// <returns></returns>
        //    public ReportParameter[] getReportParameters()
        //    {
        //        const string nula = null;
        //        ReportParameter[] lista = new ReportParameter[arrayParametrosRdl.Length];
        //        for (int i = 0; i < (arrayParametrosRdl.Length); i++)
        //        {
        //            Int64 aux;
        //            ReportParameter param;
        //            if ((string.IsNullOrEmpty(arrayParametrosRdl[i][1]))
        //                || ((Int64.TryParse(arrayParametrosRdl[i][1], out aux)) && aux < 0))
        //                param = new ReportParameter(arrayParametrosRdl[i][0], nula);
        //            else
        //            {
        //                param = new ReportParameter(arrayParametrosRdl[i][0], arrayParametrosRdl[i][1]);
        //            }
        //            lista[i] = param;
        //        }

        //        return lista;
        //    }
        //}

        #endregion Classes
    }
}