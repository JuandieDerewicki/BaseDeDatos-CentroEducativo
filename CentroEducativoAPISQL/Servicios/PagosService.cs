using CentroEducativoAPISQL.Modelos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace CentroEducativoAPISQL.Servicios
{
    public class PagosService : IPagosService
    {
        private readonly MiDbContext _context;

        public PagosService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pago>> ObtenerPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        public async Task<Pago> ObtenerPagoPorId(int id)
        {
            return await _context.Pagos.FindAsync(id);
        }

        public async Task<IEnumerable<Pago>> ObtenerPagoPorDniUsuario(string dniUsuario)
        {
            return await _context.Pagos.Where(p => p.Usuario.dni == dniUsuario).ToListAsync();
        }

        public async Task<Pago> AgregarPago(Pago nuevoPago)
        {
            _context.Pagos.Add(nuevoPago);
            await _context.SaveChangesAsync();
            return nuevoPago;
        }

        public async Task PagoEditar(int id, Pago pagoActualizado)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                throw new KeyNotFoundException("Pago no encontrado.");
            }

            // Actualiza los campos del pago según tus necesidades
            pago.monto = pagoActualizado.monto;
            pago.fecha_pago = pagoActualizado.fecha_pago;
            pago.tipo_pago = pagoActualizado.tipo_pago;
            pago.fecha_vencimiento = pagoActualizado.fecha_vencimiento;
            pago.concepto = pagoActualizado.concepto;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                throw new KeyNotFoundException("Pago no encontrado.");
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
        }


        public async Task<byte[]> GenerarDocumento(int idPago)
        {
            var pago = await _context.Pagos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.id_pago == idPago);

            using (MemoryStream ms = new MemoryStream())
            {
                var data = QuestPDF.Fluent.Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        
                        page.Margin(30);

                        page.Header().Row(row =>
                        {
                            string imagePath = @"https://i.imgur.com/RyVmq11.jpg";

                            //row.ConstantItem(140).Height(60).Placeholder();
                            row.ConstantItem(150).Image(imagePath);


                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("Centro Educativo - Educar Para Transformar").Bold().FontSize(14);
                                col.Item().AlignCenter().Text("Marcelo T. Alvear 9676 - Resistencia, Chaco").FontSize(9);
                                col.Item().AlignCenter().Text("3624-541290").Bold().FontSize(14);
                                col.Item().AlignCenter().Text("educarparatransformar@gmail.com").Bold().FontSize(14);
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text($"Factura: {pago.nro_factura}").FontColor("#fff");
                                col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("CUIT: 30000000000000007");
                                col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("Responsable Inscripto");
                                col.Item().Border(1).BorderColor("#257272").AlignCenter().Text($"Fecha de emisión: {pago.fecha_pago}");
                            });


                        });

                        page.Content().PaddingVertical(15).Column(col1 =>
                        {

                            col1.Item().Column(col2 =>
                            {
                                col2.Item().Text("Datos del cliente:").Underline().Bold();

                                col2.Item().Text(txt =>
                                {
                                    txt.Span("Nombre y apellido:").SemiBold().FontSize(10);
                                    txt.Span($"{pago.Usuario.nombreCompleto}").FontSize(10);
                                });
                                col2.Item().Text(txt =>
                                {
                                    txt.Span("Documento:").SemiBold().FontSize(10);
                                    txt.Span($"{pago.Usuario.dni}").FontSize(10);
                                });
                                col2.Item().Text(txt =>
                                {
                                    txt.Span("Teléfono:").SemiBold().FontSize(10);
                                    txt.Span($"{pago.Usuario.telefono}").FontSize(10);
                                });
                                col2.Item().Text(txt =>
                                {
                                    txt.Span("Correo:").SemiBold().FontSize(10);
                                    txt.Span($"{pago.Usuario.correo}").FontSize(10);
                                });
                            });


                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#257272").Padding(2).Text("Concepto").FontColor("#fff");
                                    header.Cell().Background("#257272").Padding(2).Text("Tipo de pago").FontColor("#fff");
                                    header.Cell().Background("#257272").Padding(2).Text("Fecha de vencimiento").FontColor("#fff");
                                    header.Cell().Background("#257272").Padding(2).Text("Monto total").FontColor("#fff");
                                });

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"{pago.concepto}").FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"{pago.tipo_pago}").FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"{pago.fecha_vencimiento}").FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"{pago.monto}").FontSize(10);

                                col1.Item().AlignRight().Text($"Total: {pago.monto}").FontSize(12);

                                col1.Spacing(10);
                            });

                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Pagina").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                }).GeneratePdf();

                return ms.ToArray();
            }
            //Stream stream = new MemoryStream(data);
            //return File(stream,"application/pdf","detallefactura.pdf");
        }



        public async Task<byte[]> GenerarFacturaPagoPDFAsync(int idPago)
        {
            var pago = await _context.Pagos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.id_pago == idPago);

            if (pago == null)
            {
                throw new KeyNotFoundException("Pago no encontrado");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (iTextSharp.text.Document doc = new iTextSharp.text.Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Agregar el logotipo de la empresa
                    /*string imagePath = @"C:\Users\bocaj\OneDrive\Documentos\Facultad\ProyectoCentroEducativo\CentroEducativoPruebas\CentroEducativoAPISQL\Logo\logocentroeducativo.jpg";*/ // Ajusta la ruta de la imagen del logotipo
                    string imagePath = @"https://i.imgur.com/RyVmq11.jpg";
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    image.ScaleToFit(100, 100);
                    image.SetAbsolutePosition(50, 750);
                    doc.Add(image);

                    // Título
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    Paragraph title = new Paragraph("Factura de Pago");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    // Agregar datos del pago
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"Numero de factura: {pago.nro_factura}"));
                    doc.Add(new Paragraph($"Monto: {pago.monto}"));
                    doc.Add(new Paragraph($"Concepto: {pago.concepto}"));
                    doc.Add(new Paragraph($"Tipo de pago: {pago.tipo_pago}"));

                    // Agregar los datos del usuario
                    if (pago.Usuario != null)
                    {
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph($"Nombre del usuario: {pago.Usuario.nombreCompleto}"));
                        doc.Add(new Paragraph($"Correo del usuario: {pago.Usuario.correo}"));
                        // Agrega otros datos del usuario según tus necesidades
                    }

                    // Crear una tabla para el pie de página
                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 100;
                    PdfPCell cell = new PdfPCell(new Phrase($"Fecha de Pago: {pago.fecha_pago}"));
                    cell.Border = 0;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    doc.Add(table);
                }

                return ms.ToArray();
            }
        }

        //public async Task<List<Usuario>> ObtenerUsuariosSinPagosAsync()
        //{
        //    var usuariosSinPagos = await _context.Usuarios
        //        .Where(u => u.Pagos == null || !u.Pagos.Any())
        //        .ToListAsync();

        //    return usuariosSinPagos;
        //}

        public async Task<List<Usuario>> ObtenerUsuariosSinPagosAsync()
        {
            var usuariosSinPagos = await _context.Usuarios
                .Where(u => u.RolesUsuarios.tipo_rol == "Alumno" && (u.Pagos == null || !u.Pagos.Any()))
                .ToListAsync();

            return usuariosSinPagos;
        }


        public async Task<byte[]> GenerarUsuariosSinPagosPDF(List<Usuario> usuarios)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Agregar el logotipo (ajusta la ruta de la imagen)
                    string imagePath = @"https://i.imgur.com/RyVmq11.jpg";
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    image.ScaleToFit(100, 100);
                    image.SetAbsolutePosition(50, 750);
                    doc.Add(image);

                    // Título
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    Paragraph title = new Paragraph("Usuarios con cuotas impagas");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    // Detalles de usuarios
                    foreach (var usuario in usuarios)
                    {
                        doc.Add(new Paragraph("DNI: " + usuario.dni));
                        doc.Add(new Paragraph("Nombre Completo: " + usuario.nombreCompleto));
                        doc.Add(new Paragraph("Correo: " + usuario.correo));
                        // Agrega más detalles aquí según tu modelo de datos
                        doc.Add(new Paragraph("\n"));
                    }

                    // División
                    doc.Add(new Paragraph("--------------------------------------------------"));

                    // Fecha
                    doc.Add(new Paragraph("Fecha de Emisión: " + DateTime.Now.ToString("dd/MM/yyyy")));

                    doc.Close();
                    writer.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el PDF de usuarios con cuotas impagas.", ex);
            }
        }

        public async Task<byte[]> GenerarInformeIngresosPDFAsync()
        {
            try
            {
                var pagos = await _context.Pagos.ToListAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (iTextSharp.text.Document doc = new iTextSharp.text.Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                        doc.Open();

                        // Agregar el logotipo (ajusta la ruta de la imagen)
                        string imagePath = "https://i.imgur.com/RyVmq11.jpg";
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                        image.ScaleToFit(100, 100);
                        image.SetAbsolutePosition(50, 750);
                        doc.Add(image);

                        // Título
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("\n"));
                        Paragraph title = new Paragraph("Informe de Ingresos");
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        // Suma de montos
                        double totalMonto = 0;

                        // Número total de pagos
                        int totalPagos = pagos.Count;

                        // Agregar detalles de pagos
                        foreach (var pago in pagos)
                        {
                            // Convierte el monto de cadena a double
                            if (double.TryParse(pago.monto, out double monto))
                            {
                                totalMonto += monto;
                            }

                            doc.Add(new Paragraph("\n"));
                            doc.Add(new Paragraph($"Número de factura: {pago.nro_factura}"));
                            doc.Add(new Paragraph($"Monto: {pago.monto}"));
                            doc.Add(new Paragraph($"Concepto: {pago.concepto}"));
                            doc.Add(new Paragraph($"Tipo de pago: {pago.tipo_pago}"));

                            if (pago.Usuario != null)
                            {
                                doc.Add(new Paragraph($"Nombre del usuario: {pago.Usuario.nombreCompleto}"));
                                doc.Add(new Paragraph($"Correo del usuario: {pago.Usuario.correo}"));
                            }
                        }

                        // Agregar el total al pie de página
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph($"Total de pagos: {totalPagos}"));
                        doc.Add(new Paragraph($"Total de ingresos: {totalMonto:C}"));

                        // Fecha
                        PdfPTable table = new PdfPTable(1);
                        table.WidthPercentage = 100;
                        PdfPCell cell = new PdfPCell(new Phrase($"Fecha de Emisión: {DateTime.Now.ToString("dd/MM/yyyy")}"));
                        cell.Border = 0;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(cell);
                        doc.Add(table);
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el informe de ingresos PDF.", ex);
            }
        }

}
    public interface IPagosService
        {
            Task<List<Pago>> ObtenerPagos();
            Task<Pago> ObtenerPagoPorId(int id);
            Task<IEnumerable<Pago>> ObtenerPagoPorDniUsuario(string dniUsuario);
            Task<Pago> AgregarPago(Pago nuevoPago);
            Task PagoEditar(int id, Pago pagoActualizado);
            Task EliminarPago(int id);
            Task<byte[]> GenerarFacturaPagoPDFAsync(int idPago);

            Task<byte[]> GenerarUsuariosSinPagosPDF(List<Usuario> usuarios);
            Task<List<Usuario>> ObtenerUsuariosSinPagosAsync();

            Task<byte[]> GenerarInformeIngresosPDFAsync();
            Task<byte[]> GenerarDocumento(int idPago);
    }
}