//using CentroEducativoAPISQL.Modelos;
//using PdfSharp.Drawing;
//using PdfSharp.Pdf;

//namespace CentroEducativoAPISQL.Servicios
//{
//    public class PagosService
//    {
//        private readonly MiDbContext _dbContext;

//        public PagosService(MiDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public List<Usuario> ObtenerAlumnosConCuotasImpagas()
//        {
//            var alumnosImpagos = _dbContext.Usuarios.Where(alumno => alumno.CuotaPagada == false).ToList();
//            return alumnosImpagos;
//        }

//        public ResultadoRegistro RegistrarPago(PagoRequestModel pago)
//        {
//            if (!DatosValidos(pago))
//            {
//                return new ResultadoRegistro("Los datos del pago no son válidos.");
//            }

//            var nuevoPago = new Pago
//            {
//                monto = pago.Monto,
//                fecha_pago = pago.FechaPago,
//                tipo_pago = pago.TipoPago,
//                fecha_vencimiento = pago.FechaVencimiento,
//                concepto = pago.Concepto,
//                id_usuario = pago.IdUsuario
//            };

//            _dbContext.Pagos.Add(nuevoPago);
//            _dbContext.SaveChanges();

//            return new ResultadoRegistro("El pago se ha registrado exitosamente.");
//        }

//        public byte[] GenerarFactura(int pagoId)
//        {
//            var pago = _dbContext.Pagos.FirstOrDefault(p => p.id_pago == pagoId);

//            if (pago == null)
//            {
//                return null; // Pago no encontrado.
//            }

//            // Crear un nuevo documento PDF
//            PdfDocument document = new PdfDocument();
//            PdfPage page = document.AddPage();
//            XGraphics gfx = XGraphics.FromPdfPage(page);
//            XFont font = new XFont("Verdana", 12);

//            // Contenido de la factura
//            string contenidoFactura = $"Monto: {pago.monto}\n" +
//                                     $"Fecha de Pago: {pago.fecha_pago}\n" +
//                                     $"Tipo de Pago: {pago.tipo_pago}\n" +
//                                     $"Fecha de Vencimiento: {pago.fecha_vencimiento}\n" +
//                                     $"Concepto: {pago.concepto}";

//            // Dibuja el contenido en la factura
//            gfx.DrawString(contenidoFactura, font, XBrush.Black, new XRect(100, 100, page.Width, page.Height), XStringFormats.TopLeft);

//            // Guarda el PDF en un flujo de memoria
//            byte[] pdfBytes;
//            using (var pdfStream = new System.IO.MemoryStream())
//            {
//                document.Save(pdfStream, false);
//                pdfBytes = pdfStream.ToArray();
//            }

//            return pdfBytes;
//        }

//        public List<InformeIngresos> ObtenerInformeIngresos(DateTime fechaInicio, DateTime fechaFin)
//        {
//            var informe = _dbContext.Pagos
//                .Where(p => p.fecha_pago >= fechaInicio && p.fecha_pago <= fechaFin)
//                .GroupBy(p => p.TipoPago)
//                .Select(g => new InformeIngresos
//                {
//                    TipoPago = g.Key,
//                    TotalIngresos = g.Sum(p => p.Monto)
//                })
//                .ToList();

//            return informe;
//        }

//        private bool DatosValidos(PagoRequestModel pago)
//        {
//            // Validación de datos
//            if (pago.Monto <= 0 || pago.FechaPago > DateTime.Now || pago.FechaVencimiento < DateTime.Now)
//            {
//                return false; // Datos no válidos
//            }

//            return true;
//        }
//    }
//}
