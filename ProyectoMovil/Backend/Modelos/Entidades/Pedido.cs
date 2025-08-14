using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class Pedido
    {

        public class IngresarPedido {

            public int IdCliente { get; set; }
            public string CodigoPedido { get; set; }
            public decimal Total { get; set; }
            public string Domicilio { get; set; }
        }

        public class ObtenerPedido { 
        
        public int IdCliente { get; set; }
        
        }

        public class variosPedidos
        {

            public int IdCliente { get; set; }
            public string CodigoPedido { get; set; }
            public int Estado { get; set; }
            public decimal Total { get; set; }
            public string Domicilio { get; set; }
        }


    }
}
