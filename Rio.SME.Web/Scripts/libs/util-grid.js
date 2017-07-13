$.fn.grid = function (opcoes) {

    var defaults = {
        bServerSide: true,
        sAjaxSource: "",
        sPaginationType: "full_numbers",
        sScrollX: "98%",
        sScrollY: ($(window).height() - 280),
        bProcessing: true,
        bAutoWidth: true,
        bPaginate: true,
        aLengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        iDisplayLength: 25,
        fnServerData: function (sSource, aoData, fnCallback) {
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
        },
        fnDrawCallback: function (oSettings) {

        },
        aaSorting: [[0, 'asc']],
        "aoColumnDefs": [],
        oLanguage: setGridLanguage()
    };

    var settings = $.extend({}, defaults, opcoes);

    var gridValue = $("#" + this.attr("id")).dataTable({
        bServerSide: settings.bServerSide,
        bProcessing: settings.bProcessing,
        bAutoWidth: settings.bAutoWidth,
        bPaginate: settings.bPaginate,
        sScrollX: settings.sScrollX,
        sScrollY: settings.sScrollY,
        sAjaxSource: settings.sAjaxSource,
        sPaginationType: settings.sPaginationType,
        aoColumns: settings.aoColumns,
        aLengthMenu: settings.aLengthMenu,
        iDisplayLength: settings.iDisplayLength,
        fnServerData: settings.fnServerData,
        fnDrawCallback: settings.fnDrawCallback,
        fnCreatedRow: settings.fnCreatedRow,
        fnRowCallback: settings.fnRowCallback,
        aaSorting: settings.aaSorting,
        "aoColumnDefs": settings.aoColumnDefs,
        oLanguage: settings.oLanguage
    });

    escondeDadosGrid(this.attr("id"));

    $(window).resize(function () {
        $('.dataTables_scrollBody').css('height', ($(window).height() - 280));
    });

    return gridValue;
};


// Retorna a Linha Selecionada 
function fnGetSelected(oTableLocal) {
    var aReturn = new Array();
    var aTrs = oTableLocal.fnGetNodes();
    for (var i = 0 ; i < aTrs.length ; i++) {
        if ($(aTrs[i]).hasClass('row_selected')) {
            aReturn.push(oTableLocal.fnGetData()[i]);
        }
    }
    return aReturn[0];
}

// Pinta Linha Selecionada
function fnSelectLine() {
    $(".grid tbody").click(function (event) {
        //Se Linha já está selecionada
        var parentTr = $(event.target.parentNode);
        var childNode = parentTr.siblings('tr.child').find('td:nth(1)');

        /* bugfix mobile: remoção da coluna duplicada com os detalhes da linha */
        if (childNode)
            childNode.remove();

        if (!parentTr.hasClass('row_selected')) {
            $(".grid tbody tr").each(function () {
                $(this).removeClass('row_selected');
            });

            parentTr.addClass('row_selected');
        }
    });
}

$(function () {
    //Executa ao completar uma requisição ajax.     
    $(document).ajaxComplete(function (event, request, settings) {
        // Não Autorizado        
        fnSelectLine();
    });
});
