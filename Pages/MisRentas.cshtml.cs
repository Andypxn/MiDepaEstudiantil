using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiDepaEstudiantil.Pages
{
    public class MisRentasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MisRentasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Propiedad> Rentas { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener el correo del usuario autenticado
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            // Consultar propiedades marcadas como ocupadas para este propietario
            Rentas = await _context.Propiedades
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Correo == userEmail && !p.Disponible)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostRegistrarAsync(int id, string telefono)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound();
            }

            // Marcar como ocupada
            propiedad.Disponible = false;

            // Opcional: Guardar el teléfono del arrendatario (puedes añadir una propiedad en el modelo)
            // propiedad.ArrendatarioTelefono = telefono;

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostDesvincularAsync(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound();
            }

            // Marcar como disponible
            propiedad.Disponible = true;

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
    }
}
