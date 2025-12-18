using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Medico_Pacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMedico.Text = Session["NombreMedico"]?.ToString();
                CargarPacientes();
            }
        }
        private void CargarPacientes()
        {
            Paciente_negocio pacNeg = new Paciente_negocio();
            GridViewPacientes.DataSource = pacNeg.ListarPacientes();
            GridViewPacientes.DataBind();
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            bool tieneletra = false;
            string letras = "^[0-9]+$";

            
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtBuscarDNI.Text, letras))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Solo se aceptan numeros";
                tieneletra = true;
            }
            

            if (tieneletra == false)
            {
                Label1.Text = "";
                Paciente_negocio pacNeg = new Paciente_negocio();
                GridViewPacientes.DataSource = null;
                GridViewPacientes.DataSource = pacNeg.ListarPorDni(txtBuscarDNI.Text);
                GridViewPacientes.DataBind();
            }
        }

        protected void GridViewPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPacientes.PageIndex = e.NewPageIndex;
            CargarPacientes();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_medico.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            Paciente_clinica Datos = new Paciente_clinica();
            GridViewPacientes.DataSource = Datos.Listarpacientes();
            GridViewPacientes.DataBind();
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            txtBuscarDNI.Text = "";
        }

    }
}