using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Request
{
    public class ReqCliente
    {

        public class ReqRegistrarse {
        
            public Cliente.Registrarse registrarse { get; set; }
        
        }

        public class ReqLogin {
            
            public Cliente.Login login { get; set; }

        }


        public class ReqGeneracionNumeroVerificacion
        {

           public Cliente.GeneracionNumeroVerificacion generacion { get; set; }
        }




    }
}
