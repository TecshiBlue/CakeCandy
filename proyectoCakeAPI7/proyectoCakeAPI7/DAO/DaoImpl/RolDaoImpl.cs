using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.DAO.DaoImpl
{
    public class RolDaoImpl : IRolDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Rol> GetAll()
        {
            var lista = new List<Rol>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_ROL_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_ROL", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE_ROL", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Rol
                        {
                            idRol = (int)reader["idRol"],
                            nombreRol = reader["nombreRol"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Rol GetById(int id)
        {
            Rol rol = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_ROL_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_ROL", id);
                cmd.Parameters.AddWithValue("@NOMBRE_ROL", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rol = new Rol
                        {
                            idRol = (int)reader["idRol"],
                            nombreRol = reader["nombreRol"].ToString()
                        };
                    }
                }
            }
            return rol;
        }

        public void Insert(Rol r)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_ROL_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE_ROL", r.nombreRol);
                cmd.Parameters.AddWithValue("@ID_ROL", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Rol r)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_ROL_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_ROL", id);
                cmd.Parameters.AddWithValue("@NOMBRE_ROL", r.nombreRol);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_ROL_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_ROL", id);
                cmd.Parameters.AddWithValue("@NOMBRE_ROL", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}