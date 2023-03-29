using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IEditorialRepository : IGeneralRepository<Editoriale>
    {
        public Task<List<Editoriale>> GetAll(CancellationToken cancellationToken);
    }

}
