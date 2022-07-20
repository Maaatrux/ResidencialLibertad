using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class PisoLogica
    {
        private static PisoLogica instancia = null;
        public PisoLogica() { }

        public static PisoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PisoLogica();
                }
                return instancia;
            }
        }

        public List<Piso> Listar()
        {
            List<Piso> Lista = new List<Piso>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT piso_id,descripcion,estado FROM Piso", conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Piso()
                            {
                                Piso_id = Convert.ToInt32(dr["piso_id"]),
                                Descripcion = dr["descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Piso>();
                }
            }
            return Lista;
        }

        public bool RegistrarPiso(Piso piso)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarPiso", conexion);
                    cmd.Parameters.AddWithValue("descripcion", piso.Descripcion);
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

        public bool ActualizarPiso(Piso piso)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizarPiso", conexion);
                    cmd.Parameters.AddWithValue("piso_id", piso.Piso_id);
                    cmd.Parameters.AddWithValue("descripcion", piso.Descripcion);
                    cmd.Parameters.AddWithValue("estado", piso.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

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

        public bool EliminarPiso(int id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Piso WHERE piso_id = @id", conexion);
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