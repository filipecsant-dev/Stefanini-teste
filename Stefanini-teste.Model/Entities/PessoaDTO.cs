using Stefanini.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model.Entities
{
    [Table("Pessoa")]
    public class PessoaDTO : Disable
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        [Required]
        public string Nome { get; set; }

        [MaxLength(11)]
        [Required]
        public string CPF { get; set; }

        [Required]
        [ForeignKey("Cidade")]
        public int Id_Cidade { get; set; }
        public CidadeDTO Cidade { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
