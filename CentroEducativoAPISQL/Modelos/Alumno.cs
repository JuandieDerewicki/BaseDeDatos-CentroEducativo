using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CentroEducativoAPISQL.Modelos
{
    public class Alumno
    {
        [Key]
        public int id_alumno { get; set; } // Clave Primaria

        public string id_usuario { get; set; } // Este campo establece la relacion con la tabla Usuarios, se utiliza como Clave Foranea para establecer la relacion entre el alumno y el usuario asociado. Esto significa que esta propiedad se asocia con la CP que es el dni de Usuarios. Cada alumno esta vinculado a un usuario especifico 

        public int id_nivelEducativo { get; set; } // Este campo representa la relación con la tabla NivelEducativo. Esta propiedad se asocia con la clave primaria id_nivelEducativo de la tabla NivelEducativo.

        [ForeignKey("id_usuario")]
        public Usuarios Usuario { get; set; }

        [ForeignKey("id_nivelEducativo")]
        public NivelEducativo NivelEducativo { get; set; }
    }
}
