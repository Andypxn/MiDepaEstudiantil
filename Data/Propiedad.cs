using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using MiDepaEstudiantil.Data;

namespace MiDepaEstudiantil.Models
{
    public class Propiedad
    {
        [Key]    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Direccion { get; set; }
    public required string Alcaldia { get; set; }
    public required string Tipo { get; set; } // Casa, Departamento, Cuarto
    
    [Precision(18, 2)] //pone el decimal con precision de 18 digitos con 2 digitos despues de punto decimal
    public decimal Precio { get; set; }

    public required string Detalles { get; set; }
    public bool Disponible { get; set; }
    public string Imagen { get; set; } = "images/default-property.jpg";  // La URL o ruta de la imagen almacenada en el servidor

        // Relación con Usuario
        public int UsuarioId { get; set; } // Clave foránea

        // Navegación opcional si es compatible con tu entorno
        public Usuario? Usuario { get; set; } // Relación de navegación (nullable si es opcional)
    }


}
