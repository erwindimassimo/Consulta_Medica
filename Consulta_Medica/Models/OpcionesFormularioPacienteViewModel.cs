using Microsoft.AspNetCore.Mvc.Rendering;

namespace Consulta_Medica.Models
{
    public class OpcionesFormularioPacienteViewModel
    {
        public IEnumerable<SelectListItem>? ListaTiposIdentificaciones { get; set; }
        public IEnumerable<SelectListItem>? ListaARSs { get; set; }
        public IEnumerable<SelectListItem>? ListaTiposContactos { get; set; }
        public IEnumerable<SelectListItem>? ListaSexos {  get; set; }
    }
}
