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
    public class CarritoController : ApiController
    {
        // GET: Carrito
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Carrito/IngresarCarrito")]
        public ResCarrito.ResIngresarCarrito IngresarCarrito(ReqCarrito.ReqIngresarCarrito req)
        {
            return new LogCarrito().ingresarCarrito(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Carrito/ObtenerCarrito")]
        public ResCarrito.ResObtenerCarrito ObtenerProductos(ReqCarrito.ReqObtenerCarrito req)
        {
            return new LogCarrito().obtenerCarritos(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Carrito/EliminarCarrito")]
        public ResCarrito.ResEliminarCarrito EliminarCarritp(ReqCarrito.ReqEliminarCarrito req)
        {
            return new LogCarrito().eliminarCarrito(req);
        }


    }
}