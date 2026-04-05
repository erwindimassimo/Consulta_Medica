using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El paciente es obligatorio")]
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public int? CitaId { get; set; }
        public Cita? Cita { get; set; }
        [Required(ErrorMessage = "La fecha de la consulta es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Indique el motivo de la visita")]
        public string Motivo { get; set; } = string.Empty;
        public string? Sintomas { get; set; }
        [Required(ErrorMessage = "El diagnóstico es fundamental")]
        public string Diagnostico { get; set; } = string.Empty;
        public string? NotasEvolucion { get; set; }
        public int? RecetaId { get; set; }
        public Receta? Receta { get; set; }
        public bool Activo {  get; set; }
    }
}
