using proyecto_final.Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace proyecto_final.Datos
{
    public class medico_clinica
    {
        public int crear_usuario(string nombre_usuario, string contraseña)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"
                    INSERT INTO Usuario (NombreUsuario, Contraseña, TipoUsuario)
                    OUTPUT INSERTED.IdUsuario
                    VALUES (@usuario, @contraseña, 'Medico')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", nombre_usuario);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void agregar_medico(medico m)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Medico 
                (Legajo, Dni, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento,
                 Direccion, IdLocalidad, IdEspecialidad, Telefono, Email, 
                 DiasAtencion, HoraInicio, HoraFin, Estado, IdUsuario)
                 VALUES
                (@Legajo, @Dni, @Nombre, @Apellido, @Sexo, @Nacionalidad, @FechaNacimiento,
                 @Direccion, @IdLocalidad, @IdEspecialidad, @Telefono, @Email,
                 @DiasAtencion, @HoraInicio, @HoraFin, @Estado, @IdUsuario)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Legajo", m.legajo);
                cmd.Parameters.AddWithValue("@Dni", m.dni);
                cmd.Parameters.AddWithValue("@Nombre", m.nombre);
                cmd.Parameters.AddWithValue("@Apellido", m.apellido);
                cmd.Parameters.AddWithValue("@Sexo", m.sexo);
                cmd.Parameters.AddWithValue("@Nacionalidad", m.nacionalidad);
                cmd.Parameters.AddWithValue("@FechaNacimiento", m.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@Direccion", m.direccion);
                cmd.Parameters.AddWithValue("@IdLocalidad", m.id_localidad);
                cmd.Parameters.AddWithValue("@IdEspecialidad", m.id_especialidad);
                cmd.Parameters.AddWithValue("@Telefono", m.telefono);
                cmd.Parameters.AddWithValue("@Email", m.email);
                cmd.Parameters.AddWithValue("@DiasAtencion", m.dias_atencion);
                cmd.Parameters.AddWithValue("@HoraInicio", (object)m.hora_inicio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@HoraFin", (object)m.hora_fin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", m.estado);
                cmd.Parameters.AddWithValue("@IdUsuario", m.id_usuario);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool eliminar_medico(string legajoMedico)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "DELETE FROM Medico WHERE Legajo = @Legajo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Legajo", legajoMedico);

                con.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        private medico Cargar(SqlDataReader reader)
        {
            medico m = new medico();

            m.id_medico = reader.GetInt32(0);
            m.legajo = reader.GetInt32(1);
            m.dni = reader.GetString(2);
            m.nombre = reader.GetString(3);
            m.apellido = reader.GetString(4);
            m.sexo = reader.GetString(5);
            m.nacionalidad = reader.GetString(6);
            m.fecha_nacimiento = reader.GetDateTime(7);
            m.direccion = reader.GetString(8);
            m.id_localidad = reader.GetInt32(9);
            m.id_especialidad = reader.GetInt32(10);
            m.nombre_especialidad = reader.GetString(11);
            m.telefono = reader.GetString(12);
            m.email = reader.GetString(13);
            m.dias_atencion = reader.GetString(14);
            m.hora_inicio = reader.IsDBNull(15) ? (TimeSpan?)null : reader.GetTimeSpan(15);
            m.hora_fin = reader.IsDBNull(16) ? (TimeSpan?)null : reader.GetTimeSpan(16);
            m.estado = reader.GetBoolean(17);
            m.id_usuario = reader.GetInt32(18);

            return m;
        }


        public List<medico> ListarMedicos()
        {
            List<medico> lista = new List<medico>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                    M.IdMedico,
                    M.Legajo,
                    M.Dni,
                    M.Nombre,
                    M.Apellido,
                    M.Sexo,
                    M.Nacionalidad,
                    M.FechaNacimiento,
                    M.Direccion,
                    M.IdLocalidad,
                    M.IdEspecialidad,
                    E.Nombre AS Especialidad,
                    M.Telefono,
                    M.Email,
                    M.DiasAtencion,
                    M.HoraInicio,
                    M.HoraFin,
                    M.Estado,
                    M.IdUsuario
                FROM Medico M
                INNER JOIN Especialidad E ON M.IdEspecialidad = E.IdEspecialidad
                WHERE M.Estado = 1
                ORDER BY M.Apellido, M.Nombre";

                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(Cargar(reader));
                }
            }

            return lista;
        }

        public List<medico> BuscarPorDni(string dni)
        {
            List<medico> lista = new List<medico>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                    M.IdMedico,
                    M.Legajo,
                    M.Dni,
                    M.Nombre,
                    M.Apellido,
                    M.Sexo,
                    M.Nacionalidad,
                    M.FechaNacimiento,
                    M.Direccion,
                    M.IdLocalidad,
                    M.IdEspecialidad,
                    E.Nombre AS Especialidad,
                    M.Telefono,
                    M.Email,
                    M.DiasAtencion,
                    M.HoraInicio,
                    M.HoraFin,
                    M.Estado,
                    M.IdUsuario
                FROM Medico M
                INNER JOIN Especialidad E ON M.IdEspecialidad = E.IdEspecialidad
                WHERE M.Estado = 1
                AND M.Dni LIKE '%' + @dni + '%'
                ORDER BY M.Apellido, M.Nombre";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@dni", dni);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(Cargar(reader));
                }
            }

            return lista;
        }

        public List<medico> BuscarPorLegajo(string legajo)
        {
            List<medico> lista = new List<medico>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                    M.IdMedico,
                    M.Legajo,
                    M.Dni,
                    M.Nombre,
                    M.Apellido,
                    M.Sexo,
                    M.Nacionalidad,
                    M.FechaNacimiento,
                    M.Direccion,
                    M.IdLocalidad,
                    M.IdEspecialidad,
                    E.Nombre AS Especialidad,
                    M.Telefono,
                    M.Email,
                    M.DiasAtencion,
                    M.HoraInicio,
                    M.HoraFin,
                    M.Estado,
                    M.IdUsuario
                FROM Medico M
                INNER JOIN Especialidad E ON M.IdEspecialidad = E.IdEspecialidad
                WHERE M.Estado = 1
                AND CAST(M.Legajo AS VARCHAR(20)) LIKE '%' + @legajo + '%'
                ORDER BY M.Apellido, M.Nombre";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@legajo", legajo);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(Cargar(reader));
                }
            }

            return lista;
        }

        public List<medico> ListarPorEspecialidad(int idEspecialidad)
        {
            List<medico> lista = new List<medico>();
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                // IMPORTANTE: Agregar Legajo a la consulta
                string query = @"SELECT IdMedico, Legajo, Nombre, Apellido 
                        FROM Medico 
                        WHERE IdEspecialidad = @esp AND Estado = 1";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@esp", idEspecialidad);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    medico m = new medico();
                    m.id_medico = Convert.ToInt32(reader["IdMedico"]);
                    m.legajo = Convert.ToInt32(reader["Legajo"]); // AGREGAR ESTO
                    m.nombre = reader["Nombre"].ToString();
                    m.apellido = reader["Apellido"].ToString();
                    lista.Add(m);
                }
            }
            return lista;
        }

        public List<medico> BuscarPorEspecialidad(string idEspecialidad)
        {
            List<medico> lista = new List<medico>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
                SELECT 
                    M.IdMedico,
                    M.Legajo,
                    M.Dni,
                    M.Nombre,
                    M.Apellido,
                    M.Sexo,
                    M.Nacionalidad,
                    M.FechaNacimiento,
                    M.Direccion,
                    M.IdLocalidad,
                    M.IdEspecialidad,
                    E.Nombre AS Especialidad,
                    M.Telefono,
                    M.Email,
                    M.DiasAtencion,
                    M.HoraInicio,
                    M.HoraFin,
                    M.Estado,
                    M.IdUsuario
                FROM Medico M
                INNER JOIN Especialidad E ON M.IdEspecialidad = E.IdEspecialidad
                WHERE M.Estado = 1
                AND M.IdEspecialidad = @esp
                ORDER BY M.Apellido, M.Nombre";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@esp", idEspecialidad);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(Cargar(reader));
                }
            }

            return lista;
        }


        public void modificar_medico(medico m)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = @"
            UPDATE Medico SET
                DNI = @dni,
                Nombre = @nombre,
                Apellido = @apellido,
                Sexo = @sexo,
                Nacionalidad = @nacionalidad,
                FechaNacimiento = @fecha_nacimiento,
                Direccion = @direccion,
                IdLocalidad = @id_localidad,
                IdEspecialidad = @id_especialidad,
                Telefono = @telefono,
                Email = @email,
                DiasAtencion = @dias_atencion,
                HoraInicio = @hora_inicio,
                HoraFin = @hora_fin
            WHERE Legajo = @legajo";

                SqlCommand cmd = new SqlCommand(consulta, conexion);

                cmd.Parameters.AddWithValue("@dni", m.dni);
                cmd.Parameters.AddWithValue("@nombre", m.nombre);
                cmd.Parameters.AddWithValue("@apellido", m.apellido);
                cmd.Parameters.AddWithValue("@sexo", m.sexo);
                cmd.Parameters.AddWithValue("@nacionalidad", m.nacionalidad);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", m.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@direccion", m.direccion);
                cmd.Parameters.AddWithValue("@id_localidad", m.id_localidad);
                cmd.Parameters.AddWithValue("@id_especialidad", m.id_especialidad);
                cmd.Parameters.AddWithValue("@telefono", m.telefono);
                cmd.Parameters.AddWithValue("@email", m.email);
                cmd.Parameters.AddWithValue("@dias_atencion", m.dias_atencion);
                cmd.Parameters.AddWithValue("@hora_inicio", m.hora_inicio);
                cmd.Parameters.AddWithValue("@hora_fin", m.hora_fin);

                cmd.Parameters.AddWithValue("@legajo", m.legajo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public medico BuscarPorLegajo(int legajo)
        {
            medico aux = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = @"
        SELECT m.IdMedico, m.Legajo, m.DNI, m.Nombre, m.Apellido, m.Sexo, 
               m.Nacionalidad, m.FechaNacimiento, m.Direccion, m.Telefono,
               m.IdLocalidad, m.IdEspecialidad, m.HoraInicio, m.HoraFin, m.DiasAtencion,
               l.IdProvincia
        FROM Medico m
        INNER JOIN Localidad l ON m.IdLocalidad = l.IdLocalidad
        WHERE m.Legajo = @Legajo";

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@Legajo", legajo);

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    aux = new medico
                    {
                        id_medico = dr["IdMedico"] != DBNull.Value ? (int)dr["IdMedico"] : 0,
                        legajo = dr["Legajo"] != DBNull.Value ? (int)dr["Legajo"] : 0,
                        dni = dr["DNI"].ToString(),
                        nombre = dr["Nombre"].ToString(),
                        apellido = dr["Apellido"].ToString(),
                        sexo = dr["Sexo"].ToString(),
                        nacionalidad = dr["Nacionalidad"].ToString(),
                        fecha_nacimiento = dr["FechaNacimiento"] != DBNull.Value ? (DateTime)dr["FechaNacimiento"] : DateTime.MinValue,
                        direccion = dr["Direccion"].ToString(),
                        telefono = dr["Telefono"].ToString(),
                        id_localidad = dr["IdLocalidad"] != DBNull.Value ? (int)dr["IdLocalidad"] : 0,
                        id_especialidad = dr["IdEspecialidad"] != DBNull.Value ? (int)dr["IdEspecialidad"] : 0,
                        hora_inicio = dr["HoraInicio"] != DBNull.Value ? (TimeSpan)dr["HoraInicio"] : TimeSpan.Zero,
                        hora_fin = dr["HoraFin"] != DBNull.Value ? (TimeSpan)dr["HoraFin"] : TimeSpan.Zero,
                        dias_atencion = dr["DiasAtencion"] != DBNull.Value ? dr["DiasAtencion"].ToString() : ""
                    };


                    aux.id_provincia = dr["IdProvincia"] != DBNull.Value ? (int)dr["IdProvincia"] : 0;
                }
            }

            return aux;
        }

        public medico BuscarPorIdUsuario(int idUsuario)
        {
            medico aux = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                M.IdMedico,
                M.Legajo,
                M.Dni,
                M.Nombre,
                M.Apellido,
                M.Sexo,
                M.Nacionalidad,
                M.FechaNacimiento,
                M.Direccion,
                M.IdLocalidad,
                M.IdEspecialidad,
                E.Nombre AS Especialidad,
                M.Telefono,
                M.Email,
                M.DiasAtencion,
                M.HoraInicio,
                M.HoraFin,
                M.Estado,
                M.IdUsuario
            FROM Medico M
            INNER JOIN Especialidad E ON M.IdEspecialidad = E.IdEspecialidad
            WHERE M.IdUsuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    aux = Cargar(reader);
                }
            }

            return aux;
        }


    }
}
