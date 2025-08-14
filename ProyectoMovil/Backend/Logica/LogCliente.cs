using Backend.AccesoDatos;
using Backend.Modelos;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Backend.Modelos.Extras;
using Backend.Modelos.Entidades;

namespace Backend.Logica
{
    public class LogCliente
    {

        public ResCliente.ResRegistrarse Registrarse(ReqCliente.ReqRegistrarse req)
        {
            ReqCliente.ReqGeneracionNumeroVerificacion aura = new ReqCliente.ReqGeneracionNumeroVerificacion();
            ResCliente.ResRegistrarse res = new ResCliente.ResRegistrarse();
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
                else if (string.IsNullOrEmpty(req.registrarse.Nombre))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Nombre vacio";
                    res.ListaErrores.Add(error);
                }
                else if (string.IsNullOrEmpty(req.registrarse.Apellidos))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Apellido vacio";
                    res.ListaErrores.Add(error);
                }
                else if (string.IsNullOrEmpty(req.registrarse.CorreoElectronico))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Email vacio";
                    res.ListaErrores.Add(error);
                }
                else if (string.IsNullOrEmpty(req.registrarse.Password))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Contraseña vacio";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    string nombre = "";
                    string apellidos = "";
                    int? tipoUsuario = 0;

                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_REGISTRARSE(req.registrarse.Nombre, req.registrarse.Apellidos, req.registrarse.CorreoElectronico, req.registrarse.Password, 1, ref returnId, ref nombre,ref apellidos,ref tipoUsuario,ref errorId, ref errorDescripcion);
                    if (returnId <= 0 || returnId == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        res.Resultado = true;
                        res.IdCliente = returnId;
                        res.Nombre = nombre;
                        res.Apellidos = apellidos;
                        res.Tipousuario = tipoUsuario;
                        if (aura.generacion == null) aura.generacion = new Cliente.GeneracionNumeroVerificacion();
                      aura.generacion.IdCliente = (int)returnId;
                        var respuesta = generarNumeroVerificacion(aura);
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

        // Método para obtener clientes
        public ResCliente.ResLogin Login(ReqCliente.ReqLogin req)
        {
            ResCliente.ResLogin res = new ResCliente.ResLogin();

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
                else if (string.IsNullOrEmpty(req.login.CorreoElectronico))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Email vacio";
                    res.ListaErrores.Add(error);
                }
                else if (string.IsNullOrEmpty(req.login.Password))
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Contraseña vacio";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";
                    string nombre = "";
                    string apellidos = "";
                    int? tipoUsuario = 0;

                    ConexionDBDataContext miLinq = new ConexionDBDataContext();
                    miLinq.SP_LOGIN(req.login.CorreoElectronico, req.login.Password, ref returnId, ref nombre, ref apellidos, ref tipoUsuario, ref errorId, ref errorDescripcion);
                    if (returnId <= 0 || returnId == null)
                    {
                        res.Resultado = false;
                        error.IdError = errorId;
                        error.ErrorDescripcion = errorDescripcion;
                        res.ListaErrores.Add(error);
                    }
                    else
                    {
                        res.Resultado = true;
                        res.IdCliente = returnId;
                        res.Nombre = nombre;
                        res.Apellidos = apellidos;
                        res.TipoUsuario = tipoUsuario;

                       
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


        public ResCliente.ResGeneracionNumeroVerificacion generarNumeroVerificacion(ReqCliente.ReqGeneracionNumeroVerificacion req)
        {
            ResCliente.ResGeneracionNumeroVerificacion res = new ResCliente.ResGeneracionNumeroVerificacion();

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
                else if (req.generacion.IdCliente <= 0)
                {
                    res.Resultado = false;
                    error.IdError = -1;
                    error.ErrorDescripcion = "Id Cliente";
                    res.ListaErrores.Add(error);
                }
                else
                {
                    int? returnId = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";
                    string numeroVerificacion = GenerarCodigoAleatorio(6);

                    string correo = "";
                    using (ConexionDBDataContext miLinq = new ConexionDBDataContext()) { 
                        miLinq.SP_OBTENER_CLIENTE(req.generacion.IdCliente, ref correo);
                    }
                    using (ConexionDBDataContext miLinq = new ConexionDBDataContext())
                    {
                        miLinq.SP_AGREGAR_CODIGO_CLIENTE(req.generacion.IdCliente, numeroVerificacion, ref returnId, ref errorId, ref errorDescripcion);
                        if (returnId <= 0 || returnId == null)
                        {
                            res.Resultado = false;
                            error.IdError = errorId;
                            error.ErrorDescripcion = errorDescripcion;
                            res.ListaErrores.Add(error);
                        }
                        else
                        {
                            EnvioCorreo email = new EnvioCorreo();
                            res.Resultado = true;
                            email.enviarEmail(correo, numeroVerificacion);
                        }
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

        // por si se hace


        //private ObtenerCliente FabricaDeCliente(SP_OBTENER_CLIENTESResult clienteLinq)
        //{
        //    ObtenerCliente clienteFabricado = new ObtenerCliente();
        //    clienteFabricado.IdCliente = clienteLinq.idCliente;
        //    clienteFabricado.Nombre = clienteLinq.NOMBRE;
        //    clienteFabricado.Apellidos = clienteLinq.APELLIDOS;
        //    clienteFabricado.CorreoElectronico = clienteLinq.CORREO_ELECTRONICO;
        //    clienteFabricado.TipoUsuario = clienteLinq.TIPO_USUARIO;

        //    return clienteFabricado;
        //}

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
