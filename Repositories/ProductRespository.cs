using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class ProductRespository : BaseRepository<Product>, IProductRepository
    {
        private readonly IEntityStore<Product> _productStore;

        public ProductRespository(IEntityStore<Product> productStore) : base(productStore)
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
