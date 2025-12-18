using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Datos
{
    public class Usuario_clinica
    {
        public Usuario Login(string nombreUsuario, string contraseña)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT IdUsuario, NombreUsuario, Contraseña, TipoUsuario
                    FROM Usuario
                    WHERE NombreUsuario = @user AND Contraseña = @pass";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", nombreUsuario);
                cmd.Parameters.AddWithValue("@pass", contraseña);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (int)dr["IdUsuario"],
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        Contraseña = dr["Contraseña"].ToString(),
                        TipoUsuario = dr["TipoUsuario"].ToString()
                    };
                }
            }

            return null; 
        }
        public bool validar_usuario(string nombreUsuario)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE NombreUsuario = @usuario";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public int CrearUsuario(string usuario, string contraseña, string tipo)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                INSERT INTO Usuario (NombreUsuario, Contraseña, TipoUsuario)
                OUTPUT INSERTED.IdUsuario
                VALUES (@usuario, @contraseña, @tipo)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);
                cmd.Parameters.AddWithValue("@tipo", tipo);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}