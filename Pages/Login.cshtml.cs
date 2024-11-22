using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using System.Security.Claims; // Para usar Claim, ClaimTypes, ClaimsIdentity, etc.
using Microsoft.AspNetCore.Authentication; // Para usar SignInAsync
using Microsoft.AspNetCore.Authentication.Cookies; // Para la autenticación basada en cookies



public class LoginModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public LoginModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public required string Correo { get; set; }

    [BindProperty]
    public required string Contrasena { get; set; }

    public required string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
        {
            ErrorMessage = "Todos los campos son obligatorios.";
            return Page();
        }

        // Consulta en la base de datos
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == Correo && u.Contrasena == Contrasena);

        if (usuario == null)
        {
            ErrorMessage = "Correo o contraseña incorrectos.";
            return Page();
        }

       // Autenticación exitosa
var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, usuario.Correo), // Usar el correo como identificador principal
    new Claim("Nombre", usuario.Nombre),       // Reclamo adicional para el nombre
    new Claim(ClaimTypes.Role, usuario.Rol)    // Rol del usuario
};


var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

return RedirectToPage("/Index");
    }
}
