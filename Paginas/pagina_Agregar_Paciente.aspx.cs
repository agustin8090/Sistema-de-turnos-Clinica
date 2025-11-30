using proyecto_final.Datos;
using proyecto_final.Entidad;
using proyecto_final.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class pagina_Agregar_Paciente : System.Web.UI.Page
    {
        Paciente_negocio pacNeg = new Paciente_negocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProvincias();

                CargarSexo();
                ddl_Localidad_Paciente.Items.Clear();

            }
        }
        protected void CargarSexo()
        {
            ddl_Sexo_Paciente.Items.Clear();
            ddl_Sexo_Paciente.Items.Add(new ListItem("Masculino", "M"));
            ddl_Sexo_Paciente.Items.Add(new ListItem("Femenino", "F"));
        }
        protected void CargarProvincias()
        {
            Provincia_clinica pr = new Provincia_clinica();
            ddl_Provincia_Paciente.DataSource = pr.Listar();
            ddl_Provincia_Paciente.DataTextField = "Nombre";
            ddl_Provincia_Paciente.DataValueField = "IdProvincia";
            ddl_Provincia_Paciente.DataBind();

            ddl_Provincia_Paciente.Items.Insert(0, new ListItem("Seleccione provincia...", ""));
        }
        protected void CargarLocalidades()
        {
            if (string.IsNullOrEmpty(ddl_Provincia_Paciente.SelectedValue))
            {
                ddl_Localidad_Paciente.Items.Clear();
                ddl_Localidad_Paciente.Items.Insert(0, new ListItem("Seleccione localidad...", ""));
                return;
            }

            Localidad_clinica localidad = new Localidad_clinica();
            ddl_Localidad_Paciente.DataSource = localidad.ListarPorProvincia(int.Parse(ddl_Provincia_Paciente.SelectedValue));
            ddl_Localidad_Paciente.DataTextField = "Nombre";
            ddl_Localidad_Paciente.DataValueField = "IdLocalidad";
            ddl_Localidad_Paciente.DataBind();

            ddl_Localidad_Paciente.Items.Insert(0, new ListItem("Seleccione localidad...", ""));
        }

        protected void Agregar_Paciente_Click(object sender, EventArgs e)
        {
            //VALIDACIONES DDL PARA EVITAR EXPLOSIONES
            if (string.IsNullOrEmpty(ddl_Localidad_Paciente.SelectedValue))
            {
                Label1.ForeColor= System.Drawing.Color.Red;
                Label1.Text = "Completar los campos";
                return;        
                    }
            if (string.IsNullOrEmpty(ddl_Provincia_Paciente.SelectedValue))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Completar los campos";
                return;
            }
            //CREAMOS OBJETO Y SE VAN GUARDANDO
            Paciente pac = new Paciente();
            pac.NombrePaciente = Text_Nombre_Paciente.Text.Trim();
            pac.ApellidoPaciente = Text_Apellido_Paciente.Text.Trim();
            pac.DniPaciente = Text_DNI_Paciente.Text.Trim();
            pac.SexoPaciente = ddl_Sexo_Paciente.SelectedValue;
            pac.NacionalidadPac = Text_Nacionalidad_Paciente.Text.Trim();
            DateTime fecha;
            if (!DateTime.TryParse(Text_FechaNacimiento_Paciente.Text, out fecha))
            {
                return;
            }
            pac.FechaNacimiento = fecha;
            pac.DireccionPac = Text_Direccion_Paciente.Text.Trim();
            
            pac.IdLocalidad = int.Parse(ddl_Localidad_Paciente.SelectedValue);
            pac.IdProvincia = int.Parse(ddl_Provincia_Paciente.SelectedValue);
            pac.EmailPac = Text_Mail_Paciente.Text.Trim();
            pac.TelefonoPac = Text_Telefono_Paciente.Text.Trim();

            try
            {
                //LLAMAMOS AL GUARDAR PACIENTE PARA VALIDAR EL RESTO DE CAMPOS, SI ESTA TODO CORRECTO SE INSERTA EN LA BASE DE DATOS
               
                pacNeg.GuardarPaciente(pac);
                Text_Apellido_Paciente.Text = "";
                Text_Nombre_Paciente.Text = "";
                Text_DNI_Paciente.Text = "";
                ddl_Sexo_Paciente.SelectedIndex = 0;
                Text_Nacionalidad_Paciente.Text = "";
                Text_FechaNacimiento_Paciente.Text = "";
                Text_Direccion_Paciente.Text = "";
                ddl_Localidad_Paciente.SelectedIndex = 0;
                ddl_Provincia_Paciente.SelectedIndex = 0;
                Text_Mail_Paciente.Text = "";
                Text_Telefono_Paciente.Text = "";
            }
            catch (Exception ex)
            {

                Label1.Text = "⚠ " + ex.Message;
                Label1.ForeColor = System.Drawing.Color.Red;

            }
        }

       

        protected void ddl_Provincia_Paciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades();
        }

        protected void VolverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_ABML_paciente.aspx");
        }
    }
}