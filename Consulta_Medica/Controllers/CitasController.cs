using Microsoft.AspNetCore.Mvc;
using Consulta_Medica.Models;
using Consulta_Medica.Datos;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext context;
        public CitasController(ApplicationDbContext context)
        {
            this.context = context;
        }        

        public async Task<IActionResult> Index()
        {
            var citas = await context.Citas
                .Include(p => p.Paciente)
                .Include(ec => ec.EstadoCita)
                .ToListAsync();

            var modelo = citas.Select(c => new CitaListaViewModel
            {
                Id = c.Id,
                FechaHora = c.FechaHora,
                PacienteId = c.PacienteId,
                NombreCompleto = c.Paciente.Nombres + " " + c.Paciente.PrimerApellido +
                 (!string.IsNullOrWhiteSpace(c.Paciente.SegundoApellido) ? " " + c.Paciente.SegundoApellido : ""),
                Motivo = c.Motivo,
                EstadoCitaId = c.EstadoCitaId,
                EstadoCita = c.EstadoCita.Nombre
            });

            return View(modelo);
        }
    }
}
