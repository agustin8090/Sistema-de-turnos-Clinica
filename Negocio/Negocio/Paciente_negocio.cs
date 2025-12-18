using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Negocio
{
    public class Paciente_negocio
    {
        Paciente_clinica pacDAL = new Paciente_clinica();
        public void GuardarPaciente (Paciente pac)
        {
            if( string.IsNullOrEmpty(pac.DniPaciente) || pac.DniPaciente.Length<7)
            {
                throw new Exception("El campo DNI no es valido"); 
                
            }
            if (pacDAL.validarDni(pac.DniPaciente))
                throw new Exception("El DNI ingresado ya pertenece a otro paciente");

            if (string.IsNullOrEmpty(pac.NombrePaciente))
            {
                throw new Exception("El campo Nombre debe estar completo");

            }
            if(string.IsNullOrEmpty(pac.ApellidoPaciente))
            {
                throw new Exception("El campo Apellido debe estar completo");

            }
            if (string.IsNullOrEmpty(pac.NacionalidadPac))
            {
                throw new Exception("El campo Nacionalidad debe estar completo");

            }
            if (string.IsNullOrEmpty(pac.DireccionPac))
            {
                throw new Exception("El campo Direccion debe estar completo");

            }
            if(!Regex.IsMatch(pac.EmailPac, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                
            }
            if (string.IsNullOrEmpty(pac.TelefonoPac))
            {
                throw new Exception("El campo Telefono debe estar completo");

               
            }
            pacDAL.AgregarPaciente (pac);
               

        }

        public void EliminarPaciente(string dni)
        {
            if (string.IsNullOrEmpty(dni) || dni.Length < 7)
            {
                throw new Exception("Debe ingresar un DNI válido.");
            }

            Paciente_clinica pacDAL = new Paciente_clinica();

           
            if (pacDAL.validarDni(dni) == false)
            {
                throw new Exception("No existe un paciente con ese DNI.");
            }

            bool eliminado = pacDAL.EliminarPaciente(dni);

            if (!eliminado)
            {
                throw new Exception("No se pudo eliminar el paciente.");
            }
        }

        
        
        public Paciente TraerPacientePorId(int id)
        {
            if (id <= 0) return null;

            Paciente_clinica pacDAL = new Paciente_clinica();
            return pacDAL.ObtenerPacientePorId(id);
        }

        public void ModificarPaciente(Paciente p)
        {
            if (p == null)
                throw new Exception("Paciente inválido.");

            if (p.IdPaciente <= 0)
                throw new Exception("Seleccione un paciente para modificar.");

            if (string.IsNullOrWhiteSpace(p.DniPaciente))
                throw new Exception("El DNI es obligatorio.");

            Paciente_clinica pacDAL = new Paciente_clinica();


            if (pacDAL.ExisteDniEnOtroPaciente(p.IdPaciente, p.DniPaciente))
                throw new Exception("Ya existe otro paciente con ese DNI.");

            
            bool resultado = pacDAL.ModificarPaciente(p);

            if (!resultado)
                throw new Exception("No se pudo modificar el paciente.");
        }
        

        public List<Paciente> ListarPacientes()
        {
            Paciente_clinica pacDAL = new Paciente_clinica();
            return pacDAL.Listarpacientes();
        }

        public DataTable ListarPacientesTabla()
        {
            Paciente_clinica pacData = new Paciente_clinica();
            return pacData.ListarPacientesTabla();
        }

        public List<Paciente> ListarPorDni(string dni)
        {
            return pacDAL.ListarPorDni(dni);
        }

        public List<Paciente> ListarPorSexo(string sexo)
        {
            return pacDAL.ListarPorSexo(sexo);
        }

        public List<Paciente> ListarPorEstado(bool estado)
        {
            return pacDAL.ListarPorEstado(estado);
        }


    }
}