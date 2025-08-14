using Backend.Logica;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace APIBackend.Controllers
{
    public class ClienteController : ApiController
    {
      
        // GET: Cliente
        //Post api/values
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Cliente/Registrarse")]
        public ResCliente.ResRegistrarse registrarse(ReqCliente.ReqRegistrarse req)
        {
            return new LogCliente().Registrarse(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Cliente/Login")]
        public ResCliente.ResLogin login(ReqCliente. ReqLogin req)
        {
            return new LogCliente().Login(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Cliente/GenerarNumeroVerificacion")]
        public ResCliente.ResGeneracionNumeroVerificacion GenerarNumeroVerificacion(ReqCliente.ReqGeneracionNumeroVerificacion req)
        {
            return new LogCliente().generarNumeroVerificacion(req);
        }
    }
}