using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IEditorialRepository : IGeneralRepository<Editorial>
    {
        public Task<bool> AddAsync(Editorial entity, CancellationToken cancellationToken);
    }

}
