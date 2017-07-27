function abrirModal(idModal) {
    $("#" + idModal).modal("show");
}

function fecharModal(idModal) {
    $("#" + idModal).modal("hide");
}

function fecharModalImprimir(idIframe, idDivModal) {
    $("#" + idIframe).attr("src", "");
    var idDiv = idDivModal ? idDivModal : "divModalImprimir";
    $("#" + idDiv).hide();
}

function atualizarDropDownAbas() {
    setTimeout(function () {
        $(window).resize();
    }, 200);
    //$('.nav-tabs:first').tabdrop();
    //$('.nav-tabs:last').tabdrop({ text: 'Mais' });

    //if (tabName != null && tabName != undefined && clicar) {
    //    if ($("#" + tabName).length)
    //        $("#" + tabName)[0].click();

    //} else
    //    if (tabName != null && tabName != undefined) {
    //        $("#" + tabName).click();
    //    }
}

function setGridLanguage() {
    var language = {
        oPaginate: {
            sFirst: "Primeiro",
            sLast: "Último",
            sNext: "Próximo",
            sPrevious: "Anterior"
        },
        sEmptyTable: "Nenhum registro encontrado.",
        sZeroRecords: "Nenhum registro encontrado.",
        sInfoEmpty: "",
        sLengthMenu: "Mostrar _MENU_ registros",
        sInfoPostFix: "",
        sSearch: "Buscar:",
        sInfoFiltered: "",
        sInfo: "Listando _START_ - _END_ de _TOTAL_ registros.",
        sLoadingRecords: "Aguarde...",
        sProcessing: "Processando..."
    };

    return language;
}

function escondeDadosGrid(id) {
    $(".previous.paginate_button").hide();
    $(".next.paginate_button").hide();
    $(".dataTables_filter").hide();
    $(".dataTables_processing").hide();
    $("#" + id + "_length").hide();
}

function caracteresRestantes(campoLabel, campoTextArea) {
    var qtdText = 0;
    var qtdCaracteres = 200;
    if (campoTextArea.attr("maxlength") != undefined) {
        qtdCaracteres = campoTextArea.attr("maxlength");
    }
    if (campoTextArea.val() != null && campoTextArea.val() != "") {
        qtdText = campoTextArea.val().replace(/(\r\n|\n|\r)/g, "--").length;
    }

    if (qtdText > qtdCaracteres) {
        campoTextArea.val(campoTextArea.val().substring(0, qtdCaracteres));
    } else {
        var total = qtdCaracteres - qtdText;
        campoLabel.html(total.toString() + " caracteres restantes.");
    }
}

function atualizarTextArea() {
    $(".caracteres").each(function (key, value) {
        var textArea = $("#" + $(value).attr("data-for-id"));
        if (textArea[0]) {
            caracteresRestantes($(value), textArea);
            textArea.keyup(function () {
                caracteresRestantes($(value), textArea);
            });
        }
    });
}

function pad2(number) {
    return (number < 10 ? "0" : "") + number;
}

function converterDateTimeParaHoraMinuto(datetime) {
    if (datetime !== "") {
        return pad2(datetime.getHours()) + ":" + pad2(datetime.getMinutes());
    } else {
        return "";
    }
}

function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}

$(document).ready(function () {
    setupPluginsDataTable();
    atualizarTextArea();
    atualizarCpfCnpj(true);
    atualizarCpf(true);
    atualizarCnpj(true);
    atualizarNumericos();
    handleMessages();
    ajustarGridTabs();
    atualizarCalendarios();
    configurarPopover();
    bindingHandlersKnockout();
    //initTinyMce();
    atualizarTime(true);
    AtualizarTelefone();
    configurarMascaras();
    configurarEventoMascaraSipad();
    configurarExtendersValidacao();
    atualizarCampoTime();

    App.Util.ConfigurarAcessoMobile();

    if ($.fn.tooltip)
        $('[data-toggle="tooltip"]').tooltip();

    $("#btnFechaModalImprimir").click(function () {
        $("#iframeImprimir").attr("src", "");
        $("#divModalImprimir").hide();
    });

    $("#btnFechaModalRelatorioFotografico").click(function () {
        $("#iframeRelatorioFotografico").attr("src", "");
        $("#divModalRelatorioFotografico").hide();
    });
});

function setupPluginsDataTable() {
    if ($.fn.dataTableExt) {
        $.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
            // DataTables 1.10 compatibility - if 1.10 then `versionCheck` exists.
            // 1.10's API has ajax reloading built in, so we use those abilities
            // directly.
            if (jQuery.fn.dataTable.versionCheck) {
                var api = new jQuery.fn.dataTable.Api(oSettings);

                if (sNewSource) {
                    api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
                } else {
                    api.ajax.reload(fnCallback, !bStandingRedraw);
                }
                return;
            }

            if (sNewSource !== undefined && sNewSource !== null) {
                oSettings.sAjaxSource = sNewSource;
            }

            // Server-side processing should just call fnDraw
            if (oSettings.oFeatures.bServerSide) {
                this.fnDraw();
                return;
            }

            this.oApi._fnProcessingDisplay(oSettings, true);
            var that = this;
            var iStart = oSettings._iDisplayStart;
            var aData = [];

            this.oApi._fnServerParams(oSettings, aData);

            oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
                /* Clear the old information from the table */
                that.oApi._fnClearTable(oSettings);

                /* Got the data - add it to the table */
                var aData = (oSettings.sAjaxDataProp !== "") ?
                    that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

                for (var i = 0; i < aData.length; i++) {
                    that.oApi._fnAddData(oSettings, aData[i]);
                }

                oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

                that.fnDraw();

                if (bStandingRedraw === true) {
                    oSettings._iDisplayStart = iStart;
                    that.oApi._fnCalculateEnd(oSettings);
                    that.fnDraw(false);
                }

                that.oApi._fnProcessingDisplay(oSettings, false);

                /* Callback user function - for event handlers etc */
                if (typeof fnCallback == "function" && fnCallback !== null) {
                    fnCallback(oSettings);
                }
            }, oSettings);
        };
    }
}

errors = "";
function configurarExtendersValidacao() {
    ko.validation.rules["dataMenorHoje"] = {
        validator: function (val, validate) {
            var date = val.substring(0, 2);
            var month = val.substring(3, 5);
            var year = val.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                errors = this.message;
                return false;
            } else {
                errors = "";
                return true;
            }
        },
        message: "A data não pode ser maior que o dia de hoje."
    };

    ko.validation.registerExtenders();
};

