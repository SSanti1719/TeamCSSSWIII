using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.Data
{
    public class UsuarioData
    {
        
        public static bool registrar(Usuario oUsuario)
        {
            Conexion conexion = new Conexion();
            using (SqlConnection oConexion = new SqlConnection(conexion.datosConexion()))
            {
                SqlCommand cmd = new SqlCommand("usp_registrar", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@documentoidentidad", oUsuario.DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@nombres", oUsuario.Nombres);
                cmd.Parameters.AddWithValue("@telefono", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("@correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("@ciudad", oUsuario.Ciudad);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool modificar(Usuario oUsuario)
        {
            Conexion conexion = new Conexion();
            using(SqlConnection oConexion = new SqlConnection(conexion.datosConexion()))
            {
                SqlCommand cmd = new SqlCommand("usp_modificar", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", oUsuario.IdUsuario);
                cmd.Parameters.AddWithValue("@documentoidentidad", oUsuario.DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@nombres", oUsuario.Nombres);
                cmd.Parameters.AddWithValue("@telefono", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("@correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("@ciudad", oUsuario.Ciudad);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<Usuario> listar()
        {
            List<Usuario> oListUsuario = new List<Usuario>();
            Conexion conexion = new Conexion();
            using (SqlConnection oConexion = new SqlConnection(conexion.datosConexion()))
            {
                SqlCommand cmd = new SqlCommand("usp_listar", oConexion);
                //cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oListUsuario.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                Nombres = reader["Nombres"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Ciudad = reader["Ciudad"].ToString(),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString())
                            });

                        }
                        return oListUsuario;
                    }

                }
                catch (Exception ex)
                {
                    return oListUsuario;
                }
            }
        }

        public static Usuario obtener(int idUsuario)
        {
            Usuario oUsuario = new Usuario();
            Conexion conexion = new Conexion();

            using (SqlConnection oConexion = new SqlConnection(conexion.datosConexion()))
            {
                SqlCommand cmd = new SqlCommand("usp_obtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", idUsuario);

                try
                {
                    oConexion.Open();
                  //cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oUsuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                Nombres = reader["Nombres"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Ciudad = reader["Ciudad"].ToString(),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString())
                            };
                        }
                    }
                    return oUsuario;
                }
                catch (Exception ex)
                {
                    return oUsuario;
                }
            }
        }

        public static bool eliminar(int idUsuario)
        {
            Conexion conexion = new Conexion();

            using (SqlConnection oConexion = new SqlConnection(conexion.datosConexion()))
            {
                SqlCommand cmd = new SqlCommand("usp_eliminar", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", idUsuario);

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        
    }
}