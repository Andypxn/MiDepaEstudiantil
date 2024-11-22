using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

public class RegistroModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public RegistroModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnPostAsync(string nombre, string apellido, string correo, string telefono, string contrasena, string rol)
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Devuelve la misma página si hay errores de validación
        }

        var usuario = new Usuario
        {
            Nombre = nombre,
            Apellido = apellido,
            Correo = correo,
            Telefono = telefono,
            Contrasena = contrasena,
            Rol = rol
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Redirige a la página principal después de registrar al usuario
        return RedirectToPage("/Index");
    }
}
