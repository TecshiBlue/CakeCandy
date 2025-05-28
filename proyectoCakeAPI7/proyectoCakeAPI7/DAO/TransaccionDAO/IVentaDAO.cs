using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO.Transaccion
{
    internal interface IVentaDAO
    {
        List<Venta> GetAll();
        Venta GetById(int id);
        void Insert(Venta venta);
        void Update(int id, Venta venta);
        void Delete(int id);
    }
}
