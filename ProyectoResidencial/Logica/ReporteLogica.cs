using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class ReporteLogica
    {
        private static ReporteLogica instancia = null;

        public ReporteLogica() { }

        public static ReporteLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ReporteLogica();
                }

                return instancia;
            }
        }

        public DataTable Producto(string estado, string fechaInicio, string fechaSalida)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SET DATEFORMAT dmy;");
                    sb.AppendLine("SELECT Nombre, Detalle, Precio, Cantidad, iif(estado = 1, 'Activo', 'No Activo')[Estado],CONVERT(char(10),fecha_creacion,103)[Fecha creación] FROM Producto");
                    sb.AppendLine("WHERE Estado = iif(@estado = 2, estado, @estado) AND convert(date,fecha_creacion) BETWEEN @fecha_inicio AND @fecha_fin");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fecha_fin", fechaSalida);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                }
            }
            return dt;
        }

        public DataTable Recepciones(string idhabitacion, string fechaInicio, string fechaSalida)
        {

            DataTable dt = new DataTable();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SET DATEFORMAT dmy;");
                    sb.AppendLine("SELECT h.numero[Número habitación],h.detalle[Detalle habitación] ,c.descripcion[Categoria habitación],");
                    sb.AppendLine("(u.nombre + ' ' + u.apellido)[Cliente],u.correo[Correo cliente],");
                    sb.AppendLine("CONVERT(char(10), r.fecha_entrada, 103)[Fecha entrada],CONVERT(char(10), r.fecha_salida, 103)[Fecha salida],CONVERT(char(10), r.fecha_confirmacion, 103)[Fecha confirmación],");
                    sb.AppendLine("r.precio_inicial[Precio inicial],r.abono,r.multa[Multa],r.total_pagado[Total pagado]");
                    sb.AppendLine("FROM Recepcion r");
                    sb.AppendLine("INNER JOIN Habitacion h ON h.habitacion_id = r.habitacion_id");
                    sb.AppendLine("INNER JOIN Categoria c ON c.categoria_id = h.categoria_id");
                    sb.AppendLine("INNER JOIN Usuario u ON u.usuario_id = r.usuario_id");
                    sb.AppendLine("WHERE convert(date, r.fecha_entrada) BETWEEN @fechainicio and @fechafin");
                    sb.AppendLine("and h.habitacion_id = iif(@habitacion_id = 0, h.habitacion_id,@habitacion_id)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@habitacion_id", idhabitacion);
                    cmd.Parameters.AddWithValue("@fechainicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechafin", fechaSalida);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                }
            }
            return dt;
        }
    }
}