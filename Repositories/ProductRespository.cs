using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class ProductRespository
    {
        private readonly ProductStore _productStore;

        public ProductRespository(ProductStore productStore)
        {
            _productStore = productStore;
        }

        public async Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize)
        {
            return _productStore.Entities.Skip(offset).Take(pageSize).ToArray();
        }

        public async Task<int> GetAllProductsCountAsync()
        {
            return _productStore.Entities.Count;
        }
    }
}
