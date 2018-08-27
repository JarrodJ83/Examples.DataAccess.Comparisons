using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Repositories;

namespace Services
{
    public class ProductService
    {
        private readonly ProductRespository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRespository(ProductStore.Current);
        }

        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            try
            {
                ConsoleLogger.Verbose("Getting products paged");

                Task<Product[]> getPageOfProducts = _productRepository.GetPageOfProductsAsync(offset, pageSize);
                Task<int> getAllProductsCount = _productRepository.GetAllProductsCountAsync();

                return new PagedData<Product>
                {
                    Data = await getPageOfProducts,
                    TotalRecords = await getAllProductsCount,
                    PageSize = pageSize,
                    Offset = offset
                };
            }
            catch (Exception e)
            {
                ConsoleLogger.Exception(e, "Error getting products paged");
                throw;
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            try
            {
                ConsoleLogger.Verbose("Getting product");

                return await _productRepository.GetByIdAsync(productId);
            }
            catch (Exception e)
            {
                ConsoleLogger.Exception(e, "Error getting product");
                throw;
            }
        }
    }
}
