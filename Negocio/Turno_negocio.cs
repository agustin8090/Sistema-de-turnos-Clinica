namespace proyecto_final.Negocio
{
    using proyecto_final.Datos;
    using proyecto_final.Entidad;
    using System;
    using System.Collections.Generic;
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
                // Validaciones adicionales
                if (nuevoTurno.idPaciente <= 0)
                    throw new Exception("Debe seleccionar un paciente válido");

                if (nuevoTurno.idMedico <= 0)
                    throw new Exception("Debe seleccionar un médico válido");

                if (nuevoTurno.Fecha < DateTime.Today)
                    throw new Exception("La fecha no puede ser anterior a hoy");

                // Agregar el turno
                turnoDatos.Agregar(nuevoTurno);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar turno: " + ex.Message);
            }
        }

        public List<Turno> ListarTurnosMedico(int legajo, DateTime? fecha)
        {
            Turno_clinica datos = new Turno_clinica();
            var lista = datos.Listar(); // trae todos

            var filtrados = lista
                .Where(t => t.idMedico == legajo)
                .Where(t => !fecha.HasValue || t.Fecha.Date == fecha.Value.Date)
                .ToList();

            return filtrados;
        }
    }
}