using System;
using System.Collections.Generic;

namespace proyectoCakeAPI7.Models.Transaccion
{
    public class VentaConDetallesDTO
    {
        public int idCliente { get; set; }
        public int usuarioID { get; set; }
        public List<DetalleVentaDTO> detalles { get; set; }
    }

    public class DetalleVentaDTO
    {
        public int idDulce { get; set; }
        public int cantidadDetalleVenta { get; set; }
    }
}
