using Backend.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Response
{
    public class ResCategoria : ResBase
    {

        public class ResIngresarCategoria : ResBase
        {

        }

        public class ResObtenerCategoria : ResBase
        {
            public List<Categoria.ObtenerCategoria> MiListaObtenerCategoria = new List<Categoria.ObtenerCategoria>();

        }

    }
}
