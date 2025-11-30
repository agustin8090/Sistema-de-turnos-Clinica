using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_ABML_Medico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnagregarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Agregar_Medico.aspx");
        }

        protected void btnmodificarMedico_Click(object sender, EventArgs e)
        {

        }

        protected void btnborrarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Eliminar_Medico.aspx");
        }

        protected void btnlistarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Listar_Medico.aspx");
        }

        protected void btnvolverMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_Administrador.aspx");
        }
    }
}