$(document).ready(function () {
    $("#cpf").inputmask(mascaraCpf);

    var alias = getUltimoAlias();
    if (alias.indexOf('tipo') > -1) {
        $('#titulo').html('Cadastrar Tipo de Contato');
        $('#titulo-nav').html('Cadastrar Tipo de Contato');

    } else {
        $("#id-tipo").val(alias);
        $('#titulo').html('Alterar Tipo de Contato');
        $('#titulo-nav').html('Alterar Tipo de Contato');
        BuscaDadosCadastro(normalizarInt(alias));
    }
});

function BuscaDadosCadastro(alias) {
    TipoContato_BuscaPorId(alias).then(function (data) {
        $("#nome").val(data.nome);        
    });
}

var form = document.querySelector('form');
form.addEventListener('submit', function (e) {
    e.preventDefault();
    var ele = $("#btn-submit");

    loading(ele);
    var obj = {
        id: normalizarInt($("#id-tipo").val()),
        nome: $("#nome").val(),
    };

    TipoContato_Salvar(obj).then(function () {
        $("input").val("");
        if (obj.id != "") {
            $.notify("Dados Alterados com sucesso!", "success", { delay: 2500 });
        } else {
            $.notify("Tipo de contato cadastrado com sucesso!", "success", { delay: 2500 });
        }
        unloading(ele);
        setTimeout(function () {
            window.location.href = '/gerenciar-tipos';
        }, 2000);
    }, function (err) {
        $.notify(err, "error", { delay: 2500 });
        window.location.href = '/gerenciar-tipos';
        $("input").val("");
        unloading(ele);
    });

});