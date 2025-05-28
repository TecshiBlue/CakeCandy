using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Categoria
    {

        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }

        public Categoria() { }

        public Categoria(int idCategoria, string nombreCategoria)
        {
            this.idCategoria = idCategoria;
            this.nombreCategoria = nombreCategoria;
        }
    }
}