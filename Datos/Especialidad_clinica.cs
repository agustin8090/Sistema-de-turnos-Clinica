using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using proyecto_final.Entidad;

namespace proyecto_final.Datos
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
    }
}