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
        public  async Task<bool> AddAsync(Editorial entity, CancellationToken cancellationToken)
        {
            await _context.Editoriales.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return true;
            return false;
        }

        public async Task<List<Editorial>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Editoriales = await _context.Editoriales.ToListAsync(cancellationToken);
            return Editoriales;       
        }

        public async Task<Editorial> GetbyIdAsync(int Id, CancellationToken cancellationToken, string IdString = "")
        {
            var Editorial = await _context.Editoriales.FirstOrDefaultAsync(ed => ed.EditorialId == Id, cancellationToken);
            return Editorial!;
        }

        public async Task<bool> UpdateAsync(int Id, Editorial entity, CancellationToken cancellationToken)
        {
            var Editorial = await _context.Editoriales.FirstOrDefaultAsync(ed => ed.EditorialId == Id, cancellationToken);

            if(Editorial != null)
            {
                Editorial.Sede = entity.Sede;
                Editorial.Nombre = entity.Nombre;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
           
        }
    }
}
