using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResImagen : ResBase
    {

        public class ResIngresarImagen : ResBase
        {
            //lo hace sebas
        }
        public class ResObtenerImagen : ResBase
        {
            public List<Imagen.ObtenerImagenRes> MiListaObtenerImagen = new List<Imagen.ObtenerImagenRes>();
        }


    }
}
