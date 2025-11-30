using proyecto_final.Datos;
using System;
using System.Collections.Generic;
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

        }


        protected void btneliminar_Click(object sender, EventArgs e)
        {
            string legEliminar = txtelimnar.Text.Trim();

            if (string.IsNullOrEmpty(legEliminar))
            {
                lblMensaje.Text = "Debe ingresar el Legajo del médico.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            medico_clinica medico = new medico_clinica();

            try
            {
               
               bool eliminacionExitosa = medico.eliminar_medico(legEliminar);

                if (eliminacionExitosa) 
                {
                    lblMensaje.Text = $"El médico con Legajo {legEliminar} ha sido eliminado exitosamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    txtelimnar.Text = string.Empty;
                }
                else 
                {
                    lblMensaje.Text = $"No se encontró ningún médico con Legajo {legEliminar}.";
                    lblMensaje.ForeColor = System.Drawing.Color.OrangeRed;
                }
            }
            catch (Exception ex)
            {
               
                lblMensaje.Text = "Error al intentar acceder a la base de datos: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_MEDICO.aspx");
        }
    }
}