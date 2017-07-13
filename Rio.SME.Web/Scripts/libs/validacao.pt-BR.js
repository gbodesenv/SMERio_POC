jQuery.validator.addMethod('date',
    function (value, element, params) {
        try {
            jQuery.datepicker.parseDate('dd/mm/yy', value); return true;
        } catch (e) {
            return false;
        }
    }
);

jQuery.validator.addMethod('number',
    function (value, element, params) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    }
);

jQuery.validator.addMethod('time',
    function (value, element, params) {
        var valor = value;

        var valid = (valor.search(/^\d{2}:\d{2}$/) != -1) &&
            (valor.substr(0, 2) >= 0 && valor.substr(0, 2) <= 23) &&
            (valor.substr(3, 2) >= 0 && valor.substr(3, 2) <= 59);

        return valid;
    }
);

jQuery.validator.addMethod("hora", function (value) {    
    var valor = value;
    
    if (value == "")
        return true;

    var valid = (valor.search(/^\d{2}:\d{2}$/) != -1) &&
        (valor.substr(0, 2) >= 0 && valor.substr(0, 2) <= 23) &&
        (valor.substr(3, 2) >= 0 && valor.substr(3, 2) <= 59);

    return valid;
}, "Hora em Formato Incorreto!");

jQuery.validator.classRuleSettings.hora = { hora: true };

jQuery.validator.addMethod("email", function (value) {
    var email = value;

    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var valid = re.test(email);

    if (!valid)
        mostrarAlerta("Email em formato incorreto.", 3);

    return valid;
}, "Email em Formato Incorreto!");

jQuery.validator.classRuleSettings.email = { email: true };


jQuery.validator.addMethod("cnpj", function (value) {
    cnpj = value.replace(/[^\d]+/g, '');
    return true;
    if (cnpj == '')
        return true;
    if (cnpj.length != 14 || cnpj == "00000000000000")
        return false;

    tamanho = 12; //cnpj.length - 2
    digitos = cnpj.substring(tamanho);

    valid = validarDigitosCNPJ(tamanho, cnpj.substring(0, tamanho), digitos, 0);
    if (valid) {
        tamanho++;
        valid = validarDigitosCNPJ(tamanho, cnpj.substring(0, tamanho), digitos, 1);
    }

    if (!valid)
        mostrarAlerta("CNPJ invalido!", 3);

    return valid;

}, "CNPJ invalido!");

function validarDigitosCNPJ(tamanho, numeros, digitos, verificador) {
    soma = 0;
    pos = tamanho - 7;

    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }

    resultado = (soma % 11 < 2) ? 0 : 11 - (soma % 11);

    if (resultado != digitos.charAt(verificador))
        return false;

    return true;
}
jQuery.validator.classRuleSettings.cnpj = { cnpj: true };
