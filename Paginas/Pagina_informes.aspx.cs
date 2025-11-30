using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class Pagina_informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }

        protected void lnkInforme1_Click(object sender, EventArgs e)
        {
            MostrarInforme(1);
        }

        protected void lnkInforme2_Click(object sender, EventArgs e)
        {
            MostrarInforme(2);
        }

        protected void lnkInforme3_Click(object sender, EventArgs e)
        {
            MostrarInforme(3);
        }

        protected void lnkInforme4_Click(object sender, EventArgs e)
        {
            MostrarInforme(4);
        }

        private void MostrarInforme(int numero)
        {
            pnlInforme1.Visible = (numero == 1);
            pnlInforme2.Visible = (numero == 2);
            pnlInforme3.Visible = (numero == 3);
            pnlInforme4.Visible = (numero == 4);
        }
    }
}