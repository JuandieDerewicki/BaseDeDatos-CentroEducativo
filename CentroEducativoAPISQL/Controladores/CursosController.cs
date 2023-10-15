using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CentroEducativoAPISQL.Controladores
{
    [Route("api/cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly CursosService _cursosService;

        public CursosController(CursosService cursosService)
        {
            _cursosService = cursosService;
        }

        // Endpoint para obtener todos los cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> ObtenerCursos()
        {
            try
            {
                var cursos = await _cursosService.ObtenerCursosAsync();
                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener cursos: " + ex.Message);
            }
        }

        // Endpoint para obtener un curso por su ID
        [HttpGet("{idCurso}")]
        public async Task<ActionResult<Curso>> ObtenerCursoPorId(int idCurso)
        {
            try
            {
                var curso = await _cursosService.ObtenerCursoPorIdAsync(idCurso);

                if (curso == null)
                {
                    return NotFound("Curso no encontrado");
                }

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el curso: " + ex.Message);
            }
        }

        //[HttpGet("alumnosPorDocente/{docenteId}")]
        //public IActionResult ListarAlumnosPorDocente(string docenteId)
        //{
        //    try
        //    {
        //        var alumnos = _cursosService.ObtenerAlumnosPorDocente(docenteId);
        //        return Ok(alumnos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("{docenteId}")]
        public ActionResult<List<Usuario>> ObtenerAlumnosPorDocente(string docenteId)
        {
            try
            {
                var alumnos = _cursosService.ObtenerAlumnosPorDocente(docenteId);
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los alumnos por docente: " + ex.Message);
            }
        }

        [HttpGet("alumnos")]
        public ActionResult<List<Usuario>> ObtenerAlumnosDeTodosLosCursos()
        {
            try
            {
                var alumnos = _cursosService.ObtenerAlumnosDeTodosLosCursos();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los alumnos de todos los cursos: " + ex.Message);
            }
        }

        [HttpGet("nombre/{nombreCurso}")]
        public ActionResult<Curso> ObtenerCursoPorNombre(string nombreCurso)
        {
            try
            {
                var curso = _cursosService.ObtenerCursoPorNombre(nombreCurso);
                return Ok(curso);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el curso por nombre: " + ex.Message);
            }
        }

        [HttpGet("alumnos/{nombreCurso}")]
        public ActionResult<List<Usuario>> ObtenerAlumnosPorNombreDeCurso(string nombreCurso)
        {
            try
            {
                var alumnos = _cursosService.ObtenerAlumnosPorNombreDeCurso(nombreCurso);
                return Ok(alumnos);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los alumnos por nombre de curso: " + ex.Message);
            }
        }

        [HttpGet("alumnos/id/{idCurso}")]
        public ActionResult<List<Usuario>> ObtenerAlumnosPorIdDeCurso(int idCurso)
        {
            try
            {
                var alumnos = _cursosService.ObtenerAlumnosPorIdDeCurso(idCurso);
                return Ok(alumnos);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los alumnos por ID de curso: " + ex.Message);
            }
        }

        [HttpGet("alumno/{dni}/{idCurso}")]
        public ActionResult<Usuario> BuscarAlumnoPorDNIEnCurso(string dni, int idCurso)
        {
            try
            {
                var alumno = _cursosService.BuscarAlumnoPorDNIEnCurso(dni, idCurso);
                if (alumno == null)
                {
                    return NotFound("Alumno no encontrado en el curso.");
                }
                return Ok(alumno);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar al alumno por DNI en el curso: " + ex.Message);
            }
        }

        [HttpGet("alumno/{dni}")]
        public ActionResult<Usuario> BuscarAlumnoEnCualquierCurso(string dni)
        {
            try
            {
                var alumno = _cursosService.BuscarAlumnoEnCualquierCurso(dni);
                if (alumno == null)
                {
                    return NotFound("Alumno no encontrado en ningún curso.");
                }
                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar al alumno en cualquier curso: " + ex.Message);
            }
        }

        [HttpGet("profesores/{idCurso}")]
        public ActionResult<List<Usuario>> ObtenerProfesoresPorCurso(int idCurso)
        {
            try
            {
                var profesores = _cursosService.ObtenerProfesoresPorCurso(idCurso);
                return Ok(profesores);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener profesores por curso: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> AgregarCurso([FromBody] Curso nuevoCurso)
        {
            try
            {
                var cursoCreado = await _cursosService.AgregarCursoAsync(nuevoCurso);
                return Ok(cursoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar el curso: " + ex.Message);
            }
        }

        [HttpPut("{idCurso}")]
        public async Task<ActionResult<Curso>> EditarCurso(int idCurso, [FromBody] Curso cursoActualizado)
        {
            try
            {
                var curso = await _cursosService.EditarCursoAsync(idCurso, cursoActualizado);
                return Ok(curso);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al editar el curso: " + ex.Message);
            }
        }

        [HttpDelete("{idCurso}")]
        public async Task<ActionResult<string>> EliminarCurso(int idCurso)
        {
            try
            {
                var mensaje = await _cursosService.EliminarCursoAsync(idCurso);
                return Ok(mensaje);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Curso no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el curso: " + ex.Message);
            }
        }
    }
}
