using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEducativoAPISQL.Modelos
{
    public class UsuarioClase
    {
        [Key]
        public int id_usuario_clase { get; set; } // Clave Primaria 

        public string Dni { get; set; } // Clave foránea hacia Usuario
        public int IdClase { get; set; } // Clave foránea hacia Clase

        [ForeignKey("Dni")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdClase")]
        public Clase Clase { get; set; }
    }
}
