using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class SolicitudInscripcion
    {
        [Key]
        public int id_solicitud { get; set; }

        [Required]
        [StringLength(100)]
        public string nombreCompletoSolicitante { get; set; }

        [Required]
        [StringLength(40)]
        [EmailAddress]
        public string correoSolicitante { get; set; }

        [Required]
        public string nivelEducativoSolicitante { get; set; }

        [Required]
        public string fechaNacimientoSolicitante { get; set; }

        public string id_usuario { get; set; }

        [ForeignKey("id_usuario")]

        [JsonIgnore]
        public Usuarios? Usuario { get; set; }
    }
}
