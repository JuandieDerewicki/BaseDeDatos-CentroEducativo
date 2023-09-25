//using CentroEducativoAPISQL.Modelos;
//using CentroEducativoAPISQL.Servicios;
//using Microsoft.AspNetCore.Mvc;

//// NivelEducativoController proporciona endpoints para consultar los niveles educativos disponibles utilizando los servicios proporcionados por NivelEducativoService. Cada acción del controlador corresponde a una operación específica relacionada con los niveles educativos.

//namespace CentroEducativoAPISQL.Controllers
//{
//    // El controlador NivelEducativoController es responsable de gestionar los niveles educativos
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NivelEducativoController : ControllerBase
//    {
//        // Esto permite que el controlador utilice los servicios proporcionados por NivelEducativoService para realizar operaciones relacionadas con los niveles educativos.
//        INivelEducativoService _nivelEducativoService;

//        public NivelEducativoController(INivelEducativoService nivelEducativoService)
//        {
//            _nivelEducativoService = nivelEducativoService;
//        }

//        // Obtiene una lista de los niveles educativos y devuelve un HTTP
//        [HttpGet]
//        public async Task<ActionResult<List<NivelEducativo>>> ObtenerTodosNivelesEducativosAsync()
//        {
//            var niveles = await _nivelEducativoService.ObtenerTodosNivelesEducativosAsync();
//            return Ok(niveles);
//        }

//        // Lo mismo pero lo busca por id
//        [HttpGet("{id}")]
//        public async Task<ActionResult<NivelEducativo>> ObtenerNivelEducativoPorIdAsync(int id)
//        {
//            var nivel = await _nivelEducativoService.ObtenerNivelEducativoPorIdAsync(id);
//            if (nivel == null)
//            {
//                return NotFound();
//            }
//            return Ok(nivel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CrearNivelEducativo([FromBody] NivelEducativo nivelEducativo)
//        {
//            try
//            {
//                await _nivelEducativoService.CrearNivelEducativo(nivelEducativo);
//                return Ok("Nivel educativo creado con éxito.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
//            }
//        }
//    }
//}
