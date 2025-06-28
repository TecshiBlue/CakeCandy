using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    public interface IMarcaDAO
    {
        List<Marca> GetAll();
        Marca GetById(int id);
        void Insert(Marca marca);
        void Update(int id, Marca marca);
        void Delete(int id);
    }
}
