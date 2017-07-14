using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using MSTech.Log;
using MSTech.CoreSSO.BLL;
using MSTech.CoreSSO.Entities;

namespace Rio.SME.CoreSSO
{
    public class ApplicationWEB : MSTech.Web.WebProject.ApplicationWEB, IRequiresSessionState
    {
        
        #region Propriedades

        protected new SessionWEB __SessionWEB
        {
            get { return ((SessionWEB)Session[SessSessionWEB]); }
            set { Session[SessSessionWEB] = value; }
        }

        public static int SistemaID
        {
            get
            {
                return 200;
            }
        }

      #endregion

        #region Metodos

        protected override void Session_Start(object sender, EventArgs e)
        {
            __SessionWEB = new SessionWEB();
        }

        /// <summary>
        /// Retorna a coleção em uma string única
        /// </summary>
        /// <param name="Colecao">Coleção de dados</param>
        /// <param name="nomeColecao">Nome da coleção</param>
        /// <param name="listaNaoGravar">Lista com os itens que não devem ser retornados na string</param>
        /// <returns>String única com os itens da coleção</returns>
        private static string retornaListaColecao(System.Collections.Specialized.NameValueCollection Colecao, string nomeColecao, List<string> listaNaoGravar)
        {
            string infoRequest = "\r\n*********** " + nomeColecao + " ***********";
            for (int i = 0; i < Colecao.Count; i++)
            {
                if (!(listaNaoGravar.Exists(p => p == Colecao.AllKeys[i])))
                {
                    infoRequest += "\r\n | ";
                    infoRequest += Colecao.AllKeys[i] + ": ";
                    infoRequest += Colecao[i];
                }
            }

            return infoRequest;
        }

