App.Services.Home = App.Services.Home || (function () {
    var namespace = App.Services.Home;

    namespace.BuscarUsuarioLogado = (function (usuarioLogado) {
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: UrlAction("Home/BuscaUsuarioLogado"),
            success: function (result) {
                App.Util.TratarRetorno(result, function () {
                    App.Util.PreenchePropriedades(result.content, usuarioLogado);
                });
            }
        });
    });

    namespace.BuscarInformacoesHome = (function (namespace) {
        $.ajax({
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: UrlAction("Home/BuscarInformacoesHome"),
            success: function (result) {
                App.Util.TratarRetorno(result, function () {
                    App.Util.PreenchePropriedadeViewModelComBind(global[namespace], result.content);
                });
            }
        });
    });

    namespace.PreencherComboTipoInscricao = (function(_namespace, _nomeCampo) {
        ListarTipoInscricao(function (data) {
            global[_namespace] = global[_namespace] || {};
            global[_namespace][_nomeCampo](data);
        });
    });

    ListarTipoInscricao = function (callback) {
        $.ajax({
            type: "GET",
            async: true,
            contentType: "application/json; charset=utf-8",
            url: UrlAction("Empreendimento/ListarTipoInscricao"),
            success: function (_data) {
                App.Util.TratarRetorno(_data, function () {
                    callback(_data.content);
                });
            }
        });
    };

    namespace.PreencherComboTipoProcesso = (function (_namespace, _nomeCampo) {
        ListarTipoProcesso(function (data) {
            global[_namespace] = global[_namespace] || {};
            global[_namespace][_nomeCampo](data);
        });
    });

    ListarTipoProcesso = function (callback) {
        $.ajax({
            type: "GET",
            async: true,
            contentType: "application/json; charset=utf-8",
            url: UrlAction("Processo/ListarTipoProcesso"),
            success: function (_data) {
                App.Util.TratarRetorno(_data, function () {
                    callback(_data.content);
                });
            }
        });
    };

    return namespace;
});