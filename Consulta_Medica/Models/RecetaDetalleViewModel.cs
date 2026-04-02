namespace Consulta_Medica.Models
{
    public class RecetaDetalleViewModel
    {
        public int RecetaId { get; set; }
        public DateTime FechaEmision { get; set; }
        public string PacienteNombreCompleto { get; set; } = string.Empty;
        public string PacienteIdentificacion { get; set; } = string.Empty;
        public string TipoIdentificacionNombre { get; set; } = string.Empty;
        public string? SeguroMedico { get; set; }
        public string? NotasMedicas { get; set; }
        public List<RecetaDetalleDetalleViewModel> DetallesMedicamentos { get; set; } = new();
    }
}