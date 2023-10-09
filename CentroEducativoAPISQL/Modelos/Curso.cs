using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEducativoAPISQL.Modelos
{
    public class Curso
    {
        [Key]
        public int id_curso {  get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_curso { get; set; }

        [Required]
        [StringLength(50)]
        public string aula {  get; set; }

        [Required]
        [StringLength(100)]
        public string? descripcion_curso { get; set; }

        //[ForeignKey("id_usuario")]
        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<Clase>? Clases { get; set; }
    }
}
