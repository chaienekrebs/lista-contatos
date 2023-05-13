using Contatos.Domain.DTO;
using Contatos.Domain.Entities;

namespace Contatos.Domain.Interfaces.Application
{
    public interface IPessoaService
    {
        Pessoa BuscaPorId(int id);
        ListaPaginada<Pessoa> ListaPessoas(int start, int length, string nome, string cpf, string sortColumn, string sortColumnDirection);
        void Salvar(Pessoa obj);
        void Excluir(int id);
    }
}
