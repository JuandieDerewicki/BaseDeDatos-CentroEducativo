using CentroEducativoAPISQL.Modelos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    public class UsuarioClaseService :IUsuarioClaseService
    {
        private readonly MiDbContext _context;

        public UsuarioClaseService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<string> AsignarUsuarioAClaseAsync(string dni, int idClase)
        {
            try
            {
                // Verificar si la asignación ya existe
                var existingUsuarioClase = await _context.UsuariosClases
                    .FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdClase == idClase);

                if (existingUsuarioClase != null)
                {
                    return "La relación usuario-clase ya existe.";
                }

                var nuevoUsuarioClase = new UsuarioClase
                {
                    Dni = dni,
                    IdClase = idClase
                };

                _context.UsuariosClases.Add(nuevoUsuarioClase);
                await _context.SaveChangesAsync();

                return "Usuario asignado a la clase correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al asignar usuario a la clase: {ex.Message}";
            }
        }

        public async Task<string> EliminarUsuarioDeClaseAsync(string dni, int idClase)
        {
            try
            {
                var usuarioClase = await _context.UsuariosClases
                    .FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdClase == idClase);

                if (usuarioClase == null)
                {
                    return "La relación usuario-clase no fue encontrada.";
                }

                _context.UsuariosClases.Remove(usuarioClase);
                await _context.SaveChangesAsync();

                return "Usuario eliminado de la clase con éxito.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar usuario de la clase: {ex.Message}";
            }
        }

        public async Task<List<Clase>> MostrarClasesConUsuariosAsync()
        {
            // Retorna todas las clases con usuarios asignados
            return await _context.Clases
                .Where(c => c.UsuariosClases.Any())
                .Include(c => c.UsuariosClases)
                .ToListAsync();
        }

        public async Task<List<Clase>> BuscarClasesPorDNIAsync(string dni)
        {
            // Retorna clases por DNI de usuario
            return await _context.Clases
                .Where(c => c.UsuariosClases.Any(uc => uc.Dni == dni))
                .Include(c => c.UsuariosClases)
                .ToListAsync();
        }

        public async Task<List<Usuario>> ListarAlumnosPorDocenteAsync(string dniDocente)
        {
            return await _context.Usuarios
                .Where(u => u.UsuariosClases
                    .Any(uc => uc.Clase.CursoClases
                        .Any(cc => cc.Clase.UsuariosClases
                            .Any(ud => ud.Usuario.dni == dniDocente))))
                .ToListAsync();
        }


        //public async Task<byte[]> GenerarListaAlumnosPDF(int idCurso)
        //{
        //    try
        //    {
        //        var curso = await _context.Curso.FindAsync(idCurso);
        //        if (curso == null)
        //        {
        //            throw new KeyNotFoundException("Curso no encontrado");
        //        }

        //        var alumnos = await _context.UsuariosCursos
        //            .Where(uc => uc.IdCurso == idCurso)
        //            .Select(uc => uc.Usuario)
        //            .OrderBy(uc => uc.nombreCompleto)
        //            .ToListAsync();

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
        //            Paragraph title = new Paragraph("Lista de Alumnos");
        //            title.Alignment = Element.ALIGN_CENTER;
        //            doc.Add(title);

        //            doc.Add(new Paragraph("Curso: " + curso.nombre_curso));

        //            // Agregar el nombre del docente y la materia
        //            var docente = await _context.Usuarios.FindAsync(Clase clase);
        //            if (docente != null)
        //            {
        //                doc.Add(new Paragraph("Docente: " + docente.nombreCompleto));
        //                doc.Add(new Paragraph("Materia: " + curso.materia));
        //            }

        //            // Lista de alumnos
        //            foreach (var alumno in alumnos)
        //            {
        //                doc.Add(new Paragraph("\n"));
        //                doc.Add(new Paragraph("\n"));
        //                doc.Add(new Paragraph("Nro. de Legajo: " + alumno.dni));
        //                doc.Add(new Paragraph("Apellido-Nombre: " + alumno.nombreCompleto));
        //            }

        //            // División
        //            doc.Add(new Paragraph("--------------------------------------------------"));

        //            // Fecha
        //            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

        //            // Total de alumnos
        //            doc.Add(new Paragraph("Total de Alumnos: " + alumnos.Count));

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





    }

    public interface IUsuarioClaseService
    {
        Task<string> AsignarUsuarioAClaseAsync(string dni, int idClase);
        Task<string> EliminarUsuarioDeClaseAsync(string dni, int idClase);
        Task<List<Clase>> MostrarClasesConUsuariosAsync();
        Task<List<Clase>> BuscarClasesPorDNIAsync(string dni);
        Task<List<Usuario>> ListarAlumnosPorDocenteAsync(string dniDocente);

    }

}
