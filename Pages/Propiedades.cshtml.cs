using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PropiedadesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public PropiedadesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public required List<Propiedad> PropiedadesDisponibles { get; set; }

    public async Task OnGetAsync()
    {
        // Consultar las propiedades disponibles y ordenarlas por disponibilidad
        PropiedadesDisponibles = await _context.Propiedades
            .Where(p => p.Disponible)
            .OrderBy(p => p.Titulo) // Cambia esto si deseas un orden diferente
            .ToListAsync();
    }
}
