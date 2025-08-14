using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class Cliente
    {

        public class Registrarse {

            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string CorreoElectronico { get; set; }
            public string Password { get; set; }

        }

        public class Login {

            public string CorreoElectronico { get; set; }
            public string Password { get; set; }
        }


        public class GeneracionNumeroVerificacion
        {

            public int? IdCliente { get; set; }
            
        }












    }
}
