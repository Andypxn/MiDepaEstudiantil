using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

public class CrearPropiedadModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CrearPropiedadModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public ApplicationDbContext Context => _context;

    public async Task<IActionResult> OnPostAsync(string titulo, string direccion, string alcaldia, string tipo, decimal precio, string detalles, bool disponible)
    {
        var propiedad = new Propiedad
        {
            Titulo = titulo,
            Direccion = direccion,
            Alcaldia = alcaldia,
            Tipo = tipo,
            Precio = precio,
            Detalles = detalles,
            Disponible = disponible
        };

        Context.Propiedades.Add(propiedad);
        await Context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}
