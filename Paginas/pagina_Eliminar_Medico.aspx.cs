using Datos;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Eliminar_Medico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAdmin.Text = Session["NombreAdmin"]?.ToString();
        }


        protected void btneliminar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (!int.TryParse(txtelimnar.Text.Trim(), out int legEliminar))
            {
                lblMensaje.Text = "El legajo debe ser un número entero.";
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            try
            {
                Medico_negocio medicoNeg = new Medico_negocio();

                bool eliminado = medicoNeg.EliminarMedico(legEliminar);

                if (eliminado)
                {
                    lblMensaje.Text = $"Médico con Legajo {legEliminar} eliminado correctamente.";
                    lblMensaje.ForeColor = Color.Green;
                    txtelimnar.Text = "";
                }
                else
                {
                    lblMensaje.Text = $"No existe un médico con Legajo {legEliminar}.";
                    lblMensaje.ForeColor = Color.OrangeRed;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
            }

        }

        protected void volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_MEDICO.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}