function configurarEventoMascaraSipad() {
    $(".sipad").on("blur", function (e) {
        var textoSipad = $(this).val();

        var numberLength = parseInt(textoSipad.replace("/", "")).toString().length;
        if (numberLength <= 4) {
            $(this).val("").trigger("change");
        } else {
            textoSipad = pad(textoSipad, 11);
            $(this).val(textoSipad).trigger("change");
        }
    });

    $(".sipadIntegracao").on("blur", function (e) {
        var textoSipad = $(this).val();

        var numberLength = parseInt(textoSipad.replace("/", "")).toString().length;
        if (numberLength <= 4) {
            $(this).val("").trigger("change");
        } else {
            textoSipad = pad(textoSipad, 12);
            $(this).val(textoSipad).trigger("change");
        }
    });
};

function initTinyMce(callback) {
    //Só é necessário iniciar caso algum controle defina o handler WYSIWYG
    if (ko !== undefined) {
        if (ko.bindingHandlers["wysiwyg"] !== undefined) {
            ko.bindingHandlers["wysiwyg"].defaults = {
                //selector: 'textarea#tinymce',
                language: "pt_BR",
                entity_encoding: "raw",
                statusbar: false,
                height: 300,
                plugins: [
                    "print",
                    "image advlist autolink lists link image charmap print preview anchor",
                    "textcolor",
                    "insertdatetime media table contextmenu paste",
                    "noneditable"
                ],
                setup: function (editor) {
                    editor.on("init", function (e) {
                        callback();
                    });
                },
                extended_valid_elements: "input[type|style|onclick|checked|value|id],script[type|src],iframe[src|style|width|height|scrolling|marginwidth|marginheight|frameborder]",
                relative_urls: false,
                menubar: false,
                resize: "both",
                skin: "sigavix",
                toolbar: "print | undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect | table | hr removeformat | bullist numlist | outdent indent | link unlink image | forecolor backcolor",
                fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt 40pt 44pt 48pt",
                // file_browser_callback: function (field_name, url, type, win) {
                //     if (type == 'image') {
                //         $('#image').click();
                //     }
                // }                    
            };
        }
    }
}

function bindingHandlersKnockout() {
    ko.bindingHandlers.executeOnEnter = {
        init: function (element, valueAccessor, allBindings, viewModel) {
            var callback = valueAccessor();
            $(element).keypress(function (event) {
                var keyCode = (event.which ? event.which : event.keyCode);
                if (keyCode === 13) {
                    event.preventDefault();
                    callback.call(viewModel);
                    return false;
                }
                return true;
            });
        }
    };
}

function configurarPopover() {
    if ($.fn.popover) {
        var elementName = ".triggerPopover";
        var elements = $(elementName);

        for (var i = 0; i < elements.length; i++) {
            var popoverElement = $(elements[i]);
            var destroy = popoverElement.attr("data-destroy") && popoverElement.attr("data-destroy") == "true";
            var active = popoverElement.attr("data-active") && popoverElement.attr("data-active") == "true";

            buscarMensagemAjuda(popoverElement, function (element, content) {
                if (!App.Util.VerificarVazio(content) && !App.Util.VerificarVazio(content.Conteudo)) {
                    element.popover({
                        html: "true",
                        content: '<button type="button" class="close" onclick="hidePopover(this);"><span aria-hidden="true">&times;</span></button>' +
                            '<span class="text-info">' + content.Conteudo + "</span>"
                    });

                    //element.on('shown.bs.popover', function () { validarOverflowPopover(element) });
                    element.one("mostrarPopover", function () {
                        element.popover("show");
                    });

                    element.on("hidden.bs.popover", function () {
                        marcarMensagemComoLida(this);
                    });

                    if (destroy) {
                        element.on("hidden.bs.popover", function () {
                            element.off("hidden.bs.popover");

                            if ($(this).is(":visible"))
                                $(this).popover("destroy");
                        });
                    }
                    if (active) {
                        /* bugfix: tratamento para só mostrar o popover quando o elemento estiver visível em tela */
                        mostrarPopover(element);
                        $(".conteudoAba, .container").on("scroll", function () {
                            mostrarPopover(element);
                        });

                        $('a[data-toggle="tab"]').one("click", function () {
                            setTimeout(function () { mostrarPopover(element); }, 160);
                        });
                    }
                }
            });
        }
    }
};

function hidePopover(triggerIcon) {
    $(triggerIcon).closest(".popover").popover("hide");
}

function buscarMensagemAjuda(element, callback) {
    var contentKey = element.attr("data-message");

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        contentType: "application/html; charset=utf-8",
        url: UrlAction("MensagemAjuda/BuscarMensagemAjuda") + "?codigo=" + contentKey,
        success: function (result) {
            App.Util.TratarRetorno(result, function () {
                callback(element, result.content);
            });
        }
    });
};

function mostrarPopover(element) {
    if (visivelEmTela(element) && element.is(":visible")) {
        element.trigger("mostrarPopover");
    }
};

/*bugfix: código acrescentado para evitar que o overflow do popover */
//function validarOverflowPopover(element) {
//    var popOver = element.siblings('.popover');
//    var e = popOver[0];
//    if (e.getBoundingClientRect().top < $('.conteudo-iframe')[0].getBoundingClientRect().top)
//        popOver.css({ top: '0' });
//};

function visivelEmTela(element) {
    var $window = $(window);

    var docViewTop = $window.scrollTop();
    var docViewBottom = docViewTop + $window.height();

    var elemTop = element.offset().top;
    var elemBottom = elemTop + element.height();

    return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
}

function marcarMensagemComoLida(element) {
    var contentKey = $(element).attr("data-message");

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        contentType: "application/html; charset=utf-8",
        url: UrlAction("MensagemAjuda/MarcarComoLido") + "?codigo=" + contentKey
    });
};

function handleMessages() {
    var msg = window.sessionStorage.getItem("afterRedirectMessage");
    if (msg) {
        App.Util.MostrarAlertaSucesso(msg, 4000);
        window.sessionStorage.removeItem("afterRedirectMessage");
    }
}

//Atribui parâmetros da grid para o objeto de filtro
function converterFiltro(aoData, filter) {
    filter = filter || {};

    for (propriedade in filter) {
        var campoValor = $(".tableFiltro [data-campo='" + propriedade + "'][data-bind]");

        if (campoValor.parent().prev().hasClass("tipo-filtro")) {
            if (campoValor.hasClass("calendario")) {
                var valor = campoValor.val();
                if (valor) {
                    var colunaAnterior = campoValor.parent().prev();
                    var tipo = colunaAnterior.find("select").val();
                    var valor2 = "";

                    if (tipo == "3") {
                        valor2 = campoValor.parent().find("input").last().val();
                    }

                    filter[propriedade] = { Valor: valor, Valor2: valor2, Tipo: tipo };
                }
            }
        }
    }

    for (var i = 0; i < aoData.length; i++) {
        if (aoData[i].name == "iDisplayLength") {
            filter["Take"] = aoData[i].value;
        } else if (aoData[i].name == "iDisplayStart") {
            filter["Skip"] = aoData[i].value;
        } else if (aoData[i].name == "iSortCol_0") {
            filter["CampoOrdenacao"] = aoData[i].value;
        } else if (aoData[i].name == "sSortDir_0") {
            filter["Order"] = 0;

            if (aoData[i].value == "desc")
                filter["Order"] = 1;
        }
    }
}

