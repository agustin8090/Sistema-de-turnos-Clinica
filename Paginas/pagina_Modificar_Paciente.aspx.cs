using Datos;
using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Paciente_negocio pacNeg = new Paciente_negocio();
        Provincia_negocio provNeg = new Provincia_negocio();
        Localidad_negocio locNeg = new Localidad_negocio();

        private int IdPacienteSeleccionado
        {
            get => (int)(ViewState["IdPacienteSeleccionado"] ?? -1);
            set => ViewState["IdPacienteSeleccionado"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                CargarSexo();
                CargarProvincias();
                ddl_Localidad_Paciente.Items.Insert(0, new ListItem("Seleccione localidad...", ""));
                CargarGrillaPacientes();
            }
        }

        private void CargarSexo()
        {
            ddl_Sexo_Paciente.Items.Clear();
            ddl_Sexo_Paciente.Items.Add(new ListItem("Masculino", "M"));
            ddl_Sexo_Paciente.Items.Add(new ListItem("Femenino", "F"));
        }

        private void CargarProvincias()
        {
            ddl_Provincia_Paciente.DataSource = provNeg.ListarProvincias();
            ddl_Provincia_Paciente.DataTextField = "Nombre";
            ddl_Provincia_Paciente.DataValueField = "IdProvincia";
            ddl_Provincia_Paciente.DataBind();
            ddl_Provincia_Paciente.Items.Insert(0, new ListItem("Seleccione provincia...", ""));
        }

        private void CargarLocalidades(int idProvincia)
        {
            ddl_Localidad_Paciente.Items.Clear();
            ddl_Localidad_Paciente.Items.Insert(0, new ListItem("Seleccione localidad...", ""));

            if (idProvincia > 0)
            {
                ddl_Localidad_Paciente.DataSource = locNeg.ListarLocalidadesPorProvincia(idProvincia);
                ddl_Localidad_Paciente.DataTextField = "Nombre";
                ddl_Localidad_Paciente.DataValueField = "IdLocalidad";
                ddl_Localidad_Paciente.DataBind();
            }
        }

        protected void ddl_Provincia_Paciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddl_Provincia_Paciente.SelectedValue, out int idProv))
                CargarLocalidades(idProv);
        }

        private void CargarGrillaPacientes()
        {
            GridViewPaciente.DataSource = pacNeg.ListarPacientes();
            GridViewPaciente.DataBind();
        }

        protected void GridViewPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridViewPaciente.DataKeys[GridViewPaciente.SelectedIndex].Value);

                IdPacienteSeleccionado = id;

                Paciente pac = pacNeg.TraerPacientePorId(id);

                if (pac == null)
                    throw new Exception("No se encontró el paciente.");

                Text_Nombre_Paciente.Text = pac.NombrePaciente;
                Text_Apellido_Paciente.Text = pac.ApellidoPaciente;
                Text_DNI_Paciente.Text = pac.DniPaciente;
                Text_Nacionalidad_Paciente.Text = pac.NacionalidadPac;
                Text_FechaNacimiento_Paciente.Text = pac.FechaNacimiento.ToString("yyyy-MM-dd");
                Text_Direccion_Paciente.Text = pac.DireccionPac;
                Text_Mail_Paciente.Text = pac.EmailPac;
                Text_Telefono_Paciente.Text = pac.TelefonoPac;

                ddl_Sexo_Paciente.SelectedValue = pac.SexoPaciente;
                ddl_Estado_Paciente.SelectedValue = pac.Estado ? "1" : "0";

                ddl_Provincia_Paciente.SelectedValue = pac.IdProvincia.ToString();
                CargarLocalidades(pac.IdProvincia);
                ddl_Localidad_Paciente.SelectedValue = pac.IdLocalidad.ToString();
            }
            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message;
                Label1.ForeColor = System.Drawing.Color.Red;    
            }
        }

        protected void Modificar_Paciente_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente p = new Paciente
                {
                    IdPaciente = IdPacienteSeleccionado,
                    DniPaciente = Text_DNI_Paciente.Text.Trim(),
                    NombrePaciente = Text_Nombre_Paciente.Text.Trim(),
                    ApellidoPaciente = Text_Apellido_Paciente.Text.Trim(),
                    SexoPaciente = ddl_Sexo_Paciente.SelectedValue,
                    NacionalidadPac = Text_Nacionalidad_Paciente.Text.Trim(),
                    FechaNacimiento = Convert.ToDateTime(Text_FechaNacimiento_Paciente.Text),
                    DireccionPac = Text_Direccion_Paciente.Text.Trim(),
                    IdProvincia = Convert.ToInt32(ddl_Provincia_Paciente.SelectedValue),
                    IdLocalidad = Convert.ToInt32(ddl_Localidad_Paciente.SelectedValue),
                    EmailPac = Text_Mail_Paciente.Text.Trim(),
                    TelefonoPac = Text_Telefono_Paciente.Text.Trim(),
                    Estado = ddl_Estado_Paciente.SelectedValue == "1"
                };

                pacNeg.ModificarPaciente(p);
                LimpiarCampos();

                lblMensajeModificacion.Text = "Paciente modificado correctamente.";
                lblMensajeModificacion.ForeColor = System.Drawing.Color.Green;
                lblMensajeModificacion.Visible = true;

                CargarGrillaPacientes();

            }
            catch (Exception ex)
            {
                lblMensajeModificacion.Text = "Error: " + ex.Message;
                lblMensajeModificacion.ForeColor = System.Drawing.Color.Red;
                lblMensajeModificacion.Visible = true;
            }
        }

        protected void Volver_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_paciente.aspx");
        }

        protected void GridViewPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPaciente.PageIndex = e.NewPageIndex;
            CargarGrillaPacientes();
        }
        private void LimpiarCampos()
        {
            Text_Nombre_Paciente.Text = "";
            Text_Apellido_Paciente.Text = "";
            Text_DNI_Paciente.Text = "";
            ddl_Sexo_Paciente.SelectedIndex = 0;
            Text_Nacionalidad_Paciente.Text = "";
            Text_FechaNacimiento_Paciente.Text = "";
            Text_Direccion_Paciente.Text = "";
            ddl_Provincia_Paciente.SelectedIndex = 0;

            ddl_Localidad_Paciente.Items.Clear();
            ddl_Localidad_Paciente.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

            Text_Mail_Paciente.Text = "";
            Text_Telefono_Paciente.Text = "";
            ddl_Estado_Paciente.SelectedIndex = 0;

            lblMensajeModificacion.Visible = false;
            lblMensajeModificacion.Text = "";
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDNI.Text = "";
            lblMensaje.Text = "";
            CargarGrillaPacientes();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            string dni = txtBuscarDNI.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                lblMensaje.Text = "Ingrese un DNI para buscar.";
                return;
            }

            try
            {
                var lista = pacNeg.ListarPorDni(dni);

                if (lista.Count == 0)
                {
                    lblMensaje.Text = "No se encontró ningún médico con ese DNI.";
                    GridViewPaciente.DataSource = null;
                    GridViewPaciente.DataBind();
                    return;
                }

                GridViewPaciente.DataSource = lista;
                GridViewPaciente.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }

        protected void Cancelar_Paciente_Click(object sender, EventArgs e)
        {
            Text_Nombre_Paciente.Text = "";
            Text_Apellido_Paciente.Text = "";
            Text_DNI_Paciente.Text = "";
            Text_Nacionalidad_Paciente.Text = "";
            Text_FechaNacimiento_Paciente.Text = "";
            Text_Direccion_Paciente.Text = "";
            Text_Mail_Paciente.Text = "";
            Text_Telefono_Paciente.Text = "";

            ddl_Sexo_Paciente.SelectedIndex = 0;
            ddl_Provincia_Paciente.SelectedIndex = 0;

            ddl_Localidad_Paciente.Items.Clear();
            ddl_Localidad_Paciente.Items.Add(new ListItem("-- Seleccione --", "0"));

            ddl_Estado_Paciente.SelectedValue = "1"; 

            Label1.Text = "";
        }
    }

}