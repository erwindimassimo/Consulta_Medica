namespace Consulta_Medica.Models
{
    public class RecetaDetalleCrearViewModel
    {
        public int MedicamentoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public string Concentracion { get; set; } = string.Empty;
        public string NombreLaboratorio { get; set; } = string.Empty;
        public string Instrucciones { get; set; } = string.Empty;
    }
}
