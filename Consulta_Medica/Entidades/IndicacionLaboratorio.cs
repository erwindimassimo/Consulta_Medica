using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consulta_Medica.Entidades
{
    public class IndicacionLaboratorio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; } = null!;
        public bool Activo { get; set; } = true;
        [StringLength(500, ErrorMessage = "Las instrucciones no pueden superar los 500 caracteres")]
        public string? Instrucciones { get; set; }
        public string? Observaciones { get; set; }
        [Required]
        [Display(Name = "Fecha de Solicitud")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        public virtual List<EstudioLaboratorio> EstudiosLaboratorios { get; set; } = new List<EstudioLaboratorio>();
    }
}
