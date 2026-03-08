namespace Consulta_Medica.Models
{
    public class PacienteListaViewModel
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public required string TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public bool TieneSeguro { get; set; } = false;
        public string? NombreARS { get; set; }
    }
}
