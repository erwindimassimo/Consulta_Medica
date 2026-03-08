using Microsoft.AspNetCore.Mvc.Rendering;

namespace Consulta_Medica.Models
{
    public class OpcionesFormularioCrearViewModel
    {
        public IEnumerable<SelectListItem>? ListaTiposIdentificaciones { get; set; }
        public IEnumerable<SelectListItem>? ListaARSs { get; set; }
        public IEnumerable<SelectListItem>? ListaTiposContactos { get; set; }
    }
}