App.ObjetoGlobalControladorDoAguarde = App.ObjetoGlobalControladorDoAguarde || [];
App.ObjetoGlobalControladorDoAguarde.remove = function () {
    App.ObjetoGlobalControladorDoAguarde.pop();
    if (!App.ObjetoGlobalControladorDoAguarde.length) {
        App.ObjetoGlobalControladorDoAguarde.div().hide();
    }
};
App.ObjetoGlobalControladorDoAguarde.adiciona = function () {
    if (!App.ObjetoGlobalControladorDoAguarde.length) {
        App.ObjetoGlobalControladorDoAguarde.div().show();
    }
    App.ObjetoGlobalControladorDoAguarde.push(true);
};
App.ObjetoGlobalControladorDoAguarde._div = undefined;
App.ObjetoGlobalControladorDoAguarde.div = function () {
    if (App.Util.VerificarVazio(this._div)) {
        this._div = $("#divCarregando");
    }
    return this._div;
};


$(document).bind("ajaxSend", function () {
    App.ObjetoGlobalControladorDoAguarde.adiciona();
}).bind("ajaxComplete", function () {
    App.ObjetoGlobalControladorDoAguarde.remove();
}).bind("ajaxStop", function () {
    atualizarTextArea();
    atualizarCpfCnpj(false);
    atualizarCpf(false);
    atualizarCnpj(false);
    //atualizarNumericos();
    atualizarCalendarios();
    atualizarTime(false);
    AtualizarTelefone();
    configurarMascaras();
});

App.Util = App.Util || {};

App.Util.Modal = App.Util.Modal || {};

//é possivel passar uma estrutura de botões personalizada
//e também uma de opções. Ver http://bootboxjs.com/documentation.html#bb-dialog-options
App.Util.Modal.CriarModal = function (urlActionView, title, namespace, buttons, options, removeScript, dados) {
    var message;

    //Se passar false, saiba o que está fazendo
    if (removeScript === undefined)
        removeScript = true;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "html",
        contentType: "application/html; charset=utf-8",
        data: dados,
        url: urlActionView,
        success: function (result) {
            App.Util.TratarRetorno(result, function () {
                //bold é para colocar o html inteiro dentro de uma tag e poder trabalhar com ele
                //sem usar innerHtml ou outerHtml
                var html = $(result.bold());
                //Remove os scripts que vem por meio do _ViewStart
                //e os demais (cuidado)
                if (removeScript === true)
                    html.find("script").remove();
                else {
                    html.find('script[src*="jquery"]').remove();
                    html.find('script[src*="libs"]').remove();
                }

                message = html.html();
            });
        }
    });

    if (options) {
        options.title = title;
        options.message = message;
        if (buttons)
            options.buttons = buttons;
        bootbox.dialog(options);
    } else {
        bootbox.dialog({
            title: title,
            message: message,
            closeButton: true,
            size: "large",
            show: true,
            className: "modal-tarefas",
            buttons:
            {
                fechar: {
                    label: "Fechar"
                }
            },
            onEscape: function() {
                if (typeof (tinymce) !== "undefined" && tinymce && tinymce.get("tinymce"))
                    tinymce.get("tinymce").remove();
            }
        });

        if (!App.Util.VerificarVazio(namespace))
            ko.applyBindings(global[namespace], $(".modalConteudo-bootbox")[0]);
    }
};
App.Util.Modal.Alerta = function (titulo, mensagem) {
    bootbox.dialog({
        title: titulo,
        message: mensagem,
        closeButton: true,
        show: true,
        buttons:
        {
            fechar: {
                label: "Fechar"
            }
        }
    });

    $(".modal-body").css("display", "flex");
};

//Método utilizado para fechar a modal
App.Util.Modal.FecharModal = function () {
    bootbox.hideAll();
};

App.Util.GetParameterByName = (function (name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);

    if (results === null) return $("#Hdn" + name).val();

    return decodeURIComponent(results[1].replace(/\+/g, " "));
});

App.Util.GetArrayParameterByName = (function (name) {
    var url = decodeURIComponent(window.location.search.substring(1)),
        urlParameters = url.split("&"),
        parameter,
        i;
    var arrayParameter = [];
    for (i = 0; i < urlParameters.length; i++) {
        parameter = urlParameters[i].split("=");

        if (parameter[0] === name) {
            urlParameters.map(function (item) {
                var itemParam = item.split("=");
                if (itemParam[0] === parameter[0] && itemParam[0] === name)
                    arrayParameter.push(itemParam[1]);
            });
            return arrayParameter;
        }
    };
});

App.Util.FiltroGrid = App.Util.FiltroGrid || (function (sSource, aoData, fnCallback, filter) {
    var objetoInterno = {};
    for (var prop in filter) {
        if (ko.isObservable(filter[prop]))
            objetoInterno[prop] = ko.observable(filter[prop]());
    }

    converterFiltro(aoData, objetoInterno);
    objetoInterno = $.toDictionary(objetoInterno);
    $.getJSON(sSource, objetoInterno, function (result) {
        App.Util.TratarRetorno(result, function () {
            fnCallback(result.content);
        });
    });
});

App.Util.LimparInformacoes = App.Util.LimparInformacoes || (function (model) {
    for (var propriedade in model) {
        if (model[propriedade]() !== undefined && model[propriedade]().constructor !== Array) {
            model[propriedade]("");
            if ($(".tableFiltro [data-campo='" + propriedade + "'][data-bind]").parent().prev().hasClass("tipo-filtro")) {
                $(".tableFiltro [data-campo='" + propriedade + "']").each(function () {
                    $(this).val("");
                    if (!$(this).attr("data-bind")) {
                        $(this).hide();
                        $(".tableFiltro [data-campo='" + propriedade + "'][data-bind]").parent().prev().find("select").val("0").trigger("change");
                    }
                });
            }
            //Limpa qualquer select restante
            $(".tableFiltro > tbody > tr > td").not(".tipo-filtro").find("select").val(undefined);
        }
    }
});

