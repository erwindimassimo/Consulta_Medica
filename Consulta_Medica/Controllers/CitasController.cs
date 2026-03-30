using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Consulta_Medica.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioCitas servicioCitas;

        public CitasController(ApplicationDbContext context, IServicioCitas servicioCitas)
        {
            this.context = context;
            this.servicioCitas = servicioCitas;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var citas = await context.Citas
                .Include(p => p.Paciente)
                .Include(m => m.Medico)
                .Include(ec => ec.EstadoCita)
                .OrderBy(p => p.FechaHora)
                .ToListAsync();

            var modelo = citas.Select(c => new CitaListaViewModel
            {
                Id = c.Id,
                FechaHora = c.FechaHora,
                PacienteId = c.PacienteId,
                NombreCompletoPaciente = c.Paciente.Nombres + " " + c.Paciente.PrimerApellido +
                    (!string.IsNullOrWhiteSpace(c.Paciente.SegundoApellido) ? " " + c.Paciente.SegundoApellido : ""),
                NombreCompletoMedico = $"Dr. {c.Medico.Nombres} {c.Medico.PrimerApellido}",
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
            await CargarOpciones(modelo); 
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CitaCrearViewModel modelo)
        {
            if (modelo.FechaHora < DateTime.Now)
            {
                ModelState.AddModelError("FechaHora", "No se pueden agendar citas en fechas pasadas.");
            }

            var conflicto = await context.Citas.AnyAsync(c =>
                c.MedicoId == modelo.MedicoId &&
                c.FechaHora == modelo.FechaHora &&
                c.EstadoCitaId != 4);

            if (conflicto)
            {
                ModelState.AddModelError("FechaHora", "El médico seleccionado ya tiene una cita a esa misma hora.");
            }

            if (ModelState.IsValid)
            {
                var cita = new Cita
                {
                    FechaHora = modelo.FechaHora,
                    PacienteId = modelo.PacienteId,
                    MedicoId = modelo.MedicoId,
                    Motivo = modelo.Motivo,
                    Notas = modelo.Notas,
                    EstadoCitaId = 1
                };

                context.Add(cita);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await CargarOpciones(modelo);
            return View(modelo);
        }

        private async Task CargarOpciones(CitaCrearViewModel modelo)
        {
            modelo.Opciones.ListaPacientes = await servicioCitas.ObtenerListaPacientes();
            modelo.Opciones.ListaMedicos = await servicioCitas.ObtenerListaMedicos();
        }
    }
}