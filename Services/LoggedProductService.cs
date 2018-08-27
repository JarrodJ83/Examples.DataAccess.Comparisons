using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Services.Core;

namespace Services
{
    public class LoggedProductService : IProductService
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public LoggedProductService(ILogger logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            _logger.Verbose("Getting products paged");
            try
            {
                return await _productService.GetAllProductsPagedAsync(offset, pageSize);
            }
            catch (Exception e)
            {
                _logger.Exception(e, "Error getting products paged");
                throw;
            }
            
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            _logger.Verbose($"Getting product {productId}");
            try
            {
                return await _productService.GetProductAsync(productId);
            }
            catch (Exception e)
            {
                _logger.Exception(e, $"Error getting product {productId}");
                throw;
            }
        }
    }
}
