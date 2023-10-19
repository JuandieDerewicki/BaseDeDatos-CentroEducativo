using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroEducativoAPISQL.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/cursoclase")]
    [ApiController]
    public class CursoClaseController : ControllerBase
    {
        private readonly ICursoClaseService _cursoClaseService;

        public CursoClaseController(ICursoClaseService cursoClaseService)
        {
            _cursoClaseService = cursoClaseService;
        }

        [HttpPost("agregar-clase")]
        public async Task<ActionResult<string>> AgregarClaseACurso(CursoClaseRequest request)
        {
            try
            {
                var cursoClase = new CursoClase
                {
                    IdCurso = request.IdCurso,
                    IdClase = request.IdClase
                };

                var resultado = await _cursoClaseService.AgregarClaseACursoAsync(cursoClase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar clase al curso: {ex.Message}");
            }
        }

        public class CursoClaseRequest
        {
            public int IdCurso { get; set; }
            public int IdClase { get; set; }
        }


        [HttpPost("eliminar-clase")]
        public async Task<ActionResult<string>> EliminarClaseDeCurso(CursoClase cursoClase)
        {
            try
            {
                var resultado = await _cursoClaseService.EliminarClaseDeCursoAsync(cursoClase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar clase del curso: {ex.Message}");
            }
        }

        [HttpGet("mostrar-clases/{idCurso}")]
        public async Task<ActionResult<IEnumerable<Clase>>> MostrarClasesDeCurso(int idCurso)
        {
            var clases = await _cursoClaseService.MostrarClasesDeCursoAsync(idCurso);
            return Ok(clases);
    }

    [HttpGet("cursos-con-clases")]
    public async Task<ActionResult<IEnumerable<Curso>>> MostrarCursosConClases()
        {
            var cursos = await _cursoClaseService.MostrarCursosConClasesAsync();
            return Ok(cursos);
        }

        [HttpGet("alumnosPorDocente/{dniDocente}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarAlumnosPorDocente(string dniDocente)
        {
            try
            {
                var alumnos = await _cursoClaseService.ListarAlumnosPorDocenteAsync(dniDocente);

                if (alumnos == null || !alumnos.Any())
                {
                    return NotFound();
                }

                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("generar-pdf-alumnos-por-docente/{dniDocente}")]
        public async Task<IActionResult> GenerarPDFAlumnosPorDocente(string dniDocente)
        {
            try
            {
                var pdfBytes = await _cursoClaseService.GenerarListaAlumnosPorDocentePDF(dniDocente);

                return File(pdfBytes, "application/pdf", "ListaAlumnosPorDocente.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
