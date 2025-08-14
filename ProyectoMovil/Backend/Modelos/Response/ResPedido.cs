using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResPedido : ResBase
    {

        public class ResInsertarPedido : ResBase {
        
            public string CodigoPedido {  get; set; }

        }

        public class ResObtenerPedido : ResBase
        {

          public List<Pedido.variosPedidos> MiListaObtenerPedidos = new List<Pedido.variosPedidos>();

        }

    }
}
