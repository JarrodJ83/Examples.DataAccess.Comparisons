using System.Threading.Tasks;
using DomainModel;
using Repositories;

namespace Services
{
    public class ProductService
    {
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            ProductRespository productRepository = new ProductRespository(ProductStore.Current);

            Task<Product[]> getPageOfProducts = productRepository.GetPageOfProductsAsync(offset, pageSize);
            Task<int> getAllProductsCount = productRepository.GetAllProductsCountAsync();

            return new PagedData<Product>
            {
                Data = await getPageOfProducts,
                TotalRecords = await getAllProductsCount,
                PageSize = pageSize,
                Offset = offset
            };
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            ProductRespository productRepository = new ProductRespository(ProductStore.Current);

            return await productRepository.GetByIdAsync(productId);
        }
    }
}
