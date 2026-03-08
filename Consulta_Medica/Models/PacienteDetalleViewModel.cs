using Consulta_Medica.Servicios;

namespace Consulta_Medica.Models
{
    public class PacienteDetalleViewModel
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad {  get; set; }
        public required string TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? NombreArs { get; set; }
        public string? NumeroAfiliacion { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public List<ContactoViewModel> Contactos { get; set; } = new List<ContactoViewModel>();
        public bool Activo { get; set; }
    }
}
