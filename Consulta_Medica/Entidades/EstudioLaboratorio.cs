using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class EstudioLaboratorio
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del estudio es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Categoria { get; set; }
        public bool Activo { get; set; } = true;
        public int IndicacionLaboratorioId { get; set; }
        public IndicacionLaboratorio IndicacionLaboratorio { get; set; } = null!;
    }
}
