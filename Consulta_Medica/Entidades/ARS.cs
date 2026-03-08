using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class ARS
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la aseguradora es obligatorio")]
        public string Nombre { get; set; } = string.Empty; // Ej: Senasa, Humano, Universal
    }
}
