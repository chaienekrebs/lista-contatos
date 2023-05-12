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

        public List<Pessoa> ListaPessoa()
        {
            return _repository.QueryAll().ToList();
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
