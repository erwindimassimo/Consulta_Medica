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
        public List<MedicamentoPrescrito> LineasReceta { get; set; } = new();
    }

    public class MedicamentoPrescrito
    {
        public string NombreMedicamento { get; set; } = string.Empty;
        public string PresentacionComercial { get; set; } = string.Empty;
        public string ConcentracionDosis { get; set; } = string.Empty;
        public string NombreLaboratorio { get; set; } = string.Empty;
        public string Instrucciones { get; set; } = string.Empty;
    }
}