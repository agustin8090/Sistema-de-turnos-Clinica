using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using proyecto_final.Entidad;

namespace proyecto_final.Datos
{
    public class Provincia_clinica
    {
        public List<Provincia> Listar()
        {
            List<Provincia> lista = new List<Provincia>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SELECT IdProvincia, Nombre FROM Provincia", conexion);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Provincia aux = new Provincia();
                    aux.IdProvincia = (int)lector["IdProvincia"];
                    aux.Nombre = lector["Nombre"].ToString();

                    lista.Add(aux);
                }
            }

            return lista;
        }
    }
}