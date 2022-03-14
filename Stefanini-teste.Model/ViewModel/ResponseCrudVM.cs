using Stefanini.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model.ViewModel
{
    public class ResponseCrudVM
    {
        public StatusCrud Status { get; set; }
        public string Msg { get; set; }
        public object Objeto { get; set; }
    }
}
