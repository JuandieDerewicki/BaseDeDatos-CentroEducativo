using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEducativoAPISQL.Modelos
{
    public class UsuarioCurso
    {
        [Key]
        public int id_usuario_curso { get; set; } // Clave Primaria 

        public string Dni { get; set; } 
        public int IdCurso { get; set; }

        [ForeignKey("Dni")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdCurso")]
        public Curso Curso { get; set; }

    }
}
