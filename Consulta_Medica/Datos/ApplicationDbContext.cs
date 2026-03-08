using Consulta_Medica.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Consulta_Medica.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<ARS> ARSs { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<MedicoARS> MedicosARSs { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PacienteContacto> PacientesContactos { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<RecetaDetalle> RecetaDetalles { get; set; }
        public DbSet<TipoContacto> TiposContactos { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<MedicoARS>()
                .HasKey(ma => new { ma.MedicoId, ma.ARSId });
            
            modelBuilder.Entity<PacienteContacto>()
                .HasKey(pc => new { pc.PacienteId, pc.ContactoId });
        }
    }
}
