namespace Consulta_Medica.Models
{
    public class CitaListaViewModel
    {
        public int Id { get; set; }
        public required DateTime FechaHora { get; set; }
        public int PacienteId { get; set; }
        public required string NombreCompletoPaciente { get; set; }
        public required string NombreCompletoMedico { get; set; }
        public required string Motivo { get; set; }
        public int EstadoCitaId { get; set; }
        public required string EstadoCita { get; set; }
        public int? ConsultaId { get; set; }
    }
}
