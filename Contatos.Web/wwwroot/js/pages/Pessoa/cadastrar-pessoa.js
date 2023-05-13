$(document).ready(function () {
    $("#cpf").inputmask(mascaraCpf);

    var alias = getUltimoAlias();
    if (alias.indexOf('pessoa') > -1) {
        $('#titulo').html('Cadastrar pessoa');
        $('#titulo-nav').html('Cadastrar pessoa');

    } else {
        $("#id-pessoa").val(alias);
        $('#titulo').html('Alterar pessoa');
        $('#titulo-nav').html('Alterar pessoa');
        BuscaDadosCadastro(normalizarInt(alias));
    }
});

function BuscaDadosCadastro(alias) {
    Pessoa_BuscaPorId(alias).then(function (data) {
        $("#nome").val(data.nome);
        $("#cpf").val(data.cpf);
        $("#endereco").val(data.endereco);
        
    });
}

var form = document.querySelector('form');
form.addEventListener('submit', function (e) {
    e.preventDefault();
    var ele = $("#btn-submit");

    loading(ele);
    var obj = {
        id: normalizarInt($("#id-pessoa").val()),
        nome: $("#nome").val(),
        cpf: $("#cpf").val(),
        endereco: $("#endereco").val(),
    };

    Pessoa_Salvar(obj).then(function () {
        $("input").val("");
        if (obj.id != "") {
            $.notify("Dados Alterados com sucesso!", "success", { delay: 2500 });
        } else {
            $.notify("Pessoa cadastrada com sucesso!", "success", { delay: 2500 });
        }
        unloading(ele);
        setTimeout(function () {
            window.location.href = '/gerenciar-pessoas';
        }, 2000);
    }, function (err) {
        $.notify(err, "error", { delay: 2500 });
        window.location.href = '/gerenciar-pessoas';
        $("input").val("");
        unloading(ele);
    });

});