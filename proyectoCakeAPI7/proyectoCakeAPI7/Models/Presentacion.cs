using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Presentacion
    {
        public int idPresentacion { get; set; }
        public string nombrePresentacion { get; set; }

        public Presentacion() { }

        public Presentacion(int idPresentacion, string nombrePresentacion)
        {
            this.idPresentacion = idPresentacion;
            this.nombrePresentacion = nombrePresentacion;
        }
    }
}