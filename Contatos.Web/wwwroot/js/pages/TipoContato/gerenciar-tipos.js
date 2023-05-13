$(document).ready(function () {
    buscaConsulta();

    $("#nome").change(function () {
        buscaConsulta();
    });

});


var table;
function buscaConsulta() {
     //setaCookieStatus();
    $('#div-table').hide();
    $('#table-loading').show();
    if (table != undefined) {
        table.destroy();
    }
    let nome = $('#nome').val();

    table = $("#dataTable").DataTable({
        "order": [[1, "asc"]],
        "pageLength": 15,
        "language": {
            "lengthMenu": "Exibindo _MENU_ registros",
            "info": "Mostranto _START_ até _END_ de _TOTAL_ registros",
            "search": "Observação",
            "zeroRecords": "Nenhum registro encontrado",
            "processing": "Buscando dados",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            }
        },
        "fnDrawCallback": function (oSettings) {
            $('#div-table').show();
            $('#table-loading').hide();
        },
        "initComplete": function (settings, json) {
            SetCookie('CP-Token', json.token)
        },
        "lengthChange": false,
        "processing": true,
        "scrollX": true,
        "serverSide": true,
        "filter": false,
        "orderMulti": false,
        "autoWidth": false,
        "ajax": {
            "url": ResolveUrl('TipoContato/ListaTiposContato'),
            "type": "Post",
            "datatype": "json",
            "data": {
                nome: nome,
            },
        },
        "columns": [
            {
                "data": null, orderable: false, render: function (data) {

                    let editar = '<span id="btn-editar"><button type="button" data-toggle="tooltip" data-placement="left" title="Editar Usuário" onclick="window.location.href=\'/cadastrar-tipo/' + data.id + '\'" class="btn btn-success btn-sm btn-table">' +
                        '<i class="fa fa-edit"></i>' +
                        '</button></span>&nbsp;';
                    let excluir = '<span id="btn-excluir"><button type="button" data-toggle="tooltip" data-placement="left" title="Excluir solicitação" onclick="ModalExcluir(\'' + data.id +'\', this);" class="btn btn-danger btn-sm btn-table">' +
                        '<i class="fa fa-trash"></i>' +
                        '</button></span>&nbsp;';
                    return editar + excluir;
                }
            },
            {
                "data": "nome", "name": "nome", render: function (data) {
                    return data;
                }
            },
        ],

    });
    table.on('length.dt', function (e, settings, len) {
        SetCookie('pageLength', len);
    });

}

function ModalExcluir(id) {
    $("#modal-excluir").modal('show');
    $("#id-excluir").val(id);
}

function Excluir() {
    $("#modal-excluir").modal('hide');
    var id = $("#id-excluir").val();
    TipoContato_Excluir(id).then(function () {
        buscaConsulta();
        $.notify("Tipo excluído com sucesso!", "success", { delay: 2500 });
        unloading(ele);
    }, function (err) {
        $.notify("Erro ao excluir tipo!", "error", { delay: 2500 });
    });
}