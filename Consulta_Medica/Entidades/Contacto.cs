using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe tener un tipo de contacto")]
        public int TipoContactoId { get; set; }        
        public virtual TipoContacto? TipoContacto { get; set; }
        [Required(ErrorMessage = "Debe introducir el detalle del contacto")]
        public string Valor { get; set; } = string.Empty;
        public virtual ICollection<PacienteContacto> PacienteContactos { get; set; } = new List<PacienteContacto>();
    }
}
