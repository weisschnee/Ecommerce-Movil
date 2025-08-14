using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class Carrito
    {

        public class IngresarCarrito
        {

            public int IdCliente { get; set; }
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }

        }
        public class ObtenerCarrito
        {
            public int? IdCarrito { get; set; }
            public int? IdCliente { get; set; }
            public int? IdProducto { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public int? Cantidad { get; set; }

        }

        public class ObtenerDatoCarrito { 

        public int IdCliente { get; set; }

        }

        public class EliminarCarrito { 
        
        public int? IdCarrito { get; set; }

        }





    }
}
