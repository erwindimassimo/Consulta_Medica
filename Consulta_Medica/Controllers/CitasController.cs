using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var citas = await context.Citas
                .Include(p => p.Paciente)
                .Include(ec => ec.EstadoCita)
                .OrderBy(p => p.FechaHora)
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
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new CitaCrearViewModel();

            // Llenamos las opciones del formulario
            modelo.Opciones.ListaPacientes = await context.Pacientes
                .Where(p => p.Activo)
                .OrderBy(p => p.PrimerApellido)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.PrimerApellido} {p.Nombres} ({p.Identificacion})"
                }).ToListAsync();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CitaCrearViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var cita = new Cita
                {
                    FechaHora = modelo.FechaHora,
                    PacienteId = modelo.PacienteId,
                    Motivo = modelo.Motivo,
                    Notas = modelo.Notas,
                    EstadoCitaId = 1
                };

                context.Add(cita);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay error, hay que volver a llenar las opciones antes de devolver la vista
            // (Podrías crear un método privado para no repetir este código)
            return View(modelo);
        }
    }
}
