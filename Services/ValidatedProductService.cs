using System;
using System.Threading.Tasks;
using DomainModel;
using Services.Core;

namespace Services
{
    public class ValidatedProductService : IProductService
    {
        private readonly IProductService _productService;
        private const int _maxPageSize = 100;

        public ValidatedProductService(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            if (pageSize > _maxPageSize)
                throw new Exception($"PageSize must be equal or less than {_maxPageSize}");

            return await _productService.GetAllProductsPagedAsync(offset, pageSize);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _productService.GetProductAsync(productId);
        }
    }
}
