using Datos;
using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Listar_Paciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                CargarGridview();
            }
        }

        protected void CargarGridview()
        {
            Paciente_clinica paciente_Clinica = new Paciente_clinica();
            GridViewPaciente.DataSource = paciente_Clinica.Listarpacientes();
            GridViewPaciente.DataBind();
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                List<Paciente> lista = null;
                Label1.Text = ""; 

                string dni = txtBuscarDNI.Text.Trim();
                string sexo = ddlSexo.SelectedValue;
                int estadoFiltro = int.Parse(ddlEstado.SelectedValue);

                Paciente_negocio pacNeg = new Paciente_negocio();
                lista = pacNeg.ListarPorDni(dni);

                if (!string.IsNullOrEmpty(dni))
                {
                    if (!Regex.IsMatch(dni, @"^[0-9]+$"))
                    {
                        throw new Exception("El DNI solo puede contener números.");
                    }

                    lista = pacNeg.ListarPorDni(dni);
                }

                else if (!string.IsNullOrEmpty(sexo))
                {
                    lista = pacNeg.ListarPorSexo(sexo);
                }
                else if (estadoFiltro != -1) 
                {
                    bool estadoBusqueda = (estadoFiltro == 1);
                    lista = pacNeg.ListarPorEstado(estadoBusqueda);
                }
                else
                {
                    lista = pacNeg.ListarPacientes();
                }

                GridViewPaciente.DataSource = lista;
                GridViewPaciente.DataBind();

                if (lista == null || lista.Count == 0)
                {
                    Label1.Text = "ℹ️ No se encontraron pacientes con los criterios de búsqueda.";
                    Label1.ForeColor = Color.DarkOrange;
                }
                else {}
            }
            catch (Exception ex)
            {
                Label1.Text = "❌ Error en la búsqueda: " + ex.Message;
                Label1.ForeColor = Color.Red;
            }

        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBuscarDNI.Text = "";
            ddlSexo.SelectedIndex = 0;
            ddlEstado.SelectedValue = "-1";
            Label1.Text = "";
            CargarGridview();
        }

        protected void BtnvolverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_paciente.aspx");
        }

        protected void GridViewPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPaciente.PageIndex = e.NewPageIndex;
            CargarGridview();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}