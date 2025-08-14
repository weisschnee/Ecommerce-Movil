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
    public class LogProducto
    {

        public ResProducto.ResObtenerProducto obtenerProductos()
        {
            ResProducto.ResObtenerProducto res = new ResProducto.ResObtenerProducto();
            Error error = new Error();
            try
            {
                ConexionDBDataContext miLinq = new ConexionDBDataContext();

                // Ejecutar el procedimiento almacenado y obtener los resultados
                List<SP_OBTENER_PRODUCTOSResult> resultSet = miLinq.SP_OBTENER_PRODUCTOS().ToList();

                // Mapear los resultados a objetos ObtenerProductos
                foreach (SP_OBTENER_PRODUCTOSResult productoLinq in resultSet)
                {
                    res.MiListaObtenerProducto.Add(fabricaDeProducto(productoLinq));
                }

                // Si todo va bien, se establece el resultado como verdadero
                res.Resultado = true;
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

        public ResProducto.ResSeleccionarProducto selecionarProducto(ReqProducto.ReqSeleccionarProducto req)
        {

            ResProducto.ResSeleccionarProducto res = new ResProducto.ResSeleccionarProducto();
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
                else if (String.IsNullOrEmpty(req.seleccionar.Codigo))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Codigo faltante";
                    res.ListaErrores.Add(error);
                }
                else
                {

                    int? idProducto = 0;
                    string codigo = "";
                    string nombre = "";
                    string descripcion = "";
                    int? stock = 0;
                    decimal? precio = 0;
                    string proveedorNombre = "";
                    string categoriaNombre = "";
                    int? descuento = 0;


                    int? errorId = 0;
                    string errorDescripcion = "";

                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_SELECCIONAR_PRODUCTOS(req.seleccionar.Codigo, ref idProducto, ref codigo, ref nombre, ref descripcion,
                        ref stock, ref precio, ref proveedorNombre, ref categoriaNombre, ref descuento, ref errorId, ref errorDescripcion);
                    if (idProducto <= 0 || idProducto == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        res.Resultado = true;
                        res.IdProducto = idProducto;
                        res.Codigo = codigo;
                        res.Nombre = nombre;
                        res.Descripcion = descripcion;
                        res.Stock = stock;
                        res.Precio = precio;
                        res.ProveedorNombre = proveedorNombre;
                        res.CategoriaNombre = categoriaNombre;
                        res.Descuento = descuento;

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


        public ResProducto.ResObtenerSegunCategoria obtenerProductosSegunCategoria(ReqProducto.ReqObtenerSegunCategoria req)
        {
            ResProducto.ResObtenerSegunCategoria res = new ResProducto.ResObtenerSegunCategoria();
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
                else if (req.obtenerSegun.idCategoria <= 0 )
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Categoria faltante";
                    res.ListaErrores.Add(error);
                }
                else { 
                ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                // Ejecutar el procedimiento almacenado y obtener los resultados
                List <SP_OBTENER_PRODUCTOS_SEGUN_CATEGORIAResult> resultSet = miLinq.SP_OBTENER_PRODUCTOS_SEGUN_CATEGORIA(req.obtenerSegun.idCategoria,ref idReturn, ref errorId, ref errorDescripcion).ToList();
                    if (idReturn <= 0 || idReturn == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else { 
                             // Mapear los resultados a objetos ObtenerProductos
                             foreach (SP_OBTENER_PRODUCTOS_SEGUN_CATEGORIAResult productoLinq in resultSet)
                             {
                             res.MiListaObtenerProducto.Add(fabricaDeProductoSegunCategoria(productoLinq));
                             }

                             // Si todo va bien, se establece el resultado como verdadero
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




        private Producto.ObtenerProducto fabricaDeProducto(SP_OBTENER_PRODUCTOSResult productoLinq)
        {
            Producto.ObtenerProducto productoFabricado = new Producto.ObtenerProducto();
            productoFabricado.IdProducto = productoLinq.idProducto;
            productoFabricado.Codigo = productoLinq.CODIGO;
            productoFabricado.Nombre = productoLinq.NOMBRE;
            productoFabricado.Descripcion = productoLinq.DESCRIPCION;
            productoFabricado.Stock = productoLinq.STOCK;
            productoFabricado.Precio = productoLinq.PRECIO;
            productoFabricado.ProveedorNombre = productoLinq.NOMBRE_PROVEEDOR;
            productoFabricado.CategoriaNombre = productoLinq.NOMBRE_CATEGORIA;
            productoFabricado.Descuento = productoLinq.DESCUENTO;

            return productoFabricado;
        }

        private Producto.ObtenerSegunCategoria fabricaDeProductoSegunCategoria(SP_OBTENER_PRODUCTOS_SEGUN_CATEGORIAResult productoLinq)
        {
            Producto.ObtenerSegunCategoria productoFabricado = new Producto.ObtenerSegunCategoria();
            productoFabricado.IdProducto = productoLinq.idProducto;
            productoFabricado.Codigo = productoLinq.CODIGO;
            productoFabricado.Nombre = productoLinq.NOMBRE;
            productoFabricado.Descripcion = productoLinq.DESCRIPCION;
            productoFabricado.Stock = productoLinq.STOCK;
            productoFabricado.Precio = productoLinq.PRECIO;
            productoFabricado.ProveedorNombre = productoLinq.NOMBRE_PROVEEDOR;
            productoFabricado.CategoriaNombre = productoLinq.NOMBRE_CATEGORIA;
            productoFabricado.Descuento = productoLinq.DESCUENTO;

            return productoFabricado;
        }



    }
}
