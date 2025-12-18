using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Datos
{
    public class Paciente_clinica
    {
        public void AgregarPaciente(Paciente pact) {


            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Paciente (DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento, 
                    Direccion, IdLocalidad, IdProvincia, Mail, Telefono, Estado)
                    VALUES(@DniPaciente, @Nombre, @Apellido, @Sexo, @Nacionalidad, @FechaNacimiento, 
                     @Direccion, @IdLocalidad, @IdProvincia, @Mail,@Telefono, @Estado)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DniPaciente", pact.DniPaciente);
                cmd.Parameters.AddWithValue("@Nombre", pact.NombrePaciente);
                cmd.Parameters.AddWithValue("@Apellido", pact.ApellidoPaciente);
                cmd.Parameters.AddWithValue("@Sexo", pact.SexoPaciente);
                cmd.Parameters.AddWithValue("@Nacionalidad", pact.NacionalidadPac);
                cmd.Parameters.AddWithValue("@FechaNacimiento", pact.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Direccion", pact.DireccionPac);
                cmd.Parameters.AddWithValue("@IdLocalidad", pact.IdLocalidad);
                cmd.Parameters.AddWithValue("@IdProvincia", pact.IdProvincia);
                cmd.Parameters.AddWithValue("@Mail", pact.EmailPac);
                cmd.Parameters.AddWithValue("@Telefono", pact.TelefonoPac);
                cmd.Parameters.AddWithValue("@Estado", pact.Estado);
                con.Open();

                cmd.ExecuteNonQuery();
            }


        }
        public List<Paciente> ListarPorDni(string dni)
        {
            List <Paciente> Lista = new List<Paciente>();

            using(SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                string query = @"SELECT IdPaciente, DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento,
                    Direccion, IdLocalidad, IdProvincia, Mail, Telefono, 
                    Estado FROM Paciente WHERE DniPaciente LIKE '%' + @DniPaciente + '%'";

                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@DniPaciente",dni);

                SqlDataReader reader= cmd.ExecuteReader();

                while (reader.Read()) { 
                Paciente pacto = new Paciente
                    {
                    IdPaciente= reader.GetInt32(0),
                    DniPaciente = reader.GetString(1),
                    NombrePaciente = reader.GetString(2),
                    ApellidoPaciente = reader.GetString(3),
                    SexoPaciente = reader.GetString(4),
                    NacionalidadPac = reader.GetString(5),
                    FechaNacimiento = reader.GetDateTime(6),
                    DireccionPac = reader.GetString(7),
                    IdLocalidad = reader.GetInt32(8),
                    IdProvincia = reader.GetInt32(9),
                    EmailPac = reader.GetString(10),
                    TelefonoPac = reader.GetString(11),
                    Estado = reader.GetBoolean(12)
                };
                Lista.Add(pacto);
                }
            }
            return Lista;

        }

        public DataTable ListarPacientesTabla()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"SELECT 
                                p.DniPaciente             AS [DNI],
                                p.Nombre                  AS [Nombre],
                                p.Apellido                AS [Apellido],
                                p.Sexo                    AS [Sexo],
                                p.Nacionalidad            AS [Nacionalidad],
                                p.FechaNacimiento         AS [Fecha de Nacimiento],
                                p.Direccion               AS [Dirección],
                                l.Nombre         AS [Localidad],
                                pr.Nombre      AS [Provincia],
                                p.Mail                    AS [Email],
                                p.Telefono                AS [Teléfono],
                                p.Estado                  AS [Activo]
                            FROM Paciente p
                            INNER JOIN Localidad l 
                                ON p.IdLocalidad = l.IdLocalidad
                            INNER JOIN Provincia pr 
                                ON p.IdProvincia = pr.IdProvincia;";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable tabla = new DataTable();
                da.Fill(tabla);
                return tabla;
            }
        }        
        

        public List<Paciente> Listarpacientes() {
            List<Paciente> Lista = new List<Paciente>();

            using (SqlConnection connec = Conexion.ObtenerConexion())
            {
                connec.Open();
                string query = @"SELECT IdPaciente, DniPaciente,Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento, 
                    Direccion, IdLocalidad, IdProvincia, Mail, Telefono, Estado  FROM Paciente";

                SqlCommand cmd = new SqlCommand(query, connec);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Paciente pac = new Paciente
                    {
                        IdPaciente = reader.GetInt32(0),
                        DniPaciente = reader.GetString(1),
                        NombrePaciente = reader.GetString(2),
                        ApellidoPaciente = reader.GetString(3),
                        SexoPaciente = reader.GetString(4),
                        NacionalidadPac = reader.GetString(5),
                        FechaNacimiento = reader.GetDateTime(6),
                        DireccionPac = reader.GetString(7),
                        IdLocalidad = reader.GetInt32(8),
                        IdProvincia = reader.GetInt32(9),
                        EmailPac = reader.GetString(10),
                        TelefonoPac = reader.GetString(11),
                        Estado = reader.GetBoolean(12)

                    };
                    Lista.Add(pac);

                }
            }

            return Lista;
        }

        public bool validarDni(string dni)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Paciente WHERE DniPaciente = @dni";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dni", dni);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool EliminarPaciente(string dni)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"UPDATE Paciente 
                         SET Estado = 0 
                         WHERE DniPaciente = @dni";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dni", dni);

                con.Open();
                int filas = cmd.ExecuteNonQuery();

                return filas > 0;
            }
        }

        public Paciente ObtenerPacientePorId(int id)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"SELECT IdPaciente, DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento,
                         Direccion, IdLocalidad, IdProvincia, Mail, Telefono, Estado 
                         FROM Paciente WHERE IdPaciente = @IdPaciente";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdPaciente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Paciente
                    {
                        IdPaciente = reader.GetInt32(0),
                        DniPaciente = reader.GetString(1),
                        NombrePaciente = reader.GetString(2),
                        ApellidoPaciente = reader.GetString(3),
                        SexoPaciente = reader.GetString(4),
                        NacionalidadPac = reader.GetString(5),
                        FechaNacimiento = reader.GetDateTime(6),
                        DireccionPac = reader.GetString(7),
                        IdLocalidad = reader.GetInt32(8),
                        IdProvincia = reader.GetInt32(9),
                        EmailPac = reader.GetString(10),
                        TelefonoPac = reader.GetString(11),
                        Estado = reader.GetBoolean(12)
                    };
                }

                return null;
            }
        }

        public bool ExisteDniEnOtroPaciente(int idPaciente, string dni)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"SELECT COUNT(*) 
                         FROM Paciente 
                         WHERE DniPaciente = @Dni 
                         AND IdPaciente <> @IdPaciente";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Dni", dni);
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public DataTable ListarPacientes()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, 
                         FechaNacimiento, Direccion, IdProvincia, IdLocalidad, Mail, Telefono 
                         FROM Paciente
                         WHERE Estado = 1";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public bool ModificarPaciente(Paciente p)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"
                    UPDATE Paciente SET
                        DniPaciente = @Dni,
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Sexo = @Sexo,
                        Nacionalidad = @Nacionalidad,
                        FechaNacimiento = @FechaNacimiento,
                        Direccion = @Direccion,
                        IdLocalidad = @IdLocalidad,
                        IdProvincia = @IdProvincia,
                        Mail = @Mail,
                        Telefono = @Telefono,
                        Estado = @Estado
                    WHERE IdPaciente = @IdPaciente";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@IdPaciente", p.IdPaciente);
                cmd.Parameters.AddWithValue("@Dni", p.DniPaciente);
                cmd.Parameters.AddWithValue("@Nombre", p.NombrePaciente);
                cmd.Parameters.AddWithValue("@Apellido", p.ApellidoPaciente);
                cmd.Parameters.AddWithValue("@Sexo", p.SexoPaciente);
                cmd.Parameters.AddWithValue("@Nacionalidad", p.NacionalidadPac);
                cmd.Parameters.AddWithValue("@FechaNacimiento", p.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Direccion", p.DireccionPac);
                cmd.Parameters.AddWithValue("@IdLocalidad", p.IdLocalidad);
                cmd.Parameters.AddWithValue("@IdProvincia", p.IdProvincia);
                cmd.Parameters.AddWithValue("@Mail", p.EmailPac);
                cmd.Parameters.AddWithValue("@Telefono", p.TelefonoPac);
                cmd.Parameters.AddWithValue("@Estado", p.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool CambiarEstadoPaciente(int idPaciente, bool estadoNuevo)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"UPDATE Paciente 
                         SET Estado = @Estado 
                         WHERE IdPaciente = @IdPaciente";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Estado", estadoNuevo);
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                con.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();

                return filasAfectadas > 0;
            }
        }

        public List<Paciente> ListarPorSexo(string sexo)
        {
            List<Paciente> Lista = new List<Paciente>();

            string query = @"SELECT IdPaciente, DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento,
                     Direccion, IdLocalidad, IdProvincia, Mail, Telefono, Estado 
                     FROM Paciente WHERE Sexo = @Sexo";

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Sexo", sexo);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Lista.Add(MapearPaciente(reader));
                }
            }
            return Lista;
        }
        public List<Paciente> ListarPorEstado(bool estado)
        {
            List<Paciente> Lista = new List<Paciente>();

            string query = @"SELECT IdPaciente, DniPaciente, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento,
                     Direccion, IdLocalidad, IdProvincia, Mail, Telefono, Estado 
                     FROM Paciente WHERE Estado = @Estado";

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Estado", estado);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Lista.Add(MapearPaciente(reader));
                }
            }
            return Lista;
        }

        private Paciente MapearPaciente(SqlDataReader reader)
        {
            return new Paciente
            {
                IdPaciente = reader.GetInt32(0),
                DniPaciente = reader.GetString(1),
                NombrePaciente = reader.GetString(2),
                ApellidoPaciente = reader.GetString(3),
                SexoPaciente = reader.GetString(4),
                NacionalidadPac = reader.GetString(5),
                FechaNacimiento = reader.GetDateTime(6),
                DireccionPac = reader.GetString(7),
                IdLocalidad = reader.GetInt32(8),
                IdProvincia = reader.GetInt32(9),
                EmailPac = reader.GetString(10),
                TelefonoPac = reader.GetString(11),
                Estado = reader.GetBoolean(12)
            };
        }

    }
}