using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Request
{
    public class ReqCarrito
    {

        public class ReqIngresarCarrito {
        
        public Carrito.IngresarCarrito ingresar {  get; set; }
           
        }


        public class ReqObtenerCarrito {
        
        public Carrito.ObtenerDatoCarrito obtener {  get; set; }

        }

        public class ReqEliminarCarrito
        {

            public Carrito.EliminarCarrito eliminar { get; set; }

        }


    }
}
