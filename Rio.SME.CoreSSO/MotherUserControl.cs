using System;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Web.UI;
using MSTech.CoreSSO.Entities;
using System.Web;
using MSTech.CoreSSO.BLL;

namespace Rio.SME.CoreSSO
{
    public class MotherUserControl : MSTech.Web.WebProject.MotherUserControl
    {
        #region PROPRIEDADES

        /// <summary>
        /// Retorna o módulo que o usuário está acessando (carregado no evento OnLoad da página),
        /// de acordo com a Url.
        /// </summary>
        public SYS_Modulo Modulo
        {
            get
            {
                if (HttpContext.Current.Session[SYS_Modulo.SessionName] != null)
                    return (SYS_Modulo)HttpContext.Current.Session[SYS_Modulo.SessionName];

                return new SYS_Modulo();
            }
        }

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

        #endregion

        #region Métodos


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
        
        #endregion

    }
}
