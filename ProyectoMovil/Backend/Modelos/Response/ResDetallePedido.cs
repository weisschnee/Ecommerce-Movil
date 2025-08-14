using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResDetallePedido : ResBase
    {

        public class ResObtenerDetallePedido : ResBase { 

        public List<DetallePedido.VariosDetallePedidos> MiListaObtenerDetallePedidos = new List<DetallePedido.VariosDetallePedidos>();

        }


 


    }
}
