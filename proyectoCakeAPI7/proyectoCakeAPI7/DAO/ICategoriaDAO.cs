using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    internal interface ICategoriaDAO 
    { 
        List<Categoria> GetAll();
        Categoria GetById(int id);
        void Insert(Categoria c);
        void Update(int id, Categoria c);
        void Delete(int id);
    }

}