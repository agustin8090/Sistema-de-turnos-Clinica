using proyecto_final.Entidad;
using proyecto_final.Datos;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Modificar_Medico : System.Web.UI.Page
    {
        medico_clinica datos = new medico_clinica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGridView();
                cargarSexo();
                cargarEspecialidades();
                cargarDiasAtencion();
                cargarProvincias();
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

            int legajo = int.Parse(row.Cells[1].Text);  
            medico m = datos.BuscarPorLegajo(legajo);


            txtdni.Text = m.dni;
            txtnombre.Text = m.nombre;
            txtapellido.Text = m.apellido;
            ddlsexo.SelectedValue = m.sexo;
            txtnacionalidad.Text = m.nacionalidad;
            txtfcn.Text = m.fecha_nacimiento.ToString("yyyy-MM-dd");


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

            m.legajo = int.Parse(gvmedico.SelectedRow.Cells[1].Text);

            
            m.dni = txtdni.Text;
            m.nombre = txtnombre.Text;
            m.apellido = txtapellido.Text;
            m.sexo = ddlsexo.SelectedValue;
            m.nacionalidad = txtnacionalidad.Text;
            m.fecha_nacimiento = DateTime.Parse(txtfcn.Text);

            m.direccion = txtdireccion.Text;
            m.telefono = txttelefono.Text;
            m.id_provincia = int.Parse(ddlprovincia.SelectedValue);
            m.id_localidad = int.Parse(ddllocalidad.SelectedValue);

         
            m.id_especialidad = int.Parse(ddlespecialidad.SelectedValue);
            m.hora_inicio = TimeSpan.Parse(txthorainicio.Text);
            m.hora_fin = TimeSpan.Parse(txthorafin.Text);
            m.dias_atencion = obtenerDiasSeleccionados();

            datos.modificar_medico(m);

            cargarGridView();
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
    }
}


