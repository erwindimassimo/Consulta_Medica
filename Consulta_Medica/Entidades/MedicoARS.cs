using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class MedicoARS
    {
        public int MedicoId { get; set; }
        [Required]
        public Medico Medico { get; set; } = null!;
        public int ARSId { get; set; }
        [Required]
        public ARS Arss { get; set; } = null!;
    }
}
