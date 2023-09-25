//using CentroEducativoAPISQL.Modelos;
//using Microsoft.EntityFrameworkCore;

//namespace CentroEducativoAPISQL.Servicios
//{
//    // servicio que se encarga de manejar operaciones relacionadas con los alumnos. 
//    public class AlumnoService : IAlumnoService
//    {
//        // contexto de la BD. Este contexto se utiliza para interactuar con la BD y realizar operaciones de lectura en la entidad de Alumnos.
//        private readonly MiDbContext _context;

//        public AlumnoService(MiDbContext context)
//        {
//            _context = context;
//        }

//        // Metodo que permite obtener lista de los alumnos almacenados en la BD
//        public async Task<List<Alumno>> ObtenerTodosAlumnosAsync()
//        {
//            return await _context.Alumnos.ToListAsync();
//        }

//        // Busca alumnos por ID
//        public async Task<Alumno> ObtenerAlumnoPorIdAsync(int id)
//        {
//            return await _context.Alumnos.FirstOrDefaultAsync(a => a.id_alumno == id);
//        }
//    }

//    // La interfaz IAlumnoService define los métodos que debe implementar AlumnoService y proporciona una abstracción para interactuar con los datos de los alumnos.
//    public interface IAlumnoService
//    {
//        Task<List<Alumno>> ObtenerTodosAlumnosAsync();
//        Task<Alumno> ObtenerAlumnoPorIdAsync(int id);
//    }
//}
