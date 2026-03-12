using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El motivo de consulta es obligatorio")]
        public string Motivo { get; set; } = string.Empty;
        public string? Sintomas { get; set; }
        [Required(ErrorMessage = "El diagnóstico es necesario")]
        public string Diagnostico { get; set; } = string.Empty;
        public string? NotasPrivadas { get; set; } // Notas que no salen en la impresión

        // Datos Físicos (Signos Vitales)
        public decimal? Peso { get; set; }
        public string? TensionArterial { get; set; }
        public decimal? Temperatura { get; set; }
        // Relaciones
        [Required]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        // Una consulta puede generar una receta (o varias)
        public List<Receta> Recetas { get; set; } = new List<Receta>();
    }
}
