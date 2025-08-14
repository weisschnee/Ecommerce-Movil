using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Request
{
    public class ReqPedido
    {

        public class ReqIngresarPedido {
        
            public Pedido.IngresarPedido ingresar {  get; set; }

            
        }


        public class ReqObtenerPedido
        {

            public Pedido.ObtenerPedido obtener { get; set; }


        }


    }
}
