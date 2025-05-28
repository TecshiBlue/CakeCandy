using proyectoCakeAPI7.Models.Transaccion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl
{
    public class ClienteDaoImpl : IClienteDAO
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cnx_cake"].ConnectionString;

        public List<Cliente> GetAll()
        {
            var lista = new List<Cliente>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CLIENTE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTAR TODO");
                cmd.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO", DBNull.Value);
                cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                cmd.Parameters.AddWithValue("@TELEFONO", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cliente
                        {
                            idCliente = (int)reader["idCliente"],
                            nombreCliente = reader["nombreCliente"].ToString(),
                            apellidoCliente = reader["apellidoCliente"].ToString(),
                            emailCliente = reader["emailCliente"].ToString(),
                            telefonoCliente = reader["telefonoCliente"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Cliente GetById(int id)
        {
            Cliente cliente = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CLIENTE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "CONSULTARXID");
                cmd.Parameters.AddWithValue("@ID_CLIENTE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO", DBNull.Value);
                cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                cmd.Parameters.AddWithValue("@TELEFONO", DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            idCliente = (int)reader["idCliente"],
                            nombreCliente = reader["nombreCliente"].ToString(),
                            apellidoCliente = reader["apellidoCliente"].ToString(),
                            emailCliente = reader["emailCliente"].ToString(),
                            telefonoCliente = reader["telefonoCliente"].ToString()
                        };
                    }
                }
            }
            return cliente;
        }

        public void Insert(Cliente cliente)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CLIENTE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "INSERTAR");
                cmd.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.nombreCliente);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.apellidoCliente);
                cmd.Parameters.AddWithValue("@EMAIL", cliente.emailCliente);
                cmd.Parameters.AddWithValue("@TELEFONO", cliente.telefonoCliente);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Cliente cliente)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CLIENTE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@ID_CLIENTE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.nombreCliente);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.apellidoCliente);
                cmd.Parameters.AddWithValue("@EMAIL", cliente.emailCliente);
                cmd.Parameters.AddWithValue("@TELEFONO", cliente.telefonoCliente);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("USP_CLIENTE_CRUD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INDICADOR", "ELIMINAR");
                cmd.Parameters.AddWithValue("@ID_CLIENTE", id);
                cmd.Parameters.AddWithValue("@NOMBRE", DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO", DBNull.Value);
                cmd.Parameters.AddWithValue("@EMAIL", DBNull.Value);
                cmd.Parameters.AddWithValue("@TELEFONO", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}