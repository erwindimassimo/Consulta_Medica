using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                DetallesMedicamentos = receta.RecetaDetalles.Select(rd => new RecetaDetalleDetalleViewModel
                {
                    Nombre = rd.Medicamento.Nombre,
                    Presentacion = rd.Medicamento.Presentacion ?? "N/A",
                    Concentracion = rd.Medicamento.Concentracion ?? "",
                    NombreLaboratorio = rd.Medicamento.Laboratorio?.Nombre ?? "N/A",
                    Instrucciones = rd.Indicaciones
                }).ToList()
            };

            return View(viewModel);

        }
        // GET: Recetas/Crear
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new RecetaCrearViewModel
            {
                // Llenado de selectores para la vista
                ListaPacientes = await context.Pacientes
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.Nombres} {p.PrimerApellido}"
                    }).ToListAsync(),

                ListaMedicamentos = await context.Medicamentos
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = $"{m.Nombre} - {m.Presentacion} ({m.Concentracion})"
                    }).ToListAsync()
            };

            return View(modelo);
        }

        // POST: Recetas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(RecetaCrearViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                // Usamos una transacción para asegurar la integridad de la cabecera y el detalle
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    // 1. Mapeo a la Entidad Principal (Receta)
                    var receta = new Receta
                    {
                        PacienteId = modelo.PacienteId,
                        FechaReceta = modelo.FechaReceta,
                        Observaciones = modelo.Observaciones
                    };

                    context.Recetas.Add(receta);
                    await context.SaveChangesAsync(); // Genera el ID de la receta

                    // 2. Mapeo de la lista de Detalles
                    if (modelo.Detalles != null && modelo.Detalles.Any())
                    {
                        foreach (var item in modelo.Detalles)
                        {
                            var detalle = new RecetaDetalle
                            {
                                RecetaId = receta.Id,
                                MedicamentoId = item.MedicamentoId,
                                Indicaciones = item.Instrucciones // Asegúrate que en la entidad se llame Indicaciones
                            };
                            context.RecetasDetalles.Add(detalle);
                        }
                        await context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Error al guardar. Verifique los datos e intente de nuevo.");
                }
            }

            // Si hay error, recargamos las listas para no romper la vista
            modelo.ListaPacientes = await context.Pacientes
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = $"{p.Nombres} {p.PrimerApellido}" })
                .ToListAsync();

            modelo.ListaMedicamentos = await context.Medicamentos
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                .ToListAsync();

            return View(modelo);
        }
        // GET: Recetas/Editar/5
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            // 1. Buscar la receta con sus detalles y el medicamento relacionado
            var receta = await context.Recetas
                .Include(r => r.RecetaDetalles)
                .ThenInclude(d => d.Medicamento)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receta == null) return NotFound();

            // 2. Mapear la entidad al ViewModel de edición
            var modelo = new RecetaEditarViewModel
            {
                Id = receta.Id,
                PacienteId = receta.PacienteId,
                FechaReceta = receta.FechaReceta,
                Observaciones = receta.Observaciones,

                // Mapeamos las líneas existentes al modelo de edición
                Detalles = receta.RecetaDetalles.Select(d => new RecetaDetalleEditarViewModel
                {
                    Id = d.Id, // IMPORTANTE: El ID de la fila de detalle
                    MedicamentoId = d.MedicamentoId,
                    Nombre = d.Medicamento.Nombre,
                    Instrucciones = d.Indicaciones
                }).ToList(),

                // Cargamos los combos
                ListaPacientes = await context.Pacientes
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = $"{p.Nombres} {p.PrimerApellido}" })
                    .ToListAsync(),
                ListaMedicamentos = await context.Medicamentos
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                    .ToListAsync()
            };

            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(RecetaEditarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    var receta = await context.Recetas.FindAsync(modelo.Id);
                    if (receta == null) return NotFound();

                    // 1. Actualizar Cabecera
                    receta.PacienteId = modelo.PacienteId;
                    receta.FechaReceta = modelo.FechaReceta;
                    receta.Observaciones = modelo.Observaciones;
                    context.Recetas.Update(receta);

                    // 2. Actualizar Detalles (Estrategia: Limpiar y Reinsertar)
                    // Borramos los detalles que tenía antes
                    var detallesAntiguos = context.RecetasDetalles.Where(d => d.RecetaId == receta.Id);
                    context.RecetasDetalles.RemoveRange(detallesAntiguos);

                    // Insertamos los detalles que vienen de la vista (nuevos o editados)
                    if (modelo.Detalles != null)
                    {
                        foreach (var item in modelo.Detalles)
                        {
                            context.RecetasDetalles.Add(new RecetaDetalle
                            {
                                RecetaId = receta.Id,
                                MedicamentoId = item.MedicamentoId,
                                Indicaciones = item.Instrucciones
                            });
                        }
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Error al actualizar la receta.");
                }
            }

            // Si falla, recargar listas
            modelo.ListaPacientes = await context.Pacientes.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombres }).ToListAsync();
            return View(modelo);
        }
    }
}
