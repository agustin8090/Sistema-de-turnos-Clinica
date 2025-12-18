using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class Informes
    {
        public (int cant_presentes, int cant_ausentes, int cant_total,
        double porc_presentes, double porc_ausentes)
        obtener_asistencia(int idEspecialidad = 0)
        {
            int presentes = 0, ausentes = 0, total = 0;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                    SUM(CASE WHEN t.Estado = 1 THEN 1 ELSE 0 END) AS Presentes,
                    SUM(CASE WHEN (t.Estado = 0 OR t.Estado IS NULL) THEN 1 ELSE 0 END) AS Ausentes,
                    COUNT(*) AS Total
                FROM Turno t
                INNER JOIN Medico m ON t.Legajo = m.Legajo
                WHERE (@IdEspecialidad = 0 OR m.IdEspecialidad = @IdEspecialidad)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IdEspecialidad", idEspecialidad);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    presentes = lector["Presentes"] != DBNull.Value ? Convert.ToInt32(lector["Presentes"]) : 0;
                    ausentes = lector["Ausentes"] != DBNull.Value ? Convert.ToInt32(lector["Ausentes"]) : 0;
                    total = lector["Total"] != DBNull.Value ? Convert.ToInt32(lector["Total"]) : 0;
                }
            }

            double porc_pres = 0, porc_aus = 0;

            if (total > 0)
            {
                porc_pres = (presentes * 100.0) / total;
                porc_aus = (ausentes * 100.0) / total;
            }

            return (presentes, ausentes, total, porc_pres, porc_aus);
        }


        public List<(string especialidad, int total, int presentes, int ausentes, double porcentaje)> ObtenerPromedioPorEspecialidad()
        {
            var lista = new List<(string, int, int, int, double)>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                e.Nombre AS Especialidad,
                COUNT(t.IdTurno) AS Total,
                SUM(CASE WHEN t.Estado = 1 THEN 1 ELSE 0 END) AS Presentes,
                SUM(CASE WHEN t.Estado = 0 THEN 1 ELSE 0 END) AS Ausentes,
                (CAST(SUM(CASE WHEN t.Estado = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(t.IdTurno)) * 100 AS PorcentajeAsistencia
                 FROM Turno t
                 INNER JOIN Medico m ON t.Legajo = m.Legajo
                 INNER JOIN Especialidad e ON m.IdEspecialidad = e.IdEspecialidad
                 GROUP BY e.Nombre
                 ORDER BY PorcentajeAsistencia DESC";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add((
                        dr["Especialidad"].ToString(),
                        Convert.ToInt32(dr["Total"]),
                        Convert.ToInt32(dr["Presentes"]),
                        Convert.ToInt32(dr["Ausentes"]),
                        Convert.ToDouble(dr["PorcentajeAsistencia"])
                    ));
                }
            }
            return lista;
        }

        public List<(string paciente, string especialidad, DateTime fecha, string estado)>
            ObtenerTurnosEntreFechas(DateTime desde, DateTime hasta)
        {
            var lista = new List<(string paciente, string especialidad, DateTime fecha, string estado)>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                    SELECT 
                        p.Nombre + ' ' + p.Apellido AS Paciente,
                        e.Nombre AS Especialidad,
                        t.Fecha AS Fecha,
                        CASE WHEN t.Estado = 1 THEN 'Presente' ELSE 'Ausente' END AS Estado
                    FROM Turno t
                    INNER JOIN Paciente p ON t.IdPaciente = p.IdPaciente
                    INNER JOIN Medico m ON t.Legajo = m.Legajo
                    INNER JOIN Especialidad e ON m.IdEspecialidad = e.IdEspecialidad
                    WHERE t.Fecha BETWEEN @desde AND @hasta
                    ORDER BY t.Fecha ASC;
                ";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add((
                        dr["Paciente"].ToString(),
                        dr["Especialidad"].ToString(),
                        Convert.ToDateTime(dr["Fecha"]),
                        dr["Estado"].ToString()
                    ));
                }
            }

            return lista;
        }
    }
}

    
