using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class Localidad_negocio
    {
        Localidad_clinica locDAL = new Localidad_clinica();

        public List<Localidad> ListarLocalidadesPorProvincia(int idProvincia)
        {
            return locDAL.ListarPorProvincia(idProvincia);
        }
    }
}