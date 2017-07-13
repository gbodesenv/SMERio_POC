
using Rio.SME.Web.Generico;
using System;
using System.Text;
using System.Web;

namespace Rio.SME.Web.Helper
{
    public class FiltroHelper : IHtmlString
    {
        #region Constantes

        const string divBarra = @"<div class='d1 divBarra'><label class='labelGrupo'></label><span class='textSpan'>/</span></div>";
        const string divTipo = @"<div class='d2 divGrupo'><label class='labelinput'>Tipo:</label><select class='inputGrupoSeq' id='TipoPasta' name='TipoPasta'> 
                                <option value=''></option>
                                replaceItem
                                </select></div>";
        const string divOrgao = @"<div class='d2 divGrupo'><label class='labelGrupo'>Orgão:</label><input class='inputGrupo right' id='SiglaOrgaoProcesso' maxlength='15' name='SiglaOrgaoProcesso' type='text' value='' data-original-title='' title=''></div>";
        const string divCodigoPasta = @"<div class='d2 divGrupoNome'><label class='labelinput'></label><span class='textSpanBold'>Código Pasta:</span></div>";
        const string divCodigoProcesso = @"<div class='d2 divGrupoNome'><label class='labelinput'></label><span class='textSpanBold'>Código Processo:</span></div>";

        #endregion Constantes

        #region Propriedades
        public TipoFiltro Filtro { get; set; }
        public string IdFiltro { get; set; }
        public string IdFiltro2 { get; set; }
        public string IdTipoFiltro { get; set; }
        public string IdTipoFiltro2 { get; set; }
        public string ClasseFiltro { get; set; }
        public string ClasseFiltro2 { get; set; }
        public string ClasseTipoFiltro { get; set; }
        public string ClasseTipoFiltro2 { get; set; }
        public string CampoFiltro { get; set; }
        public string TituloFiltro { get; set; }
        public bool ExibirTipoFiltro { get; set; }
        private bool FiltroDuplo { get; set; }
        public bool FiltroTipoContem { get; set; }
        public object Obj { get; set; }

        public enum TipoFiltro
        {
            Inteiro,
            String,
            Data,
            Moeda,
            Pasta,
            Processo
        }
        #endregion

