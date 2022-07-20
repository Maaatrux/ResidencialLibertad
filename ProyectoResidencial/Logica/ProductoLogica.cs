using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class ProductoLogica
    {
        private static ProductoLogica instancia = null;

        public ProductoLogica() { }

        public static ProductoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ProductoLogica();
                }

                return instancia;
            }
        }

        public List<Producto> Listar()
        {
            List<Producto> Lista = new List<Producto>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT producto_id,nombre,detalle,precio,cantidad,estado FROM Producto", conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Producto()
                            {
                                Producto_id = Convert.ToInt32(dr["producto_id"]),
                                Nombre = dr["nombre"].ToString(),
                                Detalle = dr["detalle"].ToString(),
                                Precio = Convert.ToInt32(dr["precio"].ToString()),
                                Cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Producto>();
                }
            }
            return Lista;
        }

        public bool RegistrarProducto(Producto producto)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarProducto", conexion);
                    cmd.Parameters.AddWithValue("nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("detalle", producto.Detalle);
                    cmd.Parameters.AddWithValue("precio", Convert.ToInt32(producto.PrecioS));
                    cmd.Parameters.AddWithValue("cantidad", producto.Cantidad);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizarProducto(Producto producto)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizarProducto", conexion);
                    cmd.Parameters.AddWithValue("producto_id", producto.Producto_id);
                    cmd.Parameters.AddWithValue("nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("detalle", producto.Detalle);
                    cmd.Parameters.AddWithValue("precio", Convert.ToInt32(producto.PrecioS));
                    cmd.Parameters.AddWithValue("cantidad", producto.Cantidad);
                    cmd.Parameters.AddWithValue("estado", producto.Estado);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Producto WHERE producto_id = @id", conexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}