using iTextSharp.text.pdf.parser;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroEducativoAPISQL.Modelos
{
    public class Nota
    {
        [Key]
        public int id_nota { get; set; }

        [Required]
        public string nota { get; set; }
        public string? id_docente { get; set; }
        public string? id_alumno { get; set; }
        public int? id_clase { get; set; }

        [Required]
        public string fecha { get; set; }

        [ForeignKey("id_docente")]
        public Usuario? Docente { get; set; }

        [ForeignKey("id_alumno")]
        public Usuario? Alumno { get; set; }

        [ForeignKey("id_clase")]
        public Clase? Clase { get; set; }
    }
}
