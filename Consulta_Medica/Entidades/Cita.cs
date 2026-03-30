using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La fecha y hora son obligatorias")]
        public DateTime FechaHora { get; set; }
        [Required(ErrorMessage = "El/la médico es obligatorio")]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;
        [Required(ErrorMessage = "El/la paciente es obligatorio")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;        
        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        public string Motivo { get; set; } = string.Empty;
        public string? Notas { get; set; }    
        public int EstadoCitaId { get; set; }
        public EstadoCita EstadoCita { get; set; } = null!;
    }
}
