using CentroEducativoAPISQL.Controllers;
using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CentroEducativoAPISQL.Servicios
{
    //  servicio que se encarga de manejar operaciones relacionadas con las noticias. 
    public class NoticiasService : INoticiasService
    {
        // representa el contexto de la BD. Este contexto se utiliza para interactuar con la BD y realizar operaciones de lectura en la entidad de Noticias.
        private readonly MiDbContext _context;

        public NoticiasService(MiDbContext context)
        {
            _context = context;
        }
        
        // Metodo que obtiene lista de todas las noticias almacenadas en la BD
        public async Task<List<Noticia>> ObtenerTodasNoticiasAsync()
        {
            return await _context.Noticias.ToListAsync();
        }

        // Buscar noticia por id
        public async Task<Noticia> ObtenerNoticiaPorIdAsync(int id)
        {
            return await _context.Noticias.FirstOrDefaultAsync(n => n.id_noticia == id);
        }

        public async Task<List<Noticia>> ObtenerNoticiasPorUsuarioAsync(string idUsuario)
        {
            try
            {
                // Verifica si el usuario existe en la base de datos
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.dni == idUsuario);

                if (usuario != null)
                {
                    // Obtén las noticias asociadas al usuario por su ID
                    var noticias = await _context.Noticias
                        .Where(n => n.id_usuario == idUsuario)
                        .ToListAsync();

                    return noticias;
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
        public async Task<Noticia> CrearNoticia(Noticia noticia, string idUsuario)
        {
            try
            {
                // Verifica si el usuario tiene el rol de "Autoridades" o "Docentes"
                var usuario = await _context.Usuarios
                    .Include(u => u.RolesUsuarios)
                    .FirstOrDefaultAsync(u => u.dni == idUsuario);

                if (usuario != null && (usuario.RolesUsuarios?.tipo_rol == "Autoridad" || usuario.RolesUsuarios?.tipo_rol == "Docente"))
                {
                    // Establece la fecha de publicación
                    //noticia.fechaPublicacion = DateTime.Now;

                    // Asigna el ID del usuario a la noticia
                    noticia.id_usuario = idUsuario;

                    // Agrega la noticia a la base de datos
                    _context.Noticias.Add(noticia);
                    await _context.SaveChangesAsync();
                    return noticia;
                }
                else
                {
                    throw new UnauthorizedAccessException("No tienes permiso para crear noticias.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la noticia.", ex);
            }
        }

        public async Task<Noticia> EditarNoticia(int id, Noticia noticia, string idUsuario)
        {
            try
            {
                // Verifica si el usuario tiene el rol de "Autoridades" o "Docentes"
                var usuario = await _context.Usuarios
                    .Include(u => u.RolesUsuarios)
                    .FirstOrDefaultAsync(u => u.dni == idUsuario);

                if (usuario != null && (usuario.RolesUsuarios?.tipo_rol == "Autoridades" || usuario.RolesUsuarios?.tipo_rol == "Docentes"))
                {
                    // Busca la noticia por su ID en la base de datos
                    var noticiaExistente = await _context.Noticias.FindAsync(id);

                    if (noticiaExistente != null)
                    {
                        // Realiza las actualizaciones necesarias en la noticia existente
                        noticiaExistente.titulo = noticia.titulo;
                        noticiaExistente.parrafos = noticia.parrafos;
                        noticiaExistente.imagenes = noticia.imagenes;
                        noticiaExistente.redactor = noticia.redactor;
                        noticiaExistente.fecha = noticia.fecha;
                        // Puedes realizar más actualizaciones según tus necesidades

                        // Guarda los cambios en la base de datos
                        await _context.SaveChangesAsync();
                        return noticiaExistente;
                    }
                    else
                    {
                        throw new KeyNotFoundException("La noticia no existe.");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("No tienes permiso para editar noticias.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la noticia.", ex);
            }
        }

        public async Task<string> EliminarNoticia(int idNoticia)
        {
            try
            {
                // Busca la noticia por su ID en la base de datos
                var noticia = await _context.Noticias.FindAsync(idNoticia);

                if (noticia == null)
                {
                    throw new KeyNotFoundException("La noticia no existe.");
                }

                // Elimina los comentarios relacionados
                var comentariosRelacionados = await _context.Comentarios
                    .Where(c => c.id_noticia == idNoticia)
                    .ToListAsync();

                _context.Comentarios.RemoveRange(comentariosRelacionados);

                // Elimina la noticia
                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();

                return "Noticia eliminada con éxito.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la noticia.", ex);
            }
        }


        //public async Task<string> EliminarNoticia(int idNoticia, string idUsuario)
        //{
        //    try
        //    {
        //        // Busca la noticia por su ID en la base de datos
        //        var noticia = await _context.Noticias.FindAsync(idNoticia);

        //        if (noticia == null)
        //        {
        //            throw new KeyNotFoundException("La noticia no existe.");
        //        }

        //        // Verifica si el usuario tiene el rol de "Autoridades" o "Docentes"
        //        var usuario = await _context.Usuarios
        //            .Include(u => u.RolesUsuarios)
        //            .FirstOrDefaultAsync(u => u.dni == idUsuario);

        //        if (usuario != null && (usuario.RolesUsuarios?.tipo_rol == "Autoridad" || usuario.RolesUsuarios?.tipo_rol == "Docente"))
        //        {
        //            // Elimina los comentarios relacionados
        //            var comentariosRelacionados = await _context.Comentarios
        //                .Where(c => c.id_noticia == idNoticia)
        //                .ToListAsync();

        //            _context.Comentarios.RemoveRange(comentariosRelacionados);

        //            // Elimina la noticia
        //            _context.Noticias.Remove(noticia);
        //            await _context.SaveChangesAsync();

        //            return "Noticia eliminada con éxito.";
        //        }
        //        else
        //        {
        //            throw new UnauthorizedAccessException("No tienes permiso para eliminar noticias.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al eliminar la noticia.", ex);
        //    }
        //}

    }

    // La interfaz INoticiasService define los métodos que debe implementar NoticiasService y proporciona una abstracción para interactuar con los datos de las noticias.
    public interface INoticiasService
    {
        Task<List<Noticia>> ObtenerTodasNoticiasAsync();
        Task<Noticia> ObtenerNoticiaPorIdAsync(int id);
        Task<List<Noticia>> ObtenerNoticiasPorUsuarioAsync(string dni);
        Task<Noticia> CrearNoticia(Noticia noticia, string idUsuario);
        Task<Noticia> EditarNoticia(int id, Noticia noticia, string usuario);
        //Task<string> EliminarNoticia(int id,string usuario);

        Task<string> EliminarNoticia(int id);
    }
}
