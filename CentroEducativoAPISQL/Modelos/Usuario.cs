using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CentroEducativoAPISQL.Modelos
{
    public class Usuario
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
        public string fechaNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string contraseña { get; set; }

        // Propiedad para almacenar el hash de la contraseña
        public string hash { get; set; }

        [Required]
        [StringLength(15)]
        public string telefono { get; set; }

        public int id_rol { get; set; } 

        [ForeignKey("id_rol")] // Clave Foranea para relacionar un Usuario con su rol en la tabla "Roles"
        public Rol RolesUsuarios { get; set; } // Representa la relación de navegación a Roles a través de la clave foránea id_rol. Esto permite acceder al rol asociado a un usuario directamente desde un objeto Usuarios.

        public int? id_curso { get; set; }   

        [ForeignKey("id_curso")]

        public Curso? Cursos { get; set; }

        public int? id_clase {  get; set; }

        [ForeignKey("id_clase")]
        public Clase? clase { get; set; }

        [JsonIgnore]
        public ICollection<Pago>? Pagos { get; set; }

        [JsonIgnore]
        public ICollection<Comentario>? Comentarios { get; set; }
        [JsonIgnore]
        public ICollection<SolicitudInscripcion>? SolicitudesInscripcion { get; set; }
        [JsonIgnore]
        public ICollection<Noticia>? Noticias { get; set; }
    }
}
