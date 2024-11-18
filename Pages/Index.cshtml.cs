using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Models;
using MiDepaEstudiantil.Data;

public  class IndexModel(ApplicationDbContext context) : PageModel
{
    private readonly ApplicationDbContext _context = context;

    public required List<Propiedad> Propiedades { get; set; }

    public async Task OnGetAsync(string tipo, string alcaldia)
    {
        var query = _context.Propiedades.AsQueryable();

        if (!string.IsNullOrEmpty(tipo))
            query = query.Where(p => p.Tipo == tipo);

        if (!string.IsNullOrEmpty(alcaldia))
            query = query.Where(p => p.Alcaldia == alcaldia);

        Propiedades = await _context.Propiedades.ToListAsync();
    }
}
