using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;


namespace MiDepaEstudiantil.Pages
{
    [Authorize] //restringe a solo los que tienen la sesion inicida
    public  class MiCuentaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MiCuentaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Telefono { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener el ID del usuario de la sesión actual
          var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login"); // Si no hay sesión, redirige a Login
            }
          
            // Buscar el usuario en la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == userEmail);

            if (usuario == null)
            {
                return RedirectToPage("/Login"); // Redirige si no se encuentra el usuario
            }

            // Asignar valores para mostrar en la vista
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Correo = usuario.Correo;
            Telefono = usuario.Telefono;

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
{
    // Obtener el rol del usuario actual antes de cerrar sesión
    var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

    // Guardar el rol en la sesión (opcional)
    if (!string.IsNullOrEmpty(userRole))
    {
        HttpContext.Session.SetString("LastUserRole", userRole);
    }

    // Cerrar sesión y eliminar la cookie de autenticación
    await HttpContext.SignOutAsync();

    return RedirectToPage("/Login");
}

    }
}
