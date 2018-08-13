using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class Products : BaseRepository<Product>
    {
        private readonly ProductStore _productStore;

        public Products(ProductStore productStore) : base(productStore)
        {
            _productStore = productStore;
        }
    }
}
