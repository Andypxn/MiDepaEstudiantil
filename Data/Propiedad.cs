using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiDepaEstudiantil.Data;

namespace MiDepaEstudiantil.Models
{
    public class Propiedad
    {
        [Key]    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Direccion { get; set; }
    public string Alcaldia { get; set; }
    public string Tipo { get; set; } // Casa, Departamento, Cuarto
    public decimal Precio { get; set; }
    public string Detalles { get; set; }
    public bool Disponible { get; set; }
        // Relación con Usuario
        public int UsuarioId { get; set; } // Clave foránea

        // Navegación opcional si es compatible con tu entorno
        public Usuario? Usuario { get; set; } // Relación de navegación (nullable si es opcional)
    }
}
