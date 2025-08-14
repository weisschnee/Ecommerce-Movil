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
    public class ImagenController : ApiController
    {
        // GET: Imagen
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APITienda/Imagen/ObtenerImagenProducto")]
        public ResImagen.ResObtenerImagen Post(ReqImagen.ReqObtenerImagen req)
        {
            return new LogImagen().ObtenerImagenProducto(req);
        }
    }
}