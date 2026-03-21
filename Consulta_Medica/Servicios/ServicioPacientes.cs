    using Consulta_Medica.Datos;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Consulta_Medica.Servicios
{
    public interface IServicioPacientes
    {
        int CalcularEdad(DateTime fechaNacimiento);
        Task<OpcionesFormularioPacienteViewModel> ObtenerOpcionesFormulario();
    }
    public class ServicioPacientes : IServicioPacientes
    {
        private ApplicationDbContext context;

        public ServicioPacientes(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Now;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }      
    
        public async Task<OpcionesFormularioPacienteViewModel> ObtenerOpcionesFormulario()
        {
            var opciones = new OpcionesFormularioPacienteViewModel();
            
            opciones.ListaTiposIdentificaciones = await context.TiposIdentificaciones
                .OrderBy(x => x.Id)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre
                }).ToListAsync();
                        
            opciones.ListaARSs = await context.ARSs
                .OrderBy(x => x.Id)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Nombre
                }).ToListAsync();
            
            opciones.ListaTiposContactos = await context.TiposContactos
            .OrderBy(x => x.Id)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombre
            }).ToListAsync();

            opciones.ListaSexos = await context.Sexos
                .OrderBy(s => s.Id)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nombre
                }).ToListAsync();

            return opciones;
        }
    }    
}
