namespace Consulta_Medica.Models
{
    public class RecetaListaViewModel
    {
        public int Id { get; set; }
        public DateTime FechaReceta { get; set; }
        public string NombreCompletoPaciente { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public string PacienteIdentificacion { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
