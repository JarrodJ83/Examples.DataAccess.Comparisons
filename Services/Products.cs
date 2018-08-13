using System;
using System.Linq;
using DomainModel;
using Repositories.Core;

namespace Services
{
    public class Products : IProductService
    {
        private readonly IRepository<Product> _productsRepository;

        public Products(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public Product[] GetAllProductsPaged(int offset, int pageSize)
        {
            return _productsRepository.List().Skip(offset).Take(pageSize).ToArray();
        }
    }
}
