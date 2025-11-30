using proyecto_final.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Borrar_Paciente : System.Web.UI.Page
    {
        Paciente_negocio pacNeg = new Paciente_negocio();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            string dni = txtEliminar.Text.Trim();


            if (!System.Text.RegularExpressions.Regex.IsMatch(dni, "^[0-9]+$"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('El DNI debe contener solo números.');", true);
                return;
            }

            try
            {
                pacNeg.EliminarPaciente(dni);

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Paciente eliminado correctamente.');", true);

                txtEliminar.Text = "";
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('" + ex.Message + "');", true);
            }
        }

        protected void volveralMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Paciente.aspx");
        }

        protected void volver_Click(object sender, EventArgs e)
        {

        }
    }
}