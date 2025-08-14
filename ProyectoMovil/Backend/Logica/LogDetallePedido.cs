using Backend.AccesoDatos;
using Backend.Modelos;
using Backend.Modelos.Entidades;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Logica
{
    public class LogDetallePedido
    {

        public ResDetallePedido.ResObtenerDetallePedido ObtenerDetallePedido(ReqDetallePedido.ReqObtenerDetallePedido req) {


            ResDetallePedido.ResObtenerDetallePedido res = new ResDetallePedido.ResObtenerDetallePedido();
            Error error = new Error();
            try
            {

                if (req == null)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Request nullo";
                    res.ListaErrores.Add(error);
                }
                else if (String.IsNullOrEmpty(req.obtener.CodigoPedido))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Codigo faltante";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    // Ejecutar el procedimiento almacenado y obtener los resultados
                    List<SP_OBTENER_DETALLE_PEDIDOS_CLIENTEResult> resultSet = miLinq.SP_OBTENER_DETALLE_PEDIDOS_CLIENTE(req.obtener.CodigoPedido?.ToUpper()).ToList();

                    if (res.MiListaObtenerDetallePedidos == null)
                    {
                        res.MiListaObtenerDetallePedidos = new List<DetallePedido.VariosDetallePedidos>();
                    }
                    // Mapear los resultados a objetos ObtenerProductos
                    foreach (SP_OBTENER_DETALLE_PEDIDOS_CLIENTEResult detallePedidosLinq in resultSet)
                    {
                        res.MiListaObtenerDetallePedidos.Add(fabricaDetallePedido(detallePedidosLinq));
                    }

                    // Si todo va bien, se establece el resultado como verdadero
                    res.Resultado = true;

                }
            }
            catch
            {
                res.Resultado = false;
                error.IdError = 500;
                error.ErrorDescripcion = "ERROR INTERNO";
                res.ListaErrores.Add(error);
            }

            return res;



            DetallePedido.VariosDetallePedidos fabricaDetallePedido(SP_OBTENER_DETALLE_PEDIDOS_CLIENTEResult detallePedidosLinq) {

                DetallePedido.VariosDetallePedidos DetallePedidoFabricado = new DetallePedido.VariosDetallePedidos();


                DetallePedidoFabricado.IdDetallePedido = detallePedidosLinq.idDetallePedido;
                DetallePedidoFabricado.CodigoPedido = detallePedidosLinq.CODIGO_PEDIDO;
                DetallePedidoFabricado.IdProducto = detallePedidosLinq.idProducto;
                DetallePedidoFabricado.Nombre = detallePedidosLinq.NOMBRE;
                DetallePedidoFabricado.Cantidad = detallePedidosLinq.CANTIDAD;
                DetallePedidoFabricado.Precio = detallePedidosLinq.PRECIO;


                return DetallePedidoFabricado;

            }
        }






    }
}
