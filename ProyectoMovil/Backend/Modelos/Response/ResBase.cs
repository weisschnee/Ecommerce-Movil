using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResBase
    {
        public bool Resultado {  get; set; }
        public List<Error> ListaErrores = new List<Error>();
    }
}
