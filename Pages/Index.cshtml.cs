using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Propiedad> Propiedades { get; set; } = new();

    public async Task OnGetAsync(string? tipo, string? alcaldia)
    {
        var query = _context.Propiedades.AsQueryable();

        // Filtro por tipo
        if (!string.IsNullOrEmpty(tipo))
        {
            query = query.Where(p => p.Tipo == tipo);
        }

        // Filtro por alcaldía
        if (!string.IsNullOrEmpty(alcaldia))
        {
            query = query.Where(p => p.Alcaldia == alcaldia);
        }

        // Obtener propiedades disponibles
        Propiedades = await query.Where(p => p.Disponible).ToListAsync();
    }
}
