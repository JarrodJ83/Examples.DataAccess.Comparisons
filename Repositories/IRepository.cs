using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;

namespace Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task<IEnumerable<TEntity>> ListAsync(Func<TEntity, bool> predicate);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
