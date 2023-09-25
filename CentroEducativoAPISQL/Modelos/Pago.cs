using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CentroEducativoAPISQL.Modelos
{
    public class Pago
    {

        [Key]
        public int id_pago {  get; set; }

        [Required]
        [StringLength(20)]
        public int monto {  get; set; }

        [Required]
        [StringLength(50)]
        public string fecha_pago { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_pago { get; set; }

        [Required]
        [StringLength(50)]
        public string fecha_vencimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string concepto { get; set; }

        public string id_usuario { get; set; }

        [ForeignKey("id_usuario")] // Clave Foranea que establece la relacion entre el pago y el usuario que lo realizó
        public Usuarios Usuario { get; set; }
    }
}
