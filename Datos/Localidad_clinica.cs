using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using proyecto_final.Entidad;

namespace proyecto_final.Datos
{
    public class Localidad_clinica
    {
        public List<Localidad> ListarPorProvincia(int idProvincia)
        {
            List<Localidad> lista = new List<Localidad>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT IdLocalidad, Nombre FROM Localidad WHERE IdProvincia = @idProv",
                    conexion
                );

                comando.Parameters.AddWithValue("@idProv", idProvincia);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Localidad aux = new Localidad();
                    aux.IdLocalidad = (int)lector["IdLocalidad"];
                    aux.Nombre = lector["Nombre"].ToString();

                    lista.Add(aux);
                }
            }

            return lista;
        }
    }
}