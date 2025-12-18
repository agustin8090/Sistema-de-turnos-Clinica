using Datos;
using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class Pagina_Medico_Turno : System.Web.UI.Page
    {
        Turno_negocio turnoNegocio = new Turno_negocio();
        Paciente_negocio pacienteNegocio = new Paciente_negocio();

        public static string UltimoFiltro { get; set; } = "Todos";
        public static object ValorUltimoFiltro { get; set; } = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMedico.Text = Session["NombreMedico"]?.ToString();
                FiltrarYMostrarTurnos("Todos", null);
            }
        }
        private void FiltrarYMostrarTurnos(string tipoFiltro, object valorFiltro)
        {
            int legajoMedico = Convert.ToInt32(Session["LegajoMedico"]);
            List<Turno> turnosBase = new List<Turno>();

            UltimoFiltro = tipoFiltro;
            ValorUltimoFiltro = valorFiltro;

            switch (tipoFiltro)
            {
                case "DNI":
                    string dni = valorFiltro.ToString();
                    if (!string.IsNullOrEmpty(dni))
                        turnosBase = turnoNegocio.ListarTurnosPorDni(legajoMedico, dni);
                    break;

                case "FECHA":
                    DateTime fecha = (DateTime)valorFiltro;
                    turnosBase = turnoNegocio.ListarTurnosMedico(legajoMedico, fecha);
                    break;

                case "ESTADO":
                    bool estado = (bool)valorFiltro;
                    turnosBase = turnoNegocio.ListarTurnosPorEstado(legajoMedico, estado);
                    break;

                case "Todos":
                default:
                    turnosBase = turnoNegocio.ListarTurnosMedico(legajoMedico, null);
                    break;
            }

            var listaFinal = turnosBase.Select(t =>
            {
                var p = pacienteNegocio.TraerPacientePorId(t.idPaciente);

                return new
                {
                    IdTurno = t.idTurno,
                    Paciente = p.NombrePaciente + " " + p.ApellidoPaciente,
                    DniPaciente = p.DniPaciente,
                    Fecha = t.Fecha,
                    Hora = t.hora.ToString(@"hh\:mm"),
                    Estado = t.estado 
                };
            }).ToList();

            gvTurnos.DataSource = listaFinal;
            gvTurnos.DataBind();

            if (listaFinal.Count == 0 && tipoFiltro != "Todos")
            {
                lblMensaje.Text = "No se encontraron turnos con el filtro aplicado.";
                lblMensaje.ForeColor = Color.Blue;
            }
            else
            {
                lblMensaje.Text = "";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDniFiltro.Text.Trim();
                string fechaStr = txtFecha.Text;
                int estadoFiltro = int.Parse(ddlEstadoTurno.SelectedValue);

                if (!string.IsNullOrEmpty(dni))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dni, @"^[0-9]+$"))
                    {
                        throw new Exception("El DNI solo puede contener números.");
                    }
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

                txtDniFiltro.Text = (!string.IsNullOrEmpty(dni) ? dni : "");
                txtFecha.Text = (!string.IsNullOrEmpty(fechaStr) ? fechaStr : "");
                ddlEstadoTurno.SelectedValue = estadoFiltro.ToString();
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
            }

            FiltrarYMostrarTurnos(UltimoFiltro, ValorUltimoFiltro);
        }


        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Estado"));

                Button btnAtender = e.Row.FindControl("btnAtender") as Button;
                if (btnAtender == null) return;

                if (!estado)
                {
                    btnAtender.Text = "Atender";
                    btnAtender.Enabled = true;
                    btnAtender.CssClass = "btn-turno btn-atender";
                }
                else
                {
                    btnAtender.Text = "Atendido";
                    btnAtender.Enabled = false;
                    btnAtender.CssClass = "btn-turno btn-atendido";
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_medico.aspx");
        }
    }
}