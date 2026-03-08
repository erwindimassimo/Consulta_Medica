using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class TipoContacto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
 