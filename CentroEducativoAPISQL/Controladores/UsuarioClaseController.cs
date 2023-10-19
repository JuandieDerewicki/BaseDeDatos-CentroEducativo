using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CentroEducativoAPISQL.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/usuarioclase")]
    [ApiController]
    public class UsuarioClaseController : ControllerBase
    {
        private readonly IUsuarioClaseService _usuarioClaseService;

        public UsuarioClaseController(IUsuarioClaseService clasesService)
        {
            _usuarioClaseService = clasesService;
        }

        [HttpPost("asignar")]
        public async Task<ActionResult<string>> AsignarUsuarioAClase(AsignarUsuarioClaseRequest request)
        {
            try
            {
                var resultado = await _usuarioClaseService.AsignarUsuarioAClaseAsync(request.Dni, request.IdClase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al asignar usuario a clase: {ex.Message}");
            }
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<string>> EliminarUsuarioDeClase(string dni, int idClase)
        {
            try
            {
                var resultado = await _usuarioClaseService.EliminarUsuarioDeClaseAsync(dni, idClase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar usuario de clase: {ex.Message}");
            }
        }

        [HttpGet("clases-con-usuarios")]
        public async Task<ActionResult<IEnumerable<Clase>>> MostrarClasesConUsuarios()
        {
            var clases = await _usuarioClaseService.MostrarClasesConUsuariosAsync();
            return Ok(clases);
        }

        [HttpGet("buscar-por-dni")]
        public async Task<ActionResult<IEnumerable<Clase>>> BuscarClasesPorDNI(string dni)
        {
            var clases = await _usuarioClaseService.BuscarClasesPorDNIAsync(dni);
            return Ok(clases);
        }

        public class AsignarUsuarioClaseRequest
        {
            public string Dni { get; set; }
            public int IdClase { get; set; }
        }

        [HttpGet("alumnosPorDocente/{dniDocente}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarAlumnosPorDocente(string dniDocente)
        {
            try
            {
                var alumnos = await _usuarioClaseService.ListarAlumnosPorDocenteAsync(dniDocente);
                if (alumnos == null)
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

        //[HttpGet("generar-pdf-alumnos-por-docente/{dniDocente}")]
        //public async Task<IActionResult> GenerarPDFAlumnosPorDocente(string dniDocente)
        //{
        //    try
        //    {
        //        var alumnos = await _usuarioClaseService.ListarAlumnosPorDocenteAsync(dniDocente);

        //        if (alumnos == null || !alumnos.Any())
        //        {
        //            return NotFound("No se encontraron alumnos para este docente.");
        //        }

        //        // Utiliza iTextSharp para generar el PDF
        //        var pdfBytes = await GenerarInformePDFAlumnosPorDocente(alumnos);

        //        // Devuelve el PDF como una respuesta
        //        return File(pdfBytes, "application/pdf", "InformeAlumnosPorDocente.pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


    }
}
