using System.ComponentModel.DataAnnotations;

namespace MiDepaEstudiantil.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public required string Correo { get; set; }

        [Required]
        [Phone]
        [StringLength(10, MinimumLength = 10)]
        public required string Telefono { get; set; }

        [Required]
        [MinLength(6)]
        public required string Contrasena { get; set; }

        [Required]
        [StringLength(20)]
        public required string Rol { get; set; } // Estudiante o Propietario

        // Relación uno-a-muchos con Propiedad
        public ICollection<Propiedad> Propiedades { get; set; } = new List<Propiedad>();
    }
}
