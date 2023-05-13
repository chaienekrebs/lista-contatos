using Contatos.Domain.DTO;
using Contatos.Domain.Entities;

namespace Contatos.Domain.Interfaces.Application
{
    public interface ITipoContatoService
    {
        TipoContato BuscaPorId(int id);
        ListaPaginada<TipoContato> ListaTiposContato(int start, int length, string nome, string sortColumn, string sortColumnDirection);
        void Salvar(TipoContato obj);
        void Excluir(int id);
    }
}
