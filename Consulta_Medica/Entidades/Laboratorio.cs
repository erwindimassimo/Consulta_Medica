using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Laboratorio
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del laboratorio es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}