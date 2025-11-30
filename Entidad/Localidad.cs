using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Entidad
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }
    }
}