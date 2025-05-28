using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    internal interface IClienteDAO
    {
        List<Cliente> GetAll();
        Cliente GetById(int id);
        void Insert(Cliente cliente);
        void Update(int id, Cliente cliente);
        void Delete(int id);
    }
}
