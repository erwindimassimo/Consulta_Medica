using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Entidades
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [StringLength(100)]
        public string PrimerApellido { get; set; } = string.Empty;
        [StringLength(100)]
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int TipoIdentificacionId { get; set; }
        public virtual TipoIdentificacion TipoIdentificacion { get; set; } = null!;        
        public string? Identificacion { get; set; }
        [Required]
        public int ARSId { get; set; }
        public virtual ARS ARS { get; set; } = null!;
        [StringLength(50)]
        public string? NumeroAfiliacion { get; set; }        
        public virtual ICollection<PacienteContacto> PacienteContactos { get; set; } = new List<PacienteContacto>();
        [Required(ErrorMessage="La dirección es obligatoria")]
        [Display(Name="Dirección completa")]
        public string Direccion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; }
    }
}
