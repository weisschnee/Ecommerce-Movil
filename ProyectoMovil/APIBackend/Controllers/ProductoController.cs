using System;
using Backend.Modelos;
using Backend.Modelos.Request;
using Backend.Logica;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Backend.Modelos.Response;
using System.Web.Http;

namespace APIBackend.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: Producto
       
        //Post api/values
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("APIBackend/Producto/ObtenerProducto")]
        public ResProducto.ResObtenerProducto obtenerProductos()
        {
            return new LogProducto().obtenerProductos();
        }

        //Post api/values
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Producto/SeleccionarProducto")]
        public ResProducto.ResSeleccionarProducto obtenerProductos(ReqProducto.ReqSeleccionarProducto req)
        {
            return new LogProducto().selecionarProducto(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("APIBackend/Producto/ObtenerCategoriaDeProductos")]
        public ResProducto.ResObtenerSegunCategoria ObtenerCategoriaProductos(ReqProducto.ReqObtenerSegunCategoria req)
        {
            return new LogProducto().obtenerProductosSegunCategoria(req);
        }


    }
}