using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Entidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
    }
}