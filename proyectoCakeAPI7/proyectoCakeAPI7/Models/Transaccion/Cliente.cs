using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models.Transaccion
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string emailCliente { get; set; }
        public string telefonoCliente { get; set; }

        public Cliente() { }

        public Cliente(int idCliente, string nombreCliente, string apellidoCliente,
                       string emailCliente, string telefonoCliente)
        {
            this.idCliente = idCliente;
            this.nombreCliente = nombreCliente;
            this.apellidoCliente = apellidoCliente;
            this.emailCliente = emailCliente;
            this.telefonoCliente = telefonoCliente;
        }
    }
}