using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Rio.SME.Web.Generico;
using Rio.SME.Web.DTO;

namespace Rio.SME.Web.Helper
{
    public class Barra
    {
        #region Variaveis

        public HtmlHelper Helper { get; set; }
        public string Titulo { get; set; }
        public string ID { get; set; }
        public string Icone { get; set; }
        public Lupa ObjLupa { get; set; }
        public object DataAttributes { get; set; }
        public bool IconeAjuda { get; set; }
        public string CodigoMensagemAjuda { get; set; }

        public List<BotaoBarra> ListaBotao = new List<BotaoBarra>();

        #endregion

        public Barra(HtmlHelper helper, string titulo, Lupa lupa, string UrlIcone, string id, object dataAttributes, bool iconeAjuda, string codigoMensagemAjuda)
        {
            Helper = helper;
            Titulo = titulo;
            ID = id;
            ObjLupa = lupa;
            Icone = UrlIcone;
            DataAttributes = dataAttributes;
            IconeAjuda = iconeAjuda;
            CodigoMensagemAjuda = codigoMensagemAjuda;
        }

        public Barra InserirBotao(BotaoBarra.TipoBotao tipoBotao, string idBotao, string tituloBotao, string hrefBotao, string legendaBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = String.IsNullOrEmpty(hrefBotao) ? "javascript:void(0);" : hrefBotao,
                IdBotao = idBotao,
                AcaoBotao = tipoBotao,
                TituloBotao = tituloBotao,
                LegendaBotao = legendaBotao,
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotao(string idBotao, string tituloBotao, string legendaBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                AcaoBotao = BotaoBarra.TipoBotao.Padrao,
                TituloBotao = tituloBotao,
                LegendaBotao = legendaBotao,
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoAbrir(string idBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                AcaoBotao = BotaoBarra.TipoBotao.Selecionar,
                TituloBotao = "Abrir",
                LegendaBotao = "Visualizar/Alterar dados",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }


        public Barra InserirBotaoIncluir(string idBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                AcaoBotao = BotaoBarra.TipoBotao.Inserir,
                TituloBotao = "Incluir",
                LegendaBotao = "Inserir novo registro",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoIncluir(string idBotao, string hRef, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = hRef,
                IdBotao = idBotao,
                AcaoBotao = BotaoBarra.TipoBotao.Inserir,
                TituloBotao = "Incluir",
                LegendaBotao = "Inserir novo registro",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoExcluir(string idBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                AcaoBotao = BotaoBarra.TipoBotao.Excluir,
                TituloBotao = "Excluir",
                LegendaBotao = "Excluir registro",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        private BotaoBarra CriarBotaoGravar(string codPermissao, string idBotao, BotaoBarra.TipoBotao tipoBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            return new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                AcaoBotao = tipoBotao,
                TituloBotao = "Gravar",
                LegendaBotao = "Salvar registro",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };
        }

        public Barra InserirBotaoGravarEditar(string codPermissao, string idBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = CriarBotaoGravar(codPermissao, idBotao, BotaoBarra.TipoBotao.Editar, esconder, permissao, obj);
            ListaBotao.Add(btn);
            return this;
        }

        public Barra InserirBotaoGravarInserir(string codPermissao, string idBotao, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = CriarBotaoGravar(codPermissao, idBotao, BotaoBarra.TipoBotao.Inserir, esconder, permissao, obj);
            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoLimpar(string idBotao, string idDivFiltro, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                IdBotao = idBotao,
                HRefBotao = "javascript:void(0);",
                AcaoBotao = BotaoBarra.TipoBotao.Limpar,
                TituloBotao = "Limpar",
                LegendaBotao = "Limpar Filtro",
                Auxiliar = idDivFiltro,
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoVoltar(bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                AcaoBotao = BotaoBarra.TipoBotao.Voltar,
                TituloBotao = "Voltar",
                LegendaBotao = "Voltar para página anterior",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoVoltar(String href, bool esconder = false, string permissao = null, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = href,
                AcaoBotao = BotaoBarra.TipoBotao.Voltar,
                TituloBotao = "Voltar",
                LegendaBotao = "Voltar para página anterior",
                Esconder = esconder,
                CodPermissao = permissao,
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoFiltrar(string id, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = id,
                AcaoBotao = BotaoBarra.TipoBotao.Filtro,
                TituloBotao = "Filtrar",
                LegendaBotao = "Filtrar",
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoRelatorio(string id, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = id,
                AcaoBotao = BotaoBarra.TipoBotao.Relatorio,
                TituloBotao = "Imprimir",
                LegendaBotao = "Imprimir",
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoFechar(string id, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = id,
                AcaoBotao = BotaoBarra.TipoBotao.Fechar,
                TituloBotao = "Fechar",
                LegendaBotao = "Fechar",
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoFecharSubModal(string id, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = id,
                AcaoBotao = BotaoBarra.TipoBotao.FecharSubModal,
                TituloBotao = "Fechar",
                LegendaBotao = "Fechar",
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }

        public Barra InserirBotaoFecharSubSubModal(string id, object obj = null)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = id,
                AcaoBotao = BotaoBarra.TipoBotao.FecharSubSubModal,
                TituloBotao = "Fechar",
                LegendaBotao = "Fechar",
                Obj = obj
            };

            ListaBotao.Add(btn);

            return this;
        }
        public Barra InserirBotaoAmbienteAnalise(string codPermissao, string idBotao, List<ItemBotao> itensHrefs = null, string legendaTipoAmbienteAnalise = "", string titulo = "", string legenda = "", bool esconder = false)
        {
            BotaoBarra btn = new BotaoBarra()
            {
                HRefBotao = "javascript:void(0);",
                IdBotao = idBotao,
                Auxiliar = UrlHelper.GenerateContentUrl(@"~/content/themes/images/imgMenuBarra.png", Helper.ViewContext.HttpContext),
                AcaoBotao = BotaoBarra.TipoBotao.AmbienteAnalise,
                TituloBotao = String.IsNullOrEmpty(titulo) ? "Ambiente de Análise" : titulo,
                LegendaBotao = String.IsNullOrEmpty(legenda) ? "Ambiente de Análise" : legenda,
                ItensBotao = itensHrefs,
                LegendaTipoAmbienteAnalise = legendaTipoAmbienteAnalise
            };
            if (!esconder)
                ListaBotao.Add(btn);

            return this;
        }

        public bool VerificaPermissao(BotaoBarra.TipoBotao acaoBotao, string permissao)
        {
            switch (acaoBotao)
            {
                //botões controlados pelas permissões
                case BotaoBarra.TipoBotao.Inserir:
                    return (new Seguranca()).VerificaPermissao(permissao ?? String.Empty);
                case BotaoBarra.TipoBotao.Excluir:
                    return (new Seguranca()).VerificaPermissao(permissao ?? String.Empty);
                case BotaoBarra.TipoBotao.Editar:
                    return (new Seguranca()).VerificaPermissao(permissao ?? String.Empty);
                case BotaoBarra.TipoBotao.Selecionar:
                    return (new Seguranca()).VerificaPermissao(permissao ?? String.Empty);
                case BotaoBarra.TipoBotao.Padrao:
                    return (new Seguranca()).VerificaPermissao(permissao ?? String.Empty);

                //botões sem permissões
                case BotaoBarra.TipoBotao.Voltar:
                    return true;
                case BotaoBarra.TipoBotao.Fechar:
                    return true;
                case BotaoBarra.TipoBotao.Limpar:
                    return true;
                case BotaoBarra.TipoBotao.FecharSubModal:
                    return true;
                case BotaoBarra.TipoBotao.FecharSubSubModal:
                    return true;
                case BotaoBarra.TipoBotao.Filtro:
                    return true;
                case BotaoBarra.TipoBotao.Relatorio:
                    return true;
                case BotaoBarra.TipoBotao.AmbienteAnalise:
                    return true;
                default:
                    return false;
            }
        }

        public string MontaHtmlBarra(string botoes)
        {
            string titulo = string.Empty;
            string icoBarra = string.Empty;
            string lupa = string.Empty;
            string srcImageLupa = UrlHelper.GenerateContentUrl(@"~/content/themes/images/icoLupa_branca.png", Helper.ViewContext.HttpContext);
            string string_htmlAttributes = UtilWeb.MontarHtmlAttribute(this.DataAttributes);

            if (string.IsNullOrEmpty(Icone))
            {
                titulo = "<div class='tituloBarra'>{0}";
            }
            else
            {
                titulo = "<div class='tituloBarra'><strong>{0}</strong>";
            }

            if (ObjLupa != null)
            {
                string popOverAjuda = string.Empty;
                if (ObjLupa.IconeAjuda)
                {
                    popOverAjuda = @"class='triggerPopover' data-toggle='popover' data-placement='right' 
                        data-message='" + ObjLupa.CodigoMensagemAjuda + "' data-destroy='true' data-active='true'";
                }

                if (string.IsNullOrEmpty(ObjLupa.FuncaoCustomizada))
                    lupa = "<a href='javascript:" + string.Format("App.Util.RedirecionarReferenciaLupa({0},{1},{2});", ObjLupa.CodigoRegistro, ObjLupa.TipoTabela, ObjLupa.FuncaoRedimencionamentoPadrao) + "' id='btnBuscarProcesso' " + popOverAjuda + " > <img class='alerta-trigger' src='" + srcImageLupa + "' border='0' /> </a>";
                else
                    lupa = "<a href='javascript:" + ObjLupa.FuncaoCustomizada + "' id='btnBuscarProcesso' " + popOverAjuda + " > <img class='alerta-trigger' src='" + srcImageLupa + "' border='0' /> </a>";
            }

            string spanTitulo = this.DataAttributes != null ? "<span " + string_htmlAttributes + "> </span>" : string.Empty;

            if (IconeAjuda)
                icoBarra = @"<i class='fa fa-info-circle triggerPopover icoBarra' data-toggle='popover' data-placement='right' 
                        data-message='" + CodigoMensagemAjuda + "' data-active='true'></i> ";
            else
                icoBarra = "<i class='fa fa-th-large icoBarra'> </i>";

            string htmlBarra = "<div class='barra'>" + icoBarra + titulo + spanTitulo + lupa + "</div>" +
                            (string.IsNullOrEmpty(botoes) ? "" :
                            "<div class='navbar-header'> " +
                            "	<button type='button' class='corbutton navbar-toggle collapsed' data-toggle='collapse' data-target='#navbar-collapseBarra" + ID + "'>" +
                            "		<span class='sr-only'>Menu</span> " +
                            "		  <span class='icon-bar corIcon'></span> " +
                            "		  <span class='icon-bar corIcon'></span> " +
                            "		<span class='icon-bar corIcon'></span> " +
                            "	</button> " +
                            "</div> " +
                            "<div id='navbar-collapseBarra" + ID + "' class='collapse navbar-collapseBarra'> " +
                            "<ul class='nav'>{1}</ul> </div>") + "</div>";

            htmlBarra = String.Format(htmlBarra, Titulo, botoes);

            return htmlBarra;
        }

        public string MontaHtmlBotoes()
        {
            String htmlBotoes = "";

            foreach (var item in ListaBotao)
            {
                var permissaoRequerida = !string.IsNullOrEmpty(item.CodPermissao);
                if (!item.Esconder && (!permissaoRequerida || (permissaoRequerida && VerificaPermissao(item.AcaoBotao, item.CodPermissao))))
                {
                    string string_htmlAttributes = UtilWeb.MontarHtmlAttribute(item.Obj);

                    if (item.AcaoBotao == BotaoBarra.TipoBotao.Filtro)
                    {
                        String li = "<li><a href='javascript:void(0);' id='{0}' alt='{1}' data-original-title='{1}' title='{1}' title='{1}' class='filter-trigger' " + string_htmlAttributes + " ><span>{2}</span></a></a></li>";
                        htmlBotoes += String.Format(li, item.IdBotao, item.TituloBotao, String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.Relatorio)
                    {
                        String li = "<li><a href='javascript:void(0);' id='{0}' alt='{1}' data-original-title='{1}' title='{1}' class='filter-trigger' " + string_htmlAttributes + " ><span>{2}</span></a></a></li>";
                        htmlBotoes += String.Format(li, item.IdBotao, item.TituloBotao, String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.Fechar)
                    {
                        String li = "<li><a href='{0}' id='{1}' alt='{2}' class='fechar-modal' data-original-title='{4}' title='{4}' {3} " + string_htmlAttributes + " ><span>{2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.FecharSubModal)
                    {
                        String li = "<li><a href='{0}' id='{1}' alt='{2}' class='fechar-sub-modal' data-original-title='{4}' title='{4}' {3} " + string_htmlAttributes + " ><span>{2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.FecharSubSubModal)
                    {
                        String li = "<li><a href='{0}' id='{1}' alt='{2}' class='fechar-sub-sub-modal' data-original-title='{4}' title='{4}' {3} " + string_htmlAttributes + " ><span>{2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.Limpar)
                    {
                        String li = "<li><a href='{0}' id='{1}' divId='{5}' alt='{2}' class='btnmodal limpar' data-original-title='{4}' title='{4}' {3} " + string_htmlAttributes + " ><span>{2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao, item.Auxiliar);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.Mapa)
                    {
                        String li = "<li><a href='{0}' id='{1}' alt='{2}' data-original-title='{4}' title='{4}' {5} {3} " + string_htmlAttributes + " ><span><i class='fa fa-map-o'></i> &nbsp; {2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao, item.Esconder ? "class='hidden'" : string.Empty);
                    }
                    else if (item.AcaoBotao == BotaoBarra.TipoBotao.AmbienteAnalise)
                    {

                        String li = "<li class='dropdown'><span title='{1}' class='dropdown-toggle bordaSub icone' style='padding-right: 7px !important;' data-toggle='dropdown' id='{0}'><img src='{2}' border='0'> {3}</span>";

                        if (item.ItensBotao != null && item.ItensBotao.Count > 0)
                        {
                            li += "<ul class='dropdown-menu' style='margin-left: -140px;'>";
                            foreach (var itemBotao in item.ItensBotao)
                            {
                                li = AdicionarSubitens(item, ref li, itemBotao);

                            }
                            li += "</ul>";
                        }

                        li += "</li>";
                        htmlBotoes += String.Format(li, item.IdBotao, item.TituloBotao, item.Auxiliar, item.LegendaTipoAmbienteAnalise);
                    }
                    else
                    {
                        String li = "<li><a href='{0}' id='{1}' alt='{2}' data-original-title='{4}' title='{4}' {5} {3} " + string_htmlAttributes + " ><span>{2}</span></a></li>";
                        htmlBotoes += String.Format(li, (String.IsNullOrEmpty(item.HRefBotao) ? "javascript:void(0);" : item.HRefBotao), item.IdBotao, item.TituloBotao, item.AcaoBotao == BotaoBarra.TipoBotao.Voltar ? "class='btn-voltar'" : "", String.IsNullOrEmpty(item.LegendaBotao) ? item.TituloBotao : item.LegendaBotao, item.Esconder ? "class='hidden'" : string.Empty);
                    }
                }
            }

            return htmlBotoes;
        }

        private static string AdicionarSubitens(BotaoBarra item, ref String li, ItemBotao itemBotao)
        {
            if (itemBotao.SubItens != null && itemBotao.SubItens.Count > 0)
            {
                li += string.Format(@"<li><a class='dropSubMenuSeta' href='{1}'>{0}</a><ul class='{2}'>", itemBotao.LegendaItemBotao, "javascript:void(0);", itemBotao.Class);
                foreach (var subItem in itemBotao.SubItens)
                {
                    if (subItem.SubItens != null && subItem.SubItens.Count > 0)
                        AdicionarSubitens(item, ref li, subItem);
                    else
                        li += string.Format(@"<li><a onclick='dropdown_menu_Click(""{1}"");'>{0}</a></li>", subItem.LegendaItemBotao, String.IsNullOrWhiteSpace(subItem.HRefBotao) ? "javascript:void(0);" : subItem.HRefBotao);
                }
                li += "</ul></li>";
            }
            else
            {
                li += string.Format(@"<li><a onclick='dropdown_menu_Click(""{1}"");'>{0}</a></li>", itemBotao.LegendaItemBotao, String.IsNullOrWhiteSpace(itemBotao.HRefBotao) ? "javascript:void(0);" : itemBotao.HRefBotao);
            }
            return li;
        }

        #region IHtmlString Members

        public HtmlString Render()
        {
            String htmlBotao = MontaHtmlBotoes();
            String htmlBarra = MontaHtmlBarra(htmlBotao);
            return new HtmlString(htmlBarra);
        }

        #endregion


    }



    public class Lupa
    {
        public int CodigoRegistro { get; set; }
        public int TipoTabela { get; set; }
        public string FuncaoRedimencionamentoPadrao { get; set; }
        public string FuncaoCustomizada { get; set; }
        public bool IconeAjuda { get; set; }
        public string CodigoMensagemAjuda { get; set; }
    }

    public class BotaoBarra
    {
        public TipoBotao AcaoBotao { get; set; }
        public string IdBotao { get; set; }
        public string TituloBotao { get; set; }
        public string HRefBotao { get; set; }
        public string CodPermissao { get; set; }
        public string LegendaBotao { get; set; }
        public string Auxiliar { get; set; }
        public bool Esconder { get; set; }
        public List<ItemBotao> ItensBotao { get; set; }
        public string LegendaTipoAmbienteAnalise { get; set; }
        public object Obj { get; set; }

        public enum TipoBotao
        {
            Inserir,
            Excluir,
            Editar,
            Selecionar,
            Voltar,
            Fechar,
            FecharSubModal,
            FecharSubSubModal,
            Padrao,
            Filtro,
            Relatorio,
            Limpar,
            AmbienteAnalise,
            Mapa
        }
    }

    public class ItemBotao
    {
        public ItemBotao(string cssClass = null)
        {
            if (!string.IsNullOrEmpty(cssClass))
                this.Class = cssClass;
            else
                this.Class = "dropdown-menu sub-menuConsulta";

            SubItens = new List<ItemBotao>();
        }
        public string HRefBotao { get; set; }
        public string LegendaItemBotao { get; set; }
        public string Auxiliar { get; set; }
        public bool Esconder { get; set; }
        public List<ItemBotao> SubItens { get; set; }
        public string Class { get; set; }
    }
}