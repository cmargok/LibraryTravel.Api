using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IEditorialRepository : IGeneralRepository<Editorial>
    {
        public Task<List<Editorial>> GetAll(CancellationToken cancellationToken);
    }

}
