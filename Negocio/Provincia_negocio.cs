using proyecto_final.Datos;
using proyecto_final.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Negocio
{
    public class Provincia_negocio
    {
        Provincia_clinica provDAL = new Provincia_clinica();

        public List<Provincia> ListarProvincias()
        {
            return provDAL.Listar(); // tu DAL ya tiene Provincia_clinica.Listar()
        }
    }
}