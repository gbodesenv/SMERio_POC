App.Controllers.Login = App.Controllers.Login || (function () {
    return {
        DesenharBackground: function () {
            var numRamdom = Math.random();
            var inum = 17;
            // o numero acima refere-se ao numero de imagens para alternar
            var rand1 = Math.round(numRamdom * (inum - 1)) + 1;
            var images = new Array;
            var nomeAutor = "";

            images[1] = $("#hdnImgBackgroundLogon").val() + "01_Arvores_de_Vitoria_Foto_Carlos_Antolini.jpg";
            images[2] = $("#hdnImgBackgroundLogon").val() + "03_Baia_de_Vitória_Terceira_Ponte_Foto_Diego_Alves.jpg";
            images[3] = $("#hdnImgBackgroundLogon").val() + "04_Baia_de_Vitoria_e_Penedo_Foto_Diego_Alves.jpg";
            images[4] = $("#hdnImgBackgroundLogon").val() + "05_Baia_de_Vitoria_e_Penedo_Foto_Diego_Alves.jpg";
            images[5] = $("#hdnImgBackgroundLogon").val() + "06_Baia_de_Vitoria_e_Penedo_Foto_Diego_Alves.jpg";
            images[6] = $("#hdnImgBackgroundLogon").val() + "07_Basilica_de_Santo_Antonio_Foto_Diego_Alves.jpg";
            images[7] = $("#hdnImgBackgroundLogon").val() + "08_Iate_Clube_de_Vitoria_Vista_do_Hotel_Confort_Foto_Diego_Alves.jpg";
            images[8] = $("#hdnImgBackgroundLogon").val() + "09_Ilha_das_Caieiras_Foto_Diego_Alves.jpg";
            images[9] = $("#hdnImgBackgroundLogon").val() + "10_Ilha_do_Frade_Fotos_Diego_Alves.jpg";
            images[10] = $("#hdnImgBackgroundLogon").val() + "11_Parque_da_Fonte_Grande_Pedra_dos_Dois_Olhos_Foto_Andre_Sobral.jpg";
            images[11] = $("#hdnImgBackgroundLogon").val() + "12_Parque_Moscoso_Foto_Diego_Alves.jpg";
            images[12] = $("#hdnImgBackgroundLogon").val() + "13_Parque_Moscoso_Foto_Diego_Alves.jpg";
            images[13] = $("#hdnImgBackgroundLogon").val() + "13_Parque_Municipal_Pedra_da_Cebola_Foto_Leonardo_Silveira.jpg";
            images[14] = $("#hdnImgBackgroundLogon").val() + "14_Praca_dos_Desejos_com_mar_ao_fundo_Vista_do_Hotel_Sheraton_Foto_Diego_Alves.jpg";
            images[15] = $("#hdnImgBackgroundLogon").val() + "15_Praia_de_Camburi_Quiosques_Foto_Yuri_Barichivich.jpg";
            images[16] = $("#hdnImgBackgroundLogon").val() + "16_Prainha_em_frente_Praca_dos_Namorados_Foto_Andre_Sobral.jpg";
            images[17] = $("#hdnImgBackgroundLogon").val() + "17_Vitoria_Vista_pelo_Mar_Foto_Elizabeth_Nader.jpg";

            //2,3,4,5,6,7,8,9,11,12,14 = Diego Alves;
            if ((rand1 >= 2 && rand1 <= 9) || rand1 == 11 || rand1 == 12 || rand1 == 14)
                nomeAutor = "Diego Alves";
            //10,16 = Andre Sobral;
            else if (rand1 == 10 || rand1 == 16)
                nomeAutor = "Andre Sobral";
            //1 = Carlos Antolini;
            else if (rand1 == 1)
                nomeAutor = "Carlos Antolini";
            //13 = Leonardo Silveira;
            else if (rand1 == 13)
                nomeAutor = "Leonardo Silveira";
            //15 = Yuri_Barichivich;
            else if (rand1 == 15)
                nomeAutor = "Yuri Barichivich";
            //17 = Elizabeth Nader;
            else if (rand1 == 17)
                nomeAutor = "Elizabeth Nader";

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
