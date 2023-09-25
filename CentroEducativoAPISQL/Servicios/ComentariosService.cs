using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    //  servicio que se encarga de manejar operaciones relacionadas con los comentarios de noticias. 
    public class ComentariosService : IComentariosService
    {
        // contexto de la BD. Este contexto se utiliza para interactuar con la BD y realizar operaciones de lectura en la entidad de Comentarios.
        private readonly MiDbContext _context;

        public ComentariosService(MiDbContext context)
        {
            _context = context;
        }

        // Permite obtener lista de todos los comentarios almacenedos en la BD
        public async Task<List<Comentarios>> ObtenerTodosComentariosAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }

        // Obtiene comentario por ID
        public async Task<Comentarios> ObtenerComentarioPorIdAsync(int id)
        {
            return await _context.Comentarios.FirstOrDefaultAsync(c => c.id_comentario == id);
        }

        public async Task<List<Comentarios>> ObtenerComentariosPorDniUsuario(string dniUsuario)
        {
            try
            {
                // Busca los comentarios asociados al usuario registrado por su DNI
                var comentariosPorDni = await _context.Comentarios
                    .Where(c => c.id_usuario == dniUsuario)
                    .ToListAsync();

                return comentariosPorDni;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los comentarios por DNI de usuario.", ex);
            }
        }

        public async Task<List<Comentarios>> ObtenerComentariosPorNombreUsuario(string nombre)
        {
            try
            {
                // Busca los comentarios asociados a usuarios anónimos por su nombre
                var comentariosPorNombre = await _context.Comentarios
                    .Where(c => c.nombre == nombre && c.id_usuario == null)
                    .ToListAsync();

                return comentariosPorNombre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los comentarios por nombre de usuario.", ex);
            }
        }



        public async Task<Comentarios> AgregarComentario(Comentarios comentario)
        {
            try
            {
                // Verifica si se proporcionó un nombre de usuario o un id de usuario
                if (!string.IsNullOrWhiteSpace(comentario.id_usuario))
                {
                    // Busca al usuario registrado en la base de datos
                    var usuario = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.dni == comentario.id_usuario);

                    if (usuario == null)
                    {
                        throw new Exception("El usuario no existe.");
                    }

                    comentario.Usuario = usuario;
                }
                else if (!string.IsNullOrWhiteSpace(comentario.nombre))
                {
                    // Comentario anónimo, no se asocia a un usuario registrado
                    comentario.Usuario = null;
                }
                else
                {
                    throw new Exception("Debes proporcionar un usuario registrado (id_usuario) o un nombre de usuario (nombre) para el comentario.");
                }

                // Agrega el comentario a la base de datos
                _context.Comentarios.Add(comentario);
                await _context.SaveChangesAsync();

                return comentario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el comentario: " + ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<Comentarios> EditarComentario(int id, Comentarios comentario)
        {
            try
            {
                // Busca el comentario por su ID en la base de datos
                var comentarioExistente = await _context.Comentarios.FindAsync(id);

                if (comentarioExistente == null)
                {
                    throw new KeyNotFoundException("El comentario no existe.");
                }

                // Edita el contenido del comentario
                comentarioExistente.contenido = comentario.contenido;

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                return comentarioExistente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el comentario.", ex);
            }
        }

        public async Task<string> EliminarComentario(int id)
        {
            try
            {
                // Busca el comentario por su ID en la base de datos
                var comentarioExistente = await _context.Comentarios.FindAsync(id);

                if (comentarioExistente == null)
                {
                    throw new KeyNotFoundException("El comentario no existe.");
                }

                // Elimina el comentario de la base de datos
                _context.Comentarios.Remove(comentarioExistente);
                await _context.SaveChangesAsync();

                return "Comentario eliminado con éxito.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el comentario.", ex);
            }
        }


    }

    // La interfaz IComentariosService define los métodos que debe implementar ComentariosService y proporciona una abstracción para interactuar con los datos de los comentarios.
    public interface IComentariosService
    {
        Task<List<Comentarios>> ObtenerTodosComentariosAsync();
        Task<Comentarios> ObtenerComentarioPorIdAsync(int id);
        Task<List<Comentarios>> ObtenerComentariosPorNombreUsuario(string nombreUsuario);
        Task<List<Comentarios>> ObtenerComentariosPorDniUsuario(string dniUsuario);
        Task<Comentarios> AgregarComentario(Comentarios comentario);
        Task<Comentarios> EditarComentario(int id, Comentarios comentario);
        Task<string> EliminarComentario(int id);

    }
}
