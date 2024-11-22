using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

namespace MiDepaEstudiantil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Propietario")] // Solo propietarios pueden acceder
    public class MisRentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MisRentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las propiedades ocupadas del propietario autenticado
        [HttpGet("ocupadas")]
        public async Task<IActionResult> GetRentas()
        {
            // Validar que el usuario esté autenticado
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var userEmail = User.Identity.Name;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Correo del usuario no disponible.");
            }

            // Obtener propiedades ocupadas asociadas al usuario propietario
            var rentas = await _context.Propiedades
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Correo == userEmail && !p.Disponible)
                .ToListAsync();

            return Ok(rentas);
        }

        // Registrar una propiedad como ocupada y asignar un arrendatario
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarArrendatario(int id, [FromBody] string telefono)
        {
            if (string.IsNullOrEmpty(telefono) || telefono.Length != 10)
            {
                return BadRequest("Número de teléfono inválido.");
            }

            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound("Propiedad no encontrada.");
            }

            if (!propiedad.Disponible)
            {
                return BadRequest("La propiedad ya está ocupada.");
            }

            // Marcar como ocupada
            propiedad.Disponible = false;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Propiedad registrada como ocupada." });
        }

        // Desvincular una propiedad y marcarla como disponible
        [HttpPost("desvincular")]
        public async Task<IActionResult> DesvincularPropiedad(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound("Propiedad no encontrada.");
            }

            if (propiedad.Disponible)
            {
                return BadRequest("La propiedad ya está marcada como disponible.");
            }

            // Marcar como disponible
            propiedad.Disponible = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Propiedad desvinculada y marcada como disponible." });
        }
    }
}
