using Microsoft.EntityFrameworkCore;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Models;

namespace Travel.Infrastructure.Persistence.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ApplicationDbContext _context;
        public AutorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Autor entity, CancellationToken cancellationToken)
        {
            await _context.Autores.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return true;
            return false;
        }

        public async Task<List<Autor>> GetAll(CancellationToken cancellationToken)
        {
            var Autores = await _context.Autores.ToListAsync(cancellationToken);
            return Autores;
        }

        public async Task<Autor> GetbyId(int Id, CancellationToken cancellationToken)
        {
            var Autor = await _context.Autores.FirstOrDefaultAsync(ed => ed.AutorId == Id, cancellationToken);
            return Autor!;
        }

        public async Task<bool> Update(int Id, Autor entity, CancellationToken cancellationToken)
        {
            var Autor = await _context.Autores.FirstOrDefaultAsync(ed => ed.AutorId == Id, cancellationToken);

            if (Autor != null)
            {
                Autor.Nombre = entity.Nombre;
                Autor.Apellido = entity.Apellido;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;

        }
    }
}
