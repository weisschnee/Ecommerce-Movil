using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResCliente : ResBase
    {

        public class ResRegistrarse : ResBase
        {

        public int? IdCliente  { get; set; }
        public string Nombre {  get; set; }
        public string Apellidos { get; set; }
        public int? Tipousuario { get; set; }

        }

        public class ResLogin : ResBase
        {

            public int? IdCliente { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int? TipoUsuario { get; set; }

        }


        public class ResGeneracionNumeroVerificacion : ResBase
        {

        }






    }
}