App.Util.confirmExit = function () {
    return "Existem alterações nesta página que não foram salvas.";
    //return;
};
App.Util.PreencherListaViewModel = function (self, listaOriginal, listaPreencher, objetoAdicionarCasoListaVazia, callback, required) {
    if (listaPreencher.length == 0 && objetoAdicionarCasoListaVazia != undefined) {
        listaPreencher.push(objetoAdicionarCasoListaVazia);
    }

    for (var i = 0; i < listaPreencher.length; i++) {
        listaOriginal.push(listaPreencher[i]);
    }

    for (var i = 0; i < listaOriginal.length; i++) {
        var objToPush = {};
        for (var prop in listaOriginal[i]) {
            objToPush[prop] = ko.observable(listaOriginal[i][prop]);

            if (required === true)
                objToPush[prop] = objToPush[prop].extend({ required: true });
        }
        App.Util.AssinarChanged(objToPush, listaOriginal[i]);

        self.push(objToPush);
    }

    self.subscribe(function (changes) {
        if (self().length != listaOriginal.length) {
            window.onbeforeunload = App.Util.confirmExit;
        } else {
            window.onbeforeunload = null;
            App.Util.VerificarListas(self, listaOriginal);
        }
    }, null, "arrayChange");

    if (callback)
        callback();
};
App.Util.PreencherListaPrimitivaViewModel = function (self, listaPreencher) {
    for (var i = 0; i < listaPreencher.length; i++) {
        self.push(listaPreencher[i]);
    }
};
App.Util.PreencherLista = function (self, listaPreencher) {
    for (var i = 0; i < listaPreencher.length; i++) {
        var objToPush = {};
        for (var prop in listaPreencher[i]) {
            objToPush[prop] = ko.observable(listaPreencher[i][prop]);
        }
        self.push(objToPush);
    }
};
App.Util.ExisteBotaoGravarVisivel = function () {
    var retorno = false;
    for (var i = 0; i < $("a>span").length; i++) {
        if ($($("a>span")[i]).text() == "Gravar")
            retorno = true;
    }
    return retorno;
};
App.Util.VerificarListas = function (self, listaOriginal) {
    if (!App.Util.ExisteBotaoGravarVisivel()) {
        window.onbeforeunload = null;
        return false;
    }

    var retorno = false;
    for (var i = 0; i < self().length; i++) {
        for (var j = 0; j < listaOriginal.length; j++) {
            for (var propLstPrm in self()[i]) {
                for (var propLstSeg in listaOriginal[j]) {
                    if (propLstPrm == propLstSeg && i === j) {
                        if (self()[i][propLstPrm]() !== listaOriginal[j][propLstSeg])
                            retorno = true;
                    }
                }
            }
        }
    }
    if (retorno == true) {
        window.onbeforeunload = App.Util.confirmExit;
    } else {
        window.onbeforeunload = null;
    }
};
App.Util.PreenchePropriedadesViewModel = function (self, objetoCompleto, NaoVerificarChanged) {
    for (propriedade in objetoCompleto) {
        self[propriedade] = ko.observable(objetoCompleto[propriedade]);
    }

    if (NaoVerificarChanged === undefined || NaoVerificarChanged === false)
        App.Util.AssinarChanged(self, objetoCompleto);
};
App.Util.AssinarChanged = function (self, objetoCompleto) {
    self.changed = ko.computed(function () {

        //if (App.Util.IsCarregando()) {
        //    return false;
        //}
        //if (App.ObjetoGlobalControladorDoAguarde.div().is(':visible')) {
        //    return false;
        ////}
        //if ($('#divCarregando').is(':visible')) {
        //    return false;
        //}
        if (!App.Util.ExisteBotaoGravarVisivel()) {
            window.onbeforeunload = null;
            return false;
        }

        var retorno = false;
        for (var propertyModel in self) {
            if (self.hasOwnProperty(propertyModel)) {
                for (var propriedadeOriginal in objetoCompleto) {
                    if (objetoCompleto.hasOwnProperty(propriedadeOriginal)) {
                        if (propertyModel === propriedadeOriginal) {
                            if ((self[propertyModel]() !== objetoCompleto[propriedadeOriginal])
                                && !((objetoCompleto[propriedadeOriginal] == undefined
                                        || objetoCompleto[propriedadeOriginal] == null
                                        || objetoCompleto[propriedadeOriginal] === "")
                                    && (self[propertyModel]() == undefined
                                        || self[propertyModel]() == null
                                        || self[propertyModel]() === "")))
                                retorno = true;
                        }
                    }
                }
            }
        }
        //Nao tem como colocar o if pro topo, já que o knockout interpreta a função
        //ao invéz de só chamar ela
        if (retorno === true && document.getElementById("divCarregando").style.display === "none") {
            window.onbeforeunload = App.Util.confirmExit;   
        } else {
            window.onbeforeunload = null;
        }

        return retorno;
    }, self.parent);
};
App.Util.PreenchePropriedadeViewModelComBind = (function (self, objetoCompleto) {
    for (var propriedade in objetoCompleto) {
        if (typeof objetoCompleto[propriedade] == "string" && objetoCompleto[propriedade].indexOf("/Date(") > -1) {
            var parsedDate = new Date(parseInt(objetoCompleto[propriedade].substr(6)));

            var jsDate = new Date(parsedDate);
            self[propriedade](jsDate.toLocaleDateString("pt"));
        } else
            self[propriedade](objetoCompleto[propriedade]);
    }
});

App.Util.PreenchePropriedades = (function (self, objetoCompleto) {
    for (var propriedade in objetoCompleto) {
        objetoCompleto[propriedade] = self[propriedade];
    }
});


App.Util.ImprimirGrid = App.Util.ImprimirGrid || (function (source, tituloBarra, grid, filter, callback) {
    App.ObjetoGlobalControladorDoAguarde.adiciona();

    for (var propriedade in filter) {
        var campoValor = $(".tableFiltro [data-campo='" + propriedade + "'][data-bind]");
        if (campoValor.parent().prev().hasClass("tipo-filtro")) {
            if (campoValor.hasClass("calendario")) {
                var valor = campoValor.val();

                if (valor) {
                    var colunaAnterior = campoValor.parent().prev();
                    var tipo = colunaAnterior.find("select").val();
                    var valor2 = "";

                    if (tipo == "3") {
                        valor2 = colunaAnterior.siblings().last().prev().find("input").val();
                    }

                    filter[propriedade] = { Valor: valor, Valor2: valor2, Tipo: tipo };
                }
            }
        }
    }

    //filter["CampoOrdenacao"] = grid.fnSettings().aaSorting[0][0];
    //filter["Skip"] = 0;
    //filter["Take"] = grid.fnSettings()._iRecordsDisplay;
    //if (grid.fnSettings().aaSorting[0][1] == "asc")
    //    filter["Order"] = 0;
    //else
    //    filter["Order"] = 1;

    var srcRelatorio = source + "?filtro=" + ko.toJSON(filter);

    $("#iframeImprimir").attr("src", srcRelatorio);
    $("#divModalImprimir").show();
    $("#divBarraModalImprimir").find(".tituloBarra").html(tituloBarra);

    var iframe = document.getElementById("iframeImprimir");

    if (navigator.userAgent.indexOf("MSIE") > -1 && !window.opera) {
        iframe.onreadystatechange = function () {
            if (iframe.readyState == "complete") {
                App.ObjetoGlobalControladorDoAguarde.remove();
                if (callback && typeof(callback) == 'function')
                    callback();
            }
        };
    } else {
        iframe.onload = function () {
            App.ObjetoGlobalControladorDoAguarde.remove();
            if (callback && typeof (callback) == 'function')
                callback();

            // no caso de o login expirar
            if (iframe.contentDocument.title == "Login Sigavix") {
                $("#divModalImprimir").hide();
                window.location = iframe.baseURI;
            }
        };
    }
});