        /// <summary>
        /// Salva log de erro no banco de dados. 
        /// Em caso de exceção salva em arquivo teste
        /// na pasta Log da raiz do site.
        /// </summary>
        /// <param name="ex">Exception</param>
        public new static void _GravaErro(Exception ex)
        {
            try
            {
                string path = String.Concat(_DiretorioFisico, "Log");
                LogError logError = new LogError(path);
                //Liga o método no delegate para salvar log no banco de dados.
                logError.SaveLogBD = delegate(string message)
                {
                    LOG_Erros entity = new LOG_Erros();
                    try
                    {
                        //Preenche a entidade com os dados necessário
                        entity.err_descricao = message;
                        entity.err_erroBase = ex.GetBaseException().Message;
                        entity.err_tipoErro = ex.GetBaseException().GetType().FullName;
                        entity.err_dataHora = DateTime.Now;
                        if (HttpContext.Current != null && HttpContext.Current.Request != null)
                        {
                            string infoRequest = "";
                            try
                            {
                                string naoGravar = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_CHAVES_NAO_GRAVAR);
                                List<string> listaNaoGravar = new List<string>(naoGravar.Split(';'));

                                bool gravarQueryString;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_QUERYSTRING), out gravarQueryString);
                                if (gravarQueryString)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.QueryString, "QueryString", listaNaoGravar);
                                }

                                bool gravarServerVariables;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_SERVERVARIABLES), out gravarServerVariables);
                                if (gravarServerVariables)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.ServerVariables, "ServerVariables", listaNaoGravar);
                                }

                                bool gravarParams;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_PARAMS), out gravarParams);
                                if (gravarParams)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.Params, "Params", listaNaoGravar);
                                }
                            }
                            catch
                            {
                                
                            }

                            entity.err_descricao = entity.err_descricao + infoRequest;

                            entity.err_ip = HttpContext.Current.Request.UserHostAddress;
                            entity.err_machineName = HttpContext.Current.Server.MachineName;
                            entity.err_caminhoArq = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                            try
                            {
                                entity.err_browser = String.Concat(new[] { HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.MajorVersion.ToString(), HttpContext.Current.Request.Browser.MinorVersionString });
                            }
                            catch
                            {
                                entity.err_browser = string.Empty;
                            }
                            if (HttpContext.Current.Session != null)
                            {
                                SessionWEB session = (SessionWEB)HttpContext.Current.Session[SessSessionWEB];
                                if (session != null)
                                {
                                    if (session.__UsuarioWEB.Usuario != null)
                                    {
                                        entity.usu_id = session.__UsuarioWEB.Usuario.usu_id;
                                        entity.usu_login = session.__UsuarioWEB.Usuario.usu_login;
                                    }
                                    if (session.__UsuarioWEB.Grupo != null)
                                    {
                                        SYS_Sistema sistema = new SYS_Sistema
                                        {
                                            sis_id = session.__UsuarioWEB.Grupo.sis_id
                                        };
                                        SYS_SistemaBO.GetEntity(sistema);
                                        entity.sis_id = sistema.sis_id;
                                        entity.sis_decricao = sistema.sis_nome;
                                    }
                                }
                            }
                        }
                        //Salva o log no banco de dados
                        LOG_ErrosBO.Save(entity);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                };
                logError.Log(ex, true);
            }
            catch { }
        }

        /// <summary>
        /// Grava log de sistema no banco de dados.
        /// </summary>
        /// <param name="acao">Ação executada pelo usuário</param>
        /// <param name="descricao">Descrição do log</param>
        /// <returns>Informa se o log de sistema foi salvo com sucesso.</returns>
        public static Guid _GravaLogSistema(LOG_SistemaTipo acao, string descricao)
        {
            try
            {
                LOG_Sistema entity = new LOG_Sistema();
                entity.log_acao = Enum.GetName(typeof(LOG_SistemaTipo), acao);
                entity.log_dataHora = DateTime.Now;
                entity.log_descricao = descricao;
                if (HttpContext.Current != null)
                {
                    //Preenche dados do host do site                    
                    LOG_SistemaBO.GenerateLogID();
                    entity.log_id = new Guid(HttpContext.Current.Session[LOG_Sistema.SessionName].ToString());
                    entity.log_ip = HttpContext.Current.Request.UserHostAddress;
                    entity.log_machineName = HttpContext.Current.Server.MachineName;
                    if (HttpContext.Current.Session != null)
                    {
                        SessionWEB session = (SessionWEB)HttpContext.Current.Session[SessSessionWEB];
                        if (session != null)
                        {
                            //Preenche dados referente ao usuário
                            if (session.__UsuarioWEB.Usuario != null)
                            {
                                entity.usu_id = session.__UsuarioWEB.Usuario.usu_id;
                                entity.usu_login = session.__UsuarioWEB.Usuario.usu_login;
                            }
                            //Preenche dados referente ao grupo do usuário
                            if (session.__UsuarioWEB.Grupo != null)
                            {
                                //Preenche os dados do grupo
                                entity.gru_id = session.__UsuarioWEB.Grupo.gru_id;
                                entity.gru_nome = session.__UsuarioWEB.Grupo.gru_nome;
                                //Preenche os dados do sistema
                                SYS_Sistema sistema = new SYS_Sistema
                                {
                                    sis_id = session.__UsuarioWEB.Grupo.sis_id
                                    , sis_nome = session.TituloSistema
                                };
                                
                                if (string.IsNullOrEmpty(sistema.sis_nome))
                                {
                                    // [Carla] Alterações para melhoria de performance.
                                    // Se o título do sistema na sessão estiver vazio, busca do banco.
                                    SYS_SistemaBO.GetEntity(sistema);
                                }

                                entity.sis_id = sistema.sis_id;
                                entity.sis_nome = sistema.sis_nome;
                                //Preenche os dados do módulo
                                SYS_Modulo modulo = (SYS_Modulo)HttpContext.Current.Session[SYS_Modulo.SessionName];
                                if (modulo != null)
                                {
                                    entity.mod_id = modulo.mod_id;
                                    entity.mod_nome = modulo.mod_nome;
                                }
                                //Preenche as entidades e unidades administrativa do grupo
                                if (session.__UsuarioWEB.GrupoUA != null)
                                {
                                    //Formata a entidade no padrão JSON
                                    //JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                                    //entity.log_grupoUA = oSerializer.Serialize(session.__UsuarioWEB.GrupoUA);
                                }
                            }
                        }
                    }
                }

                if (!LOG_SistemaBO.Save(entity))
                    throw new Exception();

                if (HttpContext.Current != null) 
                        HttpContext.Current.Session[LOG_Sistema.SessionName] = null;

                return entity.log_id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
