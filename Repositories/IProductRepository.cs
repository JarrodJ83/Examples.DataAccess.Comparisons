using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize);
        Task<int> GetAllProductsCountAsync();
    }
}
