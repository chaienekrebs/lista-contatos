using Contatos.Domain.Entities;

namespace Contatos.Domain.Interfaces.Application
{
    public interface IPessoaService
    {
        Pessoa BuscaPorId(int id);
        List<Pessoa> ListaPessoas();
        void Salvar(Pessoa obj);
        void Excluir(int id);
    }
}
