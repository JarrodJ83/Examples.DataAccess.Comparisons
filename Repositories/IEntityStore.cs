using System.Collections.Generic;
using DomainModel;

namespace Repositories
{
    public interface IEntityStore<TEntity> where TEntity : Entity
    {
        List<TEntity> Entities { get; }
    }
}
