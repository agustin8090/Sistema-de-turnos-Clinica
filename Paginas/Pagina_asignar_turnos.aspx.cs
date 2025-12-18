using Datos;
using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class Pagina_asignar_turnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                CargarEspecialidades();
                CargarPacientes();
                CargarHorasDdl();
            }
        }

        private void CargarHorasDdl()
        {
            TimeSpan horaInicio = new TimeSpan(8, 0, 0); // 08:00
            TimeSpan horaFin = new TimeSpan(17, 0, 0);   // 17:00
            TimeSpan intervalo = new TimeSpan(0, 20, 0); // 20 minutos

            ddlHora.Items.Clear();
            ddlHora.Items.Add(new ListItem("-- Seleccionar Hora --", ""));

            for (TimeSpan hora = horaInicio; hora <= horaFin; hora = hora.Add(intervalo))
            {
                string horaFormateada = hora.ToString(@"hh\:mm");
                ddlHora.Items.Add(new ListItem(horaFormateada, horaFormateada));
            }
        }

        private void CargarPacientes()
        {
            try
            {
                Paciente_clinica paciente = new Paciente_clinica();
                List<Paciente> listaPacientes = paciente.Listarpacientes();

                ddlPaciente.Items.Clear();

                ddlPaciente.DataSource = listaPacientes;
                ddlPaciente.DataTextField = "NombreCompleto";
                ddlPaciente.DataValueField = "IdPaciente";
                ddlPaciente.DataBind();

                ddlPaciente.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar pacientes: " + ex.Message + "');</script>");
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                Especialidad_clinica especialidadDatos = new Especialidad_clinica();
                List<Especialidad> listaEspecialidades = especialidadDatos.Listar();

                ddlEspecialidad.Items.Clear();

                ddlEspecialidad.DataSource = listaEspecialidades;
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataBind();

                ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar especialidades: " + ex.Message + "');</script>");
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            lblVal.Text = "";
            lblVal.CssClass = "message-error";
            lblVal.Style["display"] = "block"; 

            if (string.IsNullOrWhiteSpace(ddlEspecialidad.SelectedValue) || ddlEspecialidad.SelectedValue == "0")
            {
                lblVal.Text = "✗ Debe seleccionar una especialidad";
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlMedico.SelectedValue) || ddlMedico.SelectedValue == "0")
            {
                lblVal.Text = "✗ Debe seleccionar un médico";
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlPaciente.SelectedValue) || ddlPaciente.SelectedValue == "0")
            {
                lblVal.Text = "✗ Debe seleccionar un paciente";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                lblVal.Text = "✗ Debe seleccionar una fecha";
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlHora.SelectedValue))
            {
                lblVal.Text = "✗ Debe seleccionar una hora";
                return;
            }

            try
            {
                Turno tr = new Turno
                {
                    idPaciente = Convert.ToInt32(ddlPaciente.SelectedValue),
                    idMedico = Convert.ToInt32(ddlMedico.SelectedValue),
                    Fecha = Convert.ToDateTime(txtFecha.Text),
                    hora = TimeSpan.Parse(ddlHora.SelectedValue),
                    estado = false
                };

                Turno_negocio trn = new Turno_negocio();
                trn.AgregarTurno(tr);


                lblVal.CssClass = "message-success";
                lblVal.Text = "✓ Turno asignado correctamente";

                LimpiarCampos(); 
            }
            catch (Exception ex)
            {
                lblVal.CssClass = "message-error";
                lblVal.Text = "✗ ERROR, TURNO NO ASIGNADO: " + ex.Message;
            }
        }

        private void LimpiarCampos()
        {
            ddlEspecialidad.SelectedIndex = 0;

            ddlMedico.Items.Clear();
            ddlMedico.Items.Insert(0, new ListItem("-- Seleccionar Médico --", ""));

            ddlPaciente.SelectedIndex = 0;

            txtFecha.Text = "";

            ddlHora.Items.Clear();
            ddlHora.Items.Add(new ListItem("-- Seleccionar Hora --", ""));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);

            if (idEspecialidad != 0)
            {
                CargarMedicosPorEspecialidad(idEspecialidad);
            }
            else
            {
                ddlMedico.Items.Clear();
                ddlMedico.Items.Insert(0, new ListItem("-- Seleccionar Médico --", ""));
            }
        }

        private void CargarMedicosPorEspecialidad(int idEspecialidad)
        {
            try
            {
                medico_clinica medicoDatos = new medico_clinica();
                List<medico> listaMedicos = medicoDatos.ListarPorEspecialidad(idEspecialidad);

                ddlMedico.Items.Clear();

                if (listaMedicos != null && listaMedicos.Count > 0)
                {
                    ddlMedico.DataSource = listaMedicos;
                    ddlMedico.DataTextField = "NombreCompleto";
                    ddlMedico.DataValueField = "Legajo";
                    ddlMedico.DataBind();

                    ddlMedico.Items.Insert(0, new ListItem("-- Seleccionar Médico --", "0"));
                }
                else
                {
                    ddlMedico.Items.Insert(0, new ListItem("-- No hay médicos disponibles --", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHora.Items.Clear();
            ddlHora.Items.Add(new ListItem("-- Seleccionar Hora --", ""));

            if (ddlMedico.SelectedValue == "" || ddlMedico.SelectedValue == "0")
                return;

            int legajo = int.Parse(ddlMedico.SelectedValue);

            string connectionString = @"Data Source=localhost\SQLEXPRESS; Initial Catalog=CLINICA; Integrated Security=true";
            string query = "SELECT HoraInicio, HoraFin FROM Medico WHERE Legajo = @Legajo";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@Legajo", legajo);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    TimeSpan inicio = (TimeSpan)dr["HoraInicio"];
                    TimeSpan fin = (TimeSpan)dr["HoraFin"];

                    for (TimeSpan h = inicio; h < fin; h = h.Add(TimeSpan.FromHours(1)))
                    {
                        ddlHora.Items.Add(new ListItem(h.ToString(@"hh\:mm"), h.ToString(@"hh\:mm")));
                    }
                }
            }
        }
    }
}
