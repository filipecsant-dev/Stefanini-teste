using Stefanini.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model.ViewModel
{
    public class PessoaVM
    {

        [MaxLength(300, ErrorMessage = "Nome muito longo, informe um nome mais curto!")]
        [Required(ErrorMessage = "Necessário informar o nome.")]
        public string Nome { get; set; }

        [MaxLength(11, ErrorMessage = "Informe um CPF válido.")]
        [Required(ErrorMessage = "Necessário informar o CPF.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Necessário informar a idade.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Necessário informar a cidade.")]
        public int Id_Cidade { get; set; }

    }

    public class PessoaReadVM
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public int Idade { get; set; }

        public CidadeReadVM Cidade { get; set; }
    }

    public class PessoaInsertVM
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public int Idade { get; set; }

        public int Id_Cidade { get; set; }

        public CidadeDTO Cidade { get; set; }

    }
}
