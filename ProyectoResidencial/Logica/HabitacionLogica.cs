using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class HabitacionLogica
    {
        private static HabitacionLogica instancia = null;

        public HabitacionLogica() { }

        public static HabitacionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new HabitacionLogica();
                }

                return instancia;
            }
        }

        public List<Habitacion> Listar()
        {
            List<Habitacion> Lista = new List<Habitacion>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT h.habitacion_id,h.numero,h.detalle,h.precio,p.piso_id,p.descripcion[descripcionPiso],c.categoria_id,c.descripcion[descripcionCategoria],h.estado,");
                    query.AppendLine("eh.estado_habi_id,eh.descripcion[descripcionEstadoHabi]");
                    query.AppendLine("FROM Habitacion h");
                    query.AppendLine("INNER JOIN Piso p on p.piso_id = h.piso_id");
                    query.AppendLine("INNER JOIN Categoria c on c.categoria_id = h.categoria_id");
                    query.AppendLine("INNER JOIN Estado_habitacion eh on eh.estado_habi_id = h.estado_habi_id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Habitacion()
                            {
                                Habitacion_id = Convert.ToInt32(dr["habitacion_id"]),
                                Numero = dr["numero"].ToString(),
                                Detalle = dr["detalle"].ToString(),
                                Precio = Convert.ToInt32(dr["precio"].ToString()),
                                _piso = new Piso() { Piso_id = Convert.ToInt32(dr["piso_id"]), Descripcion = dr["descripcionPiso"].ToString() },
                                _categoria = new Categoria() { Categoria_id = Convert.ToInt32(dr["categoria_id"]), Descripcion = dr["descripcionCategoria"].ToString() },
                                _estado_habitacion = new Estado_habitacion() { Estado_habi_id = Convert.ToInt32(dr["estado_habi_id"]), Descripcion = dr["descripcionEstadoHabi"].ToString() },
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Habitacion>();
                }
            }
            return Lista;
        }

        public bool RegistrarHabitacion(Habitacion habitacion)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarHabitacion", conexion);
                    cmd.Parameters.AddWithValue("numero", habitacion.Numero);
                    cmd.Parameters.AddWithValue("detalle", habitacion.Detalle);
                    cmd.Parameters.AddWithValue("precio", Convert.ToInt32(habitacion.PrecioS));
                    cmd.Parameters.AddWithValue("piso_id", habitacion._piso.Piso_id);
                    cmd.Parameters.AddWithValue("categoria_id", habitacion._categoria.Categoria_id);
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

        public bool ActualizarHabitacion(Habitacion habitacion)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizarHabitacion", conexion);
                    cmd.Parameters.AddWithValue("habitacion_id", habitacion.Habitacion_id);
                    cmd.Parameters.AddWithValue("numero", habitacion.Numero);
                    cmd.Parameters.AddWithValue("detalle", habitacion.Detalle);
                    cmd.Parameters.AddWithValue("precio", Convert.ToInt32(habitacion.PrecioS));
                    cmd.Parameters.AddWithValue("piso_id", habitacion._piso.Piso_id);
                    cmd.Parameters.AddWithValue("categoria_id", habitacion._categoria.Categoria_id);
                    cmd.Parameters.AddWithValue("estado", habitacion.Estado);
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

        public bool EliminarHabitacion(int id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Habitacion WHERE habitacion_id = @id", conexion);
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

        public bool ActualizarEstado(int habitacion_id, int estado_id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Habitacion SET estado_habi_id = @estado_id WHERE habitacion_id = @habitacion_id ", conexion);
                    cmd.Parameters.AddWithValue("@habitacion_id", habitacion_id);
                    cmd.Parameters.AddWithValue("@estado_id", estado_id);
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