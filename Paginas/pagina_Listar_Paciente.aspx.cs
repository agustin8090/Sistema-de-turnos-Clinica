using proyecto_final.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Listar_Paciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarGridview();
        }

        protected void CargarGridview()
        {
            Paciente_clinica paciente_Clinica = new Paciente_clinica();

            GridViewPaciente.DataSource = paciente_Clinica.Listarpacientes();
            GridViewPaciente.DataBind();
        }



        protected void btneliminar_Click(object sender, EventArgs e)
        {

        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            bool tieneletra=false;
            string letras =  "^[0-9]+$";

            //VALIDAMOS QUE NO BUSQUE DNI CON ALGUNA LETRA
           if(!System.Text.RegularExpressions.Regex.IsMatch(txtBuscar.Text, letras))
            {
                Label1.ForeColor = System.Drawing.Color.Red;  
                Label1.Text = "Solo se aceptan numeros";
                tieneletra = true;
            }
            //SI PASA LA VALIDACION ENTONCES QUE MUESTRE LOS DNI SIMILARES

            if (tieneletra == false)
            {
                Label1.Text = "";
                Paciente_clinica pacc = new Paciente_clinica();
                GridViewPaciente.DataSource = null;
                GridViewPaciente.DataSource = pacc.ListarPorDni(txtBuscar.Text);
                GridViewPaciente.DataBind();
            }

            
        }

        protected void BtnvolverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_paciente.aspx");
        }
    }
}