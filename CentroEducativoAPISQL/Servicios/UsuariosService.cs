using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CentroEducativoAPISQL.Servicios
{
    public class UsuariosService : IUsuariosService
    {
        // Metodos para interacturar con la entidad Usuarios en la BD

        private readonly MiDbContext _context; // Representa el contexto de la base de datos. El contexto se utiliza para interactuar con la BD y y realizar las operaciones CRUD en Usuarios
        private readonly IRolesService _rolesService;
        public UsuariosService(MiDbContext context, IRolesService rolesService)
        {
            _context = context;
            _rolesService = rolesService;
        }
        // Obtiene la lista de todos los usuarios almacenados en la BD y recupera todos los usuarios con sus roles asociados
        public async Task<List<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _context.Usuarios.Include(u => u.RolesUsuarios).ToListAsync();
        }
        
        // Busca un usuario por su dni en la BD
        public async Task<Usuario> ObtenerUsuarioPorDocumentoAsync(string documento)
        {
            return await _context.Usuarios.Include(u => u.RolesUsuarios).FirstOrDefaultAsync(u => u.dni == documento);
        }

        public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario, string tipoUsuario)
        {
            try
            {
                // Verifica que el tipo de usuario sea válido
                if (string.IsNullOrEmpty(tipoUsuario))
                {
                    throw new ArgumentException("El tipo de usuario no puede estar vacío.", nameof(tipoUsuario));
                }

                // Verifica si el usuario ya existe en la base de datos
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.dni == usuario.dni);

                if (usuarioExistente != null)
                {
                    throw new Exception("El usuario ya existe en la base de datos.");
                }

                // Obtener el rol correspondiente desde la base de datos (o crearlo si no existe)
                var rol = await _rolesService.ObtenerRolPorTipoAsync(tipoUsuario);

                if (rol == null)
                {
                    rol = new Rol { tipo_rol = tipoUsuario };
                    await _rolesService.CrearRolAsync(rol); // Crea el rol en la base de datos
                }

                // Asigna el rol al usuario
                usuario.RolesUsuarios = rol;

                // Crea el usuario en la base de datos
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar el usuario: {ex.Message}");
            }
        }


        public async Task<Usuario> EditarUsuarioAsync(int dni, Usuario usuario)
        {
            string dniStr = dni.ToString();
            // Verificar que el DNI del usuario a editar coincide con el DNI en el objeto usuario
            if (dniStr != usuario.dni)
            {
                throw new ArgumentException("Los DNI no coinciden", nameof(dni));
            }

            try
            {
                // Verificar si el usuario existe en la base de datos
                var usuarioExistente = await _context.Usuarios
                    .Include(u => u.RolesUsuarios)
                    .FirstOrDefaultAsync(u => u.dni == dniStr);

                if (usuarioExistente == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Actualizar el rol del usuario si se especifica un tipo de usuario válido
                var tipoUsuario = usuario.RolesUsuarios?.tipo_rol;

                if (!string.IsNullOrEmpty(tipoUsuario))
                {
                    // Obtener el rol correspondiente desde la base de datos (o crearlo si no existe)
                    var rol = await _rolesService.ObtenerRolPorTipoAsync(tipoUsuario);

                    if (rol == null)
                    {
                        rol = new Rol { tipo_rol = tipoUsuario };
                        await _rolesService.CrearRolAsync(rol); // Crea el rol en la base de datos
                    }

                    usuarioExistente.RolesUsuarios = rol;
                }

                // Actualizar otras propiedades del usuario
                usuarioExistente.nombreCompleto = usuario.nombreCompleto;
                usuarioExistente.contraseña = usuario.contraseña;
                usuarioExistente.correo = usuario.correo;
                usuarioExistente.telefono = usuario.telefono;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return usuarioExistente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al editar el usuario: {ex.Message}");
            }
        }

        // Busca por dni en la BD, si se encuentra se elimina y si no se encuentra se lanza una excepcion 
        public async Task EliminarUsuarioAsync(string documento)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.dni == documento);

            if (usuarioExistente == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            _context.Usuarios.Remove(usuarioExistente);
            await _context.SaveChangesAsync();
        }
    }

    //  define los métodos que debe implementar UsuariosService y proporciona una abstracción para interactuar con la entidad de Usuarios.
    public interface IUsuariosService
    {
        Task<List<Usuario>> ObtenerTodosUsuariosAsync();
        Task<Usuario> ObtenerUsuarioPorDocumentoAsync(string documento);
        Task EliminarUsuarioAsync(string documento);
        Task<Usuario> RegistrarUsuarioAsync(Usuario usuario, string tipoUsuario);
        Task<Usuario> EditarUsuarioAsync(int dni, Usuario usuario);
    }
}
