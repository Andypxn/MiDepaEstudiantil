using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace MiDepaEstudiantil.Pages
{
    [Authorize(Roles = "Propietario")] // Solo accesible para propietarios 
    [ValidateAntiForgeryToken]
    public class CrearPropiedadModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CrearPropiedadModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public required string Titulo { get; set; }

        [BindProperty]
        public required string Direccion { get; set; }

        [BindProperty]
        public required string Alcaldia { get; set; }

        [BindProperty]
        public required string Tipo { get; set; }

        [BindProperty]
        public required decimal Precio { get; set; }

        [BindProperty]
        public required string Detalles { get; set; }

        [BindProperty]
        public required bool Disponible { get; set; }

        [BindProperty]
        public required IFormFile Imagen { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Obtener el correo del usuario autenticado
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Login");
            }

            // Buscar el usuario en la base de datos
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == userEmail);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario autenticado no existe.");
                return Page();
            }

            string? imagePath = null;

            if (Imagen != null)
            {
                try
                {
                    var extension = Path.GetExtension(Imagen.FileName).ToLowerInvariant();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError(string.Empty, "Solo se permiten imágenes en formato JPG o PNG.");
                        return Page();
                    }

                    // Guardar la imagen proporcionada
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = $"{Path.GetRandomFileName()}{Path.GetExtension(Imagen.FileName)}";
                    imagePath = Path.Combine("images", uniqueFileName);

                    var filePath = Path.Combine(_environment.WebRootPath, imagePath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(fileStream);
                    }
                }
                catch (IOException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error de E/S al guardar la imagen: {ex.Message}");
                    return Page();
                }
            }
            else
            {
                // Verificar si la imagen predeterminada existe
                var defaultImagePath = Path.Combine(_environment.WebRootPath, "images", "default-property.jpg");
                if (System.IO.File.Exists(defaultImagePath))
                {
                    imagePath = "images/default-property.jpg";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo asignar la imagen predeterminada porque no existe en el servidor.");
                    return Page(); // Retorna a la página con un mensaje de error
                }
            }

            // Crear el objeto propiedad
            var propiedad = new Propiedad
            {
                Titulo = Titulo,
                Direccion = Direccion,
                Alcaldia = Alcaldia,
                Tipo = Tipo,
                Precio = Precio,
                Detalles = Detalles,
                Disponible = Disponible,
                Imagen = imagePath,
                UsuarioId = usuario.Id // Asociar la propiedad al usuario autenticado
            };

            _context.Propiedades.Add(propiedad);
            await _context.SaveChangesAsync();

            return NotFound("error modificar");
        }
    }
}
