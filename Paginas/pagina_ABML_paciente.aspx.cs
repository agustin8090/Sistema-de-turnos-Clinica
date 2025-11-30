using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_ABML_paciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Agregar_Paciente.aspx");
        }

        protected void btnborrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Borrar_Paciente.aspx");
        }

        protected void btnlistar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_listar_paciente.aspx");
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Modificar_paciente.aspx");
        }

        protected void btnvolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}