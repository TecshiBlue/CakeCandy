using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Rol
    {
        public int idRol { get; set; }
        public string nombreRol { get; set; }

        public Rol() { }

        public Rol(int idRol, string nombreRol)
        {
            this.idRol = idRol;
            this.nombreRol = nombreRol;
        }
    }
}