using Backend.Logica;
using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResProducto : ResBase
    {

        public class ResIngresarProducto : ResBase 
        { 

        }

        public class ResObtenerProducto : ResBase
        {
        public List<Producto.ObtenerProducto> MiListaObtenerProducto = new List<Producto.ObtenerProducto>();
        }

        //res usado para trer productos por categoria
        public class ResObtenerSegunCategoria : ResBase
        {
            public List<Producto.ObtenerSegunCategoria> MiListaObtenerProducto = new List<Producto.ObtenerSegunCategoria>();
        }



        public class ResSeleccionarProducto : ResBase
        {

            public int? IdProducto { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int? Stock { get; set; }
            public decimal? Precio { get; set; }
            public string ProveedorNombre { get; set; }
            public string CategoriaNombre { get; set; }
            public int? Descuento { get; set; }

        }



    }
}
