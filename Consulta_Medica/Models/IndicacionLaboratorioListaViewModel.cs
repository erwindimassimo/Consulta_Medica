namespace Consulta_Medica.Models
{
    public class IndicacionLaboratorioListaViewModel
    {
        public int Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string PacienteIdentificacion { get; set; }
        public string NombreCompletoPaciente { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
