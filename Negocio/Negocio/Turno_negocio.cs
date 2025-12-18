namespace Negocio
{
    using Datos;
    using Entidad;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class Turno_negocio
    {

        Turno_clinica turnoDatos = new Turno_clinica();

        public List<Turno> ListarTurnos()
        {
            try
            {
                return turnoDatos.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar turnos: " + ex.Message);
            }
        }

        public void AgregarTurno(Turno nuevoTurno)
        {
            try
            {
                if (nuevoTurno.idPaciente <= 0)
                    throw new Exception("Debe seleccionar un paciente válido");

                if (nuevoTurno.idMedico <= 0)
                    throw new Exception("Debe seleccionar un médico válido");

                if (nuevoTurno.Fecha < DateTime.Today)
                    throw new Exception("La fecha no puede ser anterior a hoy");

                turnoDatos.Agregar(nuevoTurno);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar turno: " + ex.Message);
            }
        }

        public List<Turno> ListarTurnosPorEstado(int legajoMedico, bool estado)
        {
            return turnoDatos.ListarTurnosPorEstado(legajoMedico, estado);
        }

        public void CambiarEstadoTurno(int idTurno)
        {
            turnoDatos.CambiarEstadoTurno(idTurno);
        }

        public List<Turno> ListarTurnosMedico(int legajo, DateTime? fecha)
        {
            return turnoDatos.ListarPorMedico(legajo, fecha);
        }

        public List<Turno> ListarTurnosPorDni(int legajoMedico, string dniPaciente)
        {
            return turnoDatos.ListarTurnosPorDni(legajoMedico, dniPaciente);
        }

        public List<Turno> ListarTodosLosTurnos()
        {
            return turnoDatos.ListarTodosLosTurnos();
        }

        public List<Turno> ListarTurnosPorDniAdmin(string dni)
        {
            return turnoDatos.ListarTurnosPorDniAdmin(dni);
        }
    }
}