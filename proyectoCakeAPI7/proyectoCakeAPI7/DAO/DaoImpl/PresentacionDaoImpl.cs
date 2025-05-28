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
    public class PresentacionDaoImpl : IPresentacionDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Presentacion> GetAll()
        {
            var lista = new List<Presentacion>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_PRESENTACION_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Presentacion
                        {
                            idPresentacion = (int)reader["idPresentacion"],
                            nombrePresentacion = reader["nombrePresentacion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Presentacion GetById(int id)
        {
            Presentacion presentacion = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_PRESENTACION_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        presentacion = new Presentacion
                        {
                            idPresentacion = (int)reader["idPresentacion"],
                            nombrePresentacion = reader["nombrePresentacion"].ToString()
                        };
                    }
                }
            }
            return presentacion;
        }

        public void Insert(Presentacion p)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_PRESENTACION_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE", p.nombrePresentacion);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Presentacion p)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_PRESENTACION_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", id);
                cmd.Parameters.AddWithValue("@NOMBRE", p.nombrePresentacion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_PRESENTACION_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}