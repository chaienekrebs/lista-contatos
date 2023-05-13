using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contatos.Domain.Entities
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public int TipoContatoId { get; set; }
        public int PessoaId { get; set; }
        public string Valor { get; set; }

        public TipoContato TipoContato { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
