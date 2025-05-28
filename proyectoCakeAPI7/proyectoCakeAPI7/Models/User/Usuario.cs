using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models
{
    public class Usuario
    {
        public int usuarioID { get; set; }
        public string nombre { get; set; }
        public string passwordHash { get; set; }
        public int idRol { get; set; }

        public Usuario() { }

        public Usuario(int usuarioID, string nombre, string passwordHash, int idRol)
        {
            this.usuarioID = usuarioID;
            this.nombre = nombre;
            this.passwordHash = passwordHash;
            this.idRol = idRol;
        }
    }


}