App.Util.CriarDataValidando = function (stringData) {
    //formato = dd/MM/yyyy
    return App.Util.CriarDataHoraValidando(stringData + "/00:00");
};
App.Util.CriarDataHoraValidando = function (stringData) {
    //formato = dd/MM/yyyy/hh:mm

    var dataFormatada = stringData.split("/");

    if (dataFormatada.length === 4) {
        var horaFormatada = dataFormatada[3].split(":");

        if (verificarDataHoraValida(dataFormatada[2], dataFormatada[1] - 1, dataFormatada[0], horaFormatada[0], horaFormatada[1])) {
            var date = new Date(dataFormatada[2], dataFormatada[1] - 1, dataFormatada[0], horaFormatada[0], horaFormatada[1]);

            if (Object.prototype.toString.call(date) === "[object Date]") {
                if (date.getTime())
                    return date;
            }
        }
    }

    App.Util.MostrarAlertaErro("Data/Hora inválida.");
    return "";
};
App.Util.PegarDataAtual = function () {
    //formato = dd/MM/yyyy/hh:mm

    var data = new Date();
    var dia = data.getDate();
    if (dia.toString().length == 1)
        dia = "0" + dia;
    var mes = data.getMonth() + 1;
    if (mes.toString().length == 1)
        mes = "0" + mes;
    var ano = data.getFullYear();

    var hora = data.getHours();
    if (hora.toString().length == 1)
        hora = "0" + hora;

    var minuto = data.getMinutes();
    if (minuto.toString().length == 1)
        minuto = "0" + minuto;

    return dia + "/" + mes + "/" + ano + "/" + hora + ":" + minuto;
};
App.Util.VerificarVazio = function (obj) {
    // null and undefined are "empty"
    if (typeof obj === "undefined") return true;

    if (obj == null) return true;

    // Assume if it has a length property with a non-zero value
    // that that property is correct.
    if (obj.length > 0) return false;
    if (obj.length === 0) return true;

    // Otherwise, does it have any properties of its own?
    // Note that this doesn't handle
    // toString and valueOf enumeration bugs in IE < 9
    for (var key in obj) {
        if (hasOwnProperty.call(obj, key)) return false;
    }

    return false;
};

//function mostrarAlerta(mensagens, segundos, classe, functionNotificacao) {
//    var tempo = typeof (segundos) != "undefined" ? parseInt(parseInt(segundos) * parseInt(1000)) : 5000;

//    var msg = "";

//    if (classe == "erro") {
//        if (typeof (mensagens) == "string") {
//            msg = mensagens;
//        }
//        else {
//            for (i = 0; i < mensagens.length; i++) {
//                msg += mensagens[i] + "</br>";
//            }
//        }
//        $("#lblMsgErro").html(msg);
//        $("#box-erro").show();
//    }
//    else if (classe == "notificacao") {
//        if (typeof (mensagens) == "string") {
//            msg = mensagens;
//        }
//        else {
//            for (i = 0; i < mensagens.length; i++) {
//                msg += mensagens[i] + "</br>";
//            }
//        }

//        if (functionNotificacao != null && functionNotificacao != "" && functionNotificacao != undefined) {
//            $('#box-notificacao').click(function () {
//                $('#box-notificacao').hide();
//                setTimeout(functionNotificacao, 400);
//            });
//        }
//        else {
//            $('#box-notificacao').click(function () {
//                $('#box-notificacao').hide();
//            });
//        }

//        $("#lblMsgNotificacao").html(msg);
//        $("#box-notificacao").show();
//    }
//    else {
//        if (typeof (mensagens) == "string") {
//            msg = mensagens + "<a class='close'>×</a>";
//        }
//        else {
//            for (i = 0; i < mensagens.length; i++) {
//                msg += mensagens[i] + "<a class='close'>×</a> </br>";
//            }
//        }

//        notif({
//            msg: msg,
//            type: "notify",
//            position: "center",
//            fade: "true",
//            time: tempo
//        });
//    }

//    $("#btnAlertErro").focus();
//}

