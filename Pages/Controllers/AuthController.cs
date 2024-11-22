using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiDepaEstudiantil.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("logout")]
        [Authorize] // Solo usuarios autenticados pueden acceder
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("Cierre de sesión solicitado.");
            await HttpContext.SignOutAsync("Cookies"); // Cerrar sesión
            return Ok(new { message = "Sesión cerrada con éxito" });
        }
    }
}
