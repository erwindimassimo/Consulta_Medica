using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Consulta_Medica.Servicios;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Linq.Expressions;
using static Azure.Core.HttpHeader;

namespace Consulta_Medica.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioPacientes servicioPacientes;

        public PacientesController(ApplicationDbContext context, IServicioPacientes servicioPacientes)
        {
            this.context = context;
            this.servicioPacientes = servicioPacientes;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pacientes = await context.Pacientes
                .Include(p => p.TipoIdentificacion)
                .Include(p => p.ARS)
                .Where(p => p.Activo)
                .ToListAsync();

            var modelo = pacientes.Select(p => new PacienteListaViewModel
            {
                Id = p.Id,
                TipoIdentificacion = p.TipoIdentificacion?.Nombre ?? "N/A",
                Identificacion = p.Identificacion ?? "S/N",
                NombreCompleto = $"{p.Nombres} {p.PrimerApellido}",
                NombreARS = p.ARS?.Nombre ?? "Privado"
            }).ToList();

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var paciente = await context.Pacientes
                .Include(p => p.TipoIdentificacion)
                .Include(p => p.ARS)
                .Include(p => p.PacienteContactos)
                    .ThenInclude(pc => pc.Contacto)
                        .ThenInclude(c => c.TipoContacto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = new PacienteDetalleViewModel
            {
                Id = paciente.Id,
                Nombres = paciente.Nombres,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Edad = servicioPacientes.CalcularEdad(paciente.FechaNacimiento),
                Direccion = paciente.Direccion,
                Activo = paciente.Activo,
                TipoIdentificacion = paciente.TipoIdentificacion?.Nombre ?? "N/A",
                Identificacion = paciente.Identificacion ?? "S/N",
                NombreArs = paciente.ARS?.Nombre ?? "Privado",
                Contactos = paciente.PacienteContactos.Select(c => new ContactoViewModel
                {
                    TipoContactoId = c.Contacto.TipoContactoId,
                    TipoNombre = c.Contacto.TipoContacto?.Nombre ?? "Desconocido",
                    Valor = c.Contacto.Valor
                }).ToList()
            };

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new PacienteCrearViewModel
            {
                Nombres = string.Empty,
                PrimerApellido = string.Empty,
                Opciones = await servicioPacientes.ObtenerOpcionesFormulario()
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PacienteCrearViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Opciones = await servicioPacientes.ObtenerOpcionesFormulario();
                return View(modelo);
            }

            // 1. Instanciamos el paciente con todos los campos necesarios
            var paciente = new Paciente
            {
                Nombres = modelo.Nombres,
                PrimerApellido = modelo.PrimerApellido,
                SegundoApellido = modelo.SegundoApellido,
                Identificacion = modelo.Identificacion,
                TipoIdentificacionId = (int)modelo.TipoIdentificacionId!,
                FechaNacimiento = modelo.FechaNacimiento,
                Direccion = modelo.Direccion, // Corregido: Se agregó la dirección
                ARSId = (int)modelo.ARSId!,
                NumeroAfiliacion = modelo.NumeroAfiliacion,
                FechaRegistro = DateTime.Now,
                Activo = true,
                // Inicializamos la lista de relaciones para evitar NullReferenceException
                PacienteContactos = new List<PacienteContacto>()
            };

            // 2. Procesamos los contactos solo si la lista no es nula
            if (modelo.Contactos != null)
            {
                foreach (var i in modelo.Contactos)
                {
                    if (!string.IsNullOrEmpty(i.Valor))
                    {
                        var contacto = new Contacto
                        {
                            TipoContactoId = i.TipoContactoId, // Viene del Select en la vista
                            Valor = i.Valor,
                            TipoContacto = null! // Evita que EF intente crear un TipoContacto nuevo
                        };

                        var pacienteContacto = new PacienteContacto
                        {
                            Paciente = paciente,
                            Contacto = contacto
                        };

                        paciente.PacienteContactos.Add(pacienteContacto);
                    }
                }
            }

            context.Add(paciente);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var paciente = await context.Pacientes
                .Include(p => p.PacienteContactos)
                    .ThenInclude(pc => pc.Contacto)
                        .ThenInclude(c => c.TipoContacto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = new PacienteEditarViewModel
            {
                Id = paciente.Id,
                Nombres = paciente.Nombres,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                FechaNacimiento = paciente.FechaNacimiento,
                TipoIdentificacionId = paciente.TipoIdentificacionId,
                Identificacion = paciente.Identificacion,
                ARSId = paciente.ARSId,
                NumeroAfiliacion = paciente.NumeroAfiliacion,
                Direccion = paciente.Direccion,
                Activo = paciente.Activo,
                Contactos = paciente.PacienteContactos.Select(pc => new ContactoViewModel
                {
                    TipoContactoId = pc.Contacto.TipoContactoId,
                    Valor = pc.Contacto.Valor
                }).ToList(),
                Opciones = await servicioPacientes.ObtenerOpcionesFormulario()
            };

            return View(modelo);
        }        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(PacienteEditarViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Opciones = await servicioPacientes.ObtenerOpcionesFormulario();
                return View(modelo);
            }

            // 1. Buscamos el registro existente en la base de datos
            var paciente = await context.Pacientes
                .Include(p => p.PacienteContactos)
                    .ThenInclude(pc => pc.Contacto)
                .FirstOrDefaultAsync(p => p.Id == modelo.Id);

            if (paciente == null)
            {
                return NotFound();
            }

            // 2. Actualizamos los datos básicos del paciente
            paciente.Nombres = modelo.Nombres;
            paciente.PrimerApellido = modelo.PrimerApellido;
            paciente.SegundoApellido = modelo.SegundoApellido;
            paciente.FechaNacimiento = modelo.FechaNacimiento;
            paciente.Identificacion = modelo.Identificacion; // Se guarda con guiones según tu preferencia
            paciente.TipoIdentificacionId = modelo.TipoIdentificacionId;
            paciente.ARSId = modelo.ARSId;
            paciente.NumeroAfiliacion = modelo.NumeroAfiliacion;
            paciente.Direccion = modelo.Direccion;
            paciente.Activo = modelo.Activo;

            // 3. Actualizamos los contactos (Limpiamos los viejos y agregamos los nuevos)
            if (paciente.PacienteContactos.Any())
            {
                context.PacientesContactos.RemoveRange(paciente.PacienteContactos);
            }

            if (modelo.Contactos != null)
            {
                foreach (var item in modelo.Contactos)
                {
                    if (!string.IsNullOrEmpty(item.Valor))
                    {
                        paciente.PacienteContactos.Add(new PacienteContacto
                        {
                            Contacto = new Contacto
                            {
                                TipoContactoId = item.TipoContactoId,
                                Valor = item.Valor
                            }
                        });
                    }
                }
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Detalle", new { id = modelo.Id });
        }
    }
}