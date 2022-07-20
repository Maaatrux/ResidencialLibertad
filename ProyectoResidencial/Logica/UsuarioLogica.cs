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
    public class UsuarioLogica
    {
        private static UsuarioLogica _instancia = null;

        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new UsuarioLogica();
                }

                return _instancia;
            }
        }

        public List<Usuario> EncontrarUsuario()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    string query = "SELECT usuario_id, tipo_documento, rut, nombre, apellido, correo, clave, tipo_usu_id FROM Usuario";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    Usuario_id = Convert.ToInt32(dr["usuario_id"]),
                                    Tipo_documento = dr["tipo_documento"].ToString(),
                                    Rut = dr["rut"].ToString(),
                                    Nombre = dr["nombre"].ToString(),
                                    Apellido = dr["apellido"].ToString(),
                                    Correo = dr["correo"].ToString(),
                                    Clave = dr["clave"].ToString(),
                                    Tipo_Usuario = (Tipo_usuario)dr["tipo_usu_id"]
                                }

                                );
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }

        public int RegistrarCliente(Usuario usuario)
        {
            int registrado = 0;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarCliente", conexion);
                    cmd.Parameters.AddWithValue("tipo_documento", usuario.Tipo_documento);
                    cmd.Parameters.AddWithValue("rut", usuario.Rut);
                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("clave", usuario.Clave);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                }
                catch (Exception ex)
                {
                    registrado = 0;
                }
            }
            return registrado;
        }

        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.usuario_id,u.tipo_documento,u.rut,u.nombre,u.apellido,u.correo,u.clave,tp.tipo_usu_id,tp.descripcion, u.estado FROM Usuario u");
                    sb.AppendLine("INNER JOIN Tipo_usuario tp on tp.tipo_usu_id = u.tipo_usu_id");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                Usuario_id = Convert.ToInt32(dr["usuario_id"]),
                                Tipo_documento = dr["tipo_documento"].ToString(),
                                Rut = dr["rut"].ToString(),
                                Nombre = dr["nombre"].ToString(),
                                Apellido = dr["apellido"].ToString(),
                                Correo = dr["correo"].ToString(),
                                Clave = dr["clave"].ToString(),
                                TipoUsuario = new TipoUsuario() { Tipo_usu_id = Convert.ToInt32(dr["tipo_usu_id"]), Descripcion = dr["descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                }
            }
            return Lista;
        }

        public List<TipoUsuario> ListarTipoUsuario()
        {
            List<TipoUsuario> Lista = new List<TipoUsuario>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT tipo_usu_id,descripcion FROM Tipo_usuario", conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new TipoUsuario()
                            {
                                Tipo_usu_id = Convert.ToInt32(dr["tipo_usu_id"]),
                                Descripcion = dr["descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<TipoUsuario>();
                }
            }
            return Lista;
        }


        public bool Registrar(Usuario usuario)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("registrarUsuario", conexion);
                    cmd.Parameters.AddWithValue("tipo_documento", usuario.Tipo_documento);
                    cmd.Parameters.AddWithValue("rut", usuario.Rut);
                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("tipo_usu_id", usuario.TipoUsuario.Tipo_usu_id);
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

        public bool ActualizarUsuario(Usuario usuario)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizarUsuario", conexion);
                    cmd.Parameters.AddWithValue("usuario_id", usuario.Usuario_id);
                    cmd.Parameters.AddWithValue("tipo_documento", usuario.Tipo_documento);
                    cmd.Parameters.AddWithValue("rut", usuario.Rut);
                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("tipo_usu_id", usuario.TipoUsuario.Tipo_usu_id);
                    cmd.Parameters.AddWithValue("estado", usuario.Estado);
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

        public bool EliminarUsuario(int id)
        {
            bool respuesta = true;
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE usuario_id = @id", conexion);
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

        public bool CambiarClave(int idcliente, string nuevaclave)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Usuario set clave = @nuevaclave WHERE usuario_id = @id", conexion);
                    cmd.Parameters.AddWithValue("@id", idcliente);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }
    }
}

