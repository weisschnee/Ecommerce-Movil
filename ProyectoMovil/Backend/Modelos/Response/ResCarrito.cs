using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResCarrito : ResBase
    {

        public class ResIngresarCarrito : ResBase
        {
        

        
        }

        public class ResObtenerCarrito : ResBase
        {

            public List<Carrito.ObtenerCarrito> MilistaObtenerCarrito = new List<Carrito.ObtenerCarrito>();

        }


        public class ResEliminarCarrito : ResBase
        {



        }


    }
}
