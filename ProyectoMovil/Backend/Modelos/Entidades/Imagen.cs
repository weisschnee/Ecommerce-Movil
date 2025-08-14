using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class Imagen
    {

        public class Ingresar {
        
            //lo hace sebas

        }

        public class ObtenerImagenReq {

            public int IdProducto { get; set; }

        }
        
            public class ObtenerImagenRes {
            public int? IdImagen { get; set; }

            public int? IdProducto { get; set; }

            public string ImagenNombre { get; set; }
            public byte[] ImagenUrl { get; set; }
            }

    }
}
