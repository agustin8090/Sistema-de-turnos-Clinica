using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class Provincia_negocio
    {
        Provincia_clinica provDAL = new Provincia_clinica();

        public List<Provincia> ListarProvincias()
        {
            return provDAL.Listar();
        }
    }
}