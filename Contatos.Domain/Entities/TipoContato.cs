﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contatos.Domain.Entities
{
    public class TipoContato
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
