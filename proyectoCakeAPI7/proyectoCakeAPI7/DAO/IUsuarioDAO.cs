using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCakeAPI7.DAO
{
    internal interface IUsuarioDAO
    {
        Usuario ValidarCredenciales(string nombre, string password);
        void RegistrarUsuario(Usuario usuario);

        List<Usuario> GetAll();
        Usuario GetById(int id);
        void Update(int id, Usuario usuario);
        void Delete(int id);

        //metodos auxiliares
        string ComputeSha256Hash(string rawData);
        bool IsSha256Hash(string input);
    }
}
