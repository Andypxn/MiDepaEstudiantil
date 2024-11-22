using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiDepaEstudiantil.Pages
{
    [Authorize(Roles = "Propietario")]
    public class MisPropiedadesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MisPropiedadesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Propiedad> Propiedades { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener el correo del usuario autenticado
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            // Obtener el usuario autenticado
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == userEmail);
            
            if (usuario == null)
            {
                return RedirectToPage("/Login");
            }

            // Obtener las propiedades del usuario
            Propiedades = await _context.Propiedades
                .Where(p => p.UsuarioId == usuario.Id)
                .ToListAsync();

            // Si el usuario no tiene propiedades, asignar predeterminadas
            if (!Propiedades.Any())
            {
                AsignarPropiedadesPredeterminadas(usuario.Id);
                Propiedades = await _context.Propiedades
                    .Where(p => p.UsuarioId == usuario.Id)
                    .ToListAsync();
            }

            return Page();
        }

        private void AsignarPropiedadesPredeterminadas(int usuarioId)
        {
            var propiedades = new List<Propiedad>
            {
                new Propiedad
                {
                    Titulo = "Casa predeterminada",
                    Direccion = "Calle Predeterminada 123",
                    Alcaldia = "Iztapalapa",
                    Tipo = "Casa",
                    Precio = 10000,
                    Detalles = "Casa asignada automáticamente.",
                    Disponible = true,
                    Imagen = "default-property.jpg",
                    UsuarioId = usuarioId
                },
                new Propiedad
                {
                    Titulo = "Departamento predeterminado",
                    Direccion = "Av. Predeterminada 456",
                    Alcaldia = "Coyoacán",
                    Tipo = "Departamento",
                    Precio = 15000,
                    Detalles = "Departamento asignado automáticamente.",
                    Disponible = true,
                    Imagen = "default-property.jpg",
                    UsuarioId = usuarioId
                }
            };

            _context.Propiedades.AddRange(propiedades);
            _context.SaveChanges();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var propiedad = await _context.Propiedades.FindAsync(id);
                if (propiedad == null)
                {
                    return NotFound(); // Retorna error si no se encuentra la propiedad
                }

                // Eliminar la propiedad
                _context.Propiedades.Remove(propiedad);
                await _context.SaveChangesAsync();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }
    }
}
