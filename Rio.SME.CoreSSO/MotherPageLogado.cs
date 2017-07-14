using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSTech.CoreSSO.BLL;

namespace Rio.SME.CoreSSO
{
    public class MotherPageLogado : MotherPage
    {
        #region Propriedades

        /// <summary>
        /// Retorna o ent_id do usuário logado.
        /// </summary>
        public Guid Ent_ID_UsuarioLogado
        {
            get
            {
                if ((__SessionWEB.__UsuarioWEB != null) &&
                    (__SessionWEB.__UsuarioWEB.Usuario != null))
                    return __SessionWEB.__UsuarioWEB.Usuario.ent_id;

                return Guid.Empty;
            }
        }

        #endregion Propriedades

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verifica autenticação do usuário pelo Ticket da autenticação SAML
            if (!UserIsAuthenticated())
            {
                try
                {
                    // Verifica se o usuário foi enviado para a página de busca de alunos retidos do Integração Rio
                    if (Request.CurrentExecutionFilePath.ToLower().Contains("riocard/alunoretido/busca.aspx"))
                    {
                        // Cria uma sessio que será usada pra verificar se o usuário possui permissão para acessar o Integração Rio
                        Session.Add("AlunoRetido", "~/Beneficio/RioCard/AlunoRetido/Busca.aspx");
                        HttpContext.Current.Response.Redirect("~/SAML/Login.ashx", true);
                    }

                    HttpContext.Current.Response.Redirect("~/logout.ashx", true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Caso tenha grupo permissão, verifica as permissões no módulo atual para o usuário.
            if ((__SessionWEB.__UsuarioWEB.GrupoPermissao != null) && (__SessionWEB.__UsuarioWEB.GrupoPermissao.mod_id > 0))
            {
                // Verifica permissão do usuário, caso não tenha nehuma permissão na página redireciona para a Index.
                if ((!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir))
                {
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Você não possui permissão para acessar a página solicitada.", UtilBO.TipoMensagem.Alerta);
                    RedirecionarPagina("~/Index.aspx");
                }
            }

            // Registra o GATC para a página. Código implementado na MotherPageLogado
            UtilBO.RegistraGATC(Page);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Retorna as UA's de acordo com a visão do usuário, se o usuário estiver em um grupo
        /// com visão gestão ou unidade administrativa.
        /// </summary>
        /// <returns>UA's da visão do usuário, ou "" quando for outra visão de usuário.</returns>
        //public string UAsVisaoGrupo()
        //{
        //    return ESC_EscolaBO.GetUad_Ids_PermissaoUsuario
        //        (
        //            __SessionWEB.__UsuarioWEB.Grupo.gru_id
        //            , __SessionWEB.__UsuarioWEB.Usuario.usu_id
        //        );
        //}

        /// <summary>
        /// Retorna as UA's de acordo com a visão do usuário, se o usuário estiver em um grupo
        /// com visão gestão ou unidade administrativa.
        /// </summary>
        /// <returns>UA's da visão do usuário, ou "" quando for outra visão de usuário.</returns>
        //public List<Guid> UAsVisaoGrupoList()
        //{
        //    return ESC_EscolaBO.GetSelect_Uad_Ids_By_PermissaoUsuario
        //        (
        //            __SessionWEB.__UsuarioWEB.Grupo.gru_id
        //            , __SessionWEB.__UsuarioWEB.Usuario.usu_id
        //        );
        //}

        /// <summary>
        /// Seta a propriedade Enabled passada para todos os WebControl do ControlCollection
        /// passado.
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="enabled"></param>
        protected void HabilitaControles(ControlCollection controls, bool enabled)
        {
            foreach (Control c in controls)
            {
                if (c.Controls.Count > 0)
                    HabilitaControles(c.Controls, enabled);

                WebControl wb = c as WebControl;

                if (wb != null)
                    wb.Enabled = enabled;
            }
        }

   
        /// <summary>
        /// Este método vai buscar o parametro de mensagem dentro do texto e 
        /// caso este nao esteja no começo do texto substituirá as letra maiusculas do parametro.
        /// </summary>
        /// <param name="texto">Texto a ser exibido na tela</param>
        /// <param name="parametro">valor do parametro de mensagem</param>
        /// <returns></returns>
        //private static string TrataMaiusculaParametroMensagem(string texto, string parametro)
        //{
        //    int pos = texto.IndexOf(parametro);

        //    if (pos != 0)
        //    {
        //        texto = texto.Remove(pos, 1);
        //        texto = texto.Insert(pos, parametro.Substring(0, 1).ToLower());
        //    }

        //    return texto;
        //}

        #endregion Métodos
    }
}