using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Rio.SME.Web.DTO;
using Rio.SME.Domain.Entities.DTO;

namespace Rio.SME.Web.Helper
{
    /// <summary>
    /// Classe utilizada para criação do Menu
    /// </summary>
    public class Menu
    {
        #region Propriedades

        public string Id { get; set; }
        public string UrlDestino { get; set; }
        public string ChaveSeguranca { get; set; }
        public string Titulo { get; set; }
        public bool DispensaSeguranca { get; set; }
        public List<Menu> MenusInternos = new List<Menu>();

        #endregion

        #region Construtor
        public Menu(String titulo)
        {
            Titulo = titulo;
        }

        public Menu(String id, String titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public Menu(String id, String urlDestino, String chaveSeguranca, String titulo)
        {
            Id = id;
            UrlDestino = urlDestino;
            ChaveSeguranca = chaveSeguranca;
            Titulo = titulo;
        }

        public Menu(String id, String urlDestino, String chaveSeguranca, String titulo, bool dispensaSeguranca = false)
        {
            Id = id;
            UrlDestino = urlDestino;
            ChaveSeguranca = chaveSeguranca;
            Titulo = titulo;
            DispensaSeguranca = dispensaSeguranca;

        }
        #endregion

        public Menu CriarSubMenu(String id, String titulo)
        {
            Menu subMenu = new Menu(id, titulo);
            MenusInternos.Add(subMenu);

            return this;
        }

        public Menu CriarSubSubMenu(String idPai, String id, String titulo)
        {
            Menu subSubMenuPai = (from item in MenusInternos
                                  where item.Id.ToUpper().Trim().Equals(idPai.ToUpper().Trim())
                                  select item).FirstOrDefault();

            Menu subSubMenu = new Menu(id, titulo);
            subSubMenuPai.MenusInternos.Add(subSubMenu);

            return this;
        }

        public Menu AdicionarItemMenuPrincipal(String urlDestino, String chaveSeguranca, String titulo, bool dispensaSeguranca = false)
        {
            Menu subMenu = new Menu("", urlDestino, chaveSeguranca, titulo, dispensaSeguranca);
            MenusInternos.Add(subMenu);
            return this;
        }

        public Menu AdicionarItemSubMenu(String idPai, String urlDestino, String chaveSeguranca, String titulo, bool dispensaSeguranca = false)
        {
            Menu subMenuPai = (from item in MenusInternos
                               where item.Id.ToUpper().Trim().Equals(idPai.ToUpper().Trim())
                               select item).FirstOrDefault();

            subMenuPai.MenusInternos.Add(new Menu("", urlDestino, chaveSeguranca, titulo, dispensaSeguranca));

            return this;
        }

        public Menu AdicionarItemSubSubMenu(String idVo, String idPai, String urlDestino, String chaveSeguranca, String titulo, bool dispensaSeguranca = false)
        {
            Menu subMenuVo = (from item in MenusInternos
                              where item.Id.ToUpper().Trim().Equals(idVo.ToUpper().Trim())
                              select item).FirstOrDefault();

            Menu subMenuPai = (from item in subMenuVo.MenusInternos
                               where item.Id.ToUpper().Trim().Equals(idPai.ToUpper().Trim())
                               select item).FirstOrDefault();

            subMenuPai.MenusInternos.Add(new Menu("", urlDestino, chaveSeguranca, titulo, dispensaSeguranca));

            return this;
        }

        private String RetornaHtmlMenus(Menu menu)
        {
            string auxLinha = "<li><a href='{0}'><span>{1}</span></a></li>";
            string auxRet = "";
            string auxEstrutura = "<li class='dropdown'><a class='dropdown-toggle' href='#'><span>{0}&nbsp;</span><span class='caret'></span></a><ul class='dropdown-menu'>{1}</ul></li>";

            //Têm Menus internos
            if (menu.MenusInternos.Count > 0)
            {
                foreach (var item in menu.MenusInternos)
                {
                    auxRet += RetornaHtmlMenus(item);
                }
            }
            // Não contém Menus Internos
            else
            {
                if (menu.DispensaSeguranca)
                    auxRet = String.Format(auxLinha, menu.UrlDestino, menu.Titulo);
                else
                    if ((new Seguranca()).VerificaPermissao(menu.ChaveSeguranca))
                    {
                        auxRet = String.Format(auxLinha, menu.UrlDestino, menu.Titulo);
                    }

                return auxRet;
            }

            if (!String.IsNullOrEmpty(auxRet))
            {
                String retorno = String.Format(auxEstrutura, menu.Titulo, auxRet);

                return retorno;
            }
            return "";
        }

        private string MontaHtml()
        {
            StringBuilder strRetorno = new StringBuilder();
            String htmlRet = "";
            if (MenusInternos.Count == 0)
                return "";
            else
            {
                htmlRet = RetornaHtmlMenus(this);
            }

            return htmlRet;
        }

        #region IHtmlString Members

        public HtmlString Render()
        {
            String loginUsuario = "";

            // Verifica se o html do Menu já está em sessão
            if (HttpContext.Current.Session["UsuarioLogado"] != null)
            {
                loginUsuario = ((UsuarioWeb)HttpContext.Current.Session["UsuarioLogado"]).Matricula;
                //    if (HttpContext.Current.Session[String.Format("{0}-{1}", loginUsuario, this.Titulo)] != null)
                //        return new HtmlString(HttpContext.Current.Session[String.Format("{0}-{1}", loginUsuario, this.Titulo)].ToString());
            }

            String html = this.MontaHtml();
            HttpContext.Current.Session[String.Format("{0}-{1}", loginUsuario, this.Titulo)] = html;
            return new HtmlString(html);
        }

        #endregion
    }
}