using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidad;

namespace Datos
{
    public class Especialidad_clinica
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> lista = new List<Especialidad>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SELECT IdEspecialidad, Nombre FROM Especialidad", conexion);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Especialidad aux = new Especialidad();
                    aux.IdEspecialidad = (int)lector["IdEspecialidad"];
                    aux.Nombre = lector["Nombre"].ToString();

                    lista.Add(aux);
                }
            }

            return lista;
        }

        public Especialidad TraerPorId(int idEspecialidad)
        {
            Especialidad esp = null;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT IdEspecialidad, Nombre
            FROM Especialidad
            WHERE IdEspecialidad = @id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", idEspecialidad);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    esp = new Especialidad
                    {
                        IdEspecialidad = (int)dr["IdEspecialidad"],
                        Nombre = dr["Nombre"].ToString()
                    };
                }
            }
            return esp;
        }
    }
}