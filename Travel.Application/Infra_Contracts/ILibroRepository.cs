using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface ILibroRepository : IGeneralRepository<Libro>
    {
        public Task<List<AutoresHasLibro>> GetAllByAutorId(int AutorId, CancellationToken cancellationToken);
    }

}
