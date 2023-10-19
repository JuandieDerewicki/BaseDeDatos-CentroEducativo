using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CentroEducativoAPISQL.Controladores
{
    [EnableCors("ReglasCors")]
    // El controlador ComentariosController se encarga de gestionar las operaciones relacionadas con los comentarios
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly IClasesService _clasesService;

        public ClasesController(IClasesService clasesService)
        {
            _clasesService = clasesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clase>>> ObtenerTodasLasClases()
        {
            var clases = await _clasesService.ObtenerTodasLasClasesAsync();
            return Ok(clases);
        }

        [HttpGet("{idClase}")]
        public async Task<ActionResult<Clase>> ObtenerClasePorId(int idClase)
        {
            var clase = await _clasesService.ObtenerClasePorIdAsync(idClase);
            if (clase == null)
            {
                return NotFound();
            }
            return Ok(clase);
        }

        [HttpGet("buscarPorNombre")]
        public async Task<ActionResult<List<Clase>>> BuscarClasesPorNombre([FromQuery] string nombreClase)
        {
            var clases = await _clasesService.BuscarClasesPorNombreAsync(nombreClase);
            return Ok(clases);
        }

        //[HttpGet("buscarPorProfesor")]
        //public async Task<ActionResult<List<Clase>>> BuscarClasesPorProfesor([FromQuery] string nombreProfesor)
        //{
        //    var clases = await _clasesService.BuscarClasesPorProfesorAsync(nombreProfesor);
        //    return Ok(clases);
        //}

        [HttpPost]
        public async Task<ActionResult<Clase>> CrearClase([FromBody] Clase nuevaClase)
        {
            try
            {
                var clase = await _clasesService.CrearClaseAsync(nuevaClase);
                return CreatedAtAction(nameof(ObtenerClasePorId), new { idClase = clase.id_clase }, clase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idClase}")]
        public async Task<ActionResult<Clase>> ActualizarClase(int idClase, [FromBody] Clase claseActualizada)
        {
            try
            {
                var clase = await _clasesService.ActualizarClaseAsync(idClase, claseActualizada);
                return Ok(clase);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idClase}")]
        public async Task<ActionResult<string>> EliminarClase(int idClase)
        {
            try
            {
                var mensaje = await _clasesService.EliminarClaseAsync(idClase);
                return Ok(mensaje);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
