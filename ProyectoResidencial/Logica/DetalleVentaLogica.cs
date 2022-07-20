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
    public class DetalleVentaLogica
    {
        private static DetalleVentaLogica instancia = null;

        public DetalleVentaLogica() { }

        public static DetalleVentaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DetalleVentaLogica();
                }

                return instancia;
            }
        }

        public List<Detalle_venta> Listar()
        {
            List<Detalle_venta> Lista = new List<Detalle_venta>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT dv.detalle_venta_id,dv.venta_id,p.producto_id,p.nombre,p.precio,dv.cantidad,dv.subTotal FROM Detalle_venta dv");
                    query.AppendLine("INNER JOIN Producto p on p.producto_id = dv.producto_id");


                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Detalle_venta()
                            {
                                Detalle_venta_id = Convert.ToInt32(dr["detalle_venta_id"]),
                                Venta_id = Convert.ToInt32(dr["venta_id"]),
                                _producto = new Producto()
                                {
                                    Producto_id = Convert.ToInt32(dr["producto_id"]),
                                    Nombre = dr["nombre"].ToString(),
                                    Precio = Convert.ToInt32(dr["precio"].ToString()),
                                },
                                Cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                SubTotal = Convert.ToInt32(dr["subTotal"].ToString())
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Detalle_venta>();
                }
            }
            return Lista;
        }
    }
}