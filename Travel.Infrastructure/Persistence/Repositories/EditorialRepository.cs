using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Models;

namespace Travel.Infrastructure.Persistence.Repositories
{
    public class EditorialRepository : IEditorialRepository
    {
        private readonly ApplicationDbContext _context;
        public EditorialRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<bool> Add(Editoriale entity, CancellationToken cancellationToken)
        {
            await _context.Editoriales.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return true;
            return false;
        }

        public async Task<List<Editoriale>> GetAll(CancellationToken cancellationToken)
        {
            var Editoriales = await _context.Editoriales.ToListAsync(cancellationToken);
            return Editoriales;       
        }

        public async Task<Editoriale> GetbyId(int Id, CancellationToken cancellationToken)
        {
            var Editorial = await _context.Editoriales.FirstOrDefaultAsync(ed => ed.Id == Id, cancellationToken);
            return Editorial!;
        }

        public Task<bool> Update(int Id, Editoriale entity, CancellationToken cancellationToken)
        {
           
        }
    }
}
