using Backend.AccesoDatos;
using Backend.Modelos;
using Backend.Modelos.Entidades;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using Backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;


namespace Backend.Logica
{

    public class LogPedido
    {

        public  ResPedido.ResInsertarPedido IngresarPedido(ReqPedido.ReqIngresarPedido req)
        {
            ResPedido.ResInsertarPedido res = new ResPedido.ResInsertarPedido();

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
                else if (req.ingresar.IdCliente <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "ID del cliente Vacio";
                    res.ListaErrores.Add(error);

                }
                else if (string.IsNullOrEmpty(req.ingresar.Domicilio))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Domicilio vacio";
                    res.ListaErrores.Add(error);

                }
                else
                {
                    LogCarrito logCar = new LogCarrito();
                    ReqCarrito.ReqObtenerCarrito reqcar = new ReqCarrito.ReqObtenerCarrito();
                    reqcar.obtener = new Carrito.ObtenerDatoCarrito();
                    ResCarrito.ResObtenerCarrito rescar = new ResCarrito.ResObtenerCarrito();
                    reqcar.obtener.IdCliente = req.ingresar.IdCliente;





                    rescar = logCar.obtenerCarritos(reqcar);


                    foreach (var carrito in rescar.MilistaObtenerCarrito)
                    {

                        int? id = 0;
                        string nombre = "";
                        int? stock = 0;
                        decimal? precio = 0;
                        int? descuento = 0;
                        double desc = 0;
                      

                        int? errorIds = 0;
                        string errorDescripcions = "";

                        decimal? precioFinal = 0;
                        LogProducto logProd = new LogProducto();
                        ResProducto.ResSeleccionarProducto resProd = new ResProducto.ResSeleccionarProducto();
                        ReqProducto.ReqSeleccionarProducto reqProd = new ReqProducto.ReqSeleccionarProducto();



                        using (ConexionDBDataContext miLinq = new ConexionDBDataContext())
                        {

                           miLinq.SP_SELECCIONAR_PRODUCTO_CARRITOS(carrito.IdProducto, ref id,ref nombre ,ref stock, ref precio, ref descuento, ref errorIds, ref errorDescripcions);
                       
                            if (id <= 0 || id == null)
                            {
                                res.Resultado = false;       
                                error.IdError = errorIds;
                                error.ErrorDescripcion = "El Producto " + nombre + " no fue encontrado";
                                res.ListaErrores.Add(error);
                                return res;

                            }
                            else if (stock < carrito.Cantidad)
                            {
                                res.Resultado = false;
                                error.IdError = 16;
                                error.ErrorDescripcion = "No hay suficiente stock del Producto: " + nombre;
                                res.ListaErrores.Add(error);
                                return res;
                            }
                            else
                            {
                                desc = (double)descuento;
                                desc = (int)precio * (desc / 100);
                                precio -= (decimal)desc;
                                precioFinal = precio * carrito.Cantidad;

                                req.ingresar.Total += (decimal)precioFinal;

                            }


                        }
                    }

                    int? returnId = 0;
                    string codigoReturn = "";
                    int? errorId = 0;
                    string errorDescripcion = "";

                 
                    do
                    {  
                        req.ingresar.CodigoPedido = GenerarCodigoAleatorio(6);
                        using (ConexionDBDataContext miLinq = new ConexionDBDataContext())
                        {

                            miLinq.SP_INGRESAR_PEDIDO(req.ingresar.CodigoPedido.ToUpper(), req.ingresar.IdCliente, req.ingresar.Total, req.ingresar.Domicilio, ref returnId, codigoReturn, ref errorId, ref errorDescripcion);


                            if (returnId <= 0 || returnId == null)
                            {
                                res.Resultado = false;
                                error.IdError = errorId;
                                error.ErrorDescripcion = errorDescripcion;
                                if (errorId == 12) { }
                                else { res.ListaErrores.Add(error); }
                            }
                            else
                            {
                                res.Resultado = true;
                                res.CodigoPedido = req.ingresar.CodigoPedido;
                            }


                        }
                    } while (errorId == 12);
                }


            } catch
            {
                res.Resultado = false;
                error.IdError = 500;
                error.ErrorDescripcion = "ERROR INTERNO";
                res.ListaErrores.Add(error);
            }




            // de aca para ariba hay algo mal
            return res;


        }



        public ResPedido.ResObtenerPedido ObtenerPedido(ReqPedido.ReqObtenerPedido req) {

            ResPedido.ResObtenerPedido res = new ResPedido.ResObtenerPedido();
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
                else if (req.obtener.IdCliente <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Cliente faltante";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    // Ejecutar el procedimiento almacenado y obtener los resultados
                    List<SP_OBTENER_PEDIDOS_CLIENTEResult> resultSet = miLinq.SP_OBTENER_PEDIDOS_CLIENTE(req.obtener.IdCliente).ToList();

                    if (res.MiListaObtenerPedidos == null)
                    {
                        res.MiListaObtenerPedidos = new List< Pedido.variosPedidos>();
                    }
                    // Mapear los resultados a objetos ObtenerProductos
                    foreach (SP_OBTENER_PEDIDOS_CLIENTEResult productoLinq in resultSet)
                        {
                            res.MiListaObtenerPedidos.Add(fabricaPedidos(productoLinq));
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
        

        
        }


        private Pedido.variosPedidos fabricaPedidos(SP_OBTENER_PEDIDOS_CLIENTEResult pedidosLinq) {

            Pedido.variosPedidos pedidoFabricado = new Pedido.variosPedidos();

            pedidoFabricado.IdCliente = pedidosLinq.idCliente;
            pedidoFabricado.CodigoPedido = pedidosLinq.CODIGO_PEDIDO;
            pedidoFabricado.Estado = pedidosLinq.ESTADO;
            pedidoFabricado.Total = pedidosLinq.TOTAL;
            pedidoFabricado.Domicilio = pedidosLinq.DIRECCION;

            return pedidoFabricado;
        }


        private Carrito.ObtenerCarrito fabricaDeCarrito(SP_OBTENER_CARRITOSResult carritoLinq)
        {
            Carrito.ObtenerCarrito carritoFabricado = new Carrito.ObtenerCarrito();
           
            carritoFabricado.IdCliente = carritoLinq.idProducto;
            carritoFabricado.Cantidad = carritoLinq.CANTIDAD;
            return carritoFabricado;
        }

        static string GenerarCodigoAleatorio(int longitud)
        {

            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] codigo = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                codigo[i] = caracteres[random.Next(caracteres.Length)];
            }

            return new string(codigo);
        }
   

}
}
