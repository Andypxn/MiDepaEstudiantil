using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiDepaEstudiantil.Pages
{
    [Authorize] // Asegurarse de que el usuario esté autenticado
    public class CambiarContrasenaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CambiarContrasenaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string NuevaContrasena { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validar que las contraseñas coincidan
            if (NuevaContrasena != ConfirmarContrasena)
            {
                ErrorMessage = "Error, las contraseñas no coinciden.";
                return Page();
            }

            // Validar al usuario autenticado
            var userEmail = User.Identity?.Name;
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            // Buscar al usuario en la base de datos
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == userEmail);
            if (usuario == null)
            {
                ErrorMessage = "Error, usuario no encontrado.";
                return Page();
            }

            // Actualizar la contraseña del usuario
            usuario.Contrasena = NuevaContrasena; // Puedes agregar un sistema de hash aquí para mayor seguridad
            await _context.SaveChangesAsync();

            SuccessMessage = "Contraseña cambiada con éxito.";
            return RedirectToPage("/Login");

        }
    }
}
