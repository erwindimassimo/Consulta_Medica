using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Sexo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe elegir el sexo"), StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
    }
}
