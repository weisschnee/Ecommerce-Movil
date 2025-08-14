using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Request
{
    public  class ReqImagen
    {

        public class ReqIngresarImagen {
        
        }
        public class ReqObtenerImagen {
            public Imagen.ObtenerImagenReq obtener {  get; set; }
        }

       

    }
}
