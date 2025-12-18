using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_medico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMedico.Text = Session["NombreMedico"]?.ToString();
        }

        protected void btnPacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Medico_Pacientes.aspx");
        }

        protected void btnTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pagina_Medico_Turno.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("pagina_login.aspx", false);
        }
    }
}