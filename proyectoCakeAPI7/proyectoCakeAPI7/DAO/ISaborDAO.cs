using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    public interface ISaborDAO
    {
        List<Sabor> GetAll();
        Sabor GetById(int id);
        void Insert(Sabor sabor);
        void Update(int id, Sabor sabor);
        void Delete(int id);
    }
}
