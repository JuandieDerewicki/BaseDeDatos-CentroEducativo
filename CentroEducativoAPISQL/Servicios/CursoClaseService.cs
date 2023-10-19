using CentroEducativoAPISQL.Modelos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroEducativoAPISQL.Servicios
{
    public class CursoClaseService : ICursoClaseService
    {
        private readonly MiDbContext _context;

        public CursoClaseService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<string> AgregarClaseACursoAsync(CursoClase cursoClase)
        {
            try
            {
                _context.CursoClases.Add(cursoClase);
                await _context.SaveChangesAsync();
                return "Clase agregada al curso correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar clase al curso: {ex.Message}";
            }
        }


        public async Task<string> EliminarClaseDeCursoAsync(CursoClase cursoClase)
        {
            try
            {
                var existingCursoClase = await _context.CursoClases
                    .FirstOrDefaultAsync(cc => cc.IdCurso == cursoClase.IdCurso && cc.IdClase == cursoClase.IdClase);

                if (existingCursoClase == null)
                {
                    return "La relación curso-clase no fue encontrada.";
                }

                _context.CursoClases.Remove(existingCursoClase);
                await _context.SaveChangesAsync();

                return "Clase eliminada del curso con éxito.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar clase del curso: {ex.Message}";
            }
        }

        public async Task<IEnumerable<Clase>> MostrarClasesDeCursoAsync(int idCurso)
        {
            return await _context.CursoClases
                .Where(cc => cc.IdCurso == idCurso)
                .Select(cc => cc.Clase)
                .ToListAsync();
        }

        public async Task<IEnumerable<Curso>> MostrarCursosConClasesAsync()
        {
            return await _context.Curso
                .Where(c => c.CursoClases.Any())
                .Include(c => c.CursoClases)
                .ThenInclude(cc => cc.Clase)
                .ToListAsync();
        }

        public async Task<List<Usuario>> ListarAlumnosPorDocenteAsync(string dniDocente)
        {
            var alumnos = await _context.CursoClases
                .Where(cc => cc.Clase.Profesor.dni == dniDocente)
                .SelectMany(cc => cc.Curso.UsuariosCursos)
                .Select(uc => uc.Usuario)
                .Distinct()
                .ToListAsync();

            return alumnos;
        }


        //public async Task<byte[]> GenerarListaAlumnosPorDocentePDF(string dniDocente)
        //{
        //    try
        //    {
        //        var alumnos = await ListarAlumnosPorDocenteAsync(dniDocente);

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            Document doc = new Document();
        //            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
        //            doc.Open();

        //            // Agregar el logotipo (ajusta la ruta de la imagen)
        //            string imagePath = @"https://i.imgur.com/RyVmq11.jpg";
        //            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
        //            image.ScaleToFit(100, 100);
        //            image.SetAbsolutePosition(50, 750); // Ajusta la posición vertical
        //            doc.Add(image);

        //            // Título
        //            doc.Add(new Paragraph("\n"));
        //            doc.Add(new Paragraph("\n"));
        //            Paragraph title = new Paragraph("Lista de Alumnos por Docente");
        //            title.Alignment = Element.ALIGN_CENTER;
        //            doc.Add(title);

        //            // Lista de alumnos
        //            var contador = 0;
        //            foreach (var alumno in alumnos)
        //            {
        //                contador++;
        //                doc.Add(new Paragraph("\n"));
        //                doc.Add(new Paragraph("Nro. de Legajo: " + alumno.dni));
        //                doc.Add(new Paragraph("Nombre Completo: " + alumno.nombreCompleto));
        //            }

        //            // División
        //            doc.Add(new Paragraph("--------------------------------------------------"));

        //            // Fecha
        //            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

        //            // Total de alumnos
        //            doc.Add(new Paragraph("Total de Alumnos: " + contador));

        //            doc.Close();
        //            writer.Close();

        //            return ms.ToArray();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al generar el PDF.", ex);
        //    }
        //}

        //public async Task<byte[]> GenerarListaAlumnosPorDocentePDF(string dniDocente)
        //{
        //    try
        //    {

        //        var alumnos = await ListarAlumnosPorDocenteAsync(dniDocente);

        //        var docente = await _context.Usuarios
        //            .FirstOrDefaultAsync(u => u.dni == dniDocente);

        //        if (docente == null)
        //        {
        //            throw new KeyNotFoundException("Docente no encontrado");
        //        }


        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            Document doc = new Document();
        //            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
        //            doc.Open();

        //            // Agregar el logotipo (ajusta la ruta de la imagen)
        //            string imagePath = @"https://i.imgur.com/RyVmq11.jpg";
        //            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
        //            image.ScaleToFit(100, 100);
        //            image.SetAbsolutePosition(50, 750); // Ajusta la posición vertical
        //            doc.Add(image);

        //            // Título
        //            doc.Add(new Paragraph("\n"));
        //            doc.Add(new Paragraph("\n"));
        //            Paragraph title = new Paragraph("Lista de Alumnos por Docente");
        //            title.Alignment = Element.ALIGN_CENTER;
        //            doc.Add(title);

        //            var contador = 0;

        //            // Obtener detalles de las clases del profesor
        //            //var clasesDelProfesor = await _context.Clases
        //            //    .Where(clase => clase.Profesor.dni == dniDocente)
        //            //    .ToListAsync();


        //            var clasesDelDocente = await _context.Clases
        //                .Where(c => c.id_profesor == dniDocente)
        //                .ToListAsync();

        //            doc.Add(new Paragraph("Profesor: " + docente.nombreCompleto));

        //            foreach (var clase in clasesDelDocente)
        //            {
        //                //Curso curso = new Curso();
        //                // Detalles de la clase
        //                doc.Add(new Paragraph("Materia: " + clase.materia));
        //                doc.Add(new Paragraph("Horario: " + clase.hora_inicio + " - " + clase.hora_fin));
        //                // doc.Add(new Paragraph("Curso: " + curso.nombre_curso));

        //                // Lista de alumnos
        //                var alumnosDeClase = alumnos.Where(alumno => alumno.UsuariosClases.Any(uc => uc.IdClase == clase.id_clase)).ToList();
        //                foreach (var alumno in alumnosDeClase)
        //                {
        //                    contador++;
        //                    doc.Add(new Paragraph("\n"));
        //                    doc.Add(new Paragraph("Nro. de Legajo: " + alumno.dni));
        //                    doc.Add(new Paragraph("Nombre Completo: " + alumno.nombreCompleto));
        //                }

        //                // División
        //                doc.Add(new Paragraph("--------------------------------------------------"));
        //            }

        //            // Fecha
        //            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

        //            // Total de alumnos
        //            doc.Add(new Paragraph("Total de Alumnos: " + contador));

        //            doc.Close();
        //            writer.Close();

        //            return ms.ToArray();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al generar el PDF.", ex);
        //    }
        //}

        public async Task<byte[]> GenerarListaAlumnosPorDocentePDF(string dniDocente)
        {
            try
            {
                var docente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.dni == dniDocente);

                if (docente == null)
                {
                    throw new KeyNotFoundException("Docente no encontrado");
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    Document doc = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Agregar el logotipo (ajusta la ruta de la imagen)
                    string imagePath = @"https://i.imgur.com/RyVmq11.jpg";
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    image.ScaleToFit(100, 100);
                    image.SetAbsolutePosition(50, 750); // Ajusta la posición vertical
                    doc.Add(image);

                    // Título
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    Paragraph title = new Paragraph("Lista de Alumnos por Docente");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    doc.Add(new Paragraph("Docente: " + docente.nombreCompleto));

                    // Obtener las clases del docente
                    var clasesDelDocente = await _context.Clases
                        .Where(c => c.id_profesor == dniDocente)
                        .ToListAsync();

                    // Lista de alumnos
                    var contador = 0;
                    foreach (var clase in clasesDelDocente)
                    {
                        doc.Add(new Paragraph("Materia: " + clase.materia));
                        doc.Add(new Paragraph("Horario: " + clase.hora_inicio + " - " + clase.hora_fin));
                        var alumnos = await _context.UsuariosClases
                            .Where(uc => uc.IdClase == clase.id_clase)
                            .Select(uc => uc.Usuario)
                            .OrderBy(uc => uc.nombreCompleto)
                            .ToListAsync();

                        foreach (var alumno in alumnos)
                        {
                            contador++;
                            doc.Add(new Paragraph("\n"));
                            doc.Add(new Paragraph("Nro. de Legajo: " + alumno.dni));
                            doc.Add(new Paragraph("Nombre Completo: " + alumno.nombreCompleto));
                        }
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("--------------------------------------------------"));
                    }

                    // Fecha
                    doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

                    // Total de alumnos
                    doc.Add(new Paragraph("Total de Alumnos: " + contador));

                    doc.Close();
                    writer.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el PDF.", ex);
            }
        }



    }
    public interface ICursoClaseService
    {
        Task<string> AgregarClaseACursoAsync(CursoClase cursoClase);
        Task<string> EliminarClaseDeCursoAsync(CursoClase cursoClase);
        Task<IEnumerable<Clase>> MostrarClasesDeCursoAsync(int idCurso);
        Task<IEnumerable<Curso>> MostrarCursosConClasesAsync();

        Task<List<Usuario>> ListarAlumnosPorDocenteAsync(string dniDocente);
        Task<byte[]> GenerarListaAlumnosPorDocentePDF(string dniDocente);
    }
}
