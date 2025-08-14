using Backend.Logica;
using Backend.Modelos.Request;
using Backend.Modelos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIBackend.Controllers
{
    public class PedidoController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Pedido/IngresarPedido")]
        public ResPedido.ResInsertarPedido ingresarPedido(ReqPedido.ReqIngresarPedido req)
        {
             return new LogPedido().IngresarPedido(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Pedido/ObtenerPedido")]
        public ResPedido.ResObtenerPedido obtenerPedido(ReqPedido.ReqObtenerPedido req)
        {
            return new LogPedido().ObtenerPedido(req);
        }

    }
}