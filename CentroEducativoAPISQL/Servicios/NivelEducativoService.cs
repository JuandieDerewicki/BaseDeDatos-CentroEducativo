//using CentroEducativoAPISQL.Modelos;
//using Microsoft.EntityFrameworkCore;

//namespace CentroEducativoAPISQL.Servicios
//{
//    // servicio que se encarga de manejar operaciones relacionadas con los niveles educativos.
//    public class NivelEducativoService : INivelEducativoService
//    {
//        // representa el contexto de la BD. Este contexto se utiliza para interactuar con la BD y realizar operaciones de lectura en la entidad de Niveles Educativos.
//        private readonly MiDbContext _context;

//        public NivelEducativoService(MiDbContext context)
//        {
//            _context = context;
//        }

//        // Metodo para obtener lista de los tipos de niveles educativos almacenados en la BD
//        public async Task<List<NivelEducativo>> ObtenerTodosNivelesEducativosAsync()
//        {
//            return await _context.NivelesEducativos.ToListAsync();
//        }

//        // Metodo que busca nivel educativo por id
//        public async Task<NivelEducativo> ObtenerNivelEducativoPorIdAsync(int id)
//        {
//            return await _context.NivelesEducativos.FirstOrDefaultAsync(n => n.id_nivelEducativo == id);
//        }

//        public async Task<NivelEducativo> CrearNivelEducativo(NivelEducativo nivelEducativo)
//        {
//            // Lógica para crear un nivel educativo y devolverlo
//            _context.NivelesEducativos.Add(nivelEducativo);
//            await _context.SaveChangesAsync();

//            return nivelEducativo;
//        }


//    }

//    // La interfaz INivelEducativoService define los métodos que debe implementar NivelEducativoService y proporciona una abstracción para interactuar con los niveles educativos.
//    public interface INivelEducativoService
//    {
//        Task<List<NivelEducativo>> ObtenerTodosNivelesEducativosAsync();
//        Task<NivelEducativo> ObtenerNivelEducativoPorIdAsync(int id);

//        Task<NivelEducativo> CrearNivelEducativo(NivelEducativo nivelEducativo);
//    }
//}
