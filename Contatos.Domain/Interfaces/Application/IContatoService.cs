using Contatos.Domain.DTO;
using Contatos.Domain.Entities;

namespace Contatos.Domain.Interfaces.Application
{
    public interface IContatoService
    {
        Contato BuscaPorId(int id);
        ListaPaginada<Contato> ListaContatos(int start, int length, int idPessoa, int idTipo, string valor, string sortColumn, string sortColumnDirection);
        void Salvar(Contato obj);
        void Excluir(int id);
    }
}
