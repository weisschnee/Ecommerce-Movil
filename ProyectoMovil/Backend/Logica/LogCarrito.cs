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
    public class LogCarrito
    {

        public ResCarrito.ResIngresarCarrito ingresarCarrito(ReqCarrito.ReqIngresarCarrito req) { 
        
            ResCarrito.ResIngresarCarrito res = new ResCarrito.ResIngresarCarrito();
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
                    error.ErrorDescripcion = "Cliente incorrecto";
                    res.ListaErrores.Add(error);
                    
                }
                else if (req.ingresar.IdProducto <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Producto incorrecto";
                    res.ListaErrores.Add(error);
                }
                else if (req.ingresar.Cantidad <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Cantidad incorrecta";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";


                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_INGRESAR_CARRITO(req.ingresar.IdCliente, req.ingresar.IdProducto, req.ingresar.Cantidad, ref returnId, ref errorId, ref errorDescripcion);
                    if (returnId <= 0 || returnId == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        //Todo Bien
                        res.Resultado = true;
                    }
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


        public ResCarrito.ResObtenerCarrito obtenerCarritos(ReqCarrito.ReqObtenerCarrito req)
        {
            ResCarrito.ResObtenerCarrito res = new ResCarrito.ResObtenerCarrito();
            Error error = new Error();
            try
            {
                ConexionDBDataContext miLinq = new ConexionDBDataContext();

                // Ejecutar el procedimiento almacenado y obtener los resultados
                List<SP_OBTENER_CARRITOSResult> resultSet = miLinq.SP_OBTENER_CARRITOS(req.obtener.IdCliente).ToList();

                // Mapear los resultados a objetos ObtenerCarrito
                foreach (SP_OBTENER_CARRITOSResult carritoLinq in resultSet)
                {
                    res.MilistaObtenerCarrito.Add(fabricaDeCarrito(carritoLinq));
                }

                // Si todo va bien, se establece el resultado como verdadero
                res.Resultado = true;
            }
            catch
            {
                // Si ocurre un error, se establece el resultado como falso
                res.Resultado = false;
                error.IdError = 500;
                error.ErrorDescripcion = "500 - Error Interno!!!";
                res.ListaErrores.Add(error);
            }

            return res;
        }


        public ResCarrito.ResEliminarCarrito eliminarCarrito(ReqCarrito.ReqEliminarCarrito req)
        {

            ResCarrito.ResEliminarCarrito res = new ResCarrito.ResEliminarCarrito();
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
                else if (req.eliminar.IdCarrito <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Cliente incorrecto";
                    res.ListaErrores.Add(error);

                }
                else
                {
                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";


                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_ELIMINAR_CARRITO(req.eliminar.IdCarrito, ref returnId, ref errorId, ref errorDescripcion);
                    if (returnId <= 0 || returnId == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        //Todo Bien
                        res.Resultado = true;
                    }
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




        private Carrito.ObtenerCarrito fabricaDeCarrito(SP_OBTENER_CARRITOSResult carritoLinq)
        {
            Carrito.ObtenerCarrito carritoFabricado = new Carrito.ObtenerCarrito();
            carritoFabricado.IdCarrito = carritoLinq.idCarrito;
            carritoFabricado.IdCliente = carritoLinq.idCliente;
            carritoFabricado.IdProducto = carritoLinq.idProducto;
            carritoFabricado.Codigo = carritoLinq.CODIGO;
            carritoFabricado.Nombre = carritoLinq.NOMBRE;
            carritoFabricado.Cantidad = carritoLinq.CANTIDAD;
            return carritoFabricado;
        }




    }
}
