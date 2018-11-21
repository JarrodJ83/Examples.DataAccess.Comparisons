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
            #region
            // TODO: DIP Violation (Creating its own concrete instance)
            #endregion
            _productRepository = new ProductRespository(ProductStore.Current);
        }

        #region
        // TODO: SRP Violation (logging, validation, and business logic)
        #endregion
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            try
            {
                if(pageSize > 100)
                    throw new Exception("Page size cannot exceed 100");
                #region
                // TODO: DIP Violation (Accessing dependency directly via static)
                #endregion
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
                return await GetByIdFromRepository(_productRepository, productId);
            }
            catch (Exception e)
            {
                ConsoleLogger.Exception(e, "Error getting product");
                throw;
            }
        }

        #region
        // TODO: LSP Violation (ProductRepository doesn't implement GetByIdAsync)
        #endregion
        async Task<T> GetByIdFromRepository<T>(BaseRepository<T> baseRepo, int id) where T: Entity
        {
            return await baseRepo.GetByIdAsync(id);
        }
    }
}
