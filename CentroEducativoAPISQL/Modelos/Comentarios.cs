using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class Comentarios
    {
        [Key]
        public int id_comentario { get; set; } // Clave Primaria

        [Required]
        public string contenido { get; set; }

        public string? nombre { get; set; }  

        [Required]
        public string fechaHoraComentario { get; set; }

        public string? id_usuario { get; set; }

        public int id_noticia { get; set; }

        [ForeignKey("id_usuario")] // Clave Foranea que estable relacion entre el comentario y el usuario que la hizo
       public Usuarios? Usuario { get; set; } // Propiedad que te permite acceder al objeto Usuarios relacionado con este comentario. Esta propiedad ayuda a obtener más información sobre el autor del comentario

        [ForeignKey("id_noticia")] // Clave Foranea que establece relacion entre el comentario y  la noticia que se refiere
        public Noticias? Noticia { get; set; } // Propiedad que te permite acceder al objeto Noticias relacionado con este comentario. Esta propiedad ayuda a obtener más información sobre la noticia a la que se refiere el comentario.
    }
}
