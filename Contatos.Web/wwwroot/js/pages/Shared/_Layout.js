$(document).ready(function () {
    buscarDadosAcesso().then(function (data) {
        let idEmpresa = GetCookie('id-empresa');
        Empresa_BuscaEmpresasPorId(idEmpresa).then(function (data) {
            $('#nome-prefeitura').html(data.nome);

            var base64 = "data:image/png;base64," + data.logoString;
            document.getElementById('brasao').src = base64;
        });
    });
});

function buscarDadosAcesso() {
    return new Promise((resolve, reject) => {
        let idUsuario = GetCookie('id-usuario');
        let nomeUsuario = GetCookie('nome-usuario');
        let idEmpresa = GetCookie('id-empresa');
        let tipoUsuario = GetCookie('tipo-usuario');
        if (!idUsuario || !nomeUsuario || !idEmpresa|| !tipoUsuario) {
            Usuario_Autenticar().then(function (usuario) {
                $('#dados-usuario-logado').html('Usuário Logado: ' + usuario.nome);
                SetCookie('id-usuario', usuario.id);
                SetCookie('nome-usuario', usuario.nome);
                SetCookie('permissao', usuario.nivelAcesso);
                SetCookie('id-empresa', usuario.empresaId);
                resolve();
            }, function (err) {
                sair();
            });
        } else {
            $('#dados-usuario-logado').html('Usuário Logado: ' + nomeUsuario);
            resolve();
        }
    });
}
