using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Entidad
{
    public class Paciente
    {

        public int IdPaciente {  get; set; }
        public string DniPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }

        public string NombreCompleto
        {
            get { return NombrePaciente + ", " + ApellidoPaciente; }
        }

        public string SexoPaciente { get; set; }
        public string NacionalidadPac { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DireccionPac {get; set; }
        public int IdLocalidad { get; set; }
        public int IdProvincia {  get; set; }
        public string EmailPac {  get; set; }
        public string TelefonoPac { get; set; }
        public bool Estado { get; set; }
    }
}