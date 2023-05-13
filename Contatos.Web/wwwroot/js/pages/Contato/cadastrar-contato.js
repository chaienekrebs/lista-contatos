$(document).ready(function () {
    $("#cpf").inputmask(mascaraCpf);
    loadPessoas().then(function () {
        loadTipos().then(function () {
            var alias = getUltimoAlias();
            if (alias.indexOf('contato') > -1) {
                $('#titulo').html('Cadastrar Contato');
                $('#titulo-nav').html('Cadastrar Contato');

            } else {
                $("#id-contato").val(alias);
                $('#titulo').html('Alterar Contato');
                $('#titulo-nav').html('Alterar Contato');
                BuscaDadosCadastro(normalizarInt(alias));
            }
        });
    });
});

function BuscaDadosCadastro(alias) {
    Contato_BuscaPorId(alias).then(function (data) {
        $("#pessoa").val(data.pessoaId);
        $('#pessoa').selectpicker('refresh');
        $("#tipo").val(data.tipoContatoId);
        $('#tipo').selectpicker('refresh');
        $("#valor").val(data.valor);
    });
}

var form = document.querySelector('form');
form.addEventListener('submit', function (e) {
    e.preventDefault();
    var ele = $("#btn-submit");

    loading(ele);
    var obj = {
        id: normalizarInt($("#id-contato").val()),
        pessoaId: normalizarInt($("#pessoa").val()),
        tipoContatoId: normalizarInt($("#tipo").val()),
        valor: $("#valor").val(),
    };

    Contato_Salvar(obj).then(function () {
        $("input").val("");
        if (obj.id != "") {
            $.notify("Dados Alterados com sucesso!", "success", { delay: 2500 });
        } else {
            $.notify("Contato cadastrado com sucesso!", "success", { delay: 2500 });
        }
        unloading(ele);
        setTimeout(function () {
            window.location.href = '/gerenciar-contatos';
        }, 2000);
    }, function (err) {
        $.notify(err, "error", { delay: 2500 });
        window.location.href = '/gerenciar-contatos';
        $("input").val("");
        unloading(ele);
    });

});


function loadPessoas() {
    return new Promise((resolve) => {
        Pessoa_ListaPessoas().then(function (data) {

            for (let i = 0; i < data.length; i++) {
                let obj = data[i];
                addOptionPessoas(obj.id, obj.nome);
            }
            $('#pessoa').selectpicker('refresh');

            resolve();

        });
    });
}

function addOptionPessoas(id, valor) {
    var option = new Option(valor, id);
    var select = document.getElementById("pessoa");
    select.add(option);
}

function loadTipos() {
    return new Promise((resolve) => {
        TipoContato_ListaTipos().then(function (data) {

            for (let i = 0; i < data.length; i++) {
                let obj = data[i];
                addOptionTipos(obj.id, obj.nome);
            }
            $('#tipo').selectpicker('refresh');

            resolve();

        });
    });
}

function addOptionTipos(id, valor) {
    var option = new Option(valor, id);
    var select = document.getElementById("tipo");
    select.add(option);
}
