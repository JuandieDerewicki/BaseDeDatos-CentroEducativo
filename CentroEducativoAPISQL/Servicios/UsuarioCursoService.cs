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
    public class UsuarioCursoService : IUsuarioCursoService
    {
        private readonly MiDbContext _context;

        public UsuarioCursoService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosEnCursoAsync(int idCurso)
        {
            try
            {
                var usuarios = await _context.UsuariosCursos
                    .Where(uc => uc.IdCurso == idCurso)
                    .Select(uc => uc.Usuario)
                    .ToListAsync();

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios en el curso.", ex);
            }
        }

        public Usuario BuscarUsuarioPorDNIEnCurso(string dni, int idCurso)
        {
            try
            {
                var usuario = _context.UsuariosCursos
                    .Where(uc => uc.Dni == dni && uc.IdCurso == idCurso)
                    .Select(uc => uc.Usuario)
                    .FirstOrDefault();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar un usuario en el curso por DNI.", ex);
            }
        }

        public List<Usuario> BuscarUsuariosEnTodosLosCursos()
        {
            try
            {
                var usuarios = _context.UsuariosCursos
                    .Select(uc => uc.Usuario)
                    .Distinct()
                    .ToList();

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuarios en todos los cursos.", ex);
            }
        }

        //public async Task<UsuarioCurso> AsignarUsuarioACursoAsync(UsuarioCurso usuarioCurso)
        //{
        //    _context.UsuariosCursos.Add(usuarioCurso);
        //    await _context.SaveChangesAsync();
        //    return usuarioCurso;
        //}

        public async Task<string> AsignarUsuarioACursoAsync(string dni, int idCurso)
        {
            try
            {
                var existingUsuarioCurso = await _context.UsuariosCursos
                    .FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdCurso == idCurso);

                if (existingUsuarioCurso != null)
                {
                    return "La relación usuario-curso ya existe.";
                }

                var nuevoUsuarioCurso = new UsuarioCurso
                {
                    Dni = dni,
                    IdCurso = idCurso
                };

                _context.UsuariosCursos.Add(nuevoUsuarioCurso);
                await _context.SaveChangesAsync();

                return "Usuario asignado al curso correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al asignar usuario al curso: {ex.Message}";
            }
        }

        public async Task<string> DesasignarUsuarioDeCursoAsync(string dni, int idCurso)
        {
            try
            {
                var usuarioCurso = await _context.UsuariosCursos.FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdCurso == idCurso);

                if (usuarioCurso == null)
                {
                    return "Relación usuario-curso no encontrada.";
                }

                _context.UsuariosCursos.Remove(usuarioCurso);
                await _context.SaveChangesAsync();

                return "Usuario desasignado del curso.";
            }
            catch (Exception ex)
            {
                return $"Error al desasignar usuario de curso: {ex.Message}";
            }
        }

        public async Task<string> EliminarUsuarioDeCursoAsync(string dni, int idCurso)
        {
            try
            {
                var usuarioCurso = await _context.UsuariosCursos
                    .FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdCurso == idCurso);

                if (usuarioCurso == null)
                {
                    return "Usuario no encontrado en el curso.";
                }

                _context.UsuariosCursos.Remove(usuarioCurso);
                await _context.SaveChangesAsync();

                return "Usuario eliminado del curso con éxito.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario de un curso.", ex);
            }
        }

        public async Task<string> EditarUsuarioEnCursoAsync(string dni, int idCurso, UsuarioCurso usuarioCurso)
        {
            try
            {
                var existingUsuarioCurso = await _context.UsuariosCursos
                    .FirstOrDefaultAsync(uc => uc.Dni == dni && uc.IdCurso == idCurso);

                if (existingUsuarioCurso == null)
                {
                    return "Usuario no encontrado en el curso.";
                }

                existingUsuarioCurso.Dni = usuarioCurso.Dni;
                existingUsuarioCurso.IdCurso = usuarioCurso.IdCurso;

                _context.Entry(existingUsuarioCurso).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return "Usuario editado en el curso correctamente.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar usuario en un curso.", ex);
            }
        }

        public async Task<byte[]> GenerarListaAlumnosPDF(int idCurso)
        {
            try
            {
                var curso = await _context.Curso.FindAsync(idCurso);
                if (curso == null)
                {
                    throw new KeyNotFoundException("Curso no encontrado");
                }

                var alumnos = await _context.UsuariosCursos
                    .Where(uc => uc.IdCurso == idCurso)
                    .Select(uc => uc.Usuario)
                    .OrderBy(uc => uc.nombreCompleto) // Ordenar alfabéticamente por nombre
                    .ToListAsync();

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
                    Paragraph title = new Paragraph("Lista de Alumnos");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    doc.Add(new Paragraph("Curso: " + curso.nombre_curso));
                    // Lista de alumnos
                    foreach (var alumno in alumnos)
                    {
                        
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("Nro. de Legajo: " + alumno.dni));
                        doc.Add(new Paragraph("Apellido-Nombre: " + alumno.nombreCompleto));
                    }

                    // División
                    doc.Add(new Paragraph("--------------------------------------------------"));

                    // Fecha
                    doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));

                    // Total de alumnos
                    doc.Add(new Paragraph("Total de Alumnos: " + alumnos.Count));

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
    public interface IUsuarioCursoService
    {
        Task<IEnumerable<Usuario>> ListarUsuariosEnCursoAsync(int idCurso);
        Usuario BuscarUsuarioPorDNIEnCurso(string dni, int idCurso);
        List<Usuario> BuscarUsuariosEnTodosLosCursos();
        Task<string> AsignarUsuarioACursoAsync(string dni, int idcurso);
        Task<string> DesasignarUsuarioDeCursoAsync(string dni, int idCurso);
        Task<string> EliminarUsuarioDeCursoAsync(string dni, int idCurso);
        Task<string> EditarUsuarioEnCursoAsync(string dni, int idCurso, UsuarioCurso usuarioCurso);

        Task<byte[]> GenerarListaAlumnosPDF(int idCurso);
    }
}
