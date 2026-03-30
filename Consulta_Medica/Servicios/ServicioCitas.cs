using Consulta_Medica.Datos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Servicios
{
    public interface IServicioCitas
    {
        Task<IEnumerable<SelectListItem>> ObtenerListaPacientes();
        Task<IEnumerable<SelectListItem>> ObtenerListaMedicos();
        Task<IEnumerable<SelectListItem>> ObtenerListaEstados();
    }
    public class ServicioCitas : IServicioCitas
    {
        private readonly ApplicationDbContext context;

        public ServicioCitas(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Obtiene los pacientes activos para el Dropdown
        public async Task<IEnumerable<SelectListItem>> ObtenerListaPacientes()
        {
            return await context.Pacientes
                .Where(p => p.Activo)
                .OrderBy(p => p.PrimerApellido)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.PrimerApellido} {p.Nombres} ({p.Identificacion})"
                }).ToListAsync();
        }

        // Obtiene los médicos para el Dropdown
        public async Task<IEnumerable<SelectListItem>> ObtenerListaMedicos()
        {
            return await context.Medicos
                .OrderBy(m => m.PrimerApellido)
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"Dr. {m.PrimerApellido} {m.Nombres} - {m.Especialidad}"
                }).ToListAsync();
        }

        // Opcional: Obtener los estados de la cita (Programada, Cancelada, etc.)
        public async Task<IEnumerable<SelectListItem>> ObtenerListaEstados()
        {
            return await context.EstadosCitas // Asegúrate que el DbSet se llame así
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                }).ToListAsync();
        }
    }
}