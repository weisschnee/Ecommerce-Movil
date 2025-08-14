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
    public class LogCategoria
    {

        public ResCategoria.ResIngresarCategoria ingresarCategoria(ReqCategoria.ReqIngresarCategoria req) {

            ResCategoria.ResIngresarCategoria res = new ResCategoria.ResIngresarCategoria();
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
                else if (string.IsNullOrEmpty(req.categoria.Nombre))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Nombre incorrecto";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_INGRESAR_CATEGORIA(req.categoria.Nombre, ref returnId, ref errorId, ref errorDescripcion);
                    if (returnId <= 0 || returnId == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        // Todo Bien
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

        public ResCategoria.ResObtenerCategoria Obtenercategoria() {

            ResCategoria.ResObtenerCategoria res = new ResCategoria.ResObtenerCategoria();
            Error error = new Error();

            try
            {
                // Conexión al Linq
                ConexionDBDataContext miLinq = new ConexionDBDataContext();

                // Obtener datos desde el procedimiento almacenado
                List<SP_OBTENER_CATEGORIASResult> resultSet = miLinq.SP_OBTENER_CATEGORIAS().ToList();

                // Inicializar la lista si no está inicializada
                if (res.MiListaObtenerCategoria == null)
                {
                    res.MiListaObtenerCategoria = new List<Categoria.ObtenerCategoria>();
                }

                // Convertir los resultados a la lista de ObtenerCategoria
                foreach (SP_OBTENER_CATEGORIASResult unaCategoria in resultSet)
                {
                    res.MiListaObtenerCategoria.Add(this.FabricaDeCategoria(unaCategoria));
                }

                // Si todo va bien, el resultado es verdadero
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


        private Categoria.ObtenerCategoria FabricaDeCategoria(SP_OBTENER_CATEGORIASResult categoriaLinq)
        {
            Categoria.ObtenerCategoria categoriaFabricada = new Categoria.ObtenerCategoria
            {
                IdCategoria = categoriaLinq.idCategoria,
                Nombre = categoriaLinq.NOMBRE
            };
            return categoriaFabricada;
        }

    }
}
