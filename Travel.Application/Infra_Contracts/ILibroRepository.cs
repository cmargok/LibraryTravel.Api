using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface ILibroRepository : IGeneralRepository<Libro>
    {
        public Task<List<Libro>> GetAllByAutorId(int AutorId, CancellationToken cancellationToken);
    }

}
