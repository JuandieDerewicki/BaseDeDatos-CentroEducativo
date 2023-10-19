using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// SolicitudInscripcionController proporciona endpoints para consultar, aceptar y rechazar solicitudes de inscripción utilizando los servicios proporcionados por SolicitudInscripcionService. Cada acción del controlador corresponde a una operación específica relacionada con las solicitudes de inscripción.

namespace CentroEducativoAPISQL.Controllers
{
    [EnableCors("ReglasCors")]
    // El controlador SolicitudInscripcionController es responsable de gestionar las solicitudes de inscripción
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudInscripcionController : ControllerBase
    {
        // Recibe el servicio para que el controlador lo utilice para realizar las operaciones relacionadas con las solicitudes
        ISolicitudInscripcionService _solicitudInscripcionService;

        public SolicitudInscripcionController(ISolicitudInscripcionService solicitudInscripcionService)
        {
            _solicitudInscripcionService = solicitudInscripcionService;
        }

        // Lista todas las solicitudes y las devuelve en HTTP
        [HttpGet]
        public async Task<ActionResult<List<SolicitudInscripcion>>> ObtenerTodasSolicitudesInscripcionAsync()
        {
            var solicitudes = await _solicitudInscripcionService.ObtenerTodasSolicitudesInscripcionAsync();
            return Ok(solicitudes);
        }

        // Lo mismo pero los lista por id
        [HttpGet("ObtenerSolicitudesPorId/{id}")]
        public async Task<ActionResult<SolicitudInscripcion>> ObtenerSolicitudInscripcionPorIdAsync(int id)
        {
            var solicitud = await _solicitudInscripcionService.ObtenerSolicitudInscripcionPorIdAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return Ok(solicitud);
        }

        [HttpGet("ObtenerSolicitudesPorUsuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerSolicitudesPorUsuario(string idUsuario)
        {
            try
            {
                // Usa el servicio para obtener las noticias por documento de usuario
                var solicitudes = await _solicitudInscripcionService.ObtenerSolicitudesPorUsuarioAsync(idUsuario);

                return Ok(solicitudes);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("El usuario no existe.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AgregarSolicitud")]
        public async Task<ActionResult<SolicitudInscripcion>> AgregarSolicitudInscripcion(SolicitudInscripcion solicitudInscripcion)
        {
            var nuevaSolicitud = await _solicitudInscripcionService.AgregarSolicitudInscripcion(solicitudInscripcion);
            return Ok(nuevaSolicitud);

        }

        [HttpPut("EditarSolicitud/{id}")]
        public async Task<ActionResult<SolicitudInscripcion>> EditarSolicitudInscripcion(int id, SolicitudInscripcion solicitudInscripcion)
        {
            var solicitudActualizada = await _solicitudInscripcionService.EditarSolicitudInscripcion(id, solicitudInscripcion);
            return Ok(solicitudActualizada);
        }

        [HttpDelete("EliminarSolicitud/{id}")]
        public async Task<ActionResult<SolicitudInscripcion>> EliminarSolicitudInscripcion(int id)
        {
            var solicitudEliminada = await _solicitudInscripcionService.EliminarSolicitudInscripcion(id);
            return Ok(solicitudEliminada);
        }

    }
}
