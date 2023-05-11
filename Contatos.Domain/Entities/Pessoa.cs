using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contatos.Domain.Entities
{
    [Table("Pessoa", Schema = "pessoa")]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
