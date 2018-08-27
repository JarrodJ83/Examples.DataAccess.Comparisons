using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Repositories;
using Services.Core;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
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

        public async Task<Product> GetProductAsync(int productId)
        {
            ProductRespository productRepository = new ProductRespository(ProductStore.Current);

            return await productRepository.GetByIdAsync(productId);
        }
    }
}
