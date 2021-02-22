$(document).ready(function () {
    var formato = { minimumFractionDigits: 2, style: 'currency', currency: 'BRL' };

    $.fn.dataTable.moment('DD/MM/YYYY HH:mm');

    $('#dataTable').DataTable({
        //dom: 'BlTfgt<"row"<"col-md-6"i><"col-md-6"p>>',
        //dom: "<'form-inline'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'fB>>t<'form-inline pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        dom: "<'row'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'f>>t<'row pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        buttons: ['print'],
        paging: true,
        responsive: true,
        searching: true,
        language: {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando _START_ de _END_ do total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Exibindo _MENU_  registros",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Busca Rápida",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
        //, columnDefs: [
        //    { type: 'date-dd-mmm-yyyy', targets: 0 }
        //]
    });

    $('#dataTableReport').DataTable({
        //dom: 'BlTfgt<"row"<"col-md-6"i><"col-md-6"p>>',
        //dom: "<'form-inline'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'fB>>t<'form-inline pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        dom: "<'row'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'f>>t<'row pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        buttons: ['print'],
        paging: true,
        responsive: true,
        searching: true,
        language: {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando _START_ de _END_ do total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Exibindo _MENU_  registros",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Busca Rápida",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\R$,]/g, '') / 100 :
                    typeof i === 'number' ?
                        i : 0;
            };

            var ths = $('#dataTableReport').find('th');
            var cont = 0;
            $.each(ths, function () {
                if ($(this).attr("data-totaliza") == "true") {
                    return false;
                } else {
                    cont += 1;
                }
            })

            // Total over all pages
            total = api
                .column(cont)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(cont, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            //// Update footer
            //$(api.column(4).footer()).html(
            //    pageTotal.toLocaleString('pt-BR', formato) + ' ( ' + total.toLocaleString('pt-BR', formato) + ' total)'
            //);

            var retorno = pageTotal.toLocaleString('pt-BR', formato)
            if (pageTotal != total) {
                retorno = retorno + ' ( ' + total.toLocaleString('pt-BR', formato) + ' total)'
            }

            $('#total').html(
                retorno
            );
        }
        //, columnDefs: [
        //    { type: 'date-dd-mmm-yyyy', targets: 0 }
        //]
    });

    $('#dataTableReportArgupado').DataTable({
        //dom: 'BlTfgt<"row"<"col-md-6"i><"col-md-6"p>>',
        //dom: "<'form-inline'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'fB>>t<'form-inline pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        dom: "<'row'<'col-md-6 col-xs-12'l><'col-md-6 col-xs-12'f>>t<'row pull-down'<'col-md-6 col-xs-12'i><'col-md-6 col-xs-12'p>>",
        buttons: ['print'],
        paging: true,
        responsive: true,
        searching: true,
        language: {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando _START_ de _END_ do total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Exibindo _MENU_  registros",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Busca Rápida",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\R$,]/g, '') / 100 :
                    typeof i === 'number' ?
                        i : 0;
            };

            var ths = $('#dataTableReportArgupado').find('th');
            var cont = 0;
            $.each(ths, function () {
                if ($(this).attr("data-totaliza") == "true") {
                    return false;
                } else {
                    cont += 1;
                }
            })

            // Total over all pages
            total = api
                .column(cont)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(cont, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            //// Update footer
            //$(api.column(4).footer()).html(
            //    pageTotal.toLocaleString('pt-BR', formato) + ' ( ' + total.toLocaleString('pt-BR', formato) + ' total)'
            //);

            var retorno = pageTotal.toLocaleString('pt-BR', formato)
            if (pageTotal != total) {
                retorno = retorno + ' ( ' + total.toLocaleString('pt-BR', formato) + ' total)'
            }

            $('#total').html(
                retorno
            );
        }
        //, columnDefs: [
        //    { type: 'date-dd-mmm-yyyy', targets: 0 }
        //]
    });

    $('#dataTableDetail').DataTable({
        "bFilter": false,
        "paging": false,
        "ordering": false,
        "info": false,
        responsive: true
    });
});