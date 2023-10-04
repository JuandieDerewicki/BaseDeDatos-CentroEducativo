using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class Noticia
    {
        [Key]
        public int id_noticia { get; set; } // Clave Primaria

        [Required]
        [StringLength(100)]
        public string titulo { get; set; }

        [Required]
        public string parrafos { get; set; }

        [Required]
        public string imagenes { get; set; }

        [Required]
        public string redactor { get; set; }

        [Required]
        public string fecha { get; set; }

        public string id_usuario { get; set; }

        [ForeignKey("id_usuario")] // Clave Foranea que establece la relacion entre la noticia y el usuario que la creo

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public ICollection<Comentario>? Comentarios { get; set; } // Coleccion de comentarios que indicia que una noticia puede tener varios comentarios asociados 

    }
}
