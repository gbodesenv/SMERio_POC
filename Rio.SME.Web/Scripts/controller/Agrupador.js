App.Controllers.Agrupador = App.Controllers.Agrupador || (function () {
    return {
        ModelOriginalFiltro: {},
        GridAgrupador: {},

        Filtrar: function (filter, divModalFiltrar) {
            App.Services.Agrupador().FiltrarGrid(filter, divModalFiltrar, App.Controllers.Agrupador.GridAgrupador);
        },

        Listar: function (idTabela, namespaceFiltro) {
            App.Controllers.Agrupador.ModelOriginalFiltro = new App.Models.Agrupador();
            global[namespaceFiltro] = global[namespaceFiltro] || {};
            App.Util.PreenchePropriedadesViewModel(global[namespaceFiltro], App.Controllers.Agrupador.ModelOriginalFiltro, true);
            ko.applyBindings(global[namespaceFiltro], document.getElementById("ListarAgrupador"));

            App.Controllers.Agrupador.GridAgrupador = App.Services.Agrupador().ListarCompleto(idTabela, App.Controllers.Agrupador.GridAgrupador);

            $('#' + idTabela + ' tbody').dblclick(function () {
                App.Controllers.Agrupador().Abrir();
            });
        },

        Imprimir: function (url, titulobarra, filtro) {
            App.Util.ImprimirGrid(url, titulobarra, App.Controllers.Agrupador.GridAgrupador, filtro);
        },
        
        Abrir: function () {           
            var linha = fnGetSelected(App.Controllers.Agrupador.GridAgrupador);
            if (linha) {
                window.location = UrlAction("Agrupadores/Editar") + "?id=" + linha.Codigo;
            } else
                App.Util.MostrarAlertaSucesso("Selecione um Agrupador na lista", 4000);
        },

        InserirEditar: function (namespace) {            
            App.Controllers.Agrupador.ModelOriginalAgrupador = new App.Models.Agrupador();
            global[namespace] = global[namespace] || {};
            App.Util.PreenchePropriedadesViewModel(global[namespace], App.Controllers.Agrupador.ModelOriginalAgrupador);

            global[namespace].Errors = ko.validation.group(global[namespace]);
            
            ko.applyBindings(global[namespace], document.getElementById("InserirEditarAgrupador"));
                        
            global[namespace].Nome = global[namespace].Nome.extend({ required: true });

            var idRegistro = App.Util.GetParameterByName('id');
            idRegistro = idRegistro == 'undefined' || idRegistro == null ? $("#idAgrupador").val() : idRegistro;
            if (idRegistro) {
                App.Services.Agrupador().Buscar(idRegistro, namespace, App.Controllers.Agrupador.ModelOriginalAgrupador);
            }
        },
        
        Salvar: function (namespace) {
            App.Services.Agrupador().Salvar(namespace);
        },
    }
});