using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class NivelEducativo
    {
        [Key]
        public int id_nivelEducativo { get; set; } // Clave primaria

        [Required]
        [StringLength(50)]
        public string tipo_nivel { get; set; } // Almacena el tipo o nombre de nivel educativo como "Inicial", "Primario", "Secundario"

        [JsonIgnore]
        public ICollection<Alumno>? Alumnos { get; set; } // Esta propiedad representa una coleccion de Alumnos que indica que un nivel educativo puede estar relacionado con varios alumnos que pertenecen a un nivel educativo especifico
    }
}
