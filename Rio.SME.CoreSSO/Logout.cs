using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using MSTech.CoreSSO.BLL;
using MSTech.SAML20;
using MSTech.SAML20.Bindings;
using MSTech.SAML20.Configuration;
using MSTech.SAML20.Schemas.Core;
using MSTech.SAML20.Schemas.Protocol;
using MSTech.Validation.Exceptions;

namespace Rio.SME.CoreSSO
{
    public class Logout : MotherPage, IHttpHandler, IRequiresSessionState
    {
        #region Propriedades

        private ResponseType SAMLResponse { get; set; }

        private LogoutRequestType SAMLRequest { get; set; }

        #endregion Propriedades

        #region Métodos

        /// <summary>
        /// You will need to configure this handler in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        #region IHttpHandler Members

        public new bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public new void ProcessRequest(HttpContext context)
        {
            try
            {
                // Carrega as configurações do ServiceProvider
                ServiceProvider config = ServiceProvider.GetConfig();
                ServiceProviderEndpoint spend = SAMLUtility.GetServiceProviderEndpoint(config.ServiceEndpoint, SAMLTypeSSO.logout);

                // Verifica configuração do ServiceProvider para logout
                if (spend == null)
                    throw new ValidationException("Não foi possível encontrar as configurações do ServiceProvider para logout.");

                // ***** RESPONSE *****
                if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLResponse]))
                {
                    // Recupera LogoutResponse
                    string samlresponse = context.Request[HttpBindingConstants.SAMLResponse];
                    XmlDocument doc = new XmlDocument();
                    doc.PreserveWhitespace = true;
                    doc.LoadXml(samlresponse);
                    SAMLResponse = SAMLUtility.DeserializeFromXmlString<ResponseType>(doc.InnerXml);

                    FormsAuthentication.SignOut();
                    if (context.Session != null)
                        context.Session.Abandon();

                    string url = spend.redirectUrl;
                    HttpContext.Current.Response.Redirect(url, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
                // ***** REQUEST *****
                else if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLRequest]))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    // Criação e configuração LogoutRequest
                    SAMLRequest = new LogoutRequestType();
                    SAMLRequest.ID = SAMLUtility.GenerateID();
                    SAMLRequest.Version = SAMLUtility.VERSION;
                    SAMLRequest.IssueInstant = DateTime.UtcNow.AddMinutes(10);
                    SAMLRequest.SessionIndex = new string[] { context.Session.SessionID };

                    NameIDType nameID = new NameIDType();
                    nameID.Format = SAMLUtility.NameIdentifierFormats.Transient;
                    nameID.Value = spend.localpath;

                    SAMLRequest.Item = nameID;
                    SAMLRequest.Issuer = new NameIDType();
                    SAMLRequest.Issuer.Value = config.id;

                    MemoryStream ms = new MemoryStream();
                    using (StreamWriter writer = new StreamWriter(new DeflateStream(ms, CompressionMode.Compress, true), Encoding.GetEncoding("iso-8859-1")))
                    {
                        writer.Write(SAMLUtility.SerializeToXmlString(SAMLRequest));
                        writer.Close();
                    }
                    string message = HttpUtility.UrlEncode(Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length, Base64FormattingOptions.None));
                    HttpRedirectBinding binding = new HttpRedirectBinding(message, spend.localpath);
                    binding.SendRequest(context, spend.redirectUrl);
                }
            }
            catch (ValidationException ex)
            {
                ErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                ErrorMessage("Não foi possível atender a solicitação.");
            }
        }

        #endregion IHttpHandler Members

        private void ErrorMessage(string message)
        {
            UtilBO.CreateHtmlFormMessage
                (
                    this.Context.Response.Output
                    , "SAML SSO"
                    , UtilBO.GetErroMessage(message + "<br />Clique no botão voltar e tente novamente.", UtilBO.TipoMensagem.Erro)
                    , string.Concat(__SessionWEB.UrlCoreSSO, "/Sistema.aspx")
                 );
        }

        #endregion Métodos
    }
}