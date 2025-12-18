using Datos;
using Entidad;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Listar_Medico : System.Web.UI.Page
    {
        medico_clinica Datos = new medico_clinica();

        Medico_negocio medicoNeg = new Medico_negocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                CargarListado();
                CargarEspecialidades();
            }
        }

        private void CargarListado()
        {
            GridViewMedico.DataSource = medicoNeg.ListarMedicos();
            GridViewMedico.DataBind();
        }

        private void CargarEspecialidades()
        {
            try
            {
                Especialidad_clinica espDatos = new Especialidad_clinica();
                var lista = espDatos.Listar();

                ddlEspecialidad.Items.Clear();

                // ¡Este es el ítem "Ninguna especialidad"! Debe tener Value="0"
                ddlEspecialidad.Items.Add(new ListItem("-- Seleccionar --", "0"));

                ddlEspecialidad.DataSource = lista;
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error al cargar especialidades: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Visible = true;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<medico> lista = null;
                lblMensaje.Visible = false;

                string dni = txtBuscarDNI.Text.Trim();
                string leg = txtBuscarLegajo.Text.Trim();

                // El valor 0 es para "-- Seleccionar --". 
                // Solo un valor > 0 debería considerarse un filtro activo.
                int esp = int.Parse(ddlEspecialidad.SelectedValue);

                int estadoFiltro = int.Parse(ddlEstado.SelectedValue);
                List<string> diasSeleccionados = new List<string>();

                foreach (ListItem item in cblDiasAtencion.Items)
                {
                    if (item.Selected)
                    {
                        diasSeleccionados.Add(item.Value);
                    }
                }

                // *** LÓGICA DE FILTRADO PRIORITARIA AJUSTADA ***

                // 1. DNI (Prioridad máxima de búsqueda individual)
                if (!string.IsNullOrEmpty(dni))
                {
                    // ... (validaciones de filtros cruzados)
                    lista = medicoNeg.BuscarPorDni(dni);
                }
                // 2. LEGAJO (Siguiente prioridad de búsqueda individual)
                else if (!string.IsNullOrEmpty(leg))
                {
                    // ... (validaciones de filtros cruzados)
                    if (!int.TryParse(leg, out int legajo))
                        throw new Exception("El legajo debe ser numérico.");

                    lista = medicoNeg.BuscarPorLegajo(legajo);
                }
                // 3. ESPECIALIDAD (Filtro por lista/ddl activo)
                // **AQUÍ ESTÁ EL CAMBIO CLAVE: Solo entra si esp > 0**
                else if (esp > 0)
                {
                    // ... (validaciones de filtros cruzados)
                    lista = medicoNeg.BuscarPorEspecialidad(esp);
                }
                // 4. DIAS DE ATENCIÓN (Filtro por CheckBoxList)
                else if (diasSeleccionados.Count > 0)
                {
                    // ... (validaciones de filtros cruzados)
                    lista = medicoNeg.BuscarPorDias(diasSeleccionados);
                }
                // 5. ESTADO (Activo/Inactivo - Filtro por ddl)
                else if (estadoFiltro != -1) // -1 es "Todos"
                {
                    bool estadoBusqueda = (estadoFiltro == 1);
                    lista = medicoNeg.BuscarPorEstado(estadoBusqueda);
                }
                // 6. LISTAR TODOS (ningún filtro aplicado)
                else
                {
                    lista = medicoNeg.ListarMedicos();
                }

                GridViewMedico.DataSource = lista;
                GridViewMedico.DataBind();

                if (lista == null || lista.Count == 0)
                {
                    lblMensaje.Text = "ℹ️ No se encontraron médicos con los criterios de búsqueda.";
                    lblMensaje.ForeColor = Color.DarkOrange;
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Visible = true;
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDNI.Text = "";
            txtBuscarLegajo.Text = "";
            ddlEspecialidad.SelectedIndex = 0;
            ddlEstado.SelectedValue = "-1"; // Ajuste: Seleccionar el valor "Todos"
            cblDiasAtencion.ClearSelection();
            lblMensaje.Visible = false;

            CargarListado();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Medico.aspx");
        }

        protected void GridViewMedico_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridViewMedico.PageIndex = e.NewPageIndex;
            CargarListado();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}
