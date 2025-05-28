using proyectoCakeAPI7.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace proyectoCakeAPI7.DAO.DaoImpl
{
    public class CategoriaDaoImpl : ICategoriaDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Categoria> GetAll()
        {
            var lista = new List<Categoria>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CATEGORIA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Categoria
                        {
                            idCategoria = (int)reader["idCategoria"],
                            nombreCategoria = reader["nombreCategoria"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Categoria GetById(int id)
        {
            Categoria cat = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CATEGORIA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cat = new Categoria
                        {
                            idCategoria = (int)reader["idCategoria"],
                            nombreCategoria = reader["nombreCategoria"].ToString()
                        };
                    }
                }
            }
            return cat;
        }

        public void Insert(Categoria c)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CATEGORIA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE", c.nombreCategoria);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Categoria c)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CATEGORIA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", c.nombreCategoria);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CATEGORIA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}