App.Util.MostrarAlertaSucesso = function (mensagens, tempo) { //, functionNotificacao) {
    //alert(mensagem);
    //
    //if (typeof (mensagens) == "string") {
    //    msg = mensagens;
    //}
    //else {
    //    for (i = 0; i < mensagens.length; i++) {
    //        msg += mensagens[i] + "</br>";
    //    }
    //}

    //if (functionNotificacao != null && functionNotificacao != "" && functionNotificacao != undefined) {
    //    $('#box-notificacao').click(function () {
    //        $('#box-notificacao').hide();
    //        setTimeout(functionNotificacao, 400);
    //    });
    //}
    //else {
    //    $('#box-notificacao').click(function () {
    //        $('#box-notificacao').hide();
    //    });
    //}

    //$("#lblMsgNotificacao").html(msg);
    //$("#box-notificacao").show();
    var msg = "";
    if (typeof (mensagens) == "string") {
        msg = mensagens + "<a class='close'>×</a>";
    } else {
        var i;
        for (i = 0; i < mensagens.length; i++) {
            msg += mensagens[i] + "<a class='close'>×</a> </br>";
        }
    }

    notif({
        msg: msg,
        type: "notify",
        position: "center",
        fade: "true",
        time: tempo
    });
};
App.Util.MostrarAlertaErro = function (mensagens) {
    var msg = "";
    if (typeof (mensagens) == "string") {
        msg = mensagens;
    } else {
        for (var i = 0; i < mensagens.length; i++) {
            msg += mensagens[i] + "</br>";
        }
    }
    $("#contentModalError").html(msg);
    $("#box-erro").modal("show");
};
App.Util.ConfigurarAcessoMobile = function () {
    var isMobile = $(document).width() <= 992;
    $(".disable-xs input[type=checkbox]").attr("disabled", isMobile);

    if (isMobile) {
        $("li:has(.hidden-xs)").remove();
    }
};
App.Util.AtualizarCalendarios = function () {
    atualizarCalendarios();
};
App.Util.MostrarAlertaConfirma = function (message, funcConfirm, funcCancel) {
    $("#contentModalConfirm").html(message);
    if (funcConfirm !== null && funcConfirm !== undefined) {
        $("#btnOkConfirm").off("click").on("click", function () {
            funcConfirm();
            $("#modalConfirma").modal("hide");
        });
    } else {
        $("#btnOkConfirm").off("click").on("click", function () {
            $("#modalConfirma").modal("hide");
        });
    }

    if (funcCancel !== null && funcCancel !== undefined) {
        $("#btnCancelConfirm").off("click").on("click", function () {
            funcCancel();
            $("#modalConfirma").modal("hide");
        });
    } else {
        $("#btnCancelConfirm").off("click").on("click", function () {
            $("#modalConfirma").modal("hide");
        });
    }

    $("#modalConfirma").modal("show");
};
App.Util.MontarDadosParaEnvio = function (dataToSent, namespace) {
    for (var prop in global[namespace]) {
        for (var propri in dataToSent) {
            if (prop === propri) {
                dataToSent[propri] = global[namespace][prop]();
            }
        }
    }
};
App.Util.TratarRetorno = function (result, callback) {
    if (typeof result.success !== "undefined") {
        if (result.success === true) {
            callback();

            if (result.showMessage === true) {
                App.Util.MostrarAlertaSucesso(result.message, 4000);

                window.sessionStorage.setItem("afterRedirectMessage", result.message);
                //Se nenhum redirect acontecer em 1 segundo, limpa a mensagem
                setTimeout(function () {
                    window.sessionStorage.removeItem("afterRedirectMessage");
                }, 1000);
            }
        } else if (result.success === false) {
            if (result.NaoAutorizado && result.NaoAutorizado === true) {
                window.onbeforeunload = null;
                window.location = UrlAction(result.url);
            } else {
                App.Util.MostrarAlertaErro(result.message);
            }
        }
    } else {
        //Caso a controller nao crie o parametro, é melhor chamar o callback mesmo assim
        callback();
    }
};
App.Util.SetarFocoErroAba = function () {
    var inputErro = $(".input-validation-error:first");
    if (inputErro) {
        var id = inputErro.parents(".tab-pane").attr("id");
        if (id)
            $("a[href='#" + id + "']")[0].click();
    }
};
App.Util.notificate = App.Util.notificate || {};

function UrlAction(sUrlControllerAction) {
    var prefixoSubAplicacao = "";
    var pathArray;

    if (location.hostname == "localhost") { // DESENVOLVIMENTO LOCAL
        prefixoSubAplicacao = "/";

        //} else if (location.port != "") { // IP SEM CAMINHO DNS
    } else if (location.hostname == "ccnet.pmv.local") {
        pathArray = location.pathname.split("/");
        if (pathArray[0] == "") {
            prefixoSubAplicacao = "/" + pathArray[1] + "/" + pathArray[2] + "/";
        } else {
            prefixoSubAplicacao = "/" + pathArray[0] + "/" + pathArray[1] + "/";
        }
    } else { // SERVIDOR IIS
        pathArray = location.pathname.split("/");
        if (pathArray[0] == "") {
            prefixoSubAplicacao = "/" + pathArray[1] + "/";
        } else {
            prefixoSubAplicacao = "/" + pathArray[0] + "/";
        }
    }

    return prefixoSubAplicacao + sUrlControllerAction;
}

function atualizarCpfCnpj(primeiraChamada) {
    $(".cpfcnpj").attr("maxlength", "18"); //Máximo CNPJ

    //SÓ É NECESSARIO CHAMAR ISSO UMA VEZ E O UTIL.JS JA FAZ ISSO AUTOMATICAMENTE
    if (primeiraChamada === true) {
        $(document).on("keypress", ".cpfcnpj", function () {
            cpfCnpj(this);
        });
    }
}

function cpfCnpj(obj) {
    //Remove tudo o que não é dígito
    var v = $(obj).val().replace(/\D/g, "");

    if (v.length <= 11) { //CPF
        //Coloca um ponto entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um ponto entre o terceiro e o quarto dígitos
        //de novo (para o segundo bloco de números)
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um hífen entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    } else { //CNPJ
        //Coloca ponto entre o segundo e o terceiro dígitos
        v = v.replace(/^(\d{2})(\d)/, "$1.$2");

        //Coloca ponto entre o quinto e o sexto dígitos
        v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");

        //Coloca uma barra entre o oitavo e o nono dígitos
        v = v.replace(/\.(\d{3})(\d)/, ".$1/$2");

        //Coloca um hífen depois do bloco de quatro dígitos
        v = v.replace(/(\d{4})(\d)/, "$1-$2");
    }

    $(obj).val(v);
}

function atualizarCnpj(primeiraChamada) {
    var inputs = $(".cnpj").attr("maxlength", "18"); //Máximo CNPJ
    $.each(inputs, function () {
        formatarCnpj(this);
    });

    //SÓ É NECESSARIO CHAMAR ISSO UMA VEZ E O UTIL.JS JA FAZ ISSO AUTOMATICAMENTE
    if (primeiraChamada === true) {
        $(document).on("keypress", ".cnpj", function () {
            formatarCnpj(this);
        });
    }
}

function formatarCnpj(obj) {
    //Remove tudo o que não é dígito
    var v = $(obj).val().replace(/\D/g, "");

    if (v.length <= 14) {
        //Coloca ponto entre o segundo e o terceiro dígitos
        v = v.replace(/^(\d{2})(\d)/, "$1.$2");

        //Coloca ponto entre o quinto e o sexto dígitos
        v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");

        //Coloca uma barra entre o oitavo e o nono dígitos
        v = v.replace(/\.(\d{3})(\d)/, ".$1/$2");

        //Coloca um hífen depois do bloco de quatro dígitos
        v = v.replace(/(\d{4})(\d)/, "$1-$2");
    }

    $(obj).val(v);
}

function atualizarCpf(primeiraChamada) {
    var inputs = $(".cpf").attr("maxlength", "14"); //Máximo CPF
    $.each(inputs, function () {
        formatarCpf(this);
    });

    //SÓ É NECESSARIO CHAMAR ISSO UMA VEZ E O UTIL.JS JA FAZ ISSO AUTOMATICAMENTE
    if (primeiraChamada === true) {
        $(document).on("keypress", ".cpf", function () {
            formatarCpf(this);
        });
    }
}

