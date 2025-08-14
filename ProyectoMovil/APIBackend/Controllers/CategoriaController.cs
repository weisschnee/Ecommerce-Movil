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
    public class CategoriaController : ApiController
    {
        // GET: Categoria

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("APIBackend/Categoria/ObtenerCategorias")]
        public ResCategoria.ResObtenerCategoria obtenerCategoria()
        {
            return new LogCategoria().Obtenercategoria();
        }
    }
}