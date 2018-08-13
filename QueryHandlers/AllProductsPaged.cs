using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;
using Repositories.Core;

namespace QueryHandlers
{
    public class AllProductsPaged : IQueryHandler<Queries.AllProductsPaged, Product[]>
    {
        private readonly IEntityStore<Product> _productStore;

        public AllProductsPaged(IEntityStore<Product> productStore)
        {
            _productStore = productStore;
        }
        public async Task<Product[]> FetchAsync(Queries.AllProductsPaged query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _productStore.Entities.Skip(query.Offset).Take(query.PageSize).ToArray();
        }
    }
}
