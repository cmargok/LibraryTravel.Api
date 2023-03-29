namespace Travel.Application.Infra_Contracts
{
    public interface IGeneralRepository<T>
    {
        public Task<T> GetbyId(int Id, CancellationToken cancellationToken);

        public Task<bool> Add(T entity, CancellationToken cancellationToken);

        public Task<bool> Update(int Id, T entity, CancellationToken cancellationToken);

    }

}
