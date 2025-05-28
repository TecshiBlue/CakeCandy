using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.Models.Transaccion
{
    public class DetalleVenta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public int idDulce { get; set; }
        public int cantidadDetalleVenta { get; set; }


        public DetalleVenta() { }

        public DetalleVenta(int idDetalleVenta, int idVenta, int idDulce, int cantidadDetalleVenta)
        {
            this.idDetalleVenta = idDetalleVenta;
            this.idVenta = idVenta;
            this.idDulce = idDulce;
            this.cantidadDetalleVenta = cantidadDetalleVenta;
        }
    }
}