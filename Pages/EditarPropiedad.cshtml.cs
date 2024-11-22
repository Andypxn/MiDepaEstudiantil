using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace MiDepaEstudiantil.Pages
{
    [Authorize(Roles = "Propietario")]
    public class EditarPropiedadModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditarPropiedadModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Propiedad Propiedad { get; set; } = new Propiedad
        {
            Titulo = string.Empty,
            Direccion = string.Empty,
            Alcaldia = string.Empty,
            Tipo = string.Empty,
            Detalles = string.Empty,
            Precio = 0, // Valor predeterminado para un campo decimal
            Disponible = true, // Asumir que inicialmente está disponible
            Imagen = "images/default-property.jpg", // Ruta predeterminada de la imagen
            UsuarioId = 0 // Establece un valor predeterminado para evitar problemas de referencia nula
        };


        [BindProperty]
        public IFormFile? NuevaImagen { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Propiedad = await _context.Propiedades.FirstOrDefaultAsync(p => p.Id == id);

            if (Propiedad == null)
            {
                return NotFound("Propiedad no encontrada.");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var propiedadExistente = await _context.Propiedades.FirstOrDefaultAsync(p => p.Id == Propiedad.Id);
            Console.WriteLine(propiedadExistente);
            Console.WriteLine("-----PROPIEDAD------");

            if (propiedadExistente == null)
            {
                return NotFound("Propiedad no encontrada.");
            }

            // Actualizar la imagen si se proporciona una nueva
            if (NuevaImagen != null)
            {
                try
                {
                    var extension = Path.GetExtension(NuevaImagen.FileName).ToLowerInvariant();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError(string.Empty, "Solo se permiten imágenes en formato JPG o PNG.");
                        return Page();
                    }

                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = $"{Path.GetRandomFileName()}{extension}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await NuevaImagen.CopyToAsync(fileStream);
                    }

                    propiedadExistente.Imagen = uniqueFileName;
                }
                catch (IOException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al guardar la imagen: {ex.Message}");
                    return Page();
                }
            }

            // Actualizar el resto de los campos
            propiedadExistente.Titulo = Propiedad.Titulo;
            propiedadExistente.Direccion = Propiedad.Direccion;
            propiedadExistente.Alcaldia = Propiedad.Alcaldia;
            propiedadExistente.Tipo = Propiedad.Tipo;
            propiedadExistente.Precio = Propiedad.Precio;
            propiedadExistente.Detalles = Propiedad.Detalles;
            propiedadExistente.Disponible = Propiedad.Disponible;

            await _context.SaveChangesAsync();

            return NotFound("Error");
        }
    }
}
