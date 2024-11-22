using MiDepaEstudiantil.Data;
using MiDepaEstudiantil.Models;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Crear usuarios si no existen
        if (!context.Usuarios.Any())
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Correo = "juan.perez@example.com",
                    Telefono = "5551234567",
                    Contrasena = "123456",
                    Rol = "Propietario"
                },
                new Usuario
                {
                    Nombre = "Maria",
                    Apellido = "Lopez",
                    Correo = "maria.lopez@example.com",
                    Telefono = "5559876543",
                    Contrasena = "abcdef",
                    Rol = "Estudiante"
                },
                new Usuario
                {
                    Nombre = "Carlos",
                    Apellido = "Gomez",
                    Correo = "carlos.gomez@example.com",
                    Telefono = "5557654321",
                    Contrasena = "123abc",
                    Rol = "Propietario"
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }

        // Asignar propiedades predeterminadas a cada propietario
        var propietarios = context.Usuarios.Where(u => u.Rol == "Propietario").ToList();

        foreach (var propietario in propietarios)
        {
            // Verificar si el propietario ya tiene propiedades
            if (!context.Propiedades.Any(p => p.UsuarioId == propietario.Id))
            {
                var propiedades = new List<Propiedad>
                {
                    new Propiedad
                    {
                        Titulo = $"Casa de {propietario.Nombre}",
                        Direccion = "Calle Principal 123",
                        Alcaldia = "Iztapalapa",
                        Tipo = "Casa",
                        Precio = 12000,
                        Detalles = "Casa espaciosa con jardín.",
                        Disponible = true,
                        Imagen = "default-property.jpg",
                        UsuarioId = propietario.Id
                    },
                    new Propiedad
                    {
                        Titulo = $"Departamento de {propietario.Nombre}",
                        Direccion = "Av. Central 456",
                        Alcaldia = "Coyoacán",
                        Tipo = "Departamento",
                        Precio = 18000,
                        Detalles = "Departamento moderno en excelente ubicación.",
                        Disponible = true,
                        Imagen = "default-property.jpg",
                        UsuarioId = propietario.Id
                    }
                };

                context.Propiedades.AddRange(propiedades);
            }
        }

        context.SaveChanges();
    }
}
