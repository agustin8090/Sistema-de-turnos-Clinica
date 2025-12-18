using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class Especialidad_negocio
    {
        Especialidad_clinica espDAl = new Especialidad_clinica();
        public Especialidad TraerPorId(int idEspecialidad)
        {
            if (idEspecialidad <= 0)
                throw new Exception("El id de la especialidad debe ser válido.");

            return espDAl.TraerPorId(idEspecialidad);
        }

    }
}
