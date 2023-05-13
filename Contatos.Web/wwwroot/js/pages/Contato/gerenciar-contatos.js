$(document).ready(function () {

    loadPessoas().then(function () {
        loadTipos().then(function () {
            buscaConsulta();
        });
    });

    $("#nome, #idPessoa, #idTipo").change(function () {
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
    let valor = $('#valor').val();
    let idPessoa = $('#idPessoa').val();
    let idTipo = $('#idTipo').val();

    table = $("#dataTable").DataTable({
        "order": [[3, "asc"]],
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
            "url": ResolveUrl('Contato/ListaContatos'),
            "type": "Post",
            "datatype": "json",
            "data": {
                idPessoa: idPessoa,
                idTipo: idTipo,
                valor: valor,
            },
        },
        "columns": [
            {
                "data": null, orderable: false, render: function (data) {

                    let editar = '<span id="btn-editar"><button type="button" data-toggle="tooltip" data-placement="left" title="Editar Usuário" onclick="window.location.href=\'/cadastrar-contato/' + data.id + '\'" class="btn btn-success btn-sm btn-table">' +
                        '<i class="fa fa-edit"></i>' +
                        '</button></span>&nbsp;';
                    let excluir = '<span id="btn-excluir"><button type="button" data-toggle="tooltip" data-placement="left" title="Excluir solicitação" onclick="ModalExcluir(\'' + data.id + '\', this);" class="btn btn-danger btn-sm btn-table">' +
                        '<i class="fa fa-trash"></i>' +
                        '</button></span>&nbsp;';
                    return editar + excluir;
                }
            },
            {
                "data": "pessoa.nome", orderable: false, render: function (data) {
                    return data;
                }
            },
            {
                "data": "tipoContato.nome", orderable: false, render: function (data) {
                    return data;
                }
            },
            {
                "data": "valor", "name": "valor", render: function (data) {
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
    Contato_Excluir(id).then(function () {
        buscaConsulta();
        $.notify("Contato excluído com sucesso!", "success", { delay: 2500 });
        unloading(ele);
    }, function (err) {
        $.notify("Erro ao excluir contato!", "error", { delay: 2500 });
    });
}

function loadPessoas() {
    return new Promise((resolve) => {
        Pessoa_ListaPessoas().then(function (data) {
            addOptionPessoas("", "Nenhum");
            for (let i = 0; i < data.length; i++) {
                let obj = data[i];
                addOptionPessoas(obj.id, obj.nome);
            }
            $('#idPessoa').val("");
            $('#idPessoa').selectpicker('refresh');

            resolve();

        });
    });
}

function addOptionPessoas(id, valor) {
    var option = new Option(valor, id);
    var select = document.getElementById("idPessoa");
    select.add(option);
}

function loadTipos() {
    return new Promise((resolve) => {
        TipoContato_ListaTipos().then(function (data) {
            addOptionTipos("", "Nenhum");
            for (let i = 0; i < data.length; i++) {
                let obj = data[i];
                addOptionTipos(obj.id, obj.nome);
            }
            $('#idTipo').val("");
            $('#idTipo').selectpicker('refresh');

            resolve();

        });
    });
}

function addOptionTipos(id, valor) {
    var option = new Option(valor, id);
    var select = document.getElementById("idTipo");
    select.add(option);
}
