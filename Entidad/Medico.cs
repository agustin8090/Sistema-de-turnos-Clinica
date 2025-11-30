using System;

namespace proyecto_final.Entidad
{
    public class medico
    {
        public int id_medico { get; set; }
        public int legajo { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string NombreCompleto
        {
            get { return nombre + ", " + apellido; }
        }
        public string sexo { get; set; }
        public string nacionalidad { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string direccion { get; set; }
        public int id_localidad { get; set; }

        public int id_provincia { get; set; }
        public int id_especialidad { get; set; }
        public string nombre_especialidad { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string dias_atencion { get; set; }

        public TimeSpan? hora_inicio { get; set; }
        public TimeSpan? hora_fin { get; set; }

        public bool estado { get; set; }
        public int id_usuario { get; set; }
    }
}
