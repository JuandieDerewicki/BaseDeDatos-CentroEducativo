using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// NoticiasController proporciona endpoints para consultar los datos de las noticias utilizando los servicios proporcionados por NoticiasService. Cada acción del controlador corresponde a una operación específica relacionada con las noticias.

namespace CentroEducativoAPISQL.Controllers
{
    // El controlador NoticiasController es responsable de gestionar los datos de las noticias 
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        INoticiasService _noticiasService;

        public NoticiasController(INoticiasService noticiasService)
        {
            _noticiasService = noticiasService;
        }

        // Obtiene todas las noticias y devuelve rta en HTTP
        [HttpGet]
        public async Task<ActionResult<List<Noticia>>> ObtenerTodasNoticiasAsync()
        {
            var noticias = await _noticiasService.ObtenerTodasNoticiasAsync();
            return Ok(noticias);
        }

        // Busca las noticias por id, parecido a la anterior
        [HttpGet("ObtenerNoticiasPorId/{id}")]
        public async Task<ActionResult<Noticia>> ObtenerNoticiaPorIdAsync(int id)
        {
            var noticia = await _noticiasService.ObtenerNoticiaPorIdAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            return Ok(noticia);
        }

        [HttpGet("ObtenerNoticiasPorUsuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerNoticiasPorUsuario(string idUsuario)
        {
            try
            {
                // Usa el servicio para obtener las noticias por documento de usuario
                var noticias = await _noticiasService.ObtenerNoticiasPorUsuarioAsync(idUsuario);

                return Ok(noticias);
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


        [HttpPost("CrearNoticia")]
        public async Task<IActionResult> CrearNoticia(Noticia noticia)
        {
            try
            {
                // Obtén el ID del usuario actual desde la solicitud (esto puede variar según tu implementación)
                var idUsuarioActual = noticia.id_usuario;

                // Usa el servicio para crear la noticia
                var result = await _noticiasService.CrearNoticia(noticia, idUsuarioActual);

                return Ok("Noticia creada con éxito.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes permiso para crear noticias.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditarNoticia/{idNoticia}")]
        public async Task<IActionResult> EditarNoticia(int id, Noticia noticia)
        {
            try
            {
                // Obtén el ID del usuario actual desde la solicitud (esto puede variar según tu implementación)
                var idUsuarioActual = noticia.id_usuario;

                // Usa el servicio para editar la noticia
                var result = await _noticiasService.EditarNoticia(id, noticia, idUsuarioActual);

                return Ok("Noticia editada con éxito.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes permiso para editar noticias.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("La noticia no existe.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("EliminarNoticia/{idNoticia}")]
        //public async Task<IActionResult> EliminarNoticia(int idNoticia, Noticia noticia)
        //{
        //    try
        //    {
        //        // Obtén el ID del usuario actual desde la solicitud (esto puede variar según tu implementación)
        //        var idUsuarioActual = noticia.id_usuario;

        //        // Usa el servicio para eliminar la noticia
        //        var mensaje = await _noticiasService.EliminarNoticia(idNoticia, idUsuarioActual);

        //        return Ok(mensaje);
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Forbid("No tienes permiso para eliminar noticias.");
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound("La noticia no existe.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("EliminarNoticia/{idNoticia}")]
        public async Task<IActionResult> EliminarNoticia(int idNoticia)
        {
            try
            {
                // Usa el servicio para eliminar la noticia
                var mensaje = await _noticiasService.EliminarNoticia(idNoticia);

                return Ok(mensaje);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("La noticia no existe.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
