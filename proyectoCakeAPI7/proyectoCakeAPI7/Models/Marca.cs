using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Marca
    {
        public int idMarca { get; set; }
        public string nombreMarca { get; set; }
        public string paisOrigenMarca { get; set; }

        public Marca() { }

        public Marca(int idMarca, string nombreMarca, string paisOrigenMarca)
        {
            this.idMarca = idMarca;
            this.nombreMarca = nombreMarca;
            this.paisOrigenMarca = paisOrigenMarca;
        }
    }
}