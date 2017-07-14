﻿using System;
using System.Xml;
using MSTech.CoreSSO.BLL;

namespace Rio.SME.CoreSSO
{
    public class MotherMasterPage : MSTech.Web.WebProject.MotherMasterPage
    {
        #region PROPRIEDADES

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

        public string _VS_versao
        {
            get
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    String strRet = String.Empty;

                    xmlDoc.Load(ApplicationWEB._DiretorioFisico + "version.xml");
                    XmlNode xmlNd = xmlDoc.SelectSingleNode("//versionNumber");

                    if (xmlNd != null)
                        strRet = String.Format("Versão: {0}.{1}.{2}.{3}", xmlNd.ChildNodes[0].Attributes["value"].Value,
                            xmlNd.ChildNodes[1].Attributes["value"].Value,
                            xmlNd.ChildNodes[2].Attributes["value"].Value,
                            xmlNd.ChildNodes[3].Attributes["value"].Value
                            );


                    return strRet;
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Retorna o tema atual.
        /// </summary>
        public TemaPadraoSite TemaAtual
        {
            get
            {
                return (TemaPadraoSite)Enum.Parse(typeof(TemaPadraoSite), Page.Theme, true);
            }
        }

        #endregion

        #region METODOS
        public string RetornaLoginFormatado(string login)
        {
            if (!String.IsNullOrEmpty(login))
            {                
                // Corta o nome se tiver mais que 16 caracteres.
                if (login.Length > 16)
                    login = login.Substring(0, 13) + "...";
            }

            return login;
        }
        #endregion
    }
}
