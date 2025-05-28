using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.DAO.DaoImpl
{
    public class SaborDaoImpl : ISaborDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Sabor> GetAll()
        {
            var lista = new List<Sabor>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_SABOR_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_SABOR", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Sabor
                        {
                            idSabor = (int)reader["idSabor"],
                            nombreSabor = reader["nombreSabor"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Sabor GetById(int id)
        {
            Sabor sabor = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_SABOR_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_SABOR", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sabor = new Sabor
                        {
                            idSabor = (int)reader["idSabor"],
                            nombreSabor = reader["nombreSabor"].ToString()
                        };
                    }
                }
            }
            return sabor;
        }

        public void Insert(Sabor s)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_SABOR_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE", s.nombreSabor);
                cmd.Parameters.AddWithValue("@ID_SABOR", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Sabor s)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_SABOR_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_SABOR", id);
                cmd.Parameters.AddWithValue("@NOMBRE", s.nombreSabor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_SABOR_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_SABOR", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}