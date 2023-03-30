using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface ILibroRepository : IGeneralRepository<Libro>
    {
        public Task<List<AutoresHasLibro>> GetAllByAutorId(int AutorId, CancellationToken cancellationToken);

        public Task<bool> ExistsAsync(string isbn, CancellationToken cancellationToken);
        public Task<bool> AddAsync(Libro entity, int AutorId, CancellationToken cancellationToken);
    }

}
