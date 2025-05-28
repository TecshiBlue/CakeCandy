using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models.Transaccion
{
    public class Venta
    {
        public int idVenta { get; set; }
        public DateTime fechaVenta { get; set; }
        public int idCliente { get; set; }
        public int usuarioID { get; set; }

        public Venta() { }

        public Venta(int idVenta, DateTime fechaVenta, int idCliente, int usuarioID)
        {
            this.idVenta = idVenta;
            this.fechaVenta = fechaVenta;
            this.idCliente = idCliente;
            this.usuarioID = usuarioID;
        }
    }

}