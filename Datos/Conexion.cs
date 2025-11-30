using System.Data.SqlClient;

public class Conexion
{
    private static string cadena =
        @"Data Source=localhost\sqlexpress; Initial Catalog=CLINICA; Integrated Security=True";

    public static SqlConnection ObtenerConexion()
    {
        return new SqlConnection(cadena);
    }
}
