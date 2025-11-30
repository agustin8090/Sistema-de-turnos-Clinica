using proyecto_final.Datos;
using proyecto_final.Entidad;
using proyecto_final.Negocio;
using System;
using System.Collections.Generic;
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
                CargarEspecialidades();
                CargarPacientes();
                cargarhorasddl();

            }
        }
        private void cargarhorasddl()
        {
            TimeSpan horaInicio = new TimeSpan(8, 0, 0); // 08:00
            TimeSpan horaFin = new TimeSpan(17, 0, 0);   // 17:00
            TimeSpan intervalo = new TimeSpan(0, 20, 0); // 20 minutos

            ddlHora.Items.Clear();
            ddlHora.Items.Add(new ListItem("-- Seleccionar Hora --", ""));

            for (TimeSpan hora = horaInicio; hora <= horaFin; hora = hora.Add(intervalo))
            {
                string horaFormateada = hora.ToString(@"hh\:mm");

                if (hora <= horaFin)
                {
                    ddlHora.Items.Add(new ListItem(horaFormateada, horaFormateada));
                }
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

                throw ex;
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                // Usar tu clase de datos existente
                Especialidad_clinica especialidadDatos = new Especialidad_clinica();
                List<Especialidad> listaEspecialidades = especialidadDatos.Listar();

                // Limpiar el DropDownList
                ddlEspecialidad.Items.Clear();

                // Agregar opción por defecto
                ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

                // Configurar el DataSource
                ddlEspecialidad.DataSource = listaEspecialidades;
                ddlEspecialidad.DataTextField = "Nombre";           // Nombre de la especialidad
                ddlEspecialidad.DataValueField = "IdEspecialidad";  // Id de la especialidad
                ddlEspecialidad.DataBind();

                // Volver a agregar la opción por defecto después del DataBind
                ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            }
            catch (Exception ex)
            {
                // Manejar error
                // Puedes mostrar un mensaje al usuario o registrar el error
                Response.Write("<script>alert('Error al cargar especialidades: " + ex.Message + "');</script>");
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            lblVal.Text = "Iniciando proceso...";
            lblVal.ForeColor = System.Drawing.Color.Blue;

            try
            {
                // Validar que los campos no estén vacíos
                if (ddlEspecialidad.SelectedValue == "0" || string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
                {
                    lblVal.Text = "✗ Debe seleccionar una especialidad";
                    lblVal.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (ddlMedico.SelectedValue == "0" || string.IsNullOrEmpty(ddlMedico.SelectedValue))
                {
                    lblVal.Text = "✗ Debe seleccionar un médico";
                    lblVal.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (ddlPaciente.SelectedValue == "0" || string.IsNullOrEmpty(ddlPaciente.SelectedValue))
                {
                    lblVal.Text = "✗ Debe seleccionar un paciente";
                    lblVal.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(txtFecha.Text))
                {
                    lblVal.Text = "✗ Debe seleccionar una fecha";
                    lblVal.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(ddlHora.Text))
                {
                    lblVal.Text = "✗ Debe seleccionar una hora";
                    lblVal.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                lblVal.Text = "Creando turno...";
                Turno tr = new Turno();
                Turno_negocio trn = new Turno_negocio();

                tr.idPaciente = Convert.ToInt32(ddlPaciente.SelectedValue);
                tr.idMedico = Convert.ToInt32(ddlMedico.SelectedValue); // Esto ahora será el Legajo
                tr.Fecha = Convert.ToDateTime(txtFecha.Text);
                tr.hora = TimeSpan.Parse(ddlHora.Text);
                tr.estado = true;

                trn.AgregarTurno(tr);

                lblVal.Text = "✓ Turno asignado correctamente";
                lblVal.ForeColor = System.Drawing.Color.Green;

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Turno asignado correctamente');",
                    true
                );

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                lblVal.Text = "✗ ERROR: " + ex.Message;
                lblVal.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LimpiarCampos()
        {
            ddlEspecialidad.SelectedIndex = 0;
            ddlMedico.Items.Clear();
            ddlMedico.Items.Insert(0, new ListItem("-- Seleccionar Médico --", "0"));
            ddlPaciente.SelectedIndex = 0;
            txtFecha.Text = "";
            ddlHora.Items.Clear();
           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);

            if(idEspecialidad != 0)
            {
                cargarMedicosPorEspecialidad(idEspecialidad);
            }
        }

        private void cargarMedicosPorEspecialidad(int idEspecialidad)
        {
            try
            {
                medico_clinica medico = new medico_clinica();
                List<medico> listaMedicos = medico.ListarPorEspecialidad(idEspecialidad);

                ddlMedico.Items.Clear();

                if (listaMedicos != null && listaMedicos.Count > 0)
                {
                    ddlMedico.DataSource = listaMedicos;
                    ddlMedico.DataTextField = "NombreCompleto"; // o "Nombre"
                    ddlMedico.DataValueField = "Legajo";  // CAMBIAR A LEGAJO
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
    } 
}