using Microsoft.EntityFrameworkCore;
using CentroEducativoAPISQL.Modelos;

public class MiDbContext : DbContext
{

    // Punto de entrada para interactuar con la BD y define cómo se mapean los modelos a tablas de base de datos y cómo se establecen las relaciones entre ellas
    public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { } // Metodo base del constructor de ef, no le hacemos implementacion pq solo va a contener los valores base para poder hacer la configuracion de las opciones que podamos crear a futuro para ef. Estas opciones generalmente se configuran en el inicio de la aplicación para especificar la cadena de conexión y otros detalles de configuración.

    // DbSet para las entidades mapeadas a la base de datos que representan tablas
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Roles> Roles { get; set; }

    public DbSet<Curso> Curso { get; set; }
    //public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<SolicitudInscripcion> SolicitudesInscripcion { get; set; }
    //public DbSet<NivelEducativo> NivelesEducativos { get; set; }
    public DbSet<Noticias> Noticias { get; set; }
    public DbSet<Comentarios> Comentarios { get; set; }

    // El método OnModelCreating se anula para permitir la configuración personalizada del modelo y definir relaciones y restricciones. Aquí configuramos las relaciones entre las tablas utilizando Fluent API. 
    // Los metodos override son protected siempre 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relaciones y restricciones

        // Relación Usuarios-Roles (1:N)
        modelBuilder.Entity<Usuarios>()
            .HasOne(u => u.RolesUsuarios)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.id_rol);

        // En la relación Usuarios y Roles, definimos que un usuario tiene un rol y se mapea a través de la propiedad RolesUsuarios, utilizando la clave foránea id_rol.

        // Relación Usuario-Comentario (1 a N)
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Comentarios)
            .WithOne(c => c.Usuario)
            .HasForeignKey(c => c.id_usuario);
            //.OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada


        // Relación Usuario-SolicitudInscripcion (1 a N)
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.SolicitudesInscripcion)
            .WithOne(s => s.Usuario)
            .HasForeignKey(s => s.id_usuario)
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada


        // Relación Usuario-Noticia (1 a N)
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Noticias)
            .WithOne(n => n.Usuario)
            .HasForeignKey(n => n.id_usuario);
            //.OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada


        // Relación Comentario-Noticia (1 a N)
        modelBuilder.Entity<Comentarios>()
            .HasOne(c => c.Noticia)
            .WithMany(n => n.Comentarios)
            .HasForeignKey(c => c.id_noticia)
            .OnDelete(DeleteBehavior.Cascade);
        //.OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

        // Relación SolicitudInscripcion-NivelEducativo (N a 1)
        //modelBuilder.Entity<SolicitudInscripcion>()
        //    .HasOne(s => s.NivelEducativo)
        //    .WithMany()
        //    .HasForeignKey(s => s.id_nivelEducativo);

        // Relación Alumno-NivelEducativo (N a 1)
        //modelBuilder.Entity<Alumno>()
        //    .HasOne(a => a.NivelEducativo)
        //    .WithMany(ne => ne.Alumnos)
        //    .HasForeignKey(a => a.id_nivelEducativo);

        //Las relaciones Usuarios y Comentarios, Usuarios y SolicitudesInscripcion, Usuarios y Noticias, Comentarios y Noticias,
        //y otras se configuran para definir las relaciones uno a muchos entre las tablas correspondientes.

        // Restricción única en el campo correo de Usuarios
        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.correo)
            .IsUnique();

        // Restricción única en el campo dni de Usuarios
        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.dni)
            .IsUnique();
        

        // Se agregan restricciones únicas en los campos correo de la tabla Usuarios y correoSolicitante de la tabla
        // SolicitudInscripcion mediante la configuración de índices únicos.
    }
}

