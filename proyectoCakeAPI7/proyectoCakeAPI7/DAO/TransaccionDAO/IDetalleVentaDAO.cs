using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO.Transaccion
{
    internal interface IDetalleVentaDAO
    {
        List<DetalleVenta> GetAll();
        DetalleVenta GetById(int id);
        List<DetalleVenta> GetByVentaId(int ventaId);
        void Insert(DetalleVenta detalle);
        void Update(int id, DetalleVenta detalle);
        void Delete(int id);
    }
}
