using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Logica
{
    public class CategoriaLogica
    {
        private static CategoriaLogica instancia = null;

        public CategoriaLogica() { }

        public static CategoriaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CategoriaLogica();
                }

                return instancia;
            }
        }

        public List<Categoria> Listar()
        {
            List<Categoria> Lista = new List<Categoria>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT categoria_id,descripcion,estado FROM Categoria", conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Categoria()
                            {
                                Categoria_id = Convert.ToInt32(dr["categoria_id"]),
                                Descripcion = dr["descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Categoria>();
                }
            }
            return Lista;
        }

        public bool RegistrarCategoria(Categoria categoria)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarCategoria", conexion);
                    cmd.Parameters.AddWithValue("descripcion", categoria.Descripcion);
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

        public bool ActualizarCategoria(Categoria categoria)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizarCategoria", conexion);
                    cmd.Parameters.AddWithValue("categoria_id", categoria.Categoria_id);
                    cmd.Parameters.AddWithValue("descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("estado", categoria.Estado);
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

        public bool EliminarCategoria(int id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Categoria WHERE categoria_id = @id", conexion);
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