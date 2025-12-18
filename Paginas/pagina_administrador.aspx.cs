using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_administrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAdmin.Text = Session["NombreAdmin"]?.ToString();
        }

        protected void btnPacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_paciente.aspx");
        }

        protected void btnMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Medico.aspx");
        }

        protected void btnTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pagina_asignar_turnos.aspx");
        }

        protected void btnInformes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pagina_informes.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon(); 

            Response.Redirect("pagina_login.aspx", false);
        }

        protected void btnTodosLosTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Turnos_admin.aspx");
        }
    }
}