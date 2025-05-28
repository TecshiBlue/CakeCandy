using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoCakeAPI7.Models;

namespace proyectoCakeAPI7.DAO
{
    internal interface IDulceDAO
    {
        List<Dulce> GetAll();
        Dulce GetById(int id);
        void Insert(Dulce d);
        void Update(int id, Dulce d);
        void Delete(int id);

    }
}
