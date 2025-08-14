using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Entidades
{
    public class DetallePedido
    {

        public class ObtenerDetallePedido {

            public string CodigoPedido { get; set; }

        }

        public  class VariosDetallePedidos { 
        public int IdDetallePedido {  get; set; }
        public string CodigoPedido { get; set; }
        public int IdProducto {  get; set; }
        public string Nombre {  get; set; }
        public int Cantidad { get; set; }
        public decimal Precio {  get; set; }

        }

    }
}
