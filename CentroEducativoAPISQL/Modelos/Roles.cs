using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_rol { get; set; } // Clave primaria 

        [Required]
        [StringLength(40)]
        public string tipo_rol { get; set; } // Almacena el tipo de rol que puede ser "Autoridades", "Docentes", "Padres", "Alumnos"


        [JsonIgnore]
        public ICollection<Usuarios>? Usuarios { get; set; } // Propiedad que representa coleccion de usuarios que indica que un rol puede estar relacionado con varios usuarios que tengan ese rol especifico
    }
}
