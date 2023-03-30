using System;
using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IGeneralRepository<T>
    {
        public Task<T> GetbyIdAsync(int Id, CancellationToken cancellationToken,string IdString = "");              

        public Task<bool> UpdateAsync(int Id, T entity, CancellationToken cancellationToken);

        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    }

}
