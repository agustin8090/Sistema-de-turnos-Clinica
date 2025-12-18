using Datos;
using Entidad;
using Negocio;
using Negocio.Negocio;
using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Agregar_Medico : System.Web.UI.Page
    {
        Medico_negocio medNeg = new Medico_negocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                cargarProvincias();
                cargarEspecialidades();
            }
        }

        private void cargarProvincias()
        {
            Provincia_clinica datos = new Provincia_clinica();
            ddl_Provincia_Medico.DataSource = datos.Listar();
            ddl_Provincia_Medico.DataTextField = "Nombre";
            ddl_Provincia_Medico.DataValueField = "IdProvincia";
            ddl_Provincia_Medico.DataBind();

            ddl_Provincia_Medico.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        private void cargarLocalidades(int idProvincia)
        {
            Localidad_clinica datos = new Localidad_clinica();
            ddl_Localidad_Medico.DataSource = datos.ListarPorProvincia(idProvincia);
            ddl_Localidad_Medico.DataTextField = "Nombre";
            ddl_Localidad_Medico.DataValueField = "IdLocalidad";
            ddl_Localidad_Medico.DataBind();

            ddl_Localidad_Medico.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        private void cargarEspecialidades()
        {
            Especialidad_clinica datos = new Especialidad_clinica();
            ddl_Especialidad_Medico.DataSource = datos.Listar();
            ddl_Especialidad_Medico.DataTextField = "Nombre";
            ddl_Especialidad_Medico.DataValueField = "IdEspecialidad";
            ddl_Especialidad_Medico.DataBind();

            ddl_Especialidad_Medico.Items.Insert(0, new ListItem("-- Seleccionar Especialidad --", ""));
        }

        protected void ddl_Provincia_Medico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Provincia_Medico.SelectedValue != "")
            {
                int idProv = int.Parse(ddl_Provincia_Medico.SelectedValue);
                cargarLocalidades(idProv);
            }
            else
            {
                ddl_Localidad_Medico.Items.Clear();
                ddl_Localidad_Medico.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
            }
        }

        protected void btn_Agregar_Medico_Click(object sender, EventArgs e)
        {


            try
            {
                Usuario_negocio usuarioNeg = new Usuario_negocio();
                Medico_negocio medicoNeg = new Medico_negocio();

                medico m = new medico();

                if (string.IsNullOrWhiteSpace(txt_Legajo_Medico.Text))
                    throw new Exception("Debe ingresar el legajo");
                m.legajo = int.Parse(txt_Legajo_Medico.Text.Trim());

                m.dni = txt_DNI_Medico.Text.Trim();
                m.nombre = txt_Nombre_Medico.Text.Trim();
                m.apellido = txt_Apellido_Medico.Text.Trim();
                m.sexo = ddl_Sexo_Medico.SelectedValue;
                m.nacionalidad = txt_Nacionalidad_Medico.Text.Trim();

                if (string.IsNullOrWhiteSpace(txt_FechaNacimiento_Medico.Text))
                    throw new Exception("Debe ingresar la fecha de nacimiento");
                m.fecha_nacimiento = DateTime.Parse(txt_FechaNacimiento_Medico.Text);

                m.telefono = txt_Telefono_Medico.Text.Trim();
                m.direccion = txt_Direccion_Medico.Text.Trim();
                m.email = txt_Mail_Medico.Text.Trim();

                if (string.IsNullOrEmpty(ddl_Localidad_Medico.SelectedValue))
                    throw new Exception("Debe seleccionar una localidad");
                m.id_localidad = int.Parse(ddl_Localidad_Medico.SelectedValue);

                if (string.IsNullOrEmpty(ddl_Especialidad_Medico.SelectedValue))
                    throw new Exception("Debe seleccionar una especialidad");
                m.id_especialidad = int.Parse(ddl_Especialidad_Medico.SelectedValue);

                m.dias_atencion = obtener_dias_atencion();
                if (string.IsNullOrWhiteSpace(m.dias_atencion))
                    throw new Exception("Debe seleccionar al menos un día de atención");

                TimeSpan horaInicio;
                TimeSpan horaFin;

                if (!TimeSpan.TryParseExact(txt_HoraInicio_Medico.Text, @"hh\:mm", null, out horaInicio))
                    throw new Exception("La hora de inicio debe tener formato Hora:Minutos");

                if (!TimeSpan.TryParseExact(txt_HoraFin_Medico.Text, @"hh\:mm", null, out horaFin))
                    throw new Exception("La hora de fin debe tener formato Hora:Minutos");

                m.hora_inicio = horaInicio;
                m.hora_fin = horaFin;

                if (m.hora_fin <= m.hora_inicio)
                    throw new Exception("La hora de fin debe ser mayor a la hora de inicio");


                m.estado = true;

                string usuario = txt_Usuario_Medico.Text.Trim();
                string contraseña = txt_Contraseña_Medico.Text.Trim();

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new Exception("Debe ingresar un nombre de usuario");

                if (string.IsNullOrWhiteSpace(contraseña))
                    throw new Exception("Debe ingresar una contraseña");

                if (txt_Contraseña_Medico.Text.Trim() != txt_RepetirContraseña_Medico.Text.Trim())
                {
                    throw new Exception("La contraseña y la repetición no coinciden.");
                }

                usuarioNeg.ValidarUsuario(usuario, contraseña);
                medicoNeg.ValidarMedico(m);

                int idUsuario = usuarioNeg.CrearUsuario(usuario, contraseña, "Medico");
                m.id_usuario = idUsuario;

                medicoNeg.AgregarMedico(m);

                LimpiarCampos();

                lbl_Mensaje.Text = "✔ Médico guardado correctamente";
                lbl_Mensaje.ForeColor = Color.Green;
                lbl_Mensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lbl_Mensaje.Text = "❌ " + ex.Message;
                lbl_Mensaje.ForeColor = Color.Red;
                lbl_Mensaje.Visible = true;
                return;
            }
            
        }

        private string obtener_dias_atencion()
        {
            string dias = "";

            if (chk_Lunes.Checked) dias += "Lunes ";
            if (chk_Martes.Checked) dias += "Martes ";
            if (chk_Miercoles.Checked) dias += "Miércoles ";
            if (chk_Jueves.Checked) dias += "Jueves ";
            if (chk_Viernes.Checked) dias += "Viernes ";
            if (chk_Sabado.Checked) dias += "Sábado ";

            return dias.Trim();
        }

        protected void btn_Cancelar_Medico_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Medico.aspx");
        }

        private void LimpiarCampos()
        { 
            txt_Legajo_Medico.Text = "";
            txt_DNI_Medico.Text = "";
            txt_Nombre_Medico.Text = "";
            txt_Apellido_Medico.Text = "";
            ddl_Sexo_Medico.SelectedIndex = 0;
            txt_Nacionalidad_Medico.Text = "";
            txt_FechaNacimiento_Medico.Text = "";
            txt_Telefono_Medico.Text = "";
            txt_Direccion_Medico.Text = "";
            txt_Mail_Medico.Text = "";
            ddl_Provincia_Medico.SelectedIndex = 0;
            ddl_Localidad_Medico.Items.Clear();
            ddl_Localidad_Medico.Items.Add(new ListItem("-- Seleccionar --", ""));
            ddl_Especialidad_Medico.SelectedIndex = 0;

            chk_Lunes.Checked = false;
            chk_Martes.Checked = false;
            chk_Miercoles.Checked = false;
            chk_Jueves.Checked = false;
            chk_Viernes.Checked = false;
            chk_Sabado.Checked = false;

            txt_HoraInicio_Medico.Text = "";
            txt_HoraFin_Medico.Text = "";

            txt_Usuario_Medico.Text = "";
            txt_Contraseña_Medico.Text = "";
            txt_RepetirContraseña_Medico.Text = "";

            lbl_Mensaje.Visible = false;
            lbl_Mensaje.Text = "";  
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}
