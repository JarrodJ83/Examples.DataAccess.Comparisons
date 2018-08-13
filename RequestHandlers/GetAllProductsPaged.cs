using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;
using Requests;

namespace RequestHandlers
{
    public class GetAllProductsPaged : IRequestHandler<Requests.GetAllProductsPaged, PagedData<Product>>
    {
        private readonly IQueryHandler<AllProductsPaged, Product[]> _allProductsPaged;
        private readonly IQueryHandler<ProductsCount, int> _productsCount;

        public GetAllProductsPaged(
            IQueryHandler<Queries.AllProductsPaged, Product[]> allProductsPaged,
            IQueryHandler<Queries.ProductsCount, int> productsCount
            )
        {
            _allProductsPaged = allProductsPaged;
            _productsCount = productsCount;
        }

        public async Task<PagedData<Product>> HandleAsync(Requests.GetAllProductsPaged request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var allProductsPaged = _allProductsPaged.FetchAsync(new Queries.AllProductsPaged
            {
                Offset = request.Offset,
                PageSize = request.PageSize
            });

            var productsCount = _productsCount.FetchAsync(new Queries.ProductsCount());

            Task.WaitAll(allProductsPaged, productsCount);

            return new PagedData<Product>
            {
                Offset = request.Offset,
                PageSize = request.PageSize,
                Data = await allProductsPaged,
                TotalRecords = await productsCount
            };
        }
    }
}
