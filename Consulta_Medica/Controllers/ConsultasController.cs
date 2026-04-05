using Consulta_Medica.Datos;
using Consulta_Medica.Entidades;
using Consulta_Medica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ConsultasController : Controller
{
    private readonly ApplicationDbContext context;

    public ConsultasController(ApplicationDbContext context)
    {
        this.context = context;
    }
 
}