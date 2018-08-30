using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;

namespace QueryHandlers
{
    public class AllProductsPaged : IQueryHandler<Queries.AllProductsPagedQry, Product[]>
    {
        private readonly IProductStore _productStore;

        public AllProductsPaged(IProductStore productStore)
        {
            _productStore = productStore;
        }
        public async Task<Product[]> FetchAsync(Queries.AllProductsPagedQry query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _productStore.Entities.Skip(query.Offset).Take(query.PageSize).ToArray();
        }
    }
}
