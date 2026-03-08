using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del medicamento es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        public string? Presentacion { get; set; } // Crear una clase ya que puede ser varias cosas
        public string? Concentracion { get; set; }

        public string? Laboratorio { get; set; } // Tambien como en presentacion
    }
}
