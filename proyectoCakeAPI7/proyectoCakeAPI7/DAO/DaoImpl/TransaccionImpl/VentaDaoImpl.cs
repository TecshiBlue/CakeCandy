using proyectoCakeAPI7.DAO.Transaccion;
using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl
{
    public class VentaDaoImpl : IVentaDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Venta> GetAll()
        {
            var lista = new List<Venta>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_USUARIO", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Venta
                        {
                            idVenta = (int)reader["idVenta"],
                            fechaVenta = (DateTime)reader["fechaVenta"],
                            idCliente = (int)reader["idCliente"],
                            usuarioID = (int)reader["usuarioID"]
                        });
                    }
                }
            }
            return lista;
        }

        public Venta GetById(int id)
        {
            Venta venta = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_USUARIO", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        venta = new Venta
                        {
                            idVenta = (int)reader["idVenta"],
                            fechaVenta = (DateTime)reader["fechaVenta"],
                            idCliente = (int)reader["idCliente"],
                            usuarioID = (int)reader["usuarioID"]
                        };
                    }
                }
            }
            return venta;
        }

        public void Insert(Venta venta)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@ID_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", venta.idCliente);
                cmd.Parameters.AddWithValue("@ID_USUARIO", venta.usuarioID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Venta venta)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", venta.idCliente);
                cmd.Parameters.AddWithValue("@ID_USUARIO", venta.usuarioID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_USUARIO", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}