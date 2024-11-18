using Microsoft.EntityFrameworkCore;
using MiDepaEstudiantil.Models;

namespace MiDepaEstudiantil.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<Propiedad> Propiedades { get; set; }
        public required DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Propiedad>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Propiedades)
                .HasForeignKey(p => p.UsuarioId); // Agregado el punto y coma

                modelBuilder.Entity<Usuario>().ToTable("Usuarios"); //to create the table in SQL for Usuario
        }
    }
}
