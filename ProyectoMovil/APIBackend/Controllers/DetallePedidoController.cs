using Backend.Logica;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIBackend.Controllers
{
    public class DetallePedidoController : ApiController
    {
        // GET: DetallePedido
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/DetallePedido/ObtenerDetallePedido")]
        public ResDetallePedido.ResObtenerDetallePedido obtenerDetallePedido(ReqDetallePedido.ReqObtenerDetallePedido req)
        {
            return new LogDetallePedido().ObtenerDetallePedido(req);
        }

    }
}