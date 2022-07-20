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
    public class RecepcionLogica
    {
        private static RecepcionLogica instancia = null;

        public RecepcionLogica() { }

        public static RecepcionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RecepcionLogica();
                }

                return instancia;
            }
        }

        public List<Recepcion> Listar()
        {
            List<Recepcion> Lista = new List<Recepcion>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT r.recepcion_id,u.nombre,u.apellido,u.rut,u.correo,h.habitacion_id,h.numero,h.detalle,ps.descripcion[DesPiso],c.descripcion[DesCategoria],");
                    query.AppendLine("convert(char(10), r.fecha_entrada, 103)[fecha_entrada], convert(char(10), r.fecha_salida, 103)[fecha_salida],r.precio_inicial,r.abono,r.precio_restante,r.observacion,r.estado");
                    query.AppendLine("FROM Recepcion r");
                    query.AppendLine("INNER JOIN Usuario u on u.usuario_id = r.usuario_id");
                    query.AppendLine("INNER JOIN HABITACION h on h.habitacion_id = r.habitacion_id");
                    query.AppendLine("INNER JOIN Piso ps on ps.piso_id = h.piso_id");
                    query.AppendLine("INNER JOIN Categoria c on c.categoria_id = h.categoria_id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Recepcion()
                            {
                                Recepcion_id = Convert.ToInt32(dr["recepcion_id"]),
                                Usuario = new Usuario()
                                {
                                    Nombre = dr["nombre"].ToString(),
                                    Apellido = dr["apellido"].ToString(),
                                    Rut = dr["rut"].ToString(),
                                    Correo = dr["correo"].ToString(),
                                },
                                Habitacion = new Habitacion()
                                {
                                    Habitacion_id = Convert.ToInt32(dr["habitacion_id"]),
                                    Numero = dr["numero"].ToString(),
                                    Detalle = dr["detalle"].ToString(),
                                    _piso = new Piso() { Descripcion = dr["DesPiso"].ToString() },
                                    _categoria = new Categoria() { Descripcion = dr["DesCategoria"].ToString() }
                                },
                                Fecha_entradaS = dr["fecha_entrada"].ToString(),
                                Fecha_salidaS = dr["fecha_salida"].ToString(),
                                Precio_inicial = Convert.ToInt32(dr["precio_inicial"].ToString()),
                                Abono = Convert.ToInt32(dr["abono"].ToString()),
                                Precio_restante = Convert.ToInt32(dr["precio_restante"].ToString()),
                                Observacion = dr["observacion"].ToString(),
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Recepcion>();
                }
            }
            return Lista;
        }

        public bool Registrar(Recepcion recepcion)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarRecepcion", conexion);
                    cmd.Parameters.AddWithValue("usuario_id", recepcion.Usuario.Usuario_id);
                    cmd.Parameters.AddWithValue("tipo_documento", recepcion.Usuario.Tipo_documento);
                    cmd.Parameters.AddWithValue("rut", recepcion.Usuario.Rut);
                    cmd.Parameters.AddWithValue("nombre", recepcion.Usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", recepcion.Usuario.Apellido);
                    cmd.Parameters.AddWithValue("correo", recepcion.Usuario.Correo);
                    cmd.Parameters.AddWithValue("habitacion_id", recepcion.Habitacion.Habitacion_id);
                    //cmd.Parameters.AddWithValue("fecha_salida", Convert.ToDateTime(recepcion.Fecha_salidaS));
                    cmd.Parameters.AddWithValue("fecha_salida", DateTime.Parse(recepcion.Fecha_salidaS, CultureInfo.CreateSpecificCulture("es-CL")));
                    cmd.Parameters.AddWithValue("precio_inicial", Convert.ToInt32(recepcion.Precio_inicialS));
                    cmd.Parameters.AddWithValue("abono", Convert.ToDecimal(recepcion.AbonoS));
                    cmd.Parameters.AddWithValue("precio_restante", Convert.ToInt32(recepcion.Precio_restanteS));
                    cmd.Parameters.AddWithValue("observacion", recepcion.Observacion);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Cerrar(Recepcion recepcion)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                conexion.Open();
                SqlTransaction transaction = conexion.BeginTransaction();

                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("UPDATE Recepcion SET fecha_confirmacion = GETDATE() , total_pagado = @totalpagado , multa = @multa, estado = 0");
                    query.AppendLine("WHERE recepcion_id = @recepcion_id");
                    query.AppendLine("");
                    query.AppendLine("UPDATE Habitacion SET estado_habi_id = 3");
                    query.AppendLine("WHERE habitacion_id = @habitacion_id");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@totalpagado", Convert.ToInt32(recepcion.Total_pagadoS));
                    cmd.Parameters.AddWithValue("@multa", Convert.ToInt32(recepcion.MultaS));
                    cmd.Parameters.AddWithValue("@recepcion_id", recepcion.Recepcion_id);
                    cmd.Parameters.AddWithValue("@habitacion_id", recepcion.Habitacion.Habitacion_id);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}