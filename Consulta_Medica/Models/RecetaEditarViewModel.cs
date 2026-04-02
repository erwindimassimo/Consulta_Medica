using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class RecetaEditarViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Deve seleccionar un paciente")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaReceta { get; set; }
        public string? Observaciones { get; set; }
        public IEnumerable<SelectListItem>? ListaPacientes { get; set; }
        public IEnumerable<SelectListItem>? ListaMedicamentos { get; set; }
        public List<RecetaDetalleEditarViewModel> Detalles { get; set; } = new();
    }
}
