using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model.ViewModel
{
    public class CidadeVM
    {
        [MaxLength(200, ErrorMessage = "Nome muito longo, informe um nome mais curto!")]
        [Required(ErrorMessage = "Necessário informar o Nome.")]
        public string Nome { get; set; }
        [MaxLength(2)]
        [Required(ErrorMessage = "Necessário informar o Estado.")]
        public string UF { get; set; }
    }

    public class CidadeReadVM
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }
    }
}

