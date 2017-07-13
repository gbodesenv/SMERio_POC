App.Controllers.Home = App.Controllers.Home || (function () {
    return {
        ModelOriginalHome: {},

        Listar: function (namespace) {
            App.Controllers.Home.ModelOriginal = new App.Models.Home();
            global[namespace] = global[namespace] || {};
            App.Util.PreenchePropriedadesViewModel(global[namespace], App.Controllers.Home.ModelOriginal, true);
            global[namespace].BuscarProcesso = ko.observable(false);
            global[namespace].BuscarEmpreendimento = ko.observable(false);
            
            global[namespace].CodigoTipoProcesso = global[namespace].CodigoTipoProcesso.extend({
                required: {
                    onlyIf: function () {
                        return global[namespace].BuscarProcesso();
                    }
                }
            });

            global[namespace].NumeroProcesso = global[namespace].NumeroProcesso.extend({
                required: {
                    onlyIf: function () {
                        return global[namespace].BuscarProcesso();
                    }
                }
            });

            global[namespace].NumeroInscricao = global[namespace].NumeroInscricao.extend({
                required: {
                    onlyIf: function () {
                        return global[namespace].BuscarEmpreendimento();
                    }
                }
            });

            global[namespace].CodigoTipoInscricao = global[namespace].CodigoTipoInscricao.extend({
                required: {
                    onlyIf: function () {
                        return global[namespace].BuscarEmpreendimento();
                    }
                }
            });

            global[namespace].ComboTipoInscricao = ko.observableArray([]);
            global[namespace].ComboTipoProcesso = ko.observableArray([]);
            global[namespace].Errors = ko.validation.group(global[namespace]);

            ko.applyBindings(global[namespace], document.getElementById("divCorpoPagina"));

            App.Services.Home().PreencherComboTipoInscricao(namespace, 'ComboTipoInscricao');
            App.Services.Home().PreencherComboTipoProcesso(namespace, 'ComboTipoProcesso');
            App.Services.Home().BuscarInformacoesHome(namespace);
        },

        BuscarProcesso: function (namespace) {
            global[namespace].BuscarProcesso(true);
            global[namespace].BuscarEmpreendimento(false);

            if (global[namespace].CodigoTipoProcesso() === "Licenciamento")
                App.Services.Processo().BuscarProcessoPorNumero(namespace);
            else if(global[namespace].CodigoTipoProcesso() === "Autuacao")
                App.Services.AutoDocumento().BuscarProcessoPorNumero(namespace);
            else if (global[namespace].CodigoTipoProcesso() === "Fiscalizacao")
                App.Services.ProcessoFiscalizacao().BuscarProcessoPorNumero(namespace);
        },

        BuscarEmpreendimento: function (namespace) {
            global[namespace].BuscarProcesso(false);
            global[namespace].BuscarEmpreendimento(true);
            App.Services.Empreendimento().BuscarEmpreendimentoPorInscricao(namespace);
        }
    }
});