function formatarCpf(obj) {
    //Remove tudo o que não é dígito
    var v = $(obj).val().replace(/\D/g, "");

    if (v.length <= 11) {
        //Coloca um ponto entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um ponto entre o terceiro e o quarto dígitos
        //de novo (para o segundo bloco de números)
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um hífen entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    }

    $(obj).val(v);
}

function atualizarTime(primeiraChamada) {
    function formatarTime(obj) {
        var valor = $(obj).val();

        if (valor.length === 4 && valor.indexOf(":") === -1 && $.isNumeric(valor)) {
            //Se isso aconteceu, o campo está sem mascara
            $(".time").mask("00:00");
            valor = valor.substr(0, 2) + ":" + valor.substr(2);
        }//Campo está sem mascara e o formato está incorreto
        else if ((valor.length > 4 && valor.indexOf(":") === -1) || (!$.isNumeric(valor) && valor.indexOf(":") === -1)) {
            $(".time").mask("00:00");
            valor = "";
        }

        if (valor.length <= 4) {
            valor = "";
        } else if (valor.length <= 5) {
            var timeSplit = valor.split(":");
            var hour = parseInt(timeSplit[0]);
            var minute = parseInt(timeSplit[1]);

            if (hour > 23 || minute > 59)
                valor = "";
        }

        $(obj).val(valor);
    }

    if (primeiraChamada === true) {
        $(document).on("focusout", ".time", function () {
            formatarTime(this);
        });
    }
}

function atualizarCampoTime() {
    var inputTime = $("input[type='time']");
    if (inputTime.length && window.innerWidth > 991 && inputTime.parent().hasClass("col-md-1"))
        inputTime.parent().attr("style", "padding-left:2px;padding-right:2px");
}

function configurarMascaras(obj) {
    mascaraTelefone($(".telefone"));
    $(".time").mask("00:00");
    $(".sipad").mask("000000/0000", {
        //clearIfNotMatch: true,
        placeholder: "______/____",
        reverse: true
    });

    //Sipad usado para buscar o código PMV na nossa base (formato mais permissivo)
    $(".sipadPmv").mask("0000000/0000", {
        placeholder: "_______/____",
        reverse: true
    });

    $(".sipadIntegracao").mask("0000000/0000", {
        //clearIfNotMatch: true,
        placeholder: "_______/____",
        reverse: true
    });

    //Unica forma que eu encontrei pra essa mascara ser aplicada
    //Em campos que já tem dados
    $(".cep").mask("00000000");
    $(".cep").mask("00000-000");
}

function mascaraTelefone(campo) {
    function trata(valor, isOnBlur) {
        valor = valor.replace(/\D/g, "");
        valor = valor.replace(/^(\d{2})(\d)/g, "($1)$2");

        if (isOnBlur) {
            valor = valor.replace(/(\d)(\d{4})$/, "$1-$2");
        } else {
            valor = valor.replace(/(\d)(\d{3})$/, "$1-$2");
        }
        return valor;
    }

    campo.on("keypress", function (evt) {
        var code = (window.event) ? window.event.keyCode : evt.which;
        var valor = this.value;

        if (valor.length >= 14 || code > 57 || (code < 48 && code != 8)) {
            return false;
        } else {
            this.value = trata(valor, false);
        }
    });

    campo.on("blur", function () {
        var valor = this.value;
        if (valor.length < 13) {
            this.value = "";
        } else {
            this.value = trata(this.value, true);
        }
    });

    campo.attr("maxLength", "14");
}

function AtualizarTelefone() {
    var inputs = $(".telefone");
    $.each(inputs, function () {
        formatarTelefone(this);
    });
}

function formatarTelefone(obj) {
    var valor = $(obj).val().replace(/\D/g, "");
    valor = valor.replace(/^(\d{2})(\d)/g, "($1)$2");
    valor = valor.replace(/(\d)(\d{3})$/, "$1-$2");

    $(obj).val(valor);
}

function formatarCpf(obj) {
    //Remove tudo o que não é dígito
    var v = $(obj).val().replace(/\D/g, "");

    if (v.length <= 11) {
        //Coloca um ponto entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um ponto entre o terceiro e o quarto dígitos
        //de novo (para o segundo bloco de números)
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um hífen entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    }

    $(obj).val(v);
}

function ajustarGridTabs() {
    //Para cada tab encontrada na tela, atribui a função de resize (utilizada p/ não ter problemas
    //quando aumentar/diminuir a tela dentro de uma tab
    $('a[data-toggle="tab"]').each(function (index) {
        $(this).on("click", (function () {
            setTimeout(function () {
                $(window).resize();
            }, 500);
        }));
    });
}

function extrairNomeArquivo(arquivo) {
    arquivo = arquivo.replace(/\\/g, "/");
    var nomeArquivo = arquivo.substring(arquivo.lastIndexOf("/") + 1);
    return nomeArquivo;
}

function atualizarNumericos() {
    $(document).on("keyup", ".numero", function () {
        if (this.value != this.value.replace(/[^0-9\.]/g, "")) {
            this.value = this.value.replace(/[^0-9\.]/g, "");
        }
    });

    $(document).on("blur", ".numero", function () {
        if (this.value != this.value.replace(/[^0-9\.]/g, "")) {
            this.value = this.value.replace(/[^0-9\.]/g, "");
        }
    });

}

function verificarDataValida(ano, mes, dia) {
    if (ano < 1000 || ano > 3000 || mes < 0 || mes > 11) {
        return false;
    }

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Anos bissextos
    if (ano % 400 == 0 || (ano % 100 != 0 && ano % 4 == 0)) {
        monthLength[1] = 29;
    }

    // range de dias por mês
    return dia > 0 && dia <= monthLength[mes];
}

function verificarDataHoraValida(ano, mes, dia, hora, minuto) {
    return hora >= 0 && hora <= 23 && minuto >= 0 && minuto <= 59 && verificarDataValida(ano, mes, dia);
}

function atualizarCalendarios() {
    var inputCalendario = $('.calendario:not([readonly="readonly"]):not([readonly="true"])');

    inputCalendario.datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true
    });

    if (inputCalendario && inputCalendario.length) {
        inputCalendario.each(function (e) {
            if ($(this).val())
                $(this).datepicker("update", $(this).val());
        });
    }
}

