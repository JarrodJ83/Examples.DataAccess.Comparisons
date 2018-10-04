using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Repositories;

namespace Services
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductService(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        #region
        // TODO: SRP Violation (logging, validation, and business logic)
        #endregion
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            try
            {
                int maxPageSize = 100;
                if(pageSize > maxPageSize)
                    throw new MaxPageSizeExceededException(maxPageSize);
                #region
                // TODO: DIP Violation (Accessing dependency directly via static)
                #endregion
                _logger.Verbose("Getting products paged");

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
                _logger.Exception(e, "Error getting products paged");
                throw;
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            try
            {
                _logger.Verbose("Getting product");
                return await GetByIdFromRepository(_productRepository, productId);
            }
            catch (Exception e)
            {
                _logger.Exception(e, "Error getting product");
                throw;
            }
        }

        #region
        // TODO: LSP Violation (ProductRepository doesn't implement GetByIdAsync)
        #endregion
        async Task<T> GetByIdFromRepository<T>(IRepository<T> baseRepo, int id) where T: Entity
        {
            return await baseRepo.GetByIdAsync(id);
        }
    }
}
