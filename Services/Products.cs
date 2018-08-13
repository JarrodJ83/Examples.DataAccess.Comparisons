using DomainModel;
using Repositories;

namespace Services
{
    public class Products : IProductService
    {
        private readonly IProductRepository _productRepository;

        public Products(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Product[] GetAllProductsPaged(int offset, int pageSize)
        {
            return _productRepository.GetAllProductsPaged(offset, pageSize);
        }
    }
}
