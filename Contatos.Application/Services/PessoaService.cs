using Contatos.Helpers;
using Contatos.Domain.DTO;
using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces.Application;
using Contatos.Domain.Interfaces.Repositories;

namespace Contatos.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private IRepositoryBase<Pessoa> _repository { get; set; }
        public PessoaService(IRepositoryBase<Pessoa> repository)
        {
            _repository = repository;
        }

        public Pessoa BuscaPorId(int id)
        {

            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            if (obj == null)
            {
                throw new Exception("Pessoa não encontrada!");
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
                throw new Exception("Houve um problema ao excluir essa Pessoa!");
            }
        }

        public ListaPaginada<Pessoa> ListaPessoas(int start, int length, string nome, string cpf, string sortColumn, string sortColumnDirection)
        {
            nome = (nome ?? "").ToUpper();
            var consulta = _repository.Query(x => (String.IsNullOrEmpty(nome) || x.Nome.ToUpper().Contains(nome)) && (String.IsNullOrEmpty(cpf) || x.CPF.Contains(cpf)));

            var lista = new ListaPaginada<Pessoa>
            {
                TotalRegistros = consulta.Count(),
                Lista = consulta.FiltrarPaginado(start, length, sortColumn, sortColumnDirection).ToList(),
            };
            return lista;
        }


        public void Salvar(Pessoa obj)
        {
            try
            {
                _repository.Save(obj);
                _repository.SaveChanges();
            }
            catch
            {
                throw new Exception("Houve um problema ao salvar essa Pessoa");
            }
        }
    }
}
