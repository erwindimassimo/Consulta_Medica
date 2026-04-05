using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class CitaEditarViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La fecha y hora son obligatorias")]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Debe elegir un paciente")]
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        public string Motivo { get; set; } = string.Empty;
        public string? Notas { get; set; }
        public int EstadoCitaId { get; set; }
        public OpcionesFormularioCitaViewModel Opciones { get; set; } = new OpcionesFormularioCitaViewModel();
    }
}
