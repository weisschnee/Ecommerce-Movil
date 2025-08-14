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
    public class LogImagen
    {

        public ResImagen.ResObtenerImagen ObtenerImagenProducto(ReqImagen.ReqObtenerImagen req)
        {
            ResImagen.ResObtenerImagen res = new ResImagen.ResObtenerImagen();
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
                else if (req.obtener.IdProducto <= 0) {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "id producto es invalido";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    using (var miLinq = new ConexionDBDataContext())
                    {
                        var resultSet = miLinq.SP_OBTENER_IMAGENES_PRODUCTO(req.obtener.IdProducto).ToList();

                        foreach (var imagenLinq in resultSet)
                        {
                            Imagen.ObtenerImagenRes imagen = FabricaDeImagen(imagenLinq);
                            res.MiListaObtenerImagen.Add(imagen);
                        }

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

        private Imagen.ObtenerImagenRes FabricaDeImagen(SP_OBTENER_IMAGENES_PRODUCTOResult imagenLinq)
        {
            return new Imagen.ObtenerImagenRes
            {
                IdImagen = imagenLinq.idImagen,
                IdProducto = imagenLinq.idProducto,
                ImagenNombre = imagenLinq.IMAGEN_NOMBRE,
                ImagenUrl = imagenLinq.IMAGENURL.ToArray() // Asegúrate de que ImagenUrl sea byte[]
            };
        }



    }
}
