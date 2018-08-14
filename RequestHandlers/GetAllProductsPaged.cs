using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Queries;
using Requests;

namespace RequestHandlers
{
    public class GetAllProductsPaged : IRequestHandler<Requests.GetAllProductsPagedRequest, PagedData<Product>>
    {
        private readonly IQueryHandler<AllProductsPagedQry, Product[]> _allProductsPaged;
        private readonly IQueryHandler<ProductsCountQry, int> _productsCount;

        public GetAllProductsPaged(
            IQueryHandler<Queries.AllProductsPagedQry, Product[]> allProductsPaged,
            IQueryHandler<Queries.ProductsCountQry, int> productsCount
            )
        {
            _allProductsPaged = allProductsPaged;
            _productsCount = productsCount;
        }

        public async Task<PagedData<Product>> HandleAsync(Requests.GetAllProductsPagedRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var allProductsPaged = _allProductsPaged.FetchAsync(new Queries.AllProductsPagedQry
            {
                Offset = request.Offset,
                PageSize = request.PageSize
            });

            var productsCount = _productsCount.FetchAsync(new Queries.ProductsCountQry());

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
