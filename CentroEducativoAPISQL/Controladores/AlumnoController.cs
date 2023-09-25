//using CentroEducativoAPISQL.Modelos;
//using CentroEducativoAPISQL.Servicios;
//using Microsoft.AspNetCore.Mvc;

//// AlumnoController proporciona endpoints para consultar los datos de los alumnos utilizando los servicios proporcionados por AlumnoService. Cada acción del controlador corresponde a una operación específica relacionada con los alumnos.

//namespace CentroEducativoAPISQL.Controllers
//{
//    // El controlador AlumnoController es responsable de gestionar los datos de los alumnos 
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AlumnoController : ControllerBase
//    {
//        // Esto permite que el controlador utilice los servicios proporcionados por AlumnoService para realizar operaciones relacionadas con los alumnos.
//        IAlumnoService _alumnoService;

//        public AlumnoController(IAlumnoService alumnoService)
//        {
//            _alumnoService = alumnoService;
//        }

//        // Obtiene lista de alumnos y devuelve rta en HTTP
//        [HttpGet]
//        public async Task<ActionResult<List<Alumno>>> ObtenerTodosAlumnosAsync()
//        {
//            var alumnos = await _alumnoService.ObtenerTodosAlumnosAsync();
//            return Ok(alumnos);
//        }

//        // Busca alumno por id, parecido a la anterior
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Alumno>> ObtenerAlumnoPorIdAsync(int id)
//        {
//            var alumno = await _alumnoService.ObtenerAlumnoPorIdAsync(id);
//            if (alumno == null)
//            {
//                return NotFound();
//            }
//            return Ok(alumno);
//        }
//    }
//}
