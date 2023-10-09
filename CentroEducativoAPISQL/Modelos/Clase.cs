using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEducativoAPISQL.Modelos
{
    public class Clase
    {
        [Key]
        public int id_clase { get; set; }

        [Required]
        [StringLength(50)]
        public string hora_inicio { get; set; }

        [Required]
        [StringLength(50)]
        public string hora_fin { get; set; }

        [Required]
        [StringLength(50)]
        public string materia { get; set; }

        public string? id_usuario { get; set; }

        [ForeignKey("id_usuario")] // Clave Foranea que establece la relacion entre la clase y el docente a cargo
        public Usuario? Usuarios { get; set; }

        public int? id_curso { get; set; }

        [ForeignKey("id_curso")] // Clave Foranea que establece la relacion entre la clase y el curso en el que se dicta
        public Curso? Curso { get; set; }
    }
}
