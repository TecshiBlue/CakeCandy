using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    internal interface IPresentacionDAO
    {
        List<Presentacion> GetAll();
        Presentacion GetById(int id);
        void Insert(Presentacion presentacion);
        void Update(int id, Presentacion presentacion);
        void Delete(int id);
    }
}
