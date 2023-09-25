using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string dni { get; set; } // Clave Primaria 

        [Required]
        [StringLength(100)]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        public string fechaNacimientoSolicitante { get; set; }

        [Required]
        [StringLength(50)]
        public string contraseña { get; set; }

        [Required]
        [StringLength(15)]
        public string telefono { get; set; }

        public int id_rol { get; set; } 

        [ForeignKey("id_rol")] // Clave Foranea para relacionar un Usuario con su rol en la tabla "Roles"
        public Roles RolesUsuarios { get; set; } // Representa la relación de navegación a Roles a través de la clave foránea id_rol. Esto permite acceder al rol asociado a un usuario directamente desde un objeto Usuarios.

        // Estas propiedades representan colecciones de comentarios, solicitudes de inscripción y noticias que están asociadas a un usuario. Esto indica que un usuario puede tener varios comentarios, solicitudes de inscripción y noticias.

        [JsonIgnore]
        public ICollection<Comentarios>? Comentarios { get; set; }
        [JsonIgnore]
        public ICollection<SolicitudInscripcion>? SolicitudesInscripcion { get; set; }
        [JsonIgnore]
        public ICollection<Noticias>? Noticias { get; set; }
    }
}
