using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

namespace MiDepaEstudiantil.Pages
{
    public class DetallesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetallesModel(ApplicationDbContext context)
        {
            _context = context;
        }

              
    public Propiedad Propiedad { get; set; } = new Propiedad
{
    Titulo = string.Empty,
    Direccion = string.Empty,
    Alcaldia = string.Empty,
    Tipo = string.Empty,
    Detalles = string.Empty,
    Precio = 0, // Valor predeterminado para un campo decimal
    Disponible = true, // Asumir que inicialmente está disponible
    Imagen = "images/default-property.jpg", // Ruta predeterminada de la imagen
    UsuarioId = 0 // Establece un valor predeterminado para evitar problemas de referencia nula
};
  
        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            // Buscar la propiedad en la base de datos usando el ID
            Propiedad = await _context.Propiedades.FirstOrDefaultAsync(p => p.Id == id);

            if (Propiedad == null)
            {
                return NotFound("Propiedad no encontrada.");
            }

            return Page();
        }
    }
}