var notify = function (message) {
    if (!window.Notification) {
        return;
    }

    if (Notification.permission === "default") {
        Notification.requestPermission(function () {
        });
    } else if (Notification.permission === "granted") {

        var notification = new Notification("Notifcação", {
            body: message,
            tag: "string única que previne notificações duplicadas",
            icon: "../Content/themes/images/imgInicio001.jpg"
        });
        notification.onshow = function () {
            //evento quando a notificação é exibida"
        },
            notification.onclick = function () {
                // evento quando a notificação é clicada"
            },
            notification.onclose = function () {
                //evento quando a notificação é fechada"
            },
            notification.onerror = function () {
                //evento quando a notificação não pode ser exibida. É disparado quando a permissão é defualt ou denied"
            };
    } else if (Notification.permission === "denied") {
        //"Usuário não deu permissão"
    }
};

/**
* encode special HTML characters, so text is safe when building HTML dynamically
* @param {String} text the text to encode
* @return {String}
*/
var encodeHTML = (function () {
    var encodeHTMLmap = {
        "&": "&amp;",
        "'": "&#39;",
        '"': "&quot;",
        "<": "&lt;",
        ">": "&gt;"
    };

    /**
    * encode character as HTML entity
    * @param {String} ch character to map to entity
    * @return {String}
    */
    function encodeHTMLmapper(ch) {
        return encodeHTMLmap[ch];
    }

    return function (text) {
        // search for HTML special characters, convert to HTML entities
        return text.replace(/[&"'<>]/g, encodeHTMLmapper);
    };
})();

/**
* encode special JavaScript characters, so text is safe when building JavaScript/HTML dynamically
* NB: conservatively assumes that HTML special characters are unsafe, and encodes them too
* @param {String} text
* @return {String}
*/
var encodeJS = (function () {
    /**
    * encode character as Unicode hexadecimal escape sequence
    * @param {String} ch character to encode
    * @return {String}
    */
    function toUnicodeHex(ch) {
        var c = ch.charCodeAt(0),
            s = c.toString(16);

        // see if we can use 2-digit hex code
        if (c < 0x100) {
            return "\\x" + ("00" + s).slice(-2);
        }

        // must use 4-digit hex code
        return "\\u" + ("0000" + s).slice(-4);
    }

    return function (text) {
        // search for JavaScript and HTML special characters, convert to Unicode hex
        return text.replace(/[\\\/"'&<>\x00-\x1f\x7f-\xa0\u2000-\u200f\u2028-\u202f]/g, toUnicodeHex);
    };
})();

// lib - $.toDictionary
(function ($) {
    // #region String.prototype.format
    // add String prototype format function if it doesn't yet exist
    if ($.isFunction(String.prototype.format) === false) {
        String.prototype.format = function () {
            var s = this;
            var i = arguments.length;
            while (i--) {
                s = s.replace(new RegExp("\\{" + i + "\\}", "gim"), arguments[i]);
            }
            return s;
        };
    }
    // #endregion

    // #region Date.prototype.toISOString
    // add Date prototype toISOString function if it doesn't yet exist
    if ($.isFunction(Date.prototype.toISOString) === false) {
        Date.prototype.toISOString = function () {
            var pad = function (n, places) {
                n = n.toString();
                for (var i = n.length; i < places; i++) {
                    n = "0" + n;
                }
                return n;
            };
            var d = this;
            return "{0}-{1}-{2}T{3}:{4}:{5}.{6}Z".format(
                d.getUTCFullYear(),
                pad(d.getUTCMonth() + 1, 2),
                pad(d.getUTCDate(), 2),
                pad(d.getUTCHours(), 2),
                pad(d.getUTCMinutes(), 2),
                pad(d.getUTCSeconds(), 2),
                pad(d.getUTCMilliseconds(), 3)
            );
        };
    }
    // #endregion

    var _flatten = function (input, output, prefix, includeNulls) {
        if ($.isPlainObject(input)) {
            for (var p in input) {
                if (includeNulls === true || typeof (input[p]) !== "undefined" && input[p] !== null) {
                    _flatten(input[p], output, prefix.length > 0 ? prefix + "." + p : p, includeNulls);
                }
            }
        } else {
            if ($.isArray(input)) {
                $.each(input, function (index, value) {
                    _flatten(value, output, "{0}[{1}]".format(prefix, index));
                });
                return;
            }
            if (!$.isFunction(input)) {
                if (input instanceof Date) {
                    output.push({ name: prefix, value: input.toISOString() });
                } else {
                    var val = typeof (input);
                    switch (val) {
                        case "boolean":
                        case "number":
                            val = input;
                            break;
                        case "object":
                            // this property is null, because non-null objects are evaluated in first if branch
                            if (includeNulls !== true) {
                                return;
                            }
                        default:
                            val = input || "";
                    }
                    output.push({ name: prefix, value: val });
                }
            } else if ($.isFunction(input)) {
                var valor = input.call();
                if (valor)
                    output.push({ name: prefix, value: valor });
            }
        }
    };

    $.extend({
        toDictionary: function (data, prefix, includeNulls) {
            /// <summary>Flattens an arbitrary JSON object to a dictionary that Asp.net MVC default model binder understands.</summary>
            /// <param name="data" type="Object">
            ///     Can either be a JSON object or a function that returns one.</data>
            ///     <param name="prefix" type="String" Optional="true">
            ///         Provide this parameter when you want the output names to be
            ///         prefixed by something (ie. when flattening simple values).
            ///     </param>
            ///     <param name="includeNulls" type="Boolean" Optional="true">
            ///         Set this to 'true' when you want null valued properties
            ///         to be included in result (default is 'false').
            ///     </param>

            // get data first if provided parameter is a function
            data = $.isFunction(data) ? data.call() : data;

            // is second argument "prefix" or "includeNulls"
            if (arguments.length === 2 && typeof (prefix) === "boolean") {
                includeNulls = prefix;
                prefix = "";
            }

            // set "includeNulls" default
            includeNulls = typeof (includeNulls) === "boolean" ? includeNulls : false;

            var result = [];
            _flatten(data, result, prefix || "", includeNulls);

            return result;
        }
    });
})(jQuery);

function validarArquivoImagem(oInput, arrayExtensoesValidas) {
    if (!arrayExtensoesValidas)
        arrayExtensoesValidas = [".jpg", ".jpeg", ".jpe", ".png"];

    if (oInput.type == "file") {
        var sFileName = oInput.value;
        if (sFileName.length > 0) {
            var blnValid = false;
            for (var j = 0; j < arrayExtensoesValidas.length; j++) {
                var sCurExtension = arrayExtensoesValidas[j];
                if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                    blnValid = true;
                    break;
                }
            }

            if (!blnValid) {
                App.Util.MostrarAlertaSucesso("Arquivo inválido, extensões permitidas são: " + arrayExtensoesValidas.join(", "), 5000);
                oInput.value = "";
                return false;
            }
        }
    }
    return true;
}