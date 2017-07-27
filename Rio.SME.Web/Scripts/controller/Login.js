App.Controllers.Login = App.Controllers.Login || (function () {
    return {
        DesenharBackground: function () {
            var numRamdom = Math.random();
            var inum = 7;
            // o numero acima refere-se ao numero de imagens para alternar
            var rand1 = Math.round(numRamdom * (inum - 1)) + 1;
            var images = new Array;
            var nomeAutor = "";

            images[1] = $("#hdnImgBackgroundLogon").val() + "rio_1_Ricardo_Cassiano.jpg";
            images[2] = $("#hdnImgBackgroundLogon").val() + "rio_2_Ricardo_Cassiano.jpg";
            images[3] = $("#hdnImgBackgroundLogon").val() + "rio_3_Ricardo_Cassiano.jpg";
            images[4] = $("#hdnImgBackgroundLogon").val() + "rio_4_Ricardo_Cassiano.jpg";
            images[5] = $("#hdnImgBackgroundLogon").val() + "rio_5_Ricardo_Cassiano.jpg";
            images[6] = $("#hdnImgBackgroundLogon").val() + "rio_6_Lia_Mattarakis.jpg";
            images[7] = $("#hdnImgBackgroundLogon").val() + "rio_7_Alexandre_Macieira.jpg";

            //1,2,3,4,5, = Ricardo Cassiano;
            if ((rand1 >= 1 && rand1 <= 5))// || rand1 == 11 || rand1 == 12 || rand1 == 14)
                nomeAutor = "Ricardo Cassiano";
            //10,16 = Andre Sobral;
            else if (rand1 == 6)
                nomeAutor = "Lia Mattarakis";
            //7 = Carlos Antolini;
            else if (rand1 == 7)
                nomeAutor = "Alexandre Macieira";
          

            var image = images[rand1];
            document.write('<body style="background-image: url(' + image + ') !important;" text="white">');

            $('#nomeAutor').text(nomeAutor);
        },

        Index: function (namespace) {

            global[namespace] =
                {
                    login: ko.observable().extend({ required: true }),
                    password: ko.observable().extend({ required: true })
                };
            global[namespace].errors = ko.validation.group(global[namespace]);
            ko.applyBindings(global[namespace]);

            //$("#btnLogin").click(function () {
            //    App.Controllers.Login().LogarSistema(namespace);
            //});

            //$('#formLogarSistema').live("keypress", function (e) {
            //    if (e.keyCode == 13) {
            //        App.Controllers.Login().LogarSistema(namespace);
            //    }
            //});
            
            $('.login-input-area > div > input').keypress(function (e) {
                if (e.which === 13) {
                    App.Controllers.Login().LogarSistema(namespace);
                    return false;
                }
            });
            
        },
        LogarSistema: function (namespace) {
            if (global[namespace].errors().length === 0) {
                App.Services.Login().EfetuarLogin(namespace);
            }
            else {
                global[namespace].errors.showAllMessages();
            }
        },
        Layout: function (namespace) {

            global[namespace] =
                {
                    ComboUnidadesGestoras: ko.observableArray([]),
                    codigoUnidadeGestora: ko.observable().extend({ required: true })
                };

            global[namespace].errors = ko.validation.group(global[namespace]);
            ko.applyBindings(global[namespace], document.getElementById("divCorpoPagina"));

            App.Services.Login().PreencherComboUnidadesGestoras(namespace, 'ComboUnidadesGestoras');
        },
        TrocarUnidadeGestora: function (namespace) {
            if (global[namespace].errors().length === 0) {
                App.Services.Login().SalvarUnidadeGestora(namespace);
            } else {
                global[namespace].errors.showAllMessages();
            }
        }
    };
});
