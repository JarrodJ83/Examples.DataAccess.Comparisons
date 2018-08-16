using System.Threading.Tasks;
using DomainModel;
using Repositories;
using Services.Core;

namespace Services
{
    public class ProductService
    {
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            ProductRespository productRepository = new ProductRespository(ProductStore.Current);

            var getPageOfProducts = productRepository.GetPageOfProductsAsync(offset, pageSize);
            var getAllProductsCount = productRepository.GetAllProductsCountAsync();

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
