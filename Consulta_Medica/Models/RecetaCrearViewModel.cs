using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class RecetaCrearViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar un paciente")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Emisión")]
        public DateTime FechaReceta { get; set; } = DateTime.Now;
        [Display(Name = "Observaciones Médicas")]
        public string? Observaciones { get; set; }
        public IEnumerable<SelectListItem>? ListaPacientes { get; set; }
        public IEnumerable<SelectListItem>? ListaMedicamentos { get; set; }
        public List<RecetaDetalleCrearViewModel> Detalles { get; set; } = new();
    }
}