using Rio.SME.Domain.Enums;
using Rio.SME.Domain.Util.Helpers;
using System;
using System.Web;
using System.Web.Mvc;

namespace Rio.SME.Web.Generico
{
    public static class Global
    {
        /// <summary>
        /// Caminho Cabeçalho dos Relatórios
        /// </summary>
        public static String RelatorioCabecalho(HttpRequestBase request)
        {
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoCab.png", wrapper);

            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoCabecalho = String.Format("{0}{1}/{2}?", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "header.html");

            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];

            if (!String.IsNullOrEmpty(caminhoTst))
                caminhoCabecalho = caminhoTst + "header.html?";

            if (!String.IsNullOrEmpty(caminhoLogo))
                caminhoCabecalho = String.Format("{0}logo={1}&", caminhoCabecalho, caminhoLogo);

            caminhoCabecalho += String.Format("&date={0}&time={1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.TimeOfDay);

            return caminhoCabecalho;
        }

        /// <summary>
        /// Caminho Rodapé dos Relatórios
        /// </summary>
        public static String RelatorioRodape(HttpRequestBase request)
        {
            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoRodape = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "footer.html");

            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];

            if (!String.IsNullOrEmpty(caminhoTst))
                caminhoRodape = caminhoTst + "footer.html";


            return caminhoRodape;
        }

        /// <summary>
        /// Margem Top do Cabeçalho do Relatório
        /// </summary>
        public static String RelatorioMargemTop
        {
            get
            {
                return "20";
            }
        }

        /// <summary>
        /// Margem Bottom do Rodapé do Relatório
        /// </summary>
        public static String RelatorioMargemBottom
        {
            get
            {
                return "15";
            }
        }

        /// <summary>
        /// Caminho do cabeçalho dos relatórios fotográficos
        /// </summary>
        public static String RelatorioFotograficoCabecalho(HttpRequestBase request, string numeroRelatorio)
        {
            var wrapper = new HttpContextWrapper(HttpContext.Current);

            string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoRelatorio.png", wrapper);

            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoCabecalho = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "headerRelatorioFotografico.html");

            if (!String.IsNullOrEmpty(caminhoLogo))
                caminhoCabecalho = String.Format("{0}?logo={1}", caminhoCabecalho, caminhoLogo);

            if (!String.IsNullOrEmpty(UtilWeb.UsuarioLogado.NomeUnidadeGestora))
                caminhoCabecalho = String.Format("{0}&unidade={1}", caminhoCabecalho, UtilWeb.UsuarioLogado.NomeUnidadeGestora);

            if (!String.IsNullOrEmpty(UtilWeb.UsuarioLogado.NomeUnidadeGestora))
                caminhoCabecalho = String.Format("{0}&usuarioLogado={1}", caminhoCabecalho, UtilWeb.UsuarioLogado.Nome);

            if (!String.IsNullOrEmpty(numeroRelatorio))
                caminhoCabecalho = String.Format("{0}&numrelatorio={1}", caminhoCabecalho, numeroRelatorio);

            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];

            //if (!String.IsNullOrEmpty(caminhoTst))
            //    caminhoCabecalho = caminhoTst + "headerRelatorioFotografico.html";

            return caminhoCabecalho;
        }

        public static String CaminhoLogo(HttpRequestBase request)
        {
            var wrapper = new HttpContextWrapper(HttpContext.Current);

            return UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoCab.png", wrapper);
        }

        #region VistaCopia

        ///// <summary>
        ///// Caminho do cabeçalho da Vista Cópia
        ///// </summary>
        //public static String RelatorioCabecalhoVistaCopia(HttpRequestBase request, Sigavix.Domain.Entities.Processo processo, string nomeUnidadePMV)
        //{
        //    var wrapper = new HttpContextWrapper(HttpContext.Current);

        //    string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoRelatorio.png", wrapper);

        //    string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
        //    string caminhoCabecalho = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "headerVistaCopia.html");

        //    if (!String.IsNullOrEmpty(caminhoLogo))
        //        caminhoCabecalho = String.Format("{0}?logo={1}", caminhoCabecalho, caminhoLogo);

        //    if (!String.IsNullOrEmpty(nomeUnidadePMV))
        //        caminhoCabecalho = String.Format("{0}&unidade={1}", caminhoCabecalho, nomeUnidadePMV);

        //    if (processo != null)
        //        caminhoCabecalho = String.Format("{0}&numProcesso={1}", caminhoCabecalho, processo.NumeroProcesso);

        //    caminhoCabecalho = String.Format("{0}&descricaoVistaCopia={1}", caminhoCabecalho, "Licenciamento_de_Atividades");


        //    return caminhoCabecalho;
        //}

        /// <summary>
        /// Caminho Rodapé dos Relatórios
        /// </summary>
        public static String RelatorioRodapeVistaCopia(HttpRequestBase request)
        {
            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoRodape = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "footerVistaCopia.html");

            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];

            if (!String.IsNullOrEmpty(caminhoTst))
                caminhoRodape = caminhoTst + "footerVistaCopia.html?";

            //caminhoRodape += String.Format("&date={0}&time={1}&usuLogado={2}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.TimeOfDay, UtilWeb.UsuarioLogado.Nome);

            if (!String.IsNullOrEmpty(UtilWeb.UsuarioLogado.Nome))
                caminhoRodape = String.Format("{0}usuarioLogado={1}", caminhoRodape, UtilWeb.UsuarioLogado.Nome.Replace(' ', '_'));

            return caminhoRodape;
        }

        /// <summary>
        /// Margem Top do Cabeçalho do Relatório
        /// </summary>
        public static String RelatorioVistaCopiaMargemTop
        {
            get
            {
                var margin = "25";
                return margin;
            }
        }

        /// <summary>
        /// Margem Bottom do Rodapé do Relatório
        /// </summary>
        public static String RelatorioVistaCopiaMargemBottom
        {
            get
            {
                var margin = "15";
                return margin;
            }
        }

        #endregion

        #region Auto Documento

        /// <summary>
        /// Caminho do cabeçalho do auto documento
        /// </summary>
        //public static String RelatorioCabecalhoAutoDocumento(HttpRequestBase request, string unidade, string numeroAutoDocumento, int? idTipoAutoDocumento, string siglaOrgao)
        //{
        //    var wrapper = new HttpContextWrapper(HttpContext.Current);

        //    string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoRelatorio.png", wrapper);

        //    string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
        //    string caminhoCabecalho = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "headerAutoDocumento.html");

        //    if (!String.IsNullOrEmpty(caminhoLogo))
        //        caminhoCabecalho = String.Format("{0}?logo={1}", caminhoCabecalho, caminhoLogo);

        //    if (!String.IsNullOrEmpty(unidade))
        //        caminhoCabecalho = String.Format("{0}&unidade={1}", caminhoCabecalho, unidade);

        //    if (!String.IsNullOrEmpty(numeroAutoDocumento))
        //        caminhoCabecalho = String.Format("{0}&numeroautodocumento={1}", caminhoCabecalho, numeroAutoDocumento);

        //    if (idTipoAutoDocumento.HasValue)
        //        caminhoCabecalho = String.Format("{0}&tipoautodocumento={1}", caminhoCabecalho, EnumHelper.GetDescriptionFromEnumValue((TipoAutoDocumentoEnum)idTipoAutoDocumento.Value).Replace(' ', '_'));

        //    if (!String.IsNullOrEmpty(siglaOrgao))
        //        caminhoCabecalho = String.Format("{0}&siglaorgao={1}", caminhoCabecalho, siglaOrgao);



        //    string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];


        //    return caminhoCabecalho;
        //}


        /// <summary>
        /// Caminho do cabeçalho do auto documento
        /// </summary>
        public static String RelatorioCabecalhoRelatorioVistoria(HttpRequestBase request, string unidade, string numeroRelatorioVistoria)
        {
            var wrapper = new HttpContextWrapper(HttpContext.Current);

            string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoRelatorio.png", wrapper);

            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoCabecalho = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "headerRelatorioVistoria.html");

            if (!String.IsNullOrEmpty(caminhoLogo))
                caminhoCabecalho = String.Format("{0}?logo={1}", caminhoCabecalho, caminhoLogo);

            if (!String.IsNullOrEmpty(unidade))
                caminhoCabecalho = String.Format("{0}&unidade={1}", caminhoCabecalho, unidade);

            if (!String.IsNullOrEmpty(numeroRelatorioVistoria))
                caminhoCabecalho = String.Format("{0}&numerorelatoriovistoria={1}", caminhoCabecalho, numeroRelatorioVistoria);


            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];


            return caminhoCabecalho;
        }


        /// <summary>
        /// Caminho Rodapé dos Relatórios
        /// </summary>
        public static String RelatorioRodapeAutoDocumento(HttpRequestBase request)
        {
            string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
            string caminhoRodape = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "footerVistaCopia.html");

            string caminhoTst = System.Configuration.ConfigurationManager.AppSettings["CaminhoRelatorioPublicacao"];

            if (!String.IsNullOrEmpty(caminhoTst))
                caminhoRodape = caminhoTst + "footerAutoDocumento.html?";

            //caminhoRodape += String.Format("&date={0}&time={1}&usuLogado={2}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.TimeOfDay, UtilWeb.UsuarioLogado.Nome);

            if (!String.IsNullOrEmpty(UtilWeb.UsuarioLogado.Nome))
                caminhoRodape = String.Format("{0}usuarioLogado={1}", caminhoRodape, UtilWeb.UsuarioLogado.Nome.Replace(' ', '_'));

            return caminhoRodape;
        }

        /// <summary>
        /// Margem Top do Cabeçalho do Relatório
        /// </summary>
        public static String RelatorioAutoDocumentoMargemTop
        {
            get
            {
                var margin = "25";
                return margin;
            }
        }

        /// <summary>
        /// Margem Bottom do Rodapé do Relatório
        /// </summary>
        public static String RelatorioAutoDocumentoMargemBottom
        {
            get
            {
                var margin = "15";
                return margin;
            }
        }

        #endregion Auto Documento


        //public static string RelatorioCabecalhoVistaCopia(HttpRequestBase request, Domain.Entities.AutoDocumento autoDocumento, string nomeUnidadePMV)
        //{

        //    var wrapper = new HttpContextWrapper(HttpContext.Current);

        //    string caminhoLogo = UrlHelper.GenerateContentUrl(@"~/content/themes/images/logoRelatorio.png", wrapper);

        //    string porta = System.Configuration.ConfigurationManager.AppSettings["PortaPublicacao"];
        //    string caminhoCabecalho = String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority) + porta + (request.ApplicationPath.EndsWith("/") ? request.ApplicationPath : request.ApplicationPath + "/"), "Relatorio", "headerVistaCopia.html");

        //    if (!String.IsNullOrEmpty(caminhoLogo))
        //        caminhoCabecalho = String.Format("{0}?logo={1}", caminhoCabecalho, caminhoLogo);

        //    if (!String.IsNullOrEmpty(nomeUnidadePMV))
        //        caminhoCabecalho = String.Format("{0}&unidade={1}", caminhoCabecalho, nomeUnidadePMV);

        //    if (autoDocumento != null)
        //        caminhoCabecalho = String.Format("{0}&numProcesso={1}", caminhoCabecalho, autoDocumento.NumeroAutoDocumento);

        //    caminhoCabecalho = String.Format("{0}&descricaoVistaCopia={1}", caminhoCabecalho, "Autuação");

        //    return caminhoCabecalho;
        //}
    }
}