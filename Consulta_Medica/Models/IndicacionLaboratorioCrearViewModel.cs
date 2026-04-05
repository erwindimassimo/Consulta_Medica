using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class IndicacionLaboratorioCrearViewModel
    {
        public int ConsultaId { get; set; }
        public string? NombreCompletoPaciente { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un estudio")]
        public int EstudioLaboratorioId { get; set; }
        public string? NombreEstudio { get; set; }
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Instrucciones { get; set; }
        public string? Observaciones { get; set; }
        public List<EstudioLaboratorioCrearViewModel> EstudiosSeleccionados { get; set; } = new();
    }
}