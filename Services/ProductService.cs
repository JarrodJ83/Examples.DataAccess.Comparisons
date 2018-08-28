using System;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Repositories;
using Repositories.Core;

namespace Services
{
    public class ProductService
    {
        private readonly ProductRespository _productRepository;

        public ProductService()
        {
            // TODO: DIP Violation (Creating its own concrete instance)
            _productRepository = new ProductRespository(ProductStore.Current);
        }

        // TODO: SRP Violation (logging, validation, and business logic)
        public async Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize)
        {
            try
            {
                if(pageSize > 100)
                    throw new Exception("Page size cannot exceed 100");

                // TODO: DIP Violation (Accessing dependency directly via static)
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

        async Task<T> GetByIdFromRepository<T>(BaseRepository<T> baseRepo, int id) where T: Entity
        {
            return await baseRepo.GetByIdAsync(id);
        }
    }
}
