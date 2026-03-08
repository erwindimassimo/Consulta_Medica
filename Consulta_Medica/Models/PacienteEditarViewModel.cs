using System.ComponentModel.DataAnnotations;

namespace Consulta_Medica.Models
{
    public class PacienteEditarViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del paciente es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [StringLength(100)]
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un tipo de identificación")]
        public int TipoIdentificacionId { get; set; }
        [StringLength(100)]
        public string? Identificacion { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una ARS")]
        public int ARSId { get; set; }
        [StringLength(50)]
        public string? NumeroAfiliacion { get; set; }
        public List<ContactoViewModel> Contactos { get; set; } = new List<ContactoViewModel>();
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Dirección completa")]
        public string Direccion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public OpcionesFormularioCrearViewModel? Opciones { get; set; }
    }
}
