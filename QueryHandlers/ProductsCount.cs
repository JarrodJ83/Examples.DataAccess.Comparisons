using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;

namespace QueryHandlers
{
    class ProductsCount : IQueryHandler<Queries.ProductsCountQry, int>
    {
        private readonly IProductStore _productStore;

        public ProductsCount(IProductStore productStore)
        {
            _productStore = productStore;
        }
        public async Task<int> FetchAsync(Queries.ProductsCountQry query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _productStore.Entities.Count;
        }
    }
}
