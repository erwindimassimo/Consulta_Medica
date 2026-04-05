using Consulta_Medica.Datos;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Controllers
{
    public class IndicacionesLaboratoriosController : Controller
    {
        private readonly ApplicationDbContext context;

        public IndicacionesLaboratoriosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var indicaciones = await context.IndicacionesLaboratorios
                .Include(p => p.Paciente)
                .OrderByDescending(i => i.FechaSolicitud)
                .ToListAsync();

            var modelo = indicaciones.Select(r => new IndicacionLaboratorioListaViewModel
            {
                Id = r.Id,
                FechaSolicitud = r.FechaSolicitud,
                PacienteIdentificacion = r.Paciente.TipoIdentificacionId == 1 ? "Menor de edad" : (r.Paciente.Identificacion ?? string.Empty),
                NombreCompletoPaciente = r.Paciente.Nombres + " " + r.Paciente.PrimerApellido +
                    (!string.IsNullOrWhiteSpace(r.Paciente.SegundoApellido) ? " " + r.Paciente.SegundoApellido : ""),
                Observaciones = r.Instrucciones
            });

            return View(modelo);
        }
    }
}