using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Mvc;

// ComentariosController proporciona endpoints para consultar los datos de los comentarios utilizando los servicios proporcionados por ComentariosService. Cada acción del controlador corresponde a una operación específica relacionada con los comentarios.

namespace CentroEducativoAPISQL.Controllers
{
    // El controlador ComentariosController se encarga de gestionar las operaciones relacionadas con los comentarios
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        // Esto permite que el controlador utilice los servicios proporcionados por ComentariosService para realizar operaciones relacionadas con los comentarios.
        IComentariosService _comentariosService;

        public ComentariosController(IComentariosService comentariosService)
        {
            _comentariosService = comentariosService;
        }

        // Obtiene los comentarios y devuelve rta en HTTP
        [HttpGet]
        public async Task<ActionResult<List<Comentarios>>> ObtenerTodosComentariosAsync()
        {
            var comentarios = await _comentariosService.ObtenerTodosComentariosAsync();
            return Ok(comentarios);
        }

        // Obtiene comentarios por id y devuelve rta en HTTP
        [HttpGet("ObtenerComentariosPorId/{id}")]
        public async Task<ActionResult<Comentarios>> ObtenerComentarioPorIdAsync(int id)
        {
            var comentario = await _comentariosService.ObtenerComentarioPorIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }


        [HttpGet("ObtenerComentariosPorDniUsuario/{dniUsuario}")]
        public async Task<IActionResult> ObtenerComentariosPorDniUsuario(string dniUsuario)
        {
            try
            {
                var comentariosPorDni = await _comentariosService.ObtenerComentariosPorDniUsuario(dniUsuario);
                return Ok(comentariosPorDni);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerComentariosNoRegistrado/{nombre}")]
        public async Task<IActionResult> ObtenerComentariosPorNombreUsuario(string nombre)
        {
            try
            {
                var comentariosPorNombre = await _comentariosService.ObtenerComentariosPorNombreUsuario(nombre);
                return Ok(comentariosPorNombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AgregarComentario")]
        public async Task<IActionResult> AgregarComentario([FromBody] Comentarios comentario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comentario.contenido))
                {
                    return BadRequest("El contenido del comentario es requerido.");
                }

                // Usa el servicio para crear el comentario
                var result = await _comentariosService.AgregarComentario(comentario);

                return Ok("Comentario creado con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditarComentario/{id}")]
        public async Task<IActionResult> EditarComentario(int id, Comentarios comentario)
        {
            try
            {
                var comentarioEditado = await _comentariosService.EditarComentario(id, comentario);

                if (comentarioEditado == null)
                {
                    return NotFound("El comentario no fue encontrado.");
                }

                return Ok(comentarioEditado);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al editar el comentario: " + ex.Message);
            }
        }

        [HttpDelete("EliminarComentario/{id}")]
        public async Task<IActionResult> EliminarComentario(int id)
        {
            try
            {
                var mensaje = await _comentariosService.EliminarComentario(id);

                return Ok(mensaje);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("El comentario no fue encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el comentario: " + ex.Message);
            }
        }


    }
}
