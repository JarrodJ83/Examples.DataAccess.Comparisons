using System.Collections.Generic;

namespace Repositories.Core
{
    public interface IEntityStore<TEntity> where TEntity : Entity
    {
        List<TEntity> Entities { get; }
    }
}
