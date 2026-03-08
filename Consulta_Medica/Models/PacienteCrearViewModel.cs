using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Consulta_Medica.Models
{
    public class PacienteCrearViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del paciente es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public required string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [StringLength(100)]
        public required string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un tipo de identificación")]
        public int TipoIdentificacionId { get; set; }
        [StringLength(100)]
        public string? Identificacion { get; set; }
        [Required]
        public int ARSId { get; set; }
        [StringLength(50)]
        public string? NumeroAfiliacion { get; set; }
        [Required(ErrorMessage = "Seleccione un tipo de contacto (Celular, Casa, etc.)")]
        public List<ContactoViewModel> Contactos { get; set; } = new List<ContactoViewModel>();
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Dirección completa")]
        public string Direccion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;        
        public OpcionesFormularioCrearViewModel Opciones { get; set; } = new OpcionesFormularioCrearViewModel();
    }  
}
