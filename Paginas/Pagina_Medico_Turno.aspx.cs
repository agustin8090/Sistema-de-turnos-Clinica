using proyecto_final.Datos;
using proyecto_final.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class Pagina_Medico_Turno : System.Web.UI.Page
    {
        Turno_negocio turnoNegocio = new Turno_negocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMedico.Text = Session["NombreMedico"]?.ToString();

            CargarTurnos(null);
        }
        private void CargarTurnos(DateTime? fecha)
        {
            int legajo = Convert.ToInt32(Session["LegajoMedico"]);

            Turno_negocio turnoNegocio = new Turno_negocio();
            Paciente_clinica pacienteDatos = new Paciente_clinica();

            var lista = turnoNegocio.ListarTurnosMedico(legajo, fecha);

            var listaFinal = lista.Select(t =>
            {
                var p = pacienteDatos.ObtenerPacientePorId(t.idPaciente);

                return new
                {
                    Paciente = p.NombrePaciente + " " + p.ApellidoPaciente,
                    DniPaciente = p.DniPaciente,
                    Fecha = t.Fecha,
                    Hora = t.hora.ToString(@"hh\:mm")
                };
            }).ToList();

            gvTurnos.DataSource = listaFinal;
            gvTurnos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                CargarTurnos(null);
                return;
            }

            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            CargarTurnos(fecha);
        }

        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            CargarTurnos(null);
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;

            DateTime? fecha = string.IsNullOrEmpty(txtFecha.Text)
                ? (DateTime?)null
                : Convert.ToDateTime(txtFecha.Text);

            CargarTurnos(fecha);
        }

    }
}