using CentroEducativoAPISQL.Modelos;
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

        // Método para obtener todos los cursos
        public async Task<IEnumerable<Curso>> ObtenerCursosAsync()
        {
            return await _context.Curso.ToListAsync();
        }

        // Método para obtener un curso por su ID
        public async Task<Curso> ObtenerCursoPorIdAsync(int idCurso)
        {
            return await _context.Curso.FirstOrDefaultAsync(c => c.id_curso == idCurso);
        }

       

        // Método para obtener los alumnos de un curso
        public async Task<IEnumerable<Usuario>> ObtenerAlumnosDelCursoAsync(int idCurso)
        {
            var curso = await _context.Curso
                .Include(c => c.Usuarios)
                .FirstOrDefaultAsync(c => c.id_curso == idCurso);

            if (curso != null)
            {
                return curso.Usuarios.Where(u => u.RolesUsuarios?.tipo_rol == "Alumno").ToList();
            }
            else
            {
                throw new KeyNotFoundException("El curso no existe.");
            }
        }


        //public List<Usuario> ObtenerAlumnosPorDocente(string docenteId)
        //{
        //    try
        //    {
        //        var cursosDelDocente = _context.Curso
        //            .Where(c => c.id_usuario == docenteId)
        //            .ToList();

        //        var alumnos = new List<Usuario>();

        //        foreach (var curso in cursosDelDocente)
        //        {
        //            var alumnosDelCurso = curso.Usuarios.ToList();
        //            alumnos.AddRange(alumnosDelCurso);
        //        }

        //        return alumnos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener los alumnos por docente.", ex);
        //    }
        //}

        public List<Usuario> ObtenerAlumnosPorDocente(string docenteId)
        {
            try
            {
                var alumnos = _context.Clases
                    .Where(clase => clase.id_usuario == docenteId) // Filtrar por docente
                    .SelectMany(clase => clase.Curso.Usuarios) // Obtener alumnos de los cursos relacionados con las clases
                    .Distinct() // Eliminar duplicados
                    .ToList();

                return alumnos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los alumnos por docente.", ex);
            }
        }

        public List<Usuario> ObtenerAlumnosDeTodosLosCursos()
        {
            try
            {
                // Obtiene todos los cursos y sus alumnos
                var cursos = _context.Curso.Include(c => c.Usuarios).ToList();

                // Extrae y concatena a todos los alumnos de todos los cursos
                var alumnos = cursos.SelectMany(curso => curso.Usuarios).ToList();

                return alumnos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los alumnos de todos los cursos.", ex);
            }
        }

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

        public List<Usuario> ObtenerAlumnosPorNombreDeCurso(string nombreCurso)
        {
            try
            {
                var curso = _context.Curso
                    .Include(c => c.Usuarios)
                    .FirstOrDefault(c => c.nombre_curso == nombreCurso);

                if (curso == null)
                {
                    throw new KeyNotFoundException("Curso no encontrado");
                }

                return curso.Usuarios.ToList();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los alumnos por nombre de curso.", ex);
            }

        }

        public List<Usuario> ObtenerAlumnosPorIdDeCurso(int idCurso)
        {
            try
            {
                var curso = _context.Curso
                    .Include(c => c.Usuarios)
                    .FirstOrDefault(c => c.id_curso == idCurso);

                if (curso == null)
                {
                    throw new KeyNotFoundException("Curso no encontrado");
                }

                return curso.Usuarios.ToList();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los alumnos por ID de curso.", ex);
            }
        }

        public Usuario BuscarAlumnoPorDNIEnCurso(string dni, int idCurso)
        {
            try
            {
                var curso = _context.Curso
                    .Include(c => c.Usuarios)
                    .FirstOrDefault(c => c.id_curso == idCurso);

                if (curso == null)
                {
                    throw new KeyNotFoundException("Curso no encontrado");
                }

                var alumno = curso.Usuarios.FirstOrDefault(u => u.dni == dni);

                return alumno;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar al alumno por DNI en el curso.", ex);
            }
        }

        public Usuario BuscarAlumnoEnCualquierCurso(string dni)
        {
            try
            {
                // Busca al alumno en todos los cursos
                var cursos = _context.Curso.Include(c => c.Usuarios).ToList();
                var alumno = cursos.SelectMany(curso => curso.Usuarios).FirstOrDefault(u => u.dni == dni);

                return alumno;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar al alumno en cualquier curso.", ex);
            }
        }

        public List<Usuario> ObtenerProfesoresPorCurso(int idCurso)
        {
            try
            {
                var curso = _context.Curso
                    .Include(c => c.Usuarios)
                    .FirstOrDefault(c => c.id_curso == idCurso);

                if (curso == null)
                {
                    throw new KeyNotFoundException("Curso no encontrado");
                }

                var profesores = curso.Usuarios.Where(u => u.id_rol == 3).ToList();

                return profesores;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener profesores por curso.", ex);
            }
        }

        // Método para agregar un nuevo curso
        public async Task<Curso> AgregarCursoAsync(Curso curso)
        {
            _context.Curso.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        // Método para editar información de un curso
        public async Task<Curso> EditarCursoAsync(int idCurso, Curso curso)
        {
            if (idCurso != curso.id_curso)
            {
                throw new Exception("El ID del curso no coincide con el curso proporcionado.");
            }

            _context.Entry(curso).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return curso;
        }

        // Método para eliminar un curso
        public async Task<string> EliminarCursoAsync(int idCurso)
        {
            var curso = await _context.Curso.FindAsync(idCurso);

            if (curso == null)
            {
                throw new KeyNotFoundException("Curso no encontrado");
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
        Task<IEnumerable<Usuario>> ObtenerAlumnosDelCursoAsync(int idCurso);
        List<Usuario> ObtenerAlumnosPorDocente(string docenteId);
        List<Usuario> ObtenerAlumnosDeTodosLosCursos();
        Curso ObtenerCursoPorNombre(string nombreCurso);
        List<Usuario> ObtenerAlumnosPorNombreDeCurso(string nombreCurso);
        List<Usuario> ObtenerAlumnosPorIdDeCurso(int idCurso);
        Usuario BuscarAlumnoPorDNIEnCurso(string dni, int idCurso);
        Usuario BuscarAlumnoEnCualquierCurso(string dni);
        List<Usuario> ObtenerProfesoresPorCurso(int idCurso);
        Task<Curso> AgregarCursoAsync(Curso curso);
        Task<Curso> EditarCursoAsync(int idCurso, Curso curso);
        Task<string> EliminarCursoAsync(int idCurso);

    }
}

