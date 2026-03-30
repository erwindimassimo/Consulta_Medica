using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Controllers
{
    public class RecetasController : Controller
    {
        private readonly ApplicationDbContext context;

        public RecetasController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var recetas = await context.Recetas
                .Include(p => p.Paciente)
                .ToListAsync();

            var modelo = recetas.Select(r => new RecetaListaViewModel
            {
                NombreCompletoPaciente = r.Paciente.Nombres + " " + r.Paciente.PrimerApellido +
                    (!string.IsNullOrWhiteSpace(r.Paciente.SegundoApellido) ? " " + r.Paciente.SegundoApellido : ""),
                Id = r.Id,
                FechaReceta = r.FechaReceta,
                PacienteId = r.Paciente.Id,
                PacienteIdentificacion = (!string.IsNullOrWhiteSpace(r.Paciente.Identificacion) ? r.Paciente.Identificacion : "Menor de edad"),
                Observaciones = r.Observaciones
            });

            return View(modelo);
        }
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await context.Recetas
                .Include(r => r.Paciente)
                    .ThenInclude(p => p.TipoIdentificacion)
                .Include(r => r.Paciente)
                    .ThenInclude(p => p.ARS)
                .Include(r => r.RecetaDetalles)
                    .ThenInclude(rd => rd.Medicamento)
                    .ThenInclude(m => m.Laboratorio)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            var viewModel = new RecetaDetalleViewModel
            {
                RecetaId = receta.Id,
                FechaEmision = receta.FechaReceta,
                PacienteNombreCompleto = $"{receta.Paciente.Nombres} {receta.Paciente.PrimerApellido} {receta.Paciente.SegundoApellido}".Trim(),
                PacienteIdentificacion = receta.Paciente.Identificacion ?? "No provista",
                TipoIdentificacionNombre = receta.Paciente.TipoIdentificacion?.Nombre ?? "ID",
                SeguroMedico = receta.Paciente.ARS?.Nombre,
                NotasMedicas = receta.Observaciones,

                LineasReceta = receta.RecetaDetalles.Select(rd => new MedicamentoPrescrito
                {
                    NombreMedicamento = rd.Medicamento.Nombre,
                    PresentacionComercial = rd.Medicamento.Presentacion ?? "N/A",
                    ConcentracionDosis = rd.Medicamento.Concentracion ?? "",
                    NombreLaboratorio = rd.Medicamento.Laboratorio?.Nombre ?? "N/A",
                    Instrucciones = rd.Indicaciones
                }).ToList()
            };

            return View(viewModel);

        }
    }
}
