using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Repositories;

namespace Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
         
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            try
            {
                ConsoleLogger.Verbose("Getting products paged");

                Task<Product[]> getPageOfProducts = _productRepository.GetPageOfProductsAsync(offset, pageSize);
                Task<int> getAllProductsCount = _productRepository.GetAllProductsCountAsync();

                Task.WaitAll(getAllProductsCount, getPageOfProducts);

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

                ProductRespository productRepository = new ProductRespository(ProductStore.Current);

                return await productRepository.GetByIdAsync(productId);
            }
            catch (Exception e)
            {
                ConsoleLogger.Exception(e, "Error getting product");
                throw;
            }
        }
    }
}
