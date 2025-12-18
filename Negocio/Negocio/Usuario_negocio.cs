using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class Usuario_negocio
    {
        Usuario_clinica usuDAl = new Usuario_clinica();
        public void ValidarUsuario(string usuario, string contraseña)
        {

            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe ingresar un nombre de usuario.");

            if (usuario.Length < 4)
                throw new Exception("El nombre de usuario debe tener al menos 4 caracteres.");

            if (usuDAl.validar_usuario(usuario))
                throw new Exception("El usuario ingresado ya existe. Elija otro.");

            if (string.IsNullOrWhiteSpace(contraseña))
                throw new Exception("Debe ingresar una contraseña.");

            if (contraseña.Length < 4)
                throw new Exception("La contraseña debe tener al menos 4 caracteres.");
        }

        public int CrearUsuario(string usuario, string contraseña, string tipo)
        {
            return usuDAl.CrearUsuario(usuario, contraseña, tipo);
        }
    }
}
