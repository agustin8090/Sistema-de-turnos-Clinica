using proyecto_final.Entidad;
using proyecto_final.Datos;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Agregar_Medico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            if (!validar_campos())
                return;

            medico m = new medico();

            m.legajo = int.Parse(txt_Legajo_Medico.Text.Trim());
            m.dni = txt_DNI_Medico.Text.Trim();
            m.nombre = txt_Nombre_Medico.Text.Trim();
            m.apellido = txt_Apellido_Medico.Text.Trim();
            m.sexo = ddl_Sexo_Medico.SelectedValue;
            m.nacionalidad = txt_Nacionalidad_Medico.Text.Trim();
            m.fecha_nacimiento = DateTime.Parse(txt_FechaNacimiento_Medico.Text);
            m.telefono = txt_Telefono_Medico.Text.Trim();
            m.direccion = txt_Direccion_Medico.Text.Trim();
            m.email = txt_Mail_Medico.Text.Trim();

            m.id_localidad = int.Parse(ddl_Localidad_Medico.SelectedValue);
            m.id_especialidad = int.Parse(ddl_Especialidad_Medico.SelectedValue);

            m.dias_atencion = obtener_dias_atencion();
            m.hora_inicio = TimeSpan.Parse(txt_HoraInicio_Medico.Text);
            m.hora_fin = TimeSpan.Parse(txt_HoraFin_Medico.Text);

            m.estado = true;

            try
            {
                medico_clinica datos = new medico_clinica();

                int id_usuario_creado = datos.crear_usuario(
                    txt_Usuario_Medico.Text.Trim(),
                    txt_Contraseña_Medico.Text.Trim()
                );

                m.id_usuario = id_usuario_creado;

                datos.agregar_medico(m);

                lbl_Mensaje.Text = "✔ Médico guardado correctamente";
                lbl_Mensaje.ForeColor = System.Drawing.Color.Green;
                lbl_Mensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lbl_Mensaje.Text = "❌ Error al guardar: " + ex.Message;
                lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                lbl_Mensaje.Visible = true;
            }
        }

        private bool validar_campos()
        {
            lbl_Mensaje.Visible = false;

            if (!int.TryParse(txt_Legajo_Medico.Text.Trim(), out _))
            {
                mostrar_error("El legajo debe ser un número válido.");
                return false;
            }

            if (!Regex.IsMatch(txt_DNI_Medico.Text.Trim(), @"^\d{7,8}$"))
            {
                mostrar_error("El DNI debe tener solo números (7 u 8 dígitos).");
                return false;
            }

            if (!Regex.IsMatch(txt_Nombre_Medico.Text.Trim(), @"^[A-Za-zÁÉÍÓÚáéíóúñÑ\s]+$"))
            {
                mostrar_error("El nombre solo puede contener letras.");
                return false;
            }

            if (!Regex.IsMatch(txt_Apellido_Medico.Text.Trim(), @"^[A-Za-zÁÉÍÓÚáéíóúñÑ\s]+$"))
            {
                mostrar_error("El apellido solo puede contener letras.");
                return false;
            }

            if (ddl_Sexo_Medico.SelectedValue == "")
            {
                mostrar_error("Debe seleccionar un sexo.");
                return false;
            }

            if (!DateTime.TryParse(txt_FechaNacimiento_Medico.Text, out _))
            {
                mostrar_error("Debe ingresar una fecha de nacimiento válida.");
                return false;
            }

            if (!Regex.IsMatch(txt_Mail_Medico.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mostrar_error("Debe ingresar un email válido.");
                return false;
            }

            if (!TimeSpan.TryParse(txt_HoraInicio_Medico.Text, out _))
            {
                mostrar_error("Debe ingresar una hora de inicio válida.");
                return false;
            }

            if (!TimeSpan.TryParse(txt_HoraFin_Medico.Text, out _))
            {
                mostrar_error("Debe ingresar una hora de fin válida.");
                return false;
            }

            if (txt_Contraseña_Medico.Text != txt_RepetirContraseña_Medico.Text)
            {
                mostrar_error("Las contraseñas no coinciden.");
                return false;
            }

            if (txt_Contraseña_Medico.Text.Trim().Length < 4)
            {
                mostrar_error("La contraseña debe tener al menos 4 caracteres.");
                return false;
            }

            if (ddl_Provincia_Medico.SelectedValue == "")
            {
                mostrar_error("Debe seleccionar una provincia.");
                return false;
            }

            if (ddl_Localidad_Medico.SelectedValue == "")
            {
                mostrar_error("Debe seleccionar una localidad.");
                return false;
            }

            if (ddl_Especialidad_Medico.SelectedValue == "")
            {
                mostrar_error("Debe seleccionar una especialidad.");
                return false;
            }

            return true;
        }

        private void mostrar_error(string mensaje)
        {
            lbl_Mensaje.Text = "⚠ " + mensaje;
            lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
            lbl_Mensaje.Visible = true;
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
    }
}
