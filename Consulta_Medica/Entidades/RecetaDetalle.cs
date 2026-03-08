using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class RecetaDetalle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RecetaId { get; set; }
        public Receta Receta { get; set; } = null!;
        [Required]
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; } = null!;
        [Required(ErrorMessage = "Debe indicar cómo usar este medicamento")]
        public string Indicaciones { get; set; } = string.Empty;
    }
}
