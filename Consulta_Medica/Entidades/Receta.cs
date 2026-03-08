using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Receta
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La fecha de la receta es obligatoria")]
        public DateTime FechaReceta { get; set; }
        public string? Observaciones { get; set; }
        [Required]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        public List<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();
    }
}
