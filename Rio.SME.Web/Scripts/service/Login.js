App.Services.Login = (function () {
    var namespace = App.Services.Login;

    namespace.EfetuarLogin = (function (namespace) {
        var url = $("#hdnUrlLogin").val() + '?returnUrl=' + $("#hdnReturnUrl").val();

        $.post(url, global[namespace], function (data) {
            App.Util.TratarRetorno(data, function () {
                window.location = data.url;
            });
        }).error(function (XMLHttpRequest) {
            verificaStatus(XMLHttpRequest.status);
        });
    });
    namespace.PreencherComboUnidadesGestoras = (function (_namespace, _nomeCampo) {
        ListarUnidadesGestoras(function (data) {
            global[_namespace] = global[_namespace] || {};
            global[_namespace][_nomeCampo](data);
        });
    });

    namespace.SalvarUnidadeGestora = (function (namespace) {
        $.ajax({
            cache: false,
            data: JSON.stringify({ codigoUnidadeGestora: global[namespace].codigoUnidadeGestora() }),
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: $("#hdnUrlTrocarUnidadeGestora").val(),
            success: function (data) {
                App.Util.TratarRetorno(data, function () {
                    window.location = data.url;
                });
            }
        });
    });
    
    return namespace;
});