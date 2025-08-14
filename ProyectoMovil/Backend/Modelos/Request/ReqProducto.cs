using Backend.Logica;
using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Request
{
    public class ReqProducto
    {

        public class ReqIngresarProducto { 
        //sebas o yo del futuro
        }

        public class ReqObtenerProducto {
           
        }

        public class ReqSeleccionarProducto { 

        public Producto.SeleccionarProducto seleccionar {  get; set; }

        }


        //usado para obtener segun categoria
        public class ReqObtenerSegunCategoria
        {

            public Producto.ObtenerProductoSegunCategoria obtenerSegun { get; set; }

        }

    }
}
