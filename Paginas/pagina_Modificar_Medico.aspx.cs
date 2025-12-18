using Entidad;
using Datos;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Negocio.Negocio;

namespace proyecto_final
{
    public partial class pagina_Modificar_Medico : System.Web.UI.Page
    {
        medico_clinica datos = new medico_clinica();
        Medico_negocio medneg = new Medico_negocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAdmin.Text = Session["NombreAdmin"]?.ToString();
                cargarGridView();
                cargarSexo();
                cargarEspecialidades();
                cargarDiasAtencion();
                cargarProvincias();
            }
        }

        protected void gvmedico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Seleccionar";
                e.Row.Cells[1].Text = "ID";
                e.Row.Cells[2].Text = "Legajo";
                e.Row.Cells[3].Text = "DNI";
                e.Row.Cells[4].Text = "Nombre";
                e.Row.Cells[5].Text = "Apellido";
                e.Row.Cells[6].Text = "Nombre Completo";
                e.Row.Cells[7].Text = "Sexo";
                e.Row.Cells[8].Text = "Nacionalidad";
                e.Row.Cells[9].Text = "Fecha de Nacimiento";
                e.Row.Cells[10].Text = "Dirección";
                e.Row.Cells[11].Text = "Localidad";
                e.Row.Cells[12].Text = "Provincia";
                e.Row.Cells[13].Text = "ID Especialidad";
                e.Row.Cells[14].Text = "Especialidad";
                e.Row.Cells[15].Text = "Teléfono";
                e.Row.Cells[16].Text = "Email";
                e.Row.Cells[17].Text = "Días de Atención";
                e.Row.Cells[18].Text = "Horario Inicio";
                e.Row.Cells[19].Text = "Horario Fin";
                e.Row.Cells[20].Text = "Estado";
                e.Row.Cells[21].Text = "Usuario";
            }
        }

        private void cargarGridView()
        {
            gvmedico.AutoGenerateSelectButton = true;
            gvmedico.DataSource = datos.ListarMedicos();
            gvmedico.DataBind();
        }

        private void cargarProvincias()
        {
            Provincia_clinica p = new Provincia_clinica();
            ddlprovincia.DataSource = p.Listar();
            ddlprovincia.DataTextField = "Nombre";
            ddlprovincia.DataValueField = "IdProvincia";
            ddlprovincia.DataBind();

            ddlprovincia.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        protected void ddlprovincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlprovincia.SelectedValue != "")
            {
                Localidad_clinica l = new Localidad_clinica();
                ddllocalidad.DataSource = l.ListarPorProvincia(int.Parse(ddlprovincia.SelectedValue));
                ddllocalidad.DataTextField = "Nombre";
                ddllocalidad.DataValueField = "IdLocalidad";
                ddllocalidad.DataBind();
            }

            ddllocalidad.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        private void cargarEspecialidades()
        {
            Especialidad_clinica esp = new Especialidad_clinica();
            ddlespecialidad.DataSource = esp.Listar();
            ddlespecialidad.DataTextField = "Nombre";
            ddlespecialidad.DataValueField = "IdEspecialidad";
            ddlespecialidad.DataBind();

        }

        private void cargarSexo()
        {
            ddlsexo.Items.Add(new ListItem("Masculino", "M"));
            ddlsexo.Items.Add(new ListItem("Femenino", "F"));
        }

        private void cargarDiasAtencion()
        {
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

            CheckBoxList1.Items.Add(new ListItem(dias[0]));
            CheckBoxList2.Items.Add(new ListItem(dias[1]));
            CheckBoxList3.Items.Add(new ListItem(dias[2]));
            CheckBoxList4.Items.Add(new ListItem(dias[3]));
            CheckBoxList5.Items.Add(new ListItem(dias[4]));
            CheckBoxList6.Items.Add(new ListItem(dias[5]));
        }

        protected void gvmedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvmedico.SelectedRow;

            int legajo = Convert.ToInt32(gvmedico.DataKeys[gvmedico.SelectedIndex].Value);
            ViewState["LegajoMedico"] = legajo;
            medico m = datos.BuscarPorLegajo(legajo);


            txtdni.Text = m.dni;
            txtnombre.Text = m.nombre;
            txtapellido.Text = m.apellido;
            ddlsexo.SelectedValue = m.sexo;
            txtnacionalidad.Text = m.nacionalidad;
            txtfcn.Text = m.fecha_nacimiento.ToString("yyyy-MM-dd");
            TextBoxmail.Text = m.email;

            txtdireccion.Text = m.direccion;
            txttelefono.Text = m.telefono;


            ddlprovincia.SelectedValue = m.id_provincia.ToString();

            Localidad_clinica l = new Localidad_clinica();
            ddllocalidad.DataSource = l.ListarPorProvincia(m.id_provincia);
            ddllocalidad.DataTextField = "Nombre";
            ddllocalidad.DataValueField = "IdLocalidad";
            ddllocalidad.DataBind();

            ddllocalidad.SelectedValue = m.id_localidad.ToString();


            ddlespecialidad.SelectedValue = m.id_especialidad.ToString();


            txthorainicio.Text = m.hora_inicio.HasValue ? m.hora_inicio.Value.ToString(@"hh\:mm") : "";
            txthorafin.Text = m.hora_fin.HasValue ? m.hora_fin.Value.ToString(@"hh\:mm") : "";


            marcarDias(m.dias_atencion);
        }

        private void marcarDias(string dias)
        {
            foreach (var lista in new[] { CheckBoxList1, CheckBoxList2, CheckBoxList3, CheckBoxList4, CheckBoxList5, CheckBoxList6 })
                foreach (ListItem item in lista.Items)
                    item.Selected = dias.Contains(item.Text);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            medico m = new medico();


            if (ViewState["LegajoMedico"] == null)
            {
                Response.Write("No hay legajo");
                return;
            }

            m.legajo = (int)ViewState["LegajoMedico"];


            m.dni = txtdni.Text;
            m.nombre = txtnombre.Text;
            m.apellido = txtapellido.Text;
            m.sexo = ddlsexo.SelectedValue;
            m.nacionalidad = txtnacionalidad.Text;
            m.fecha_nacimiento = DateTime.Parse(txtfcn.Text);
            m.email = TextBoxmail.Text;

            m.direccion = txtdireccion.Text;
            m.telefono = txttelefono.Text;
            m.id_provincia = int.Parse(ddlprovincia.SelectedValue);
            m.id_localidad = int.Parse(ddllocalidad.SelectedValue);


            m.id_especialidad = int.Parse(ddlespecialidad.SelectedValue);
            m.hora_inicio = TimeSpan.Parse(txthorainicio.Text);
            m.hora_fin = TimeSpan.Parse(txthorafin.Text);
            m.dias_atencion = obtenerDiasSeleccionados();
            string error;

            bool exito = medneg.ModificarMedico(m, out error);
            if (!exito)
            {
                lblerror.Text = error;
                lblerror.ForeColor = System.Drawing.Color.Red;
                lblerror.Visible = true;
                return;
            }
            lblerror.Text = "Médico modificado correctamente";
            lblerror.ForeColor = System.Drawing.Color.Green;
            lblerror.Visible = true;

            cargarGridView();
            LimpiarCampos();
        }

        private string obtenerDiasSeleccionados()
        {
            string dias = "";

            foreach (var lista in new[] { CheckBoxList1, CheckBoxList2, CheckBoxList3, CheckBoxList4, CheckBoxList5, CheckBoxList6 })
                foreach (ListItem item in lista.Items)
                    if (item.Selected)
                        dias += item.Text + " ";

            return dias.Trim();
        }

        protected void gvmedico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmedico.PageIndex = e.NewPageIndex;
            cargarGridView();
        }
        private void LimpiarCampos()
        {
            txtdni.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            ddlsexo.SelectedIndex = 0;
            txtnacionalidad.Text = "";
            txtfcn.Text = "";
            txtdireccion.Text = "";
            ddlprovincia.SelectedIndex = 0;
            ddllocalidad.Items.Clear();
            ddllocalidad.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            txttelefono.Text = "";
            TextBoxmail.Text = "";
            ddlespecialidad.SelectedIndex = 0;


            CheckBoxList1.ClearSelection();
            CheckBoxList2.ClearSelection();
            CheckBoxList3.ClearSelection();
            CheckBoxList4.ClearSelection();
            CheckBoxList5.ClearSelection();
            CheckBoxList6.ClearSelection();

            txthorainicio.Text = "";
            txthorafin.Text = "";

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDNI.Text = "";
            lblMensaje.Text = "";
            cargarGridView();
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
                var lista = medneg.BuscarPorDni(dni);

                if (lista.Count == 0)
                {
                    lblMensaje.Text = "No se encontró ningún médico con ese DNI.";
                    gvmedico.DataSource = null;
                    gvmedico.DataBind();
                    return;
                }

                gvmedico.DataSource = lista;
                gvmedico.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void LimpiarCheckBoxList(CheckBoxList cbl)
        {
            foreach (ListItem item in cbl.Items)
            {
                item.Selected = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtdni.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtnacionalidad.Text = "";
            txtfcn.Text = "";

            ddlsexo.SelectedIndex = 0;

            txtdireccion.Text = "";
            txttelefono.Text = "";
            TextBoxmail.Text = "";

            ddlprovincia.SelectedIndex = 0;

            ddllocalidad.Items.Clear();
            ddllocalidad.Items.Add(new ListItem("-- Seleccione --", "0"));

            ddlespecialidad.SelectedIndex = 0;

            // Días de atención (CheckBoxList)
            LimpiarCheckBoxList(CheckBoxList1);
            LimpiarCheckBoxList(CheckBoxList2);
            LimpiarCheckBoxList(CheckBoxList3);
            LimpiarCheckBoxList(CheckBoxList4);
            LimpiarCheckBoxList(CheckBoxList5);
            LimpiarCheckBoxList(CheckBoxList6);

            txthorainicio.Text = "";
            txthorafin.Text = "";

            lblerror.Visible = false;
            lblerror.Text = "";
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }

        protected void Volver_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_Medico.aspx");
        }
    }
}


