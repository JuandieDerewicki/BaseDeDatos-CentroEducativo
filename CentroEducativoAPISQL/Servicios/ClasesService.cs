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

        public async Task<List<Clase>> ObtenerTodasLasClasesAsync()
        {
            return await _context.Clases.ToListAsync();
        }

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

        public async Task<Clase> CrearClaseAsync(Clase nuevaClase)
        {
            _context.Clases.Add(nuevaClase);
            await _context.SaveChangesAsync();
            return nuevaClase;
        }

        public async Task<Clase> ActualizarClaseAsync(int idClase, Clase claseActualizada)
        {
            var clase = await _context.Clases.FirstOrDefaultAsync(c => c.id_clase == idClase);

            if (clase == null)
            {
                throw new KeyNotFoundException("Clase no encontrada");
            }

            clase.hora_inicio = claseActualizada.hora_inicio;
            clase.hora_fin = claseActualizada.hora_fin;
            clase.materia = claseActualizada.materia;
            clase.UsuariosClases = claseActualizada.UsuariosClases; // Actualiza las relaciones con usuarios

            await _context.SaveChangesAsync();

            return clase;
        }

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
        //Task<List<Clase>> BuscarClasesPorProfesorAsync(string nombreProfesor);
        Task<Clase> CrearClaseAsync(Clase nuevaClase);
        Task<Clase> ActualizarClaseAsync(int idClase, Clase claseActualizada);
        Task<string> EliminarClaseAsync(int idClase);
    }
}
