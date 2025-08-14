using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class Producto
    {

        public class IngresarProducto
        {


            //lo hace el gabriel del futuro o sebas si me da pereza
        }

        public class ObtenerProducto
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

        public class SeleccionarProducto
        {
            public string Codigo { get; set; }
        }


        //usado para obtener sergun categoria
        public class ObtenerProductoSegunCategoria
        {
            public int idCategoria { get; set; }
        }

        //usado para obtener sergun categoria
        public class ObtenerSegunCategoria
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
