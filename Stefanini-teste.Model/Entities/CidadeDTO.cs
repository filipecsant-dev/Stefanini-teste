using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model.Entities
{
    [Table("Cidade")]
    public class CidadeDTO : Disable
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Nome { get; set; }
        [MaxLength(2)]
        [Required]
        public string UF { get; set; }
    }
}
