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
    public class DulceDaoImpl : IDulceDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Dulce> GetAll()
        {
            var lista = new List<Dulce>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DULCE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PRECIO", DBNull.Value);
                cmd.Parameters.AddWithValue("@STOCK", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_SABOR", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Dulce
                        {
                            idDulce = (int)reader["idDulce"],
                            nombreDulce = reader["nombreDulce"].ToString(),
                            precioDulce = (decimal)reader["precioDulce"],
                            stockDulce = (int)reader["stockDulce"],
                            idSabor = (int)reader["idSabor"],
                            idCategoria = (int)reader["idCategoria"],
                            idMarca = (int)reader["idMarca"],
                            idPresentacion = (int)reader["idPresentacion"]
                        });
                    }
                }
            }
            return lista;
        }

        public Dulce GetById(int id)
        {
            Dulce d = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DULCE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_DULCE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PRECIO", DBNull.Value);
                cmd.Parameters.AddWithValue("@STOCK", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_SABOR", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        d = new Dulce
                        {
                            idDulce = (int)reader["idDulce"],
                            nombreDulce = reader["nombreDulce"].ToString(),
                            precioDulce = (decimal)reader["precioDulce"],
                            stockDulce = (int)reader["stockDulce"],
                            idSabor = (int)reader["idSabor"],
                            idCategoria = (int)reader["idCategoria"],
                            idMarca = (int)reader["idMarca"],
                            idPresentacion = (int)reader["idPresentacion"]
                        };
                    }
                }
            }
            return d;
        }

        public void Insert(Dulce d)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DULCE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@NOMBRE", d.nombreDulce);
                cmd.Parameters.AddWithValue("@PRECIO", d.precioDulce);
                cmd.Parameters.AddWithValue("@STOCK", d.stockDulce);
                cmd.Parameters.AddWithValue("@ID_SABOR", d.idSabor);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", d.idCategoria);
                cmd.Parameters.AddWithValue("@ID_MARCA", d.idMarca);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", d.idPresentacion);
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Dulce d)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DULCE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_DULCE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", d.nombreDulce);
                cmd.Parameters.AddWithValue("@PRECIO", d.precioDulce);
                cmd.Parameters.AddWithValue("@STOCK", d.stockDulce);
                cmd.Parameters.AddWithValue("@ID_SABOR", d.idSabor);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", d.idCategoria);
                cmd.Parameters.AddWithValue("@ID_MARCA", d.idMarca);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", d.idPresentacion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DULCE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_DULCE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@PRECIO", DBNull.Value);
                cmd.Parameters.AddWithValue("@STOCK", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_SABOR", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_PRESENTACION", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}