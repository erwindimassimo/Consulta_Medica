using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class ContactoViewModel
    {
        [Required(ErrorMessage = "El tipo de contacto es obligatorio")]
        public int TipoContactoId { get; set; }
        public string TipoNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor del contacto es obligatorio")]
        public string Valor { get; set; } = string.Empty;
    }
}
