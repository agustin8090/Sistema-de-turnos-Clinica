using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Medico_Pacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cargargrid();
        }
        protected void Cargargrid() {
            DataTable dt = new DataTable();
            dt.Columns.Add("DNI");
            dt.Columns.Add("NOMBRE");
            dt.Columns.Add("APELLIDO");
            dt.Columns.Add("SEXO");
            dt.Columns.Add("FECHA DE NACIMIENTO");
            dt.Columns.Add("LOCALIDAD");
            dt.Columns.Add("PROVINCIA");
            dt.Columns.Add("CORREO ELECTRONICO");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("OBSERVACION");



            dt.Rows.Add("23442344", "Armando", "Casas", "Masculino","06/3/2000", "Salsipuede", "Cordoba", "armando@gmail.com", "1123347777","dolor ligero en el pulgar");
            dt.Rows.Add("43442344", "Esteban", "Quito", "Masculino","06/07/1967", "Carro Quemado", "La pampa", "Esteban@gmail.com", "1167676767","Mejora de presion arterial");
            GridViewPacientes.DataSource = dt;
            GridViewPacientes.DataBind();
        }

        protected void GridViewPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}