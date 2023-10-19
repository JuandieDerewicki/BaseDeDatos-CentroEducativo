// NotaController.cs
using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CentroEducativoAPISQL.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly INotaService _notaService;

        public NotaController(INotaService notaService)
        {
            _notaService = notaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> ObtenerTodasLasNotas()
        {
            try
            {
                var notas = await _notaService.ObtenerTodasLasNotasAsync();
                return Ok(notas);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las notas.");
            }
        }

        [HttpGet("{idNota}")]
        public async Task<ActionResult<Nota>> ObtenerNotaPorId(int idNota)
        {
            try
            {
                var nota = await _notaService.ObtenerNotaPorIdAsync(idNota);
                if (nota == null)
                {
                    return NotFound("Nota no encontrada.");
                }
                return Ok(nota);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener la nota.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Nota>> AgregarNota([FromBody] Nota nuevaNota)
        {
            try
            {
                var nota = await _notaService.AgregarNotaAsync(nuevaNota);
                return CreatedAtAction("ObtenerNotaPorId", new { idNota = nota.id_nota }, nota);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar la nota.");
            }
        }
    }
}