        #region Inserir Filtro Normal
        public static FiltroHelper InserirFiltro(bool exibirTipo, TipoFiltro filtro, string idFiltro, string idTipoFiltro, string campo, string titulo, bool filtroTipoContem, object obj = null)
        {
            return new FiltroHelper()
            {
                Filtro = filtro,
                IdFiltro = !string.IsNullOrWhiteSpace(idFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                IdTipoFiltro = !string.IsNullOrWhiteSpace(idTipoFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                CampoFiltro = campo,
                TituloFiltro = titulo,
                ExibirTipoFiltro = exibirTipo,
                ClasseFiltro = "col-md-9",
                ClasseTipoFiltro = "col-md-3",
                FiltroDuplo = false,
                FiltroTipoContem = filtroTipoContem,
                Obj = obj
            };
        }

        public static FiltroHelper InserirFiltro(bool exibirTipo, TipoFiltro filtro, string idFiltro, string idTipoFiltro, string campo, string titulo, object obj = null)
        {
            return new FiltroHelper()
            {
                Filtro = filtro,
                IdFiltro = !string.IsNullOrWhiteSpace(idFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                IdTipoFiltro = !string.IsNullOrWhiteSpace(idTipoFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                CampoFiltro = campo,
                TituloFiltro = titulo,
                ExibirTipoFiltro = exibirTipo,
                ClasseFiltro = "col-md-9",
                ClasseTipoFiltro = "col-md-3",
                FiltroDuplo = false,
                Obj = obj
            };
        }

        public static FiltroHelper InserirFiltro(bool exibirTipo, TipoFiltro filtro, string idFiltro, string idTipoFiltro, string campo, string titulo, string classeFiltro, string classeTipo, object obj = null)
        {
            return new FiltroHelper()
            {
                Filtro = filtro,
                IdFiltro = !string.IsNullOrWhiteSpace(idFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                IdTipoFiltro = !string.IsNullOrWhiteSpace(idTipoFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                CampoFiltro = campo,
                TituloFiltro = titulo,
                ExibirTipoFiltro = exibirTipo,
                ClasseFiltro = classeFiltro,
                ClasseTipoFiltro = classeTipo,
                FiltroDuplo = false,
                Obj = obj
            };
        }

        public static FiltroHelper InserirFiltro(bool exibirTipo, TipoFiltro filtro, string idFiltro, string idTipoFiltro, string campo, string titulo, string classeFiltro, string classeTipo, bool filtroTipoContem, object obj = null)
        {
            return new FiltroHelper()
            {
                Filtro = filtro,
                IdFiltro = !string.IsNullOrWhiteSpace(idFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                IdTipoFiltro = !string.IsNullOrWhiteSpace(idTipoFiltro) ? idTipoFiltro : Guid.NewGuid().ToString(),
                CampoFiltro = campo,
                TituloFiltro = titulo,
                ExibirTipoFiltro = exibirTipo,
                ClasseFiltro = classeFiltro,
                ClasseTipoFiltro = classeTipo,
                FiltroDuplo = false,
                FiltroTipoContem = filtroTipoContem,
                Obj = obj
            };
        }
        #endregion

        #region OBSOLETO - Inserir Filtro Duplo

        //public static FiltroHelper InserirFiltroDuplo(TipoFiltro filtro, string idFiltro1, string idFiltro2, string idTipoFiltro1, string idTipoFiltro2, string campo, string titulo, object obj = null)
        //{
        //    return new FiltroHelper()
        //    {
        //        ExibirTipoFiltro = true,
        //        Filtro = filtro,
        //        IdFiltro = idFiltro1,
        //        IdFiltro2 = idFiltro2,
        //        IdTipoFiltro = idTipoFiltro1,
        //        IdTipoFiltro2 = idTipoFiltro2,
        //        CampoFiltro = campo,
        //        TituloFiltro = titulo,
        //        ClasseFiltro = "col-md-3",
        //        ClasseFiltro2 = "col-md-3",
        //        ClasseTipoFiltro = "col-md-2",
        //        ClasseTipoFiltro2 = "col-md-2",
        //        FiltroDuplo = true,
        //        Obj = obj
        //    };
        //}

        //public static FiltroHelper InserirFiltroDuplo(TipoFiltro filtro, string idFiltro1, string idFiltro2, string idTipoFiltro1, string idTipoFiltro2, string campo, string titulo, string classeFiltro1, string classeFiltro2, string classeTipoFiltro1, string classeTipoFiltro2, object obj = null)
        //{
        //    return new FiltroHelper()
        //    {
        //        ExibirTipoFiltro = true,
        //        Filtro = filtro,
        //        IdFiltro = idFiltro1,
        //        IdFiltro2 = idFiltro2,
        //        IdTipoFiltro = idTipoFiltro1,
        //        IdTipoFiltro2 = idTipoFiltro2,
        //        CampoFiltro = campo,
        //        TituloFiltro = titulo,
        //        ClasseFiltro = classeFiltro1,
        //        ClasseFiltro2 = classeFiltro2,
        //        ClasseTipoFiltro = classeTipoFiltro1,
        //        ClasseTipoFiltro2 = classeTipoFiltro2,
        //        FiltroDuplo = true,
        //        Obj = obj
        //    };
        //}
        #endregion

        #region Montagem dos HTML
        public string MontaTipoFiltro(string tituloFiltro, string idTipoFiltro)
        {
            string filtro = "<td><label class='labelinput'>{0}</label></td>";

            filtro += "<td class='tipo-filtro'>{1}</td>";

            string opcoes = "";

            if (Filtro == TipoFiltro.Data)
            {
                opcoes = "<select class='form-control input-sm' id='" + idTipoFiltro + "'>" +
                            "<option value='0'>Igual</option>" +
                            "<option value='1'>Menor</option>" +
                            "<option value='2'>Maior</option>" +
                            "<option value='3'>Entre</option>" +
                         "</select>";
            }

            return String.Format(filtro, tituloFiltro, opcoes);
        }

        public string MontaInput(string idFiltro, string campoFiltro, bool FiltroDuplo, object obj = null, string idFiltroNovo = "")
        {
            string string_htmlAttributes = UtilWeb.MontarHtmlAttribute(obj);

            string input;
            if (Filtro == TipoFiltro.Data)
                input = "<td class = 'col-md-8' id=" + idFiltroNovo + ">";
            else if (FiltroDuplo)
                input = "<td>";
            else
                input = "<td colspan='4'>";

            if(Filtro == TipoFiltro.Data)
                input += "<input id='{0}' data-campo='{1}' class='{2}' type='text' " + string_htmlAttributes + " />";
            else
                input += "<input id='{0}' data-campo='{1}' class='{2}' type='text' " + string_htmlAttributes + " /></td>";
            string classInput = ClasseFiltro + " form-control input-sm ";
            classInput += Filtro == TipoFiltro.Inteiro ? "inteiro" : Filtro == TipoFiltro.Data ? "data calendario" : Filtro == TipoFiltro.Moeda ? "moeda" : "";

            if (FiltroDuplo)
                classInput = classInput + " " + "campo-duplo";

            input = String.Format(input, idFiltro, campoFiltro, classInput);

            
            string classInputNovo = Filtro == TipoFiltro.Inteiro ? "inteiro" : Filtro == TipoFiltro.Data ? "calendario" : Filtro == TipoFiltro.Moeda ? "moeda" : "";
            string inputNovo = "";

            if (Filtro == TipoFiltro.Data)
            {
                inputNovo = "<span class='tipoFiltroHidden' style='display:none; padding-left: 2%; padding-right: 2%;'>Até</span><input class='{2} tipoFiltroHidden' data-campo='{1}' style='display:none' type='text'/></td>";
                inputNovo = String.Format(inputNovo, idFiltroNovo, campoFiltro, classInput);
                input += inputNovo;
            }

            return input;
        }

        public string MontaDivCombinacao()
        {
            string div = "<td class='tipo-filtro-combinacao'>" +
                         "<select>{0}</select></td>";
            string opcoes = "<option value='1'>E</option><option value='2'>OU</option>";
            div = String.Format(div, opcoes);

            return div;
        }

        #endregion

        public string MontaHtml()
        {
            StringBuilder htmlFiltro = new StringBuilder();
            
            //Monta o 1º filtro
            string filtroAux = MontaTipoFiltro(TituloFiltro, IdTipoFiltro);
            var IdFiltroNovo = Guid.NewGuid().ToString();
            string inputAux = MontaInput(IdFiltro, CampoFiltro, FiltroDuplo, Obj, IdFiltroNovo);
            string scriptAux = MontaScript(IdFiltroNovo, IdTipoFiltro);

            htmlFiltro.Append(String.Format("{0}{1}{2}", filtroAux, inputAux, scriptAux));

            if (FiltroDuplo)
            {
                // Monta o 2º filtro
                filtroAux = MontaTipoFiltro("", IdTipoFiltro2);
                inputAux = MontaInput(IdFiltro2, CampoFiltro, FiltroDuplo, Obj);

                //Insere Div do E OU
                htmlFiltro.Append(MontaDivCombinacao());

                htmlFiltro.Append(String.Format("{0}{1}", filtroAux, inputAux));
            }

            htmlFiltro = new StringBuilder("<tr>" + htmlFiltro.ToString() + "</tr>");


            return htmlFiltro.ToString();
        }

        private string MontaScript(string IdFiltro, string IdSelect)
        {
            if (Filtro == TipoFiltro.Data)
                return @"<script>$(function () {$('#" + IdSelect + "').change(function(){if($(this).val() == '3'){$('#" + IdFiltro + " > .tipoFiltroHidden').show();$('#" + IdFiltro + " > input').css('width', '45.8%');} else { $('#" + IdFiltro + " > .tipoFiltroHidden').hide(); $('#" + IdFiltro + " > input').css('width', '100%'); } });});</script>";
            else
                return "";
        }

        #region IHtmlString Members

        public string ToHtmlString()
        {
            return MontaHtml();
        }

        #endregion
    }
}