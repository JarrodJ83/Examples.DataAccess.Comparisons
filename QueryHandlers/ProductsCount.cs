using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;
using Repositories.Core;

namespace QueryHandlers
{
    class ProductsCount : IQueryHandler<Queries.ProductsCount, int>
    {
        private readonly IEntityStore<Product> _productStore;

        public ProductsCount(IEntityStore<Product> productStore)
        {
            _productStore = productStore;
        }
        public async Task<int> FetchAsync(Queries.ProductsCount query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _productStore.Entities.Count;
        }
    }
}
