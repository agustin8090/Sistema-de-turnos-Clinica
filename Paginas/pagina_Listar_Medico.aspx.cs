using proyecto_final.Datos;
using proyecto_final.Entidad;
using System;
using System.Collections.Generic;

namespace proyecto_final
{
    public partial class pagina_Listar_Medico : System.Web.UI.Page
    {
        medico_clinica Datos = new medico_clinica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListado();
            }
        }

        private void CargarListado()
        {
            GridViewMedico.DataSource = Datos.ListarMedicos();
            GridViewMedico.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            medico_clinica Datos = new medico_clinica();

            string dni = txtBuscarDNI.Text.Trim();
            string legajo = txtBuscarLegajo.Text.Trim();
            string especialidad = ddlFiltroEspecialidad.SelectedValue;

            List<medico> lista = null;

      
            if (!string.IsNullOrEmpty(dni))
                lista = Datos.BuscarPorDni(dni);

            else if (!string.IsNullOrEmpty(legajo))
                lista = Datos.BuscarPorLegajo(legajo);

            else if (!string.IsNullOrEmpty(especialidad))
                lista = Datos.BuscarPorEspecialidad(especialidad);

            else
                lista = Datos.ListarMedicos();

            GridViewMedico.DataSource = lista;
            GridViewMedico.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDNI.Text = "";
            txtBuscarLegajo.Text = "";
            ddlFiltroEspecialidad.SelectedIndex = 0;

            medico_clinica Datos = new medico_clinica();
            GridViewMedico.DataSource = Datos.ListarMedicos();
            GridViewMedico.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Medico.aspx");
        }
    }
}
