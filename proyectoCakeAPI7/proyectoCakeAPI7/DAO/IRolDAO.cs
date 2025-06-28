using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    public interface IRolDAO
    {
        List<Rol> GetAll();
        Rol GetById(int id);
        void Insert(Rol rol);
        void Update(int id, Rol rol);
        void Delete(int id);
    }
}
