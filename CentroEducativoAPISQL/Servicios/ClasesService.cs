using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    public class ClasesService : IClasesService
    {
        private readonly MiDbContext _context;

        public ClasesService(MiDbContext context)
        {
            _context = context;
        }

        // Método para obtener todas las clases
        public async Task<List<Clase>> ObtenerTodasLasClasesAsync()
        {
            return await _context.Clases.ToListAsync();
        }

        // Método para obtener una clase por su ID
        public async Task<Clase> ObtenerClasePorIdAsync(int idClase)
        {
            return await _context.Clases.FirstOrDefaultAsync(c => c.id_clase == idClase);
        }

        public async Task<List<Clase>> BuscarClasesPorNombreAsync(string nombreClase)
        {
            return await _context.Clases
                .Where(c => c.materia.Contains(nombreClase))
                .ToListAsync();
        }

        public async Task<List<Clase>> BuscarClasesPorProfesorAsync(string nombreProfesor)
        {
            return await _context.Clases
                .Where(c => c.Usuarios != null && c.Usuarios.nombreCompleto.Contains(nombreProfesor))
                .ToListAsync();
        }

            // Método para crear una nueva clase
            public async Task<Clase> CrearClaseAsync(Clase nuevaClase)
        {
            _context.Clases.Add(nuevaClase);
            await _context.SaveChangesAsync();
            return nuevaClase;
        }

        // Método para actualizar una clase existente
        public async Task<Clase> ActualizarClaseAsync(int idClase, Clase claseActualizada)
        {
            var clase = await _context.Clases.FirstOrDefaultAsync(c => c.id_clase == idClase);

            if (clase == null)
            {
                throw new KeyNotFoundException("Clase no encontrada");
            }

            // Actualiza los campos de la clase según la claseActualizada
            clase.hora_inicio = claseActualizada.hora_inicio;
            clase.hora_fin = claseActualizada.hora_fin;
            clase.materia = claseActualizada.materia;
            clase.id_usuario = claseActualizada.id_usuario;
            clase.id_curso = claseActualizada.id_curso;

            await _context.SaveChangesAsync();

            return clase;
        }

        // Método para eliminar una clase por su ID
        public async Task<string> EliminarClaseAsync(int idClase)
        {
            var clase = await _context.Clases.FirstOrDefaultAsync(c => c.id_clase == idClase);

            if (clase == null)
            {
                throw new KeyNotFoundException("Clase no encontrada");
            }

            _context.Clases.Remove(clase);
            await _context.SaveChangesAsync();

            return "Clase eliminada con éxito.";
        }
    }

    public interface IClasesService
    {
        Task<List<Clase>> ObtenerTodasLasClasesAsync();
        Task<Clase> ObtenerClasePorIdAsync(int idClase);
        Task<List<Clase>> BuscarClasesPorNombreAsync(string nombreClase);
        Task<List<Clase>> BuscarClasesPorProfesorAsync(string nombreProfesor);
        Task<Clase> CrearClaseAsync(Clase nuevaClase);
        Task<Clase> ActualizarClaseAsync(int idClase, Clase claseActualizada);
        Task<string> EliminarClaseAsync(int idClase);


    }
}
