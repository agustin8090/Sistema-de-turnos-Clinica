using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Entidad
{
    public class Turno
    {
        public int idTurno {  get; set; }
        public int idPaciente {  get; set; }
        public int idMedico {  get; set; }
        public DateTime Fecha {  get; set; }
        public TimeSpan hora {  get; set; }
        public bool estado {  get; set; }

    }
}