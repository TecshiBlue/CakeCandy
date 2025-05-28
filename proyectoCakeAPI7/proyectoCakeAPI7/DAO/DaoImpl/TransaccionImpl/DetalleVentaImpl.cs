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
    public class DetalleVentaDaoImpl : IDetalleVentaDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<DetalleVenta> GetAll()
        {
            var lista = new List<DetalleVenta>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@CANTIDAD", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new DetalleVenta
                        {
                            idDetalleVenta = (int)reader["idDetalleVenta"],
                            idVenta = (int)reader["idVenta"],
                            idDulce = (int)reader["idDulce"],
                            cantidadDetalleVenta = (int)reader["cantidadDetalleVenta"]
                        });
                    }
                }
            }
            return lista;
        }

        public DetalleVenta GetById(int id)
        {
            DetalleVenta detalle = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@CANTIDAD", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        detalle = new DetalleVenta
                        {
                            idDetalleVenta = (int)reader["idDetalleVenta"],
                            idVenta = (int)reader["idVenta"],
                            idDulce = (int)reader["idDulce"],
                            cantidadDetalleVenta = (int)reader["cantidadDetalleVenta"]
                        };
                    }
                }
            }
            return detalle;
        }

        public void Insert(DetalleVenta detalle)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_VENTA", detalle.idVenta);
                cmd.Parameters.AddWithValue("@ID_DULCE", detalle.idDulce);
                cmd.Parameters.AddWithValue("@CANTIDAD", detalle.cantidadDetalleVenta);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, DetalleVenta detalle)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_VENTA", detalle.idVenta);
                cmd.Parameters.AddWithValue("@ID_DULCE", detalle.idDulce);
                cmd.Parameters.AddWithValue("@CANTIDAD", detalle.cantidadDetalleVenta);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", id);
                cmd.Parameters.AddWithValue("@ID_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@CANTIDAD", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<DetalleVenta> GetByVentaId(int ventaId)
        {
            var lista = new List<DetalleVenta>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_DETALLE_VENTA_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXVENTA");
                cmd.Parameters.AddWithValue("@ID_DETALLE_VENTA", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_VENTA", ventaId);
                cmd.Parameters.AddWithValue("@ID_DULCE", DBNull.Value);
                cmd.Parameters.AddWithValue("@CANTIDAD", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new DetalleVenta
                        {
                            idDetalleVenta = (int)reader["idDetalleVenta"],
                            idVenta = (int)reader["idVenta"],
                            idDulce = (int)reader["idDulce"],
                            cantidadDetalleVenta = (int)reader["cantidadDetalleVenta"]
                        });
                    }
                }
            }
            return lista;
        }
    }
}