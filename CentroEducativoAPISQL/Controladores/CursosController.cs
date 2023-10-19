using Microsoft.AspNetCore.Mvc;
using CentroEducativoAPISQL.Servicios;
using CentroEducativoAPISQL.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace CentroEducativoAPISQL.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> ObtenerCursos()
        {
            var cursos = await _cursoService.ObtenerCursosAsync();
            return Ok(cursos);
        }

        [HttpGet("{idCurso}")]
        public async Task<ActionResult<Curso>> ObtenerCursoPorId(int idCurso)
        {
            var curso = await _cursoService.ObtenerCursoPorIdAsync(idCurso);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        //[HttpGet("{idCurso}/alumnos")]
        //public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerAlumnosDelCurso(int idCurso)
        //{
        //    var alumnos = await _cursoService.ObtenerAlumnosDelCursoAsync(idCurso);
        //    return Ok(alumnos);
        //}


        //[HttpGet("alumnos/docente/{docenteId}")]
        //public ActionResult<List<Usuario>> ObtenerAlumnosPorDocente(string docenteId)
        //{
        //    var alumnos = _cursoService.ObtenerAlumnosPorDocente(docenteId);
        //    return Ok(alumnos);
        //}

        //[HttpGet("alumnos/todos")]
        //public ActionResult<List<Usuario>> ObtenerAlumnosDeTodosLosCursos()
        //{
        //    var alumnos = _cursoService.ObtenerAlumnosDeTodosLosCursos();
        //    return Ok(alumnos);
        //}

        [HttpGet("nombre/{nombreCurso}")]
        public ActionResult<Curso> ObtenerCursoPorNombre(string nombreCurso)
        {
            var curso = _cursoService.ObtenerCursoPorNombre(nombreCurso);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        //[HttpGet("alumnos/nombre/{nombreCurso}")]
        //public ActionResult<List<Usuario>> ObtenerAlumnosPorNombreDeCurso(string nombreCurso)
        //{
        //    var alumnos = _cursoService.ObtenerAlumnosPorNombreDeCurso(nombreCurso);
        //    return Ok(alumnos);
        //}

        //[HttpGet("alumnos/curso/{idCurso}")]
        //public ActionResult<List<Usuario>> ObtenerAlumnosPorIdDeCurso(int idCurso)
        //{
        //    var alumnos = _cursoService.ObtenerAlumnosPorIdDeCurso(idCurso);
        //    return Ok(alumnos);
        //}

        //[HttpGet("alumnos/dni/{dni}/curso/{idCurso}")]
        //public ActionResult<Usuario> BuscarAlumnoPorDNIEnCurso(string dni, int idCurso)
        //{
        //    var alumno = _cursoService.BuscarAlumnoPorDNIEnCurso(dni, idCurso);
        //    if (alumno == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(alumno);
        //}

        //[HttpGet("alumnos/dni/{dni}")]
        //public ActionResult<Usuario> BuscarAlumnoEnCualquierCurso(string dni)
        //{
        //    var alumno = _cursoService.BuscarAlumnoEnCualquierCurso(dni);
        //    if (alumno == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(alumno);
        //}

        //[HttpGet("profesores/curso/{idCurso}")]
        //public ActionResult<List<Usuario>> ObtenerProfesoresPorCurso(int idCurso)
        //{
        //    var profesores = _cursoService.ObtenerProfesoresPorCurso(idCurso);
        //    return Ok(profesores);
        //}

        [HttpPost]
        public async Task<ActionResult<Curso>> AgregarCurso(Curso curso)
        {
            var nuevoCurso = await _cursoService.AgregarCursoAsync(curso);
            return CreatedAtAction("ObtenerCursoPorId", new { idCurso = nuevoCurso.id_curso }, nuevoCurso);
        }

        [HttpPut("{idCurso}")]
        public async Task<ActionResult<string>> EditarCurso(int idCurso, Curso curso)
        {
            var mensaje = await _cursoService.EditarCursoAsync(idCurso, curso);
            if (mensaje == "Curso no encontrado.")
            {
                return NotFound(mensaje);
            }
            return Ok(mensaje);
        }

        [HttpDelete("{idCurso}")]
        public async Task<ActionResult<string>> EliminarCurso(int idCurso)
        {
            var mensaje = await _cursoService.EliminarCursoAsync(idCurso);
            if (mensaje == "Curso no encontrado.")
            {
                return NotFound(mensaje);
            }
            return Ok(mensaje);
        }
    }
}


