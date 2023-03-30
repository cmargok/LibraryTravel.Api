using Microsoft.EntityFrameworkCore;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Models;

namespace Travel.Infrastructure.Persistence.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly ApplicationDbContext _context;
        public LibroRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Libro entity, CancellationToken cancellationToken)
        {
            await _context.Libros.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return true;
            return false;
        }

        public async Task<List<AutoresHasLibro>> GetAllByAutorId(int AutorId, CancellationToken cancellationToken)
        {

            var query = await _context.AutoresHasLibros.Include(o => o.Autor).Include(o => o.Libro).ThenInclude(p => p.Editoriales).Where(i => i.AutorId == AutorId).ToListAsync();       

            return query;
        }

        public async Task<Libro> GetbyId(int Id, CancellationToken cancellationToken)
        {
            var Libro = await _context.Libros.FirstOrDefaultAsync(ed => ed.Isbn == Id.ToString(), cancellationToken);
            return Libro!;
        }

        public async Task<bool> Update(int Id, Libro entity, CancellationToken cancellationToken)
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(ed => ed.Isbn == Id.ToString(), cancellationToken);

            if (libro != null)
            {
                libro.Sinopsis = entity.Sinopsis;
                libro.NPaginas = entity.NPaginas;
                libro.Titulo = entity.Titulo;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;

        }
    }
}
