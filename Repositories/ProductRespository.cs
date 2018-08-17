using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class ProductRespository : BaseRepository<Product>
    {
        private readonly ProductStore _productStore;

        public ProductRespository(ProductStore productStore) : base(productStore)
        {
            _productStore = productStore;
        }

        public async Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize)
        {
            return (await this.ListAsync()).Skip(offset).Take(pageSize).ToArray();
        }

        public async Task<int> GetAllProductsCountAsync()
        {
            return _productStore.Entities.Count;
        }
    }
}
