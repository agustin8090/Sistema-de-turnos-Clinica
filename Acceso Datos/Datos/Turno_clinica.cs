namespace Datos
{
    using Entidad;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class Turno_clinica
    {
        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora, Estado FROM Turno",
                    conexion);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Turno aux = new Turno();
                    aux.idTurno = (int)lector["IdTurno"];
                    aux.idPaciente = (int)lector["IdPaciente"];
                    aux.idMedico = (int)lector["Legajo"];
                    aux.Fecha = (DateTime)lector["Fecha"];
                    aux.hora = (TimeSpan)lector["Hora"];
                    aux.estado = lector["Estado"] != DBNull.Value && Convert.ToBoolean(lector["Estado"]);
                    lista.Add(aux);
                }
            }
            return lista;
        }

        public void Agregar(Turno nuevoTurno)
        {
            if (ExisteTurno(nuevoTurno.idMedico, nuevoTurno.Fecha, nuevoTurno.hora))
            {
                throw new Exception("El médico ya tiene un turno asignado en esa fecha y hora.");
            }

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(
                    "INSERT INTO Turno (IdPaciente, Legajo, Fecha, Hora, Estado) VALUES (@IdPaciente, @Legajo, @Fecha, @Hora, @Estado)",
                    conexion);

                comando.Parameters.AddWithValue("@IdPaciente", nuevoTurno.idPaciente);
                comando.Parameters.AddWithValue("@Legajo", nuevoTurno.idMedico);
                comando.Parameters.AddWithValue("@Fecha", nuevoTurno.Fecha);
                comando.Parameters.AddWithValue("@Hora", nuevoTurno.hora);
                comando.Parameters.AddWithValue("@Estado", nuevoTurno.estado);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public bool ExisteTurno(int idMedico, DateTime fecha, TimeSpan hora)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand(
                    "SELECT COUNT(*) FROM Turno WHERE Legajo = @idMedico AND Fecha = @fecha AND Hora = @hora",
                    conexion);

                comando.Parameters.AddWithValue("@idMedico", idMedico);
                comando.Parameters.AddWithValue("@fecha", fecha);

                TimeSpan horaNormalizada = new TimeSpan(hora.Hours, hora.Minutes, 0);
                comando.Parameters.AddWithValue("@hora", horaNormalizada);

                conexion.Open();
                int cantidad = (int)comando.ExecuteScalar();
                return cantidad > 0;
            }
        }

        public List<Turno> ListarPorMedico(int legajo, DateTime? fecha)
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query =
                    @"SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora, Estado
                    FROM Turno
                    WHERE Legajo = @Legajo
                    AND (@Fecha IS NULL OR Fecha = @Fecha)
                    ORDER BY Fecha, Hora";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@Legajo", legajo);
                comando.Parameters.AddWithValue("@Fecha", (object)fecha ?? DBNull.Value);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Turno aux = new Turno();
                    aux.idTurno = (int)lector["IdTurno"];
                    aux.idPaciente = (int)lector["IdPaciente"];
                    aux.idMedico = (int)lector["Legajo"];
                    aux.Fecha = (DateTime)lector["Fecha"];
                    aux.hora = (TimeSpan)lector["Hora"];
                    aux.estado = lector["Estado"] != DBNull.Value && Convert.ToBoolean(lector["Estado"]);

                    lista.Add(aux);
                }
            }

            return lista;
        }

        public void CambiarEstadoTurno(int idTurno)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                string query = "UPDATE Turno SET Estado = 1 WHERE IdTurno = @IdTurno";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdTurno", idTurno);
                cmd.ExecuteNonQuery();
            }
        }


        public List<Turno> ListarTurnosPorDni(int legajo, string dni)
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                T.IdTurno,
                T.IdPaciente,
                T.Legajo,
                T.Fecha,
                T.Hora,
                T.Estado
            FROM Turno T
            INNER JOIN Paciente P ON T.IdPaciente = P.IdPaciente
            WHERE T.Legajo = @Legajo
              AND P.DniPaciente LIKE '%' + @Dni + '%'
            ORDER BY T.Fecha, T.Hora";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Legajo", legajo);
                comando.Parameters.AddWithValue("@Dni", dni);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Turno aux = new Turno();
                    aux.idTurno = (int)lector["IdTurno"];
                    aux.idPaciente = (int)lector["IdPaciente"];
                    aux.idMedico = (int)lector["Legajo"];
                    aux.Fecha = (DateTime)lector["Fecha"];
                    aux.hora = (TimeSpan)lector["Hora"];
                    aux.estado = lector["Estado"] != DBNull.Value && Convert.ToBoolean(lector["Estado"]);

                    lista.Add(aux);
                }
            }

            return lista;
        }

        public List<Turno> ListarTurnosPorEstado(int legajoMedico, bool estado)
        {
            List<Turno> lista = new List<Turno>();

            string query = @"
                SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora, Estado
                FROM Turno
                WHERE Legajo = @IdMedico AND Estado = @Estado
            ";

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdMedico", legajoMedico);
                cmd.Parameters.AddWithValue("@Estado", estado);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(MapearTurno(reader));
                }
            }

            return lista;
        }

        private Turno MapearTurno(SqlDataReader reader)
        {
            return new Turno
            {
                idTurno = (int)reader["IdTurno"],
                idPaciente = (int)reader["IdPaciente"],
                idMedico = (int)reader["Legajo"],
                Fecha = (DateTime)reader["Fecha"],
                hora = (TimeSpan)reader["Hora"],
                estado = reader["Estado"] != DBNull.Value && Convert.ToBoolean(reader["Estado"])
            };
        }

        public List<Turno> ListarTodosLosTurnos()
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora, Estado
                         FROM Turno";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Turno t = new Turno();
                    t.idTurno = Convert.ToInt32(dr["IdTurno"]);
                    t.idPaciente = Convert.ToInt32(dr["IdPaciente"]);
                    t.idMedico = Convert.ToInt32(dr["Legajo"]);
                    t.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    t.hora = (TimeSpan)dr["Hora"];
                    t.estado = Convert.ToBoolean(dr["Estado"]);

                    lista.Add(t);
                }
            }
            return lista;
        }

        public List<Turno> ListarTurnosPorDniAdmin(string dni)
        {
            List<Turno> lista = new List<Turno>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT t.IdTurno, t.IdPaciente, t.Legajo, t.Fecha, t.Hora, t.Estado
                         FROM Turno t
                         INNER JOIN Paciente p ON p.IdPaciente = t.IdPaciente
                         WHERE p.DniPaciente LIKE @dni";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dni", "%" + dni + "%");
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Turno t = new Turno();
                    t.idTurno = Convert.ToInt32(dr["IdTurno"]);
                    t.idPaciente = Convert.ToInt32(dr["IdPaciente"]);
                    t.idMedico = Convert.ToInt32(dr["Legajo"]); 
                    t.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    t.hora = (TimeSpan)dr["Hora"];
                    t.estado = Convert.ToBoolean(dr["Estado"]);

                    lista.Add(t);
                }
            }
            return lista;
        }
    }
}