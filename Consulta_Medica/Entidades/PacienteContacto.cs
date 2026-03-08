namespace Consulta_Medica.Entidades
{
    public class PacienteContacto
    {
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; } = null!;
    }
}
