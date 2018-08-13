using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Core
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IEntityStore<TEntity> _entityStore;

        public BaseRepository(IEntityStore<TEntity> entityStore)
        {
            _entityStore = entityStore;
        }

        public TEntity GetById(int id)
        {
            return _entityStore.Entities.SingleOrDefault(product => product.Id == id);
        }

        public IEnumerable<TEntity> List()
        {
            return _entityStore.Entities;
        }

        public IEnumerable<TEntity> List(Func<TEntity, bool> predicate)
        {
            return _entityStore.Entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _entityStore.Entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _entityStore.Entities.Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            var productToUpdate = _entityStore.Entities.SingleOrDefault(product => product.Id == entity.Id);

            _entityStore.Entities.Remove(productToUpdate);

            _entityStore.Entities.Add(entity);
        }
    }
}
