using Contatos.Helpers;
using Contatos.Domain.DTO;
using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces.Application;
using Contatos.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Application.Services
{
    public class ContatoService : IContatoService
    {
        private IRepositoryBase<Contato> _repository { get; set; }
        public ContatoService(IRepositoryBase<Contato> repository)
        {
            _repository = repository;
        }

        public Contato BuscaPorId(int id)
        {

            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            if (obj == null)
            {
                throw new Exception("Contato não encontrado!");
            }
            return obj;

        }

        public void Excluir(int id)
        {
            try
            {
                _repository.Delete(id);
                _repository.SaveChanges();
            }
            catch
            {
                throw new Exception("Houve um problema ao excluir esse Contato!");
            }
        }

        public ListaPaginada<Contato> ListaContatos(int start, int length, int idPessoa, int idTipo, string valor, string sortColumn, string sortColumnDirection)
        {
            valor = (valor ?? "").ToUpper();
            var consulta = _repository.Query(x => (idPessoa == 0 || x.PessoaId == idPessoa) && (idTipo == 0 || x.TipoContatoId == idTipo) && (String.IsNullOrEmpty(valor) || x.Valor.ToUpper().Contains(valor))).Include(x => x.Pessoa).Include(x => x.TipoContato);

            var lista = new ListaPaginada<Contato>
            {
                TotalRegistros = consulta.Count(),
                Lista = consulta.FiltrarPaginado(start, length, sortColumn, sortColumnDirection).ToList(),
            };
            return lista;
        }


        public void Salvar(Contato obj)
        {
            try
            {
                _repository.Save(obj);
                _repository.SaveChanges();
            }
            catch
            {
                throw new Exception("Houve um problema ao salvar esse Contato");
            }
        }
    }
}
