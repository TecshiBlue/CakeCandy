using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class UsuarioLoginRequest
    {
        public string nombre { get; set; }
        public string password { get; set; }

        public int idRol { get; set; }

        public UsuarioLoginRequest() { }
        public UsuarioLoginRequest(string nombre, string password, int idRol)
        {
            this.nombre = nombre;
            this.password = password;
            this.idRol = idRol;
        }
    }
}