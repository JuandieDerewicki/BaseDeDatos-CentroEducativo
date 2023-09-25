using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    // servicio que se encarga de manejar operaciones relacionadas con las solicitudes de inscripción. 
    public class SolicitudInscripcionService : ISolicitudInscripcionService
    {
        // representa el contexto de la BD. Este contexto se utiliza para interactuar con la BD y realizar operaciones de lectura y escritura en la entidad de Solicitudes de Inscripción.
        private readonly MiDbContext _context; 

        public SolicitudInscripcionService(MiDbContext context)
        {
            _context = context;
        }

        // Obtiene lista de todas las solicitudes de inscripcion almacenadas en la BD
        public async Task<List<SolicitudInscripcion>> ObtenerTodasSolicitudesInscripcionAsync()
        {
            return await _context.SolicitudesInscripcion.ToListAsync();
        }

        // Recibe id y busca una solicitud por ese id
        public async Task<SolicitudInscripcion> ObtenerSolicitudInscripcionPorIdAsync(int id)
        {
            return await _context.SolicitudesInscripcion.FirstOrDefaultAsync(s => s.id_solicitud == id);
        }

        public async Task<List<SolicitudInscripcion>> ObtenerSolicitudesPorUsuarioAsync(string idUsuario)
        {
            try
            {
                // Verifica si el usuario existe en la base de datos
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.dni == idUsuario);

                if (usuario != null)
                {
                    // Obtén las noticias asociadas al usuario por su ID
                    var solicitudes = await _context.SolicitudesInscripcion
                        .Where(n => n.id_usuario == idUsuario)
                        .ToListAsync();

                    return solicitudes;
                }
                else
                {
                    throw new KeyNotFoundException("El usuario no existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las noticias del usuario.", ex);
            }
        }

        public async Task<SolicitudInscripcion> AgregarSolicitudInscripcion(SolicitudInscripcion solicitudInscripcion)
        {
            // Lógica para agregar una solicitud de inscripción (por ejemplo, agregarla a la base de datos)
            _context.SolicitudesInscripcion.Add(solicitudInscripcion);
            await _context.SaveChangesAsync();
            return solicitudInscripcion; // Devuelve la solicitud de inscripción creada
        }

        public async Task<SolicitudInscripcion> EditarSolicitudInscripcion(int id, SolicitudInscripcion solicitudInscripcion)
        {
            // Lógica para editar una solicitud de inscripción (por ejemplo, actualizarla en la base de datos)
            var solicitudExistente = await _context.SolicitudesInscripcion.FindAsync(id);

            if (solicitudExistente == null)
            {
                throw new InvalidOperationException("La solicitud de inscripción no existe.");
            }

            // Actualizar propiedades de la solicitud existente con los valores de la nueva solicitud
            solicitudExistente.nombreCompletoSolicitante = solicitudInscripcion.nombreCompletoSolicitante;
            solicitudExistente.correoSolicitante = solicitudInscripcion.correoSolicitante;
            solicitudExistente.fechaNacimientoSolicitante = solicitudInscripcion.fechaNacimientoSolicitante;

            await _context.SaveChangesAsync();

            return solicitudExistente; // Devuelve la solicitud de inscripción actualizada
        }

        public async Task<SolicitudInscripcion> EliminarSolicitudInscripcion(int id)
        {
            // Lógica para eliminar una solicitud de inscripción (por ejemplo, eliminarla de la base de datos)
            var solicitudInscripcion = await _context.SolicitudesInscripcion.FindAsync(id);

            if (solicitudInscripcion == null)
            {
                throw new InvalidOperationException("La solicitud de inscripción no existe.");
            }

            _context.SolicitudesInscripcion.Remove(solicitudInscripcion);
            await _context.SaveChangesAsync();

            return solicitudInscripcion; // Devuelve la solicitud de inscripción que se eliminó
        }

    }
    // La interfaz ISolicitudInscripcionService define los métodos que debe implementar SolicitudInscripcionService y proporciona una abstracción para interactuar con las solicitudes de inscripción.
    public interface ISolicitudInscripcionService
    {
        Task<List<SolicitudInscripcion>> ObtenerTodasSolicitudesInscripcionAsync();
        Task<SolicitudInscripcion> ObtenerSolicitudInscripcionPorIdAsync(int id);
        Task<List<SolicitudInscripcion>> ObtenerSolicitudesPorUsuarioAsync(string dni);
        Task<SolicitudInscripcion> AgregarSolicitudInscripcion(SolicitudInscripcion solicitudInscripcion);
        Task<SolicitudInscripcion> EditarSolicitudInscripcion(int id, SolicitudInscripcion solicitudInscripcion);
        Task<SolicitudInscripcion> EliminarSolicitudInscripcion(int id);
    }
}
