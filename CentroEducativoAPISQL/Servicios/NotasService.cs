// NotaService.cs
using CentroEducativoAPISQL.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentroEducativoAPISQL.Servicios
{
    public class NotaService : INotaService
    {
        private readonly MiDbContext _context;

        public NotaService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> ObtenerTodasLasNotasAsync()
        {
            return await _context.Notas.ToListAsync();
        }

        public async Task<Nota> ObtenerNotaPorIdAsync(int idNota)
        {
            return await _context.Notas.FirstOrDefaultAsync(n => n.id_nota == idNota);
        }

        public async Task<Nota> AgregarNotaAsync(Nota nuevaNota)
        {
            // Agregar validaciones si es necesario
            _context.Notas.Add(nuevaNota);
            await _context.SaveChangesAsync();
            return nuevaNota;
        }
    }
    public interface INotaService
    {
        Task<IEnumerable<Nota>> ObtenerTodasLasNotasAsync();
        Task<Nota> ObtenerNotaPorIdAsync(int idNota);
        Task<Nota> AgregarNotaAsync(Nota nuevaNota);
    }
}
