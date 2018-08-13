using System.Threading.Tasks;
using DomainModel;
using Repositories;

namespace Services
{
    public class Products : IProductService
    {
        private readonly IProductRepository _productRepository;

        public Products(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            var getPageOfProducts = _productRepository.GetPageOfProductsAsync(offset, pageSize);
            var getAllProductsCount = _productRepository.GetAllProductsCount();

            Task.WaitAll(getPageOfProducts, getAllProductsCount);

            return new PagedData<Product>
            {
                Data = await getPageOfProducts,
                TotalRecords = await getAllProductsCount,
                PageSize = pageSize,
                Offset = offset
            };
        }
    }
}
