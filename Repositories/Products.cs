using System.Linq;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class Products : BaseRepository<Product>, IProductRepository
    {
        private readonly ProductStore _productStore;

        public Products(ProductStore productStore) : base(productStore)
        {
            _productStore = productStore;
        }

        public Product[] GetAllProductsPaged(int offset, int pageSize)
        {
            return _productStore.Entities.Skip(offset).Take(pageSize).ToArray();
        }
    }
}
