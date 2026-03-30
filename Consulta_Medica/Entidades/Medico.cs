using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [RegularExpression(@"^\d{3}-?\d{7}-?\d{1}$", ErrorMessage = "Formato de cédula inválido")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        public string PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        public string Especialidad { get; set; } = string.Empty; // <-- FUNDAMENTAL

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Telefono { get; set; } // <-- Útil para contacto rápido

        public string PasswordHash { get; set; } = string.Empty;

        // Propiedad de navegación: Un médico tiene muchas citas
        public List<Cita> Citas { get; set; } = new List<Cita>(); // <-- RELACIÓN
    }
}
