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
        public  async Task<bool> Add(Editorial entity, CancellationToken cancellationToken)
        {
            await _context.Editoriales.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return true;
            return false;
        }

        public async Task<List<Editorial>> GetAll(CancellationToken cancellationToken)
        {
            var Editoriales = await _context.Editoriales.ToListAsync(cancellationToken);
            return Editoriales;       
        }

        public async Task<Editorial> GetbyId(int Id, CancellationToken cancellationToken)
        {
            var Editorial = await _context.Editoriales.FirstOrDefaultAsync(ed => ed.EditorialId == Id, cancellationToken);
            return Editorial!;
        }

        public async Task<bool> Update(int Id, Editorial entity, CancellationToken cancellationToken)
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
