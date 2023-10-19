using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public string? id_profesor { get; set; }

        [ForeignKey("id_profesor")]
        public Usuario? Profesor { get; set; }

        [JsonIgnore]
        public ICollection<CursoClase>? CursoClases { get; set; }

        [JsonIgnore]
        public ICollection<UsuarioClase>? UsuariosClases { get; set; }

        [JsonIgnore]
        public ICollection<Nota>? Notas { get; set; }

    }
}
