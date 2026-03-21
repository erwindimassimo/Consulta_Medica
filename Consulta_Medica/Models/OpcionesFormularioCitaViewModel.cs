using Microsoft.AspNetCore.Mvc.Rendering;

namespace Consulta_Medica.Models
{
    public class OpcionesFormularioCitaViewModel
    {
        public IEnumerable<SelectListItem>? ListaPacientes {  get; set; }
        public IEnumerable<SelectListItem>? ListaMedicos { get; set; }
    }
}
