using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CentroEducativoAPISQL.Controladores
{
    // El controlador ComentariosController se encarga de gestionar las operaciones relacionadas con los comentarios
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly ClasesService _clasesService;

        public ClasesController(ClasesService clasesService)
        {
            _clasesService = clasesService;
        }

        // Endpoint para obtener todas las clases
        [HttpGet]
        public async Task<ActionResult<List<Clase>>> ObtenerTodasLasClases()
        {
            try
            {
                var clases = await _clasesService.ObtenerTodasLasClasesAsync();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las clases: " + ex.Message);
            }
        }

        // Endpoint para obtener una clase por su ID
        [HttpGet("{idClase}")]
        public async Task<ActionResult<Clase>> ObtenerClasePorId(int idClase)
        {
            try
            {
                var clase = await _clasesService.ObtenerClasePorIdAsync(idClase);

                if (clase == null)
                {
                    return NotFound("Clase no encontrada");
                }

                return Ok(clase);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener la clase: " + ex.Message);
            }
        }



        // Endpoint para crear una nueva clase
        [HttpPost]
        public async Task<ActionResult<Clase>> CrearClase([FromBody] Clase nuevaClase)
        {
            try
            {
                var claseCreada = await _clasesService.CrearClaseAsync(nuevaClase);
                return Ok(claseCreada);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear la clase: " + ex.Message);
            }
        }

        // Endpoint para actualizar una clase existente
        [HttpPut("{idClase}")]
        public async Task<ActionResult<Clase>> ActualizarClase(int idClase, [FromBody] Clase claseActualizada)
        {
            try
            {
                var clase = await _clasesService.ActualizarClaseAsync(idClase, claseActualizada);
                return Ok(clase);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Clase no encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar la clase: " + ex.Message);
            }
        }

        // Endpoint para eliminar una clase por su ID
        [HttpDelete("{idClase}")]
        public async Task<ActionResult<string>> EliminarClase(int idClase)
        {
            try
            {
                var mensaje = await _clasesService.EliminarClaseAsync(idClase);
                return Ok(mensaje);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Clase no encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar la clase: " + ex.Message);
            }
        }
    }
}
