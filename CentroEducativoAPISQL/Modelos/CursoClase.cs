using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEducativoAPISQL.Modelos
{
    public class CursoClase
    {
        [Key]
        public int id_curso_clase {  get; set; }    
        public int IdCurso { get; set; } // Clave foránea hacia Curso
        public int IdClase { get; set; } // Clave foránea hacia Clase

        [ForeignKey("IdCurso")]
        public Curso Curso { get; set; }

        [ForeignKey("IdClase")]
        public Clase Clase { get; set; }
    }
}
