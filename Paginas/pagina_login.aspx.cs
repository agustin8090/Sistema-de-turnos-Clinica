using Datos;
using Entidad;
using System;
using System.Web.UI;

namespace proyecto_final
{
    public partial class pagina_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            Usuario_clinica dao = new Usuario_clinica();
            Usuario u = dao.Login(usuario, pass);

            if (u == null)
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
                return;
            }

            Session["UsuarioId"] = u.IdUsuario;
            Session["UsuarioNombre"] = u.NombreUsuario;
            Session["TipoUsuario"] = u.TipoUsuario;

            if (u.TipoUsuario == "Administrador")
            {
                Session["NombreAdmin"] = u.NombreUsuario;
                Response.Redirect("pagina_administrador.aspx");
            }
            else if (u.TipoUsuario == "Medico")
            {
                medico_clinica daoMedico = new medico_clinica();
                medico med = daoMedico.BuscarPorIdUsuario(u.IdUsuario);

                if (med == null)
                {
                    lblMensaje.Text = "No se encontró el médico asociado a este usuario.";
                    return;
                }

                
                Session["LegajoMedico"] = med.legajo;
                Session["NombreMedico"] = med.nombre + " " + med.apellido;
                Response.Redirect("pagina_medico.aspx");
            }
        }
    }
}
