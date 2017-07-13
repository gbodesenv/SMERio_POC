ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: false,
    parseInputAttributes: true,
    messageTemplate: null,    
    decorateInputElement: true,
    decorateElementOnModified: true,
    errorElementClass: 'input-validation-error',
    errorClass: 'input-validation-error',
    errorsAsTitle: false,
    grouping: {
        deep: true,
        observable: true,
        live: true
    }
}, true);