using System.Threading.Tasks;
using DomainModel;

namespace Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize);
        Task<int> GetAllProductsCountAsync();
    }
}
