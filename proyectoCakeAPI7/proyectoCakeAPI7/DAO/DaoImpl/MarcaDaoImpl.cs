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
    public class MarcaDaoImpl : IMarcaDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Marca> GetAll()
        {
            var lista = new List<Marca>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_MARCA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PAIS_ORIGEN", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Marca
                        {
                            idMarca = (int)reader["idMarca"],
                            nombreMarca = reader["nombreMarca"].ToString(),
                            paisOrigenMarca = reader["paisOrigenMarca"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Marca GetById(int id)
        {
            Marca marca = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_MARCA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_MARCA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PAIS_ORIGEN", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        marca = new Marca
                        {
                            idMarca = (int)reader["idMarca"],
                            nombreMarca = reader["nombreMarca"].ToString(),
                            paisOrigenMarca = reader["paisOrigenMarca"].ToString()
                        };
                    }
                }
            }
            return marca;
        }

        public void Insert(Marca m)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_MARCA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE", m.nombreMarca);
                cmd.Parameters.AddWithValue("@PAIS_ORIGEN", m.paisOrigenMarca);
                cmd.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Marca m)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_MARCA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_MARCA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", m.nombreMarca);
                cmd.Parameters.AddWithValue("@PAIS_ORIGEN", m.paisOrigenMarca);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_MARCA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_MARCA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PAIS_ORIGEN", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}