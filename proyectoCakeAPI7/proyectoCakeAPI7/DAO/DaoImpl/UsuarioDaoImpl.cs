using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace proyectoCakeAPI7.DAO.DaoImpl
{
    public class UsuarioDaoImpl : IUsuarioDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        // Métodos de autenticación
         public Usuario ValidarCredenciales(string nombre, string password)
        {
            string hash = ComputeSha256Hash(password);

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "VALIDAR_CREDENCIALES");
                cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", hash);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            usuarioID = (int)reader["usuarioID"],
                            nombre = reader["nombre"].ToString(),
                            idRol = (int)reader["idRol"]
                        };
                    }
                }
            }
            return null;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            // Asegurarse de que la contraseña esté hasheada
            if (!IsSha256Hash(usuario.passwordHash))
            {
                usuario.passwordHash = ComputeSha256Hash(usuario.passwordHash);
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@USUARIO_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", usuario.nombre);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", usuario.passwordHash);
                cmd.Parameters.AddWithValue("@ID_ROL", usuario.idRol);
                

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Métodos CRUD
        public List<Usuario> GetAll()
        {
            var lista = new List<Usuario>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@USUARIO_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_ROL", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            usuarioID = (int)reader["usuarioID"],
                            nombre = reader["nombre"].ToString(),
                            passwordHash = reader["passwordHash"].ToString(),
                            idRol = (int)reader["idRol"]
                        });
                    }
                }
            }
            return lista;
        }

        public Usuario GetById(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@USUARIO_ID", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_ROL", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            usuarioID = (int)reader["usuarioID"],
                            nombre = reader["nombre"].ToString(),
                            passwordHash = reader["passwordHash"].ToString(),
                            idRol = (int)reader["idRol"]
                        };
                    }
                }
            }
            return null;
        }

       

        public void Update(int id, Usuario usuario)
        {
            // Asegurarse de que la contraseña esté hasheada
            if (!IsSha256Hash(usuario.passwordHash))
            {
                usuario.passwordHash = ComputeSha256Hash(usuario.passwordHash);
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@USUARIO_ID", id);
                cmd.Parameters.AddWithValue("@NOMBRE", usuario.nombre);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", usuario.passwordHash);
                cmd.Parameters.AddWithValue("@ID_ROL", usuario.idRol);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_USUARIO_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@USUARIO_ID", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PASSWORD_HASH", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_ROL", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Métodos auxiliares
         public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

         public bool IsSha256Hash(string input)
        {
            // Un hash SHA-256 siempre tiene 64 caracteres hexadecimales
            return input != null && input.Length == 64 &&
                   System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-fA-F0-9]+$");
        }

    }
}