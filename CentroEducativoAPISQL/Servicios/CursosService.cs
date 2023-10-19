using CentroEducativoAPISQL.Modelos;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    public class CursosService : ICursoService
    {
        private readonly MiDbContext _context;

        public CursosService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> ObtenerCursosAsync()
        {
            return await _context.Curso.ToListAsync();
        }

        public async Task<Curso> ObtenerCursoPorIdAsync(int idCurso)
        {
            return await _context.Curso.FirstOrDefaultAsync(c => c.id_curso == idCurso);
        }

        //public async Task<IEnumerable<Usuario>> ObtenerAlumnosDelCursoAsync(int idCurso)
        //{
        //    var curso = await _context.Curso
        //        .Include(c => c.UsuariosCursos)
        //        .ThenInclude(uc => uc.Usuario)
        //        .FirstOrDefaultAsync(c => c.id_curso == idCurso);

        //    if (curso != null)
        //    {
        //        return curso.UsuariosCursos
        //            .Where(uc => uc.Usuario.RolesUsuarios.tipo_rol == "Alumno")
        //            .Select(uc => uc.Usuario)
        //            .ToList();
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException("El curso no existe.");
        //    }
        //}

        //public List<Usuario> ObtenerAlumnosPorDocente(string docenteId)
        //{
        //    try
        //    {
        //        var alumnos = _context.UsuariosClases
        //            .Where(uc => uc.Clase.UsuariosClases.Any(uc => uc.Usuario.dni == docenteId))
        //            .Select(uc => uc.Usuario)
        //            .Distinct()
        //            .ToList();

        //        return alumnos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener los alumnos por docente.", ex);
        //    }
        //}

        //public List<Usuario> ObtenerAlumnosDeTodosLosCursos()
        //{
        //    try
        //    {
        //        var alumnos = _context.UsuariosCursos
        //            .Select(uc => uc.Usuario)
        //            .Distinct()
        //            .ToList();

        //        return alumnos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener los alumnos de todos los cursos.", ex);
        //    }
        //}

        public Curso ObtenerCursoPorNombre(string nombreCurso)
        {
            try
            {
                var curso = _context.Curso.FirstOrDefault(c => c.nombre_curso == nombreCurso);
                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el curso por nombre.", ex);
            }
        }

        //public List<Usuario> ObtenerAlumnosPorNombreDeCurso(string nombreCurso)
        //{
        //    try
        //    {
        //        var curso = _context.Curso
        //            .Include(c => c.UsuariosCursos)
        //            .ThenInclude(uc => uc.Usuario)
        //            .FirstOrDefault(c => c.nombre_curso == nombreCurso);

        //        if (curso == null)
        //        {
        //            throw new KeyNotFoundException("Curso no encontrado");
        //        }

        //        return curso.UsuariosCursos
        //            .Select(uc => uc.Usuario)
        //            .ToList();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener los alumnos por nombre de curso.", ex);
        //    }
        //}

        //public List<Usuario> ObtenerAlumnosPorIdDeCurso(int idCurso)
        //{
        //    try
        //    {
        //        var curso = _context.Curso
        //            .Include(c => c.UsuariosCursos)
        //            .ThenInclude(uc => uc.Usuario)
        //            .FirstOrDefault(c => c.id_curso == idCurso);

        //        if (curso == null)
        //        {
        //            throw new KeyNotFoundException("Curso no encontrado");
        //        }

        //        return curso.UsuariosCursos
        //            .Select(uc => uc.Usuario)
        //            .ToList();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener los alumnos por ID de curso.", ex);
        //    }
        //}

        //public Usuario BuscarAlumnoPorDNIEnCurso(string dni, int idCurso)
        //{
        //    try
        //    {
        //        var curso = _context.Curso
        //            .Include(c => c.UsuariosCursos)
        //            .ThenInclude(uc => uc.Usuario)
        //            .FirstOrDefault(c => c.id_curso == idCurso);

        //        if (curso == null)
        //        {
        //            throw new KeyNotFoundException("Curso no encontrado");
        //        }

        //        var alumno = curso.UsuariosCursos
        //            .Where(uc => uc.Usuario.dni == dni)
        //            .Select(uc => uc.Usuario)
        //            .FirstOrDefault();

        //        return alumno;
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al buscar al alumno por DNI en el curso.", ex);
        //    }
        //}

        //public Usuario BuscarAlumnoEnCualquierCurso(string dni)
        //{
        //    try
        //    {
        //        var alumno = _context.UsuariosCursos
        //            .Where(uc => uc.Usuario.dni == dni)
        //            .Select(uc => uc.Usuario)
        //            .FirstOrDefault();

        //        return alumno;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al buscar al alumno en cualquier curso.", ex);
        //    }
        //}

        //public List<Usuario> ObtenerProfesoresPorCurso(int idCurso)
        //{
        //    try
        //    {
        //        var curso = _context.Curso
        //            .Include(c => c.UsuariosCursos)
        //            .ThenInclude(uc => uc.Usuario)
        //            .FirstOrDefault(c => c.id_curso == idCurso);

        //        if (curso == null)
        //        {
        //            throw new KeyNotFoundException("Curso no encontrado");
        //        }

        //        var profesores = curso.UsuariosCursos
        //            .Where(uc => uc.Usuario.RolesUsuarios.tipo_rol == "Profesor")
        //            .Select(uc => uc.Usuario)
        //            .ToList();

        //        return profesores;
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener profesores por curso.", ex);
        //    }
        //}

        public async Task<Curso> AgregarCursoAsync(Curso curso)
        {
            _context.Curso.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<string> EditarCursoAsync(int idCurso, Curso curso)
        {
            var existingCurso = await _context.Curso
                .Include(c => c.UsuariosCursos)
                .FirstOrDefaultAsync(c => c.id_curso == idCurso);

            if (existingCurso == null)
            {
                return "Curso no encontrado.";
            }

            existingCurso.nombre_curso = curso.nombre_curso;
            existingCurso.aula = curso.aula;
            existingCurso.descripcion_curso = curso.descripcion_curso;

            // Actualiza las relaciones con usuarios
            existingCurso.UsuariosCursos = curso.UsuariosCursos;

            _context.Entry(existingCurso).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return "Curso actualizado correctamente.";
        }

        public async Task<string> EliminarCursoAsync(int idCurso)
        {
            var curso = await _context.Curso.FindAsync(idCurso);

            if (curso == null)
            {
                return "Curso no encontrado.";
            }

            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();

            return "Curso eliminado con éxito.";
        }
    }

    public interface ICursoService
    {
        Task<IEnumerable<Curso>> ObtenerCursosAsync();
        Task<Curso> ObtenerCursoPorIdAsync(int idCurso);
        //Task<IEnumerable<Usuario>> ObtenerAlumnosDelCursoAsync(int idCurso);
        //List<Usuario> ObtenerAlumnosPorDocente(string docenteId);
        //List<Usuario> ObtenerAlumnosDeTodosLosCursos();
        Curso ObtenerCursoPorNombre(string nombreCurso);
        //List<Usuario> ObtenerAlumnosPorNombreDeCurso(string nombreCurso);
        //List<Usuario> ObtenerAlumnosPorIdDeCurso(int idCurso);
        //Usuario BuscarAlumnoPorDNIEnCurso(string dni, int idCurso);
        //Usuario BuscarAlumnoEnCualquierCurso(string dni);
        //List<Usuario> ObtenerProfesoresPorCurso(int idCurso);
        Task<Curso> AgregarCursoAsync(Curso curso);
        Task<string> EditarCursoAsync(int idCurso, Curso curso);
        Task<string> EliminarCursoAsync(int idCurso);
    }

}
