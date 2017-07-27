App.Services.Agrupador = App.Services.Agrupador || (function () {
    var namespace = App.Services.Agrupador;
    namespace.FilterProperties = namespace.FilterProperties || {};

    //AGRUPADOR - Métodos expostos.
    //=========================================================================================================================
    namespace.FiltrarGrid = _filtrarGrid;
    namespace.Salvar = _salvar;
    namespace.ListarCompleto = _listarCompleto;
    namespace.Imprimir = _imprimir;
    namespace.Salvar = _salvar;
    namespace.Buscar = _buscarAgrupador;
    //=========================================================================================================================

    function preencherGrid(idTabela, grid) {
        grid = $("#" + idTabela).grid({
            sAjaxSource: UrlAction("Agrupadores/ListarGrid"),
            aaSorting: [[2, 'desc']],
            aoColumns: [
                { 'mData': 'Codigo' },
                { 'mData': 'Nome' },
                { 'mData': 'Ativo' },
            ],
            fnServerData: function (sSource, aoData, fnCallback) {
                App.Util.FiltroGrid(sSource, aoData, fnCallback, App.Services.Agrupador.FilterProperties);
            }
        });

        return grid;
    };

    function _listarCompleto(idTabela, grid) {
        return preencherGrid(idTabela, grid);
    };

    function _salvar(namespace) {
        if (global[namespace].Errors().length === 0) {
            var dados = new App.Models.Agrupador();
            App.Util.MontarDadosParaEnvio(dados, namespace);

            $.ajax({
                data: JSON.stringify(dados),
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: UrlAction("Agrupadores/Salvar"),
                success: function (result) {
                    App.Util.TratarRetorno(result, function () {
                        window.onbeforeunload = null;
                        window.location = result.url;
                    });
                }
            });
        } else {
            global[namespace].Errors.showAllMessages();
        }
    }

    function _buscarAgrupador(idRegistro, namespace, modeloOriginal) {
        $.ajax({
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: UrlAction("Agrupadores/Buscar") + '?id=' + idRegistro,
            success: function (result) {
                App.Util.TratarRetorno(result, function () {
                    App.Util.PreenchePropriedades(result.content, modeloOriginal);
                    App.Util.PreenchePropriedadeViewModelComBind(global[namespace], result.content);
                });
            }
        });
    }

    function _filtrarGrid(filter, divModalFiltrar, grid) {
        App.Services.Agrupador.FilterProperties = filter;
        grid.fnDraw();
        fecharModal(divModalFiltrar);
    };

    function _listarCompleto(idTabela, grid) {
        return preencherGrid(idTabela, grid);
    };

    function _imprimir(idTabela, filter) {
        App.Services.Agrupador.FilterProperties = filter;
    };

    //function _salvar(namespace) {
    //    var dados = new App.Models.Agrupador();
    //    dados.ListaAgrupadorContatos = [];
    //    dados.ListaAgrupadorGrupoAtividade = [];
    //    App.Util.MontarDadosParaEnvio(dados, namespace);

    //    var pacote = {
    //        "empreendimento": dados,
    //        "listaAgrupadorPessoas": ko.toJS(removerCaracteresEspeciais(global[namespace].ListaAgrupadorPessoas())),
    //        "listaAgrupadorContatos": ko.toJS(global[namespace].NovaListaAgrupadorContatos()),
    //        "listaAgrupadorGrupoAtividade": ko.toJS(dados.ListaAgrupadorGrupoAtividade)
    //    }

    //    $.ajax({
    //        data: JSON.stringify(pacote),
    //        type: "POST",
    //        async: true,
    //        dataType: "json",
    //        contentType: "application/json; charset=utf-8",
    //        url: UrlAction("Agrupador/Salvar"),
    //        success: function (result) {
    //            App.Util.TratarRetorno(result, function () {
    //                window.onbeforeunload = null;
    //                window.location.reload();
    //            });
    //        }
    //    });
    //};

    return namespace;
});