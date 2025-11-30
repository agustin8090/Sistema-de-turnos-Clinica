namespace proyecto_final.Datos
{
    using proyecto_final.Entidad;
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
                    "SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora FROM Turno",
                    conexion);

                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Turno aux = new Turno();
                    aux.idTurno = (int)lector["IdTurno"];
                    aux.idPaciente = (int)lector["IdPaciente"];
                    aux.idMedico = (int)lector["Legajo"];  // Lee de Legajo pero guarda en idMedico
                    aux.Fecha = (DateTime)lector["Fecha"];
                    aux.hora = (TimeSpan)lector["Hora"];
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
                comando.Parameters.AddWithValue("@Legajo", nuevoTurno.idMedico);  // Inserta en Legajo
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
                comando.Parameters.AddWithValue("@hora", hora);

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
                    @"SELECT IdTurno, IdPaciente, Legajo, Fecha, Hora
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

                    lista.Add(aux);
                }
            }

            return lista;
        }
    }
}