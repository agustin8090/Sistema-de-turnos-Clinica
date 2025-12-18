using Datos;
using Entidad;
using Negocio;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final.Paginas
{
    public partial class pagina_Turnos_admin : System.Web.UI.Page
    {
        Turno_negocio turnoNegocio = new Turno_negocio();
        Paciente_negocio pacienteNegocio = new Paciente_negocio();
        Medico_negocio medicoNegocio = new Medico_negocio();
        Especialidad_negocio espNegocio = new Especialidad_negocio();

        public static string UltimoFiltro { get; set; } = "Todos";
        public static object ValorUltimoFiltro { get; set; } = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();

                CargarEspecialidades();
                ddlMedico.Items.Insert(0, new ListItem("-- Todos --", "0"));

                FiltrarYMostrarTurnos("Todos", null);
            }
        }

        private void CargarEspecialidades()
        {
            Especialidad_clinica datos = new Especialidad_clinica();
            ddlEspecialidad.DataSource = datos.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("-- Todas --", "0"));
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            ddlMedico.Items.Clear();
            ddlMedico.Items.Insert(0, new ListItem("-- Todos --", "0"));

            if (idEspecialidad == 0)
                return;

            medico_clinica medicoNegocio = new medico_clinica();
            ddlMedico.DataSource = medicoNegocio.ListarPorEspecialidad(idEspecialidad);
            ddlMedico.DataTextField = "NombreCompleto";
            ddlMedico.DataValueField = "Legajo";
            ddlMedico.DataBind();

            ddlMedico.Items.Insert(0, new ListItem("-- Todos --", "0"));
        }
        protected bool esAdmin
        {
            get
            {
                return Session["TipoUsuario"]?.ToString() == "Administrador";
            }
        }

        private void FiltrarYMostrarTurnos(string tipoFiltro, object valorFiltro)
        {
            List<Turno> turnosBase = turnoNegocio.ListarTodosLosTurnos();

            switch (tipoFiltro)
            {
                case "DNI":
                    string dni = valorFiltro.ToString();
                    turnosBase = turnosBase
                        .Where(t =>
                        {
                            var p = pacienteNegocio.TraerPacientePorId(t.idPaciente);
                            return p.DniPaciente.Contains(dni);
                        })
                        .ToList();
                    break;

                case "FECHA":
                    DateTime fecha = (DateTime)valorFiltro;
                    turnosBase = turnosBase
                        .Where(t => t.Fecha.Date == fecha.Date)
                        .ToList();
                    break;

                case "ESTADO":
                    bool estado = (bool)valorFiltro;
                    turnosBase = turnosBase
                        .Where(t => t.estado == estado)
                        .ToList();
                    break;

                case "ESPECIALIDAD":
                    dynamic datos = valorFiltro;
                    int idEspecialidad = datos.idEspecialidad;
                    int legajoMedico = datos.legajoMedico;

                    turnosBase = turnosBase
                        .Where(t =>
                        {
                            var m = medicoNegocio.BuscarPorLegajoTurnos(t.idMedico);
                            if (m == null || m.id_especialidad != idEspecialidad)
                                return false;

                            if (legajoMedico != 0)
                                return m.legajo == legajoMedico;

                            return true;
                        })
                        .ToList();
                    break;
            }

            var listaFinal = turnosBase.Select(t =>
            {
                var p = pacienteNegocio.TraerPacientePorId(t.idPaciente);
                var m = medicoNegocio.BuscarPorLegajoTurnos(t.idMedico);
                var e = espNegocio.TraerPorId(m.id_especialidad);

                return new
                {
                    IdTurno = t.idTurno,
                    Paciente = p.NombrePaciente + " " + p.ApellidoPaciente,
                    DniPaciente = p.DniPaciente,
                    Especialidad = e.Nombre,
                    Medico = m.NombreCompleto,
                    Fecha = t.Fecha,
                    Hora = t.hora.ToString(@"hh\:mm"),
                    Estado = t.estado
                };
            }).ToList();

            gvTurnos.DataSource = listaFinal;
            gvTurnos.DataBind();

            lblMensaje.Text = (listaFinal.Count == 0)
                ? "No se encontraron turnos con el filtro aplicado."
                : "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDniFiltro.Text.Trim();
                string fechaStr = txtFecha.Text;
                int estadoFiltro = int.Parse(ddlEstadoTurno.SelectedValue);

                int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                int legajoMedico = int.Parse(ddlMedico.SelectedValue);

                if (idEspecialidad != 0)
                {
                    FiltrarYMostrarTurnos("ESPECIALIDAD", new
                    {
                        idEspecialidad,
                        legajoMedico
                    });
                }
                else if (!string.IsNullOrEmpty(dni))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dni, @"^[0-9]+$"))
                        throw new Exception("El DNI solo puede contener números.");

                    FiltrarYMostrarTurnos("DNI", dni);
                }
                else if (!string.IsNullOrEmpty(fechaStr))
                {
                    if (!DateTime.TryParse(fechaStr, out DateTime fecha))
                        throw new Exception("Formato de fecha inválido.");

                    FiltrarYMostrarTurnos("FECHA", fecha);
                }
                else if (estadoFiltro != -1)
                {
                    bool estado = estadoFiltro == 1;
                    FiltrarYMostrarTurnos("ESTADO", estado);
                }
                else
                {
                    FiltrarYMostrarTurnos("Todos", null);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error: " + ex.Message;
                lblMensaje.ForeColor = Color.Red;
                gvTurnos.DataSource = null;
                gvTurnos.DataBind();
            }
        }

        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            txtDniFiltro.Text = "";
            txtFecha.Text = "";
            ddlEstadoTurno.SelectedValue = "-1";
            lblMensaje.Text = "";
            FiltrarYMostrarTurnos("Todos", null);
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            FiltrarYMostrarTurnos(UltimoFiltro, ValorUltimoFiltro);
        }

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Atender")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);

                turnoNegocio.CambiarEstadoTurno(idTurno);
                FiltrarYMostrarTurnos(UltimoFiltro, ValorUltimoFiltro);
            }
        }
        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));
                Button boton = (Button)e.Row.FindControl("btnAtender");

                if (estado)
                {
                    boton.Text = "Atendido";
                    boton.Enabled = false;
                    boton.CssClass = "btn-turno btn-atendido";
                }
                else
                {
                    boton.Text = "Atender";
                    boton.Enabled = true;
                    boton.CssClass = "btn-turno btn-atender";
                }
            }
        }

        protected void BtnvolverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}