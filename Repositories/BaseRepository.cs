using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IEntityStore<TEntity> _entityStore;

        public BaseRepository(IEntityStore<TEntity> entityStore)
        {
            _entityStore = entityStore;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return _entityStore.Entities.SingleOrDefault(product => product.Id == id);
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return _entityStore.Entities;
        }

        public async Task<IEnumerable<TEntity>> ListAsync(Func<TEntity, bool> predicate)
        {
            return _entityStore.Entities.Where(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            _entityStore.Entities.Add(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _entityStore.Entities.Remove(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var productToUpdate = _entityStore.Entities.SingleOrDefault(product => product.Id == entity.Id);

            _entityStore.Entities.Remove(productToUpdate);

            _entityStore.Entities.Add(entity);
        }
    }
}
