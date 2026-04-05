using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class ConsultaCrearViewModel
    {
        public int? CitaId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Display(Name = "Paciente")]
        public string? NombreCompletoPaciente { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Consulta")]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        [StringLength(250)]
        [Display(Name = "Motivo de la Visita")]
        public string Motivo { get; set; } = string.Empty;
        [Display(Name = "Síntomas Presentados")]
        public string? Sintomas { get; set; }
        [Required(ErrorMessage = "Debe ingresar un diagnóstico médico")]
        [Display(Name = "Diagnóstico")]
        public string Diagnostico { get; set; } = string.Empty;
        [Display(Name = "Notas de Evolución / Plan")]
        public string? NotasEvolucion { get; set; }
        [Display(Name = "¿Generar receta médica ahora?")]
        public bool GenerarReceta { get; set; } = false;
        public List<IndicacionLaboratorioCrearViewModel> Indicaciones { get; set; } = new List<IndicacionLaboratorioCrearViewModel>();
    }
}