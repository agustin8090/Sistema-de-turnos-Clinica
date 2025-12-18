using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio.Negocio
{
    public class Medico_negocio
    {
        
        medico_clinica medDAl = new medico_clinica();


        public void ValidarMedico(medico m)
        {
            
            if (m.legajo <= 0)
                throw new Exception("El legajo debe ser un número válido.");

            if (medDAl.validar_legajo(m.legajo))
                throw new Exception("El legajo ingresado ya está registrado.");

            
            if (string.IsNullOrEmpty(m.dni) || m.dni.Length < 7 || m.dni.Length > 8 || !m.dni.All(char.IsDigit))
                throw new Exception("El DNI debe contener 7 u 8 números.");

            if (medDAl.validar_dni(m.dni))
                throw new Exception("El DNI ingresado ya pertenece a otro médico.");

            
            if (string.IsNullOrEmpty(m.nombre) || !m.nombre.All(c => char.IsLetter(c) || c == ' '))
                throw new Exception("El nombre solo puede contener letras.");

            
            if (string.IsNullOrEmpty(m.apellido) || !m.apellido.All(c => char.IsLetter(c) || c == ' '))
                throw new Exception("El apellido solo puede contener letras.");

            
            if (string.IsNullOrEmpty(m.sexo))
                throw new Exception("Debe seleccionar un sexo.");

            
            if (m.fecha_nacimiento == DateTime.MinValue)
                throw new Exception("Debe ingresar una fecha válida.");

            
            if (!m.email.Contains("@") || !m.email.Contains("."))
                throw new Exception("Debe ingresar un email válido.");

            
            if (m.id_localidad <= 0)
                throw new Exception("Debe seleccionar una localidad válida.");

            
            if (m.id_especialidad <= 0)
                throw new Exception("Debe seleccionar una especialidad válida.");

            
            if (string.IsNullOrWhiteSpace(m.dias_atencion))
                throw new Exception("Debe seleccionar al menos un día de atención.");

            
            if (m.hora_inicio == null || m.hora_fin == null)
                throw new Exception("Debe ingresar un horario válido.");

            if (m.hora_fin <= m.hora_inicio)
                throw new Exception("La hora de fin debe ser mayor a la hora de inicio.");
        }

        public void AgregarMedico(medico m)
        {
            medDAl.agregar_medico(m);
        }


        public bool EliminarMedico(int legajo)
        {
            if (legajo <= 0)
                throw new Exception("Debe ingresar un legajo válido.");

            if (!medDAl.validar_legajo(legajo))
                throw new Exception("No existe ningún médico con ese legajo.");

            return medDAl.Eliminar_medico(legajo);
        }

        public List<medico> ListarMedicos()
        {
            return medDAl.ListarMedicos();
        }

        

        public bool ModificarMedico(medico m, out string error)
        {
            error = "";

             if (medDAl.ExisteMedicoDuplicado(m))
            {
                error = "Ya existe un médico con el mismo DNI o legajo.";
                return false;
            }
            medDAl.modificar_medico(m);
            return true;
        }

        public List<medico> BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Ingrese un DNI válido.");

            return medDAl.BuscarPorDni(dni);
        }

        public List<medico> BuscarPorLegajo(int legajo)
        {
            if (legajo <= 0)
                throw new Exception("El legajo debe ser un número válido.");

            return medDAl.BuscarPorLegajoListar(legajo);
        }

        public List<medico> BuscarPorEspecialidad(int idEspecialidad)
        {
            if (idEspecialidad <= 0)
                throw new Exception("Seleccione una especialidad.");

            return medDAl.BuscarPorEspecialidad(idEspecialidad);
        }

        public List<medico> BuscarPorEstado(bool estado)
        {
            return medDAl.BuscarPorEstado(estado);
        }

        public List<medico> BuscarPorDias(List<string> diasSeleccionados)
        {
            return medDAl.BuscarPorDias(diasSeleccionados);
        }

        public medico BuscarPorLegajoTurnos(int legajo)
        {
            if (legajo <= 0)
                throw new Exception("El legajo debe ser un número válido.");

            return medDAl.BuscarPorLegajo(legajo);
        }
    }
}
