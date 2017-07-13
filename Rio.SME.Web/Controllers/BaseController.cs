using Rio.SME.Domain.Contracts.Entities;
using Rio.SME.Domain.Util.ExtensionMethods;
using Rio.SME.Web.Filters;
using Rio.SME.Web.Generico;
using System;
using System.Collections;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Mvc;

namespace Rio.SME.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Método genérico para redirecionamento do botão voltar
        /// </summary>
        /// <returns>Redirecionamento do botão voltar</returns>
        [GenericoVoltarFilter(tirarDaPilha = true)]
        public ActionResult RedirecionarVoltar()
        {
            var stack = (Stack)Session["VoltarAux"];
            var url = !stack.IsNullOrEmpty() ? stack.Peek().ToString() : System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            return Redirect(url);
        }

        protected string GerarUrlVoltar()
        {
            var stack = (Stack)Session["Voltar"];
            var url = !stack.IsNullOrEmpty() ? stack.Peek().ToString() : System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            return url;
        }

        public void SalvarHashPagina(string hash)
        {
            Session["_hash_pagina_"] = hash;
        }

        /// <summary>
        /// Pega a transação atual do contexto e tenta salvar as alterações no banco
        /// </summary>
        protected void Commit()
        {
            this.CommitTransaction(UtilWeb.UsuarioLogado.CodigoPessoa);
        }

        /// <summary>
        /// Pega a transação atual do contexto e tenta salvar as alterações no banco
        /// </summary>
        /// <param name="codigoPessoa">O código da pessoa que efetuou a ação</param>
        protected void Commit(int codigoPessoa)
        {
            this.CommitTransaction(codigoPessoa);
        }

        /// <summary>
        /// Retira a url da action de Inserir da pilha caso esteja salvando pela primeira vez (Id == 0)
        /// </summary>
        /// <param name="entidade">Entidade para verificação</param>
        protected void RetirarURLPilha(IEntity entidade = null)
        {
            var stackExistente = (Stack)Session["Voltar"];

            if (!stackExistente.IsNullOrEmpty())
            {
                stackExistente.Pop();
                Session.Add("Voltar", stackExistente);
            }
        }

        /// <summary>
        /// Cria retorno padrão para o request feito pela view
        /// </summary>
        /// <param name="success">True or False</param>
        /// <param name="showMessage">Deve exibir a mensagem ao retornar para a tela</param>
        /// <param name="content">Conteúdo de retorno (caso se aplique)</param>
        /// <param name="url">Url de retorno (caso se aplique)</param>        
        /// <param name="messageError">Mensagem de erro (caso se aplique)</param>
        /// <returns>JSON de resposta</returns>
        protected JsonResult ResponseResult(bool success, bool showMessage = false, object content = null, string url = null, string messageError = null)
        {
            return Json(new
            {
                success,
                url,
                content,
                showMessage,
                message = (success && showMessage && messageError.IsNullOrEmpty()) ? "Rio.SME.Domain.Resources.MensagensSucesso.SucessoPOST" : messageError
            }, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Cria retorno para ser usado quando é necessário o retorno de muitos dados na tela, 
        /// por exemplo, conteudo de arquivos em base64
        /// </summary>
        /// <param name="success">True or False</param>
        /// <param name="showMessage">Deve exibir a mensagem ao retornar para a tela</param>
        /// <param name="content">Conteúdo de retorno (caso se aplique)</param>
        /// <param name="url">Url de retorno (caso se aplique)</param>        
        /// <param name="messageError">Mensagem de erro (caso se aplique)</param>
        /// <returns>JSON de resposta</returns>
        protected JsonResult ResponseResultRetornaConteudoArquivo(bool success, bool showMessage = false, object content = null,
            string url = null, string messageError = null)
        {
            JsonResult jsonResult = Json(new
            {
                success,
                url,
                content,
                showMessage,
                message = (success && showMessage && messageError.IsNullOrEmpty())
                        ? "Domain.Resources.MensagensSucesso.SucessoPOST"
                        : messageError
            }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //TODO: CODIGO ACIMA REVISAR
        #region Métodos auxiliares

        private void CommitTransaction(int codigoPessoa)
        {
            var unitOfWork = DependencyResolver.Current.GetService<Domain.Contracts.Data.Global.IUnitOfWork>();

            try
            {
                unitOfWork.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
#if AmbienteNT
                Debugger.Break();
#endif
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Entity:{0} Property: {1} Error: {2}",
                                                validationErrors.Entry.Entity.GetType().FullName,
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
#if AmbienteNT
                Debugger.Break();
#endif
                Trace.TraceInformation(ex.InnerException.Message);
            }
        }

        #endregion

    }
}