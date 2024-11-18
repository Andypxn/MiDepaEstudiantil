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

    public async Task OnPostAsync(string nombre, string apellido, string correo, string telefono, string contrasena, string rol)
    {
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

        RedirectToPage("/Index");
    }
}
