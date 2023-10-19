using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroEducativoAPISQL.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/usuariocurso")]
    [ApiController]
    public class UsuarioCursoController : ControllerBase
    {
        private readonly IUsuarioCursoService _usuarioCursoService;

        public UsuarioCursoController(IUsuarioCursoService usuarioCursoService)
        {
            _usuarioCursoService = usuarioCursoService;
        }

        [HttpGet("curso/{idCurso}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuariosEnCurso(int idCurso)
        {
            try
            {
                var usuarios = await _usuarioCursoService.ListarUsuariosEnCursoAsync(idCurso);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al listar usuarios en el curso: {ex.Message}");
            }
        }

        [HttpGet("curso/{idCurso}/dni/{dni}")]
        public ActionResult<Usuario> BuscarUsuarioPorDNIEnCurso(int idCurso, string dni)
        {
            try
            {
                var usuario = _usuarioCursoService.BuscarUsuarioPorDNIEnCurso(dni, idCurso);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al buscar un usuario en el curso por DNI: {ex.Message}");
            }
        }

        [HttpGet("todos")]
        public ActionResult<List<Usuario>> BuscarUsuariosEnTodosLosCursos()
        {
            try
            {
                var usuarios = _usuarioCursoService.BuscarUsuariosEnTodosLosCursos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al buscar usuarios en todos los cursos: {ex.Message}");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<UsuarioCurso>> AsignarUsuarioACurso(UsuarioCurso usuarioCurso)
        //{
        //    try
        //    {
        //        var resultado = await _usuarioCursoService.AsignarUsuarioACursoAsync(usuarioCurso);
        //        return CreatedAtAction("ObtenerUsuarioCursoPorId", new { dni = usuarioCurso.Dni, idCurso = usuarioCurso.IdCurso }, resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error al asignar usuario a curso: {ex.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<string>> AsignarUsuarioACurso(AsignarUsuarioCursoRequest request)
        {
            try
            {
                var resultado = await _usuarioCursoService.AsignarUsuarioACursoAsync(request.Dni, request.IdCurso);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al asignar usuario a curso: {ex.Message}");
            }
        }

        public class AsignarUsuarioCursoRequest
        {
            public string Dni { get; set; }
            public int IdCurso { get; set; }
        }

        [HttpDelete("{dni}/{idCurso}")]
        public async Task<ActionResult<string>> DesasignarUsuarioDeCurso(string dni, int idCurso)
        {
            try
            {
                var mensaje = await _usuarioCursoService.DesasignarUsuarioDeCursoAsync(dni, idCurso);
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al desasignar usuario de curso: {ex.Message}");
            }
        }

        [HttpDelete("dni/{dni}/curso/{idCurso}")]
        public async Task<ActionResult<string>> EliminarUsuarioDeCurso(string dni, int idCurso)
        {
            try
            {
                var mensaje = await _usuarioCursoService.EliminarUsuarioDeCursoAsync(dni, idCurso);
                if (mensaje == "Usuario no encontrado en el curso.")
                {
                    return NotFound(mensaje);
                }
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar usuario de un curso: {ex.Message}");
            }
        }

        [HttpPut("dni/{dni}/curso/{idCurso}")]
        public async Task<ActionResult<string>> EditarUsuarioEnCurso(string dni, int idCurso, UsuarioCurso usuarioCurso)
        {
            try
            {
                var mensaje = await _usuarioCursoService.EditarUsuarioEnCursoAsync(dni, idCurso, usuarioCurso);
                if (mensaje == "Usuario no encontrado en el curso.")
                {
                    return NotFound(mensaje);
                }
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al editar usuario en un curso: {ex.Message}");
            }
        }

        [HttpGet("generar-pdf/{idCurso}")]
        public async Task<IActionResult> GenerarPDF(int idCurso)
        {
            try
            {
                var pdfBytes = await _usuarioCursoService.GenerarListaAlumnosPDF(idCurso);

                return File(pdfBytes, "application/pdf", "ListaAlumnos.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

