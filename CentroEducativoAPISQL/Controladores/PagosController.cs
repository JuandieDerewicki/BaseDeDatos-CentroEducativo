using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CentroEducativoAPISQL.Controladores
{
    [Route("api/pagos")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public PagosController(IPagosService pagosService)
        {
            _pagosService = pagosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pago>>> ObtenerPagos()
        {
            var pagos = await _pagosService.ObtenerPagos();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> ObtenerPagoPorId(int id)
        {
            var pago = await _pagosService.ObtenerPagoPorId(id);
            if (pago == null)
            {
                return NotFound("Pago no encontrado.");
            }
            return Ok(pago);
        }

        [HttpGet("usuario/{dniUsuario}")]
        public async Task<ActionResult<IEnumerable<Pago>>> ObtenerPagoPorDniUsuario(string dniUsuario)
        {
            var pagos = await _pagosService.ObtenerPagoPorDniUsuario(dniUsuario);
            return Ok(pagos);
        }

        [HttpGet("sinpagos")]
        public async Task<IActionResult> ObtenerUsuariosSinPagos()
        {
            try
            {
                var usuariosSinPagos = await _pagosService.ObtenerUsuariosSinPagosAsync();

                if (usuariosSinPagos.Count == 0)
                {
                    return NotFound("No hay usuarios sin pagos.");
                }

                return Ok(usuariosSinPagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> AgregarPago([FromBody] Pago nuevoPago)
        {
            var pagoAgregado = await _pagosService.AgregarPago(nuevoPago);
            return CreatedAtAction("ObtenerPagoPorId", new { id = pagoAgregado.id_pago }, pagoAgregado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarPago(int id, [FromBody] Pago pagoActualizado)
        {
            try
            {
                await _pagosService.PagoEditar(id, pagoActualizado);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarPago/{id}")]
        public async Task<IActionResult> EliminarPago(int id)
        {
            try
            {
                await _pagosService.EliminarPago(id);
                return Ok("Pago eliminado exitosamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GenerarFacturaPDF/{idPago}")]
        public async Task<IActionResult> GenerarFacturaPDF(int idPago)
        {
            try
            {
                var pdfBytes = await _pagosService.GenerarFacturaPagoPDFAsync(idPago);
                return File(pdfBytes, "application/pdf", "FacturaPago.pdf");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GenerarPDFUsuariosConCuotasImpagas")]
        public async Task<IActionResult> GenerarPDFUsuariosConCuotasImpagas()
        {
            try
            {
                var usuariosSinPagos = await _pagosService.ObtenerUsuariosSinPagosAsync();

                if (usuariosSinPagos.Count == 0)
                {
                    return NotFound("No hay usuarios con cuotas impagas.");
                }

                var pdfBytes = await _pagosService.GenerarUsuariosSinPagosPDF(usuariosSinPagos);

                return File(pdfBytes, "application/pdf", "UsuariosConCuotasImpagas.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GenerarInformeIngresosPDF")]
        public async Task<IActionResult> GenerarInformeIngresosPDF()
        {
            try
            {
                var pdfBytes = await _pagosService.GenerarInformeIngresosPDFAsync();
                return File(pdfBytes, "application/pdf", "InformeIngresos.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}