using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{

    public class Categoria
    {
        public class IngresarCategoria
        {
            public string Nombre { get; set; }
        }

        public class ObtenerCategoria
        {
            public int IdCategoria { get; set; }
            public string Nombre { get; set; }
        }


    }
}
