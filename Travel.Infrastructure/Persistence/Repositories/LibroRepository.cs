using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> AddAsync(Libro entity, int AutorId,CancellationToken cancellationToken)
        {

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.Libros.AddAsync(entity, cancellationToken);
                

                var mix = new AutoresHasLibro
                {
                    AutorId = AutorId,
                    Isbn = entity.Isbn,
                };

                await _context.AutoresHasLibros.AddAsync(mix, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                transaction.Commit();

            }catch (Exception)
            {
                transaction.Rollback();

                return false;
            }           

            return true;
        }

        public async Task<List<Libro>> GetAllAsync(CancellationToken cancellationToken)
        {
            var libros = await _context.Libros.ToListAsync(cancellationToken);
            return libros;
        }

        public async Task<List<AutoresHasLibro>> GetAllByAutorId(int AutorId, CancellationToken cancellationToken)
        {

            var query = await _context.AutoresHasLibros.Include(o => o.Libro).ThenInclude(p => p.Editoriales).Where(i => i.AutorId == AutorId).ToListAsync();       

            return query;
        }

        public async Task<Libro> GetbyIdAsync(int Id, CancellationToken cancellationToken,string IdString = "")
        {
            var Libro = await _context.Libros.FirstOrDefaultAsync(ed => ed.Isbn == IdString, cancellationToken);
            return Libro!;
        }

        public async Task<bool> UpdateAsync(int Id, Libro entity, CancellationToken cancellationToken)
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

        public async Task<bool> ExistsAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _context.Libros.AnyAsync(c => c.Isbn == isbn);
        }

        public void DeleteForTesting(string isbn)
        {
            var delete = _context.Libros.First(ed => ed.Isbn == isbn);
            _context.Libros.Remove(delete);
            _context.SaveChanges();
        }
    }
}
