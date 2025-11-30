using proyecto_final.Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyecto_final.Datos
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
    }
}