using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Controladores;

namespace CentroEducativoAPISQL.Servicios
{
    public class RolesService : IRolesService
    {
        // métodos para interactuar con la entidad de Roles en la BD

        private readonly MiDbContext _context; // Representa el contexto de la base de datos. El contexto se utiliza para interactuar con la BD y y realizar operaciones de lectura en Roles

        public RolesService(MiDbContext context)
        {
            _context = context;
        }

        //Lista de los roles almacenados en la BD
        public async Task<List<Roles>> ObtenerTodosRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        // Recibe un id y busca un rol con ese id en la BD
        public async Task<Roles> ObtenerRolPorIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.id_rol == id);
        }


        public async Task<Roles> CrearRolAsync(Roles rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Roles> ObtenerRolPorTipoAsync(string tipoRol)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.tipo_rol == tipoRol);
        }
    }

    // La interfaz IRolesService define los métodos que debe implementar RolesService y proporciona una abstracción para interactuar con la entidad de Roles.
    public interface IRolesService
    {
        Task<List<Roles>> ObtenerTodosRolesAsync();
        Task<Roles> ObtenerRolPorIdAsync(int id);

        Task<Roles> CrearRolAsync(Roles rol);
        Task<Roles> ObtenerRolPorTipoAsync(string tipoRol);
    }
}
