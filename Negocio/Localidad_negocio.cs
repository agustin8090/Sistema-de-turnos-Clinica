using proyecto_final.Datos;
using proyecto_final.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_final.Negocio
{
    public class Localidad_negocio
    {
        Localidad_clinica locDAL = new Localidad_clinica();

        // devuelve lista de localidades para una provincia
        public List<Localidad> ListarLocalidadesPorProvincia(int idProvincia)
        {
            return locDAL.ListarPorProvincia(idProvincia); // tu DAL ya tiene Localidad_clinica.ListarPorProvincia()
        }
    }
}