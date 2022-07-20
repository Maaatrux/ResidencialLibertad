using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class VentaLogica
    {
        private static VentaLogica instancia = null;

        public VentaLogica() { }

        public static VentaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VentaLogica();
                }

                return instancia;
            }
        }

        public List<Venta> Listar()
        {
            List<Venta> Lista = new List<Venta>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT venta_id,recepcion_id,total,estado FROM Venta", conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Venta()
                            {
                                Venta_id = Convert.ToInt32(dr["venta_id"]),
                                _recepcion = new Recepcion() { Recepcion_id = Convert.ToInt32(dr["recepcion_id"]) },
                                Total = Convert.ToInt32(dr["total"].ToString()),
                                Estado = dr["estado"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Venta>();
                }
            }
            return Lista;
        }

        public bool RegistrarVenta(Venta venta)
        {
            bool respuesta = true;

            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    conexion.Open();

                    SqlTransaction transaction = conexion.BeginTransaction();

                    sb.AppendLine("DECLARE @venta_id int = 0");
                    sb.AppendLine(string.Format("INSERT INTO Venta(recepcion_id,total,estado) values ({0},{1},'{2}')", venta._recepcion.Recepcion_id, venta.Total, venta.Estado));
                    sb.AppendLine("set @venta_id = SCOPE_IDENTITY()");
                    foreach (Detalle_venta dv in venta.detalles)
                    {
                        sb.AppendLine(string.Format("INSERT INTO Detalle_venta(venta_id,producto_id,cantidad,subTotal) values({0},{1},{2},{3})", "@venta_id", dv._producto.Producto_id, dv.Cantidad, dv.SubTotal));

                        sb.AppendLine(string.Format("UPDATE Producto SET cantidad = cantidad - {0} WHERE producto_id = {1}", dv.Cantidad, dv._producto.Producto_id));
                    }
                    sb.AppendLine("SELECT @venta_id");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = transaction;
                    try
                    {
                        int venta_id = 0;
                        int.TryParse(cmd.ExecuteScalar().ToString(), out venta_id);

                        if (venta_id != 0)
                        {
                            transaction.Commit();
                            respuesta = true;
                        }
                        else
                        {
                            transaction.Rollback();
                            respuesta = false;
                        }

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        respuesta = false;
                    